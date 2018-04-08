using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectWebApplication.Models;
using System.Security.Cryptography;
using System.Text;
using DBLayer.DAL;
using System.Net.Mail;
using System.Net;
using System.Collections.Specialized;
using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace ProjectWebApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly ISession session;
        //string userID = "caifeng@email.com";
        //string password = "password1";
        //string isAdmin = "true";
        DbObj db;
        AccountGateway accGateway;

        public AccountController(IHttpContextAccessor httpContextAccessor)
        {
            this.session = httpContextAccessor.HttpContext.Session;
            this.db = new DbObj();
            this.accGateway = new AccountGateway(db);
        }

        public IActionResult Index()
        {
            return View();
        }

        // POST: Booking/Confirm/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string txtEmail, string txtPassword)
        {
            try
            {
                // TODO: Add confirm logic here
                //return RedirectToAction(nameof(Index));
                if (accGateway.ValidateLogin(txtEmail, txtPassword))
                //if (txtEmail == userID && txtPassword == password)
                {
                    Account validAcc = accGateway.SelectByEmail(txtEmail);
                    session.SetString("userID", validAcc.Email);
                    System.Diagnostics.Debug.WriteLine(validAcc.AdminRole.ToString());
                    session.SetString("isAdmin", validAcc.AdminRole.ToString());

                    //System.Diagnostics.Debug.WriteLine(message);
                    return RedirectToAction("UserHomepage");
                }
                else
                {
                    ViewBag.Message = "nonUser";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        public IActionResult UserHomepage()
        {
            if (!isLoggedIn())
            {
                return RedirectToAction("Index");
            }
            return View();
        }
         
        public IActionResult UserProfile()
        {
            if (!isLoggedIn())
            {
                return RedirectToAction("Index");
            }
            ViewData["Message"] = "UserProfile";

            var message = session.GetString("userID");
            Account acc = getUserProfile(message);

            return View(acc);
        }

        public IActionResult EditProfile(string email)
        {
            if (!isLoggedIn())
            {
                return RedirectToAction("Index");
            }
            Account acc = getUserProfile(email);
            return View(acc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // public IActionResult EditProfile(string name, string password, string bio, string displayPicURL, string title, DateTime date)
        public async Task<IActionResult> EditProfile([Bind("ID, Name, Email, AdminRole, Password, Bio, DisplayPicURL, Title, Birthday")] Account acc)
        {
            if (!isLoggedIn())
            {
                return RedirectToAction("Index");
            }
            String email = session.GetString("userID");
            IFormCollection nvc = Request.Form;

            accGateway.SetUserName(email, nvc["Name"]);
            accGateway.SetUserPassword(email, nvc["Password"]);
            accGateway.SetUserBio(email, nvc["Bio"]);
            accGateway.SetUserDisplayPicUrl(email, nvc["DisplayPicURL"]);
            accGateway.SetUserTitle(email, nvc["Title"]);
            accGateway.SetUserBirthDate(email, nvc["Birthday"]);
            return View("UserHomepage");
           
        }

        public IActionResult Admin(String searchKeyword)
        {
            if (!isLoggedIn() && !isAdmin())
            {
                return RedirectToAction("Index");
            }
            ViewData["Message"] = "Page after logging in.";
            List<Dictionary<string, string>> users = new List<Dictionary<string, string>>();
            if(searchKeyword == null)
            {
                var readResults = accGateway.SelectAll().GetEnumerator();
                while (readResults.MoveNext())
                {
                    Dictionary<string, string> user = new Dictionary<string, string>();
                    user["email"] = readResults.Current.Email;
                    user["name"] = readResults.Current.Name;
                    users.Add(user);
                }
            }
            else
            {
                Account searchResult = accGateway.SelectByEmail(searchKeyword);
                System.Diagnostics.Debug.WriteLine(searchResult);
                if (searchResult != null) { 
                    Dictionary<string, string> user = new Dictionary<string, string>();
                    user["email"] = searchResult.Email;
                    user["name"] = searchResult.Name;
                    users.Add(user);
                }
            }

            ViewBag.users = users;
            return View();
        }


        public Account getUserProfile(String email)
        {
            Account result;
            result = accGateway.SelectByEmail(email);

            return result;
        }

        public JsonResult deleteUsers(List<String> emails)
        {
            foreach (String email in emails)
            {
                deleteUser(email);
            }

            return Json(new
            {
                success = true
            });
        }

        public JsonResult deleteUser(String email)
        {
            Boolean success = accGateway.DeleteAccount(email);
            return Json(new
            {
                success = success
            });
        }

        public JsonResult resetAccounts(List<String> emails)
        {
            foreach (String email in emails)
            {
                resetAccount(email);
            }

            return Json(new
            {
                success = true
            });
        }

        public JsonResult resetAccount(String email)
        {
            Boolean success = resetPassword(email);

            return Json(new
            {
                success = success
            });
        }

        [HttpPost]
        public ActionResult Create(string name, string title, string email, string password, DateTime birthday, bool adminRole)
        {
            Account acc;
            acc = accGateway.SelectByEmail(email);

            if (email != null && acc == null)
            {
                ObjectId objId = ObjectId.GenerateNewId();
                String displayPicUrl = "";
                String bio = "";

                //Hash password before insert
                String hashPass = hashPassword(password);
                Console.WriteLine(hashPass);

                Account tempAcc = new Account(objId, name, title, email, hashPass, birthday, adminRole, displayPicUrl, bio);

                accGateway.Insert(tempAcc);
                accGateway.Save();

                IFormCollection nvc = Request.Form;
                Console.WriteLine(nvc["Email"]);
                return View(tempAcc);
            }
            else if (email != null && acc != null)
            {
                Response.WriteAsync("<script language=javascript>alert('Email is already registered.')</script>");
                return View();
            }
            return View();
        }

        public ActionResult ForgetPass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgetPass(String email)
        {
           
            Account acc = accGateway.SelectByEmail(email);

            Console.WriteLine(acc);

            if (email != null && acc != null) {
                if (resetPassword(email) == true)
                {
                    Console.WriteLine(email);
                    //Response.WriteAsync("<script language=javascript>alert('An email with your new password has been sent to you.')</script>");
                    ViewBag.message = "An email with your new password has been sent to you.";
                    return View();
                }
            }
            else if (email != null && acc == null)
            {
                //Response.WriteAsync("<script language=javascript>alert('Email is not registered.')</script>");
                ViewBag.message = "Email is not registered.";
                return View();

            }
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public String hashPassword(String rawPassword)
        {
            String hashedPassword = "";
            StringBuilder hashedPasswordBuilder = new StringBuilder();
            HashAlgorithm algorithm = SHA256.Create();
            foreach (byte b in algorithm.ComputeHash(Encoding.UTF8.GetBytes(rawPassword)))
            {
                hashedPasswordBuilder.Append(b.ToString("X2"));
            }
            hashedPassword = hashedPasswordBuilder.ToString();
            return hashedPassword;
        }

        public String generatePassword()
        {
            Random random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            String password = new string(
                Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)])
                .ToArray()
                );
            return password;
        }

        public Boolean resetPassword(String email)
        {
            String newPassword = generatePassword();
            String hashedPass = hashPassword(newPassword);
            Boolean success = accGateway.SetUserPassword(email, hashedPass);
            accGateway.Save();
            if (success)
            {
                sendResetPasswordEmail(email, newPassword);
            }
            return success;
        }

        public void sendResetPasswordEmail(String email, String password)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("PrettyPrinter2106@gmail.com", "ict2106prettyprinter");
            String to = email;

            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(to);
            mailMessage.From = new MailAddress(email);
            mailMessage.Body = "Your password has been resetted. Your new password is: "+password;
            mailMessage.Subject = "Password Reset";
            client.Send(mailMessage);
        }

        public Boolean isLoggedIn()
        {
            String userId = session.GetString("userID");
            return userId != null;

        }

        public Boolean isAdmin() {
            String isAdminStr = session.GetString("isAdmin").ToLower();
            return isAdminStr == "true";
        }

    }
}
