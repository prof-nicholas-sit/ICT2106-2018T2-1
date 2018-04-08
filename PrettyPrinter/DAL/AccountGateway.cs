using ProjectWebApplication.Models;
using System;
using System.Security.Cryptography;
using System.Text;

namespace DBLayer.DAL
{
    public class AccountGateway : DataGateway<Account>
    {
        
        public AccountGateway(DbObj dbObject) : base(dbObject)
        {
        }

        public Account SelectByEmail(string email)
        {
            var resultInArray = db.SelectWhere(typeof(Account), "Email", email).GetEnumerator();
                
            // need to get the first element of the returned enumerable
            while (resultInArray.MoveNext())
            {
                return (Account)resultInArray.Current;
            }

            return null;
        }

        public bool ValidateLogin(string email, string pw)
        {
            var acc = SelectByEmail(email);
            String pwHashCheck = hashPassword(pw);
            return acc != null && pwHashCheck.Equals(acc.Password);
        }

        public bool SetUserPassword(string email, string pw)
        {
            var acc = SelectByEmail(email);
            if (acc == null) return false;
            
            acc.Password = pw;
            Update(acc);
            Save();
            
            return true;
        }
        
        public bool SetUserBio(string email, string bio)
        {
            var acc = SelectByEmail(email);
            if (acc == null) return false;
            
            acc.Bio = bio;
            Update(acc);
            Save();

            return true;
        }
        
        public bool SetUserDisplayPicUrl(string email, string url)
        {
            var acc = SelectByEmail(email);
            if (acc == null) return false;
            
            acc.DisplayPicURL = url;
            Update(acc);
            Save();

            return true;
        }
        
        public bool SetUserTitle(string email, string title)
        {
            var acc = SelectByEmail(email);
            if (acc == null) return false;
            
            acc.Title = title;
            Update(acc);
            Save();

            return true;
        }
        
        public bool SetUserName(string email, string name)
        {
            var acc = SelectByEmail(email);
            if (acc == null) return false;
            
            acc.Name = name;
            Update(acc);
            Save();

            return true;
        }

        internal object GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool SetUserBirthDate(string email, string date)
        {
            var acc = SelectByEmail(email);
            if (acc == null) return false;

            // format is in yyyy-MM-ddThh:mm:ss, therefore we split we T, to get the date -> dateStr[0]
            string[] dateStr = date.Split("T");

            string dateSelected = dateStr[0];

            // we split again with -, so as to get individual year, month and day
            string[] splitDate = dateSelected.Split("-");

            int year = Int32.Parse(splitDate[0]);
            int month = Int32.Parse(splitDate[1]);
            int day = Int32.Parse(splitDate[2]);

            //System.Diagnostics.Debug.WriteLine("Asd");
            //System.Diagnostics.Debug.WriteLine(year);
            //System.Diagnostics.Debug.WriteLine(month);
            DateTime edittedDate = new DateTime(year, month, day);

            acc.Birthday = new DateTimeOffset(edittedDate, new TimeSpan(8,0,0)).DateTime;
            Update(acc);
            Save();

            return true;
        }

        public bool DeleteAccount(string email)
        {
            var acc = SelectByEmail(email);
            if (acc == null) return false;
            
            // TODO: delete all files belonging to this account
            // TODO: delete all access controls??
            Delete(acc);

            return true;
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
    }
}