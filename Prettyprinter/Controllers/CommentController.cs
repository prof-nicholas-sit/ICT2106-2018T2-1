using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThreadTest.Models.CommentModule;
using System.Collections.Specialized;
namespace ThreadTest.Controllers
{
    public class CommentController : Controller
    {
        const string SessionKeyUsername = "_Username";
        static List<Comment> model = new List<Comment>();
        public static StringBuilder output;



        // GET: Comment
        public ActionResult Index()
        {
            HttpContext.Session.SetString(SessionKeyUsername,"Tony Stark");


            //List<Comment> model = new List<Comment> { };
            if (model.Count() == 0){
                model = GetCommentsStub();//Get comments from database
            }

            output = new StringBuilder();

            output.Append(buildTopLevelComment());

            find(0, model, 0);
            //Insert Javascript for showing/hiding comments
            output.Append("<script type='text/javascript'>function showReplyBox(id) {document.getElementById(id).style.display = 'block';}function hideReplyBox(id) {document.getElementById(id).style.display = 'none';}</script>");
            string data = output.ToString();

            return View(model: data);
        }

        public List<Comment> GetCommentsStub()
        {
            var comment1 = new Comment();
            comment1.id = 1;
            comment1.parentId = 0;
            comment1.username = "Steve Rogers";
            comment1.description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor";

            var comment2 = new Comment();
            comment2.id = 2;
            comment2.parentId = 1;
            comment2.username = HttpContext.Session.GetString(SessionKeyUsername);
            comment2.description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit";

            var comment3 = new Comment();
            comment3.id = 3;
            comment3.parentId = 2;
            comment3.username = "Thor";
            comment3.description = "Lorem ipsum dolor!!";

            var comment4 = new Comment();
            comment4.id = 4;
            comment4.parentId = 1;
            comment4.username = "Bruce Banner";
            comment4.description = "Hulk SMASSSSHHHHHHHH!!!!";

            var comment5 = new Comment();
            comment5.id = 5;
            comment5.parentId = 3;
            comment5.username = "Loki";
            comment5.description = "Lorem ipsum dolor sit amet";

            var comment6 = new Comment();
            comment6.id = 6;
            comment6.parentId = 2;
            comment6.username = "Happy Hogan";
            comment6.description = "Ipsum dolor sit amet, consectetur adipiscing elit, sed do";

            List<Comment> commentList = new List<Comment> { };
            commentList.Add(comment1);
            commentList.Add(comment2);
            commentList.Add(comment3);
            commentList.Add(comment4);
            commentList.Add(comment5);
            commentList.Add(comment6);
            return commentList;
        }
        //Recursive function to find all the comments from a given id
        public void find(int id, List<Comment> list, int level)
        {
            foreach (var item in list.Where(n => n.parentId == id))
            {
                output.Append(buildComment(item, level));
                find(item.id, list, level + 1);

            }
        }
        public String buildTopLevelComment(){
            StringBuilder raw = new StringBuilder();
            raw.Append("<p><a href='#' onclick='showReplyBox(\"reply0\")'>Add New Comment  <span class='glyphicon glyphicon-comment'></span></a></p>");
            raw.Append("<form id='reply0' class='hiddenForm' action='/Comment/Create' method='post'>");

            raw.Append("<div class='form-group level0'>");
            raw.Append("<textarea rows='3' cols='10' class='form-control' type='text' id='Description' name='Description' value=''></textarea><br/>");
            raw.Append("<input type='hidden' id='id' name='id' value=0>");
            raw.Append("<input type='submit' value='Comment' class='btn-sm btn-default'> <button type='button' onclick='hideReplyBox(\"reply0\")' class='btn-sm btn-basic'>Cancel</button> ");
            raw.Append("</div>");
            raw.Append("</form>");
            return raw.ToString();
        }

        //Reuseable function to produce raw html for rendering
        //Be careful of the quotes ' ", double quotes must be escaped \" - Daniel
        public string buildComment(Comment item, int level)
        {
            HtmlDocumentBuilder builder = new HtmlDocumentBuilder();

            // start building
            builder.OpenDocument();

            // build the head with the title and author
            builder.BuildContent(item.username, item.description, level);

            // build the body id, parentId, username, level, currentUser
            builder.BuildAction(item.id.ToString(), item.parentId.ToString(), item.username, level, HttpContext.Session.GetString(SessionKeyUsername));

            // finished building
            builder.CloseDocument();

            return builder.GetDocument().GetString();
            /*
            StringBuilder raw = new StringBuilder();
            raw.Append("<div class='row'>");

            raw.Append("<div class='col-md-2'>");
            raw.Append("<div class='userProfile level" + level + "'>");


            //if-else, so only level 0s has arrow beside
            if (level > 0)
            {
                raw.Append("<a href=''>" + item.username + "</a>");
            }
            else
            {
                raw.Append("<span class='glyphicon glyphicon-triangle-right'></span><a href=''>" + item.username + "</a>");
            }
            raw.Append("</div>");
            raw.Append("</div>");


            //edited input to textarea, the col-md numbers, cancel button, glyphicon icons with hover text,
            raw.Append("<div class='col-md-6'>");
            raw.Append("<div class='commentDetails level" + level + "'>");
            raw.Append("<p>" + item.description + "</p>");
            raw.Append("</div>");

            //changed position of textarea to after actionbar, and gave levels also
            raw.Append("</div>");

            raw.Append("<div class='col-md-3 actionBar'>");
            raw.Append("<div class='userActions'>");
            if (item.username.Equals(HttpContext.Session.GetString(SessionKeyUsername)))
            {
                raw.Append("<a href='#' onclick='showReplyBox(\"edit" + item.id + "\")'><span class='glyphicon glyphicon-pencil' title='Edit'></span></a> | <a href='/Comment/Delete?id=" + item.id + "'><span class='glyphicon glyphicon-trash' title='Delete'></span></a> | ");
            }
            raw.Append("<a href='#' onclick='showReplyBox(\"reply" + item.id + "\")'><span class='glyphicon glyphicon-share-alt' title='Reply'></span></a> | <a href='/Comment/Like'><span class='glyphicon glyphicon-thumbs-up' title='Like'></span></a> | <a href='/Comment/Permalink'><span class='glyphicon glyphicon-pushpin' title='Permalink'></span></a></div>");
            raw.Append("</div>");


            if (item.username.Equals(HttpContext.Session.GetString(SessionKeyUsername)))
            {
                raw.Append("<form id='edit" + item.id + "' class='hiddenForm' action='/Comment/Edit' method='post'>");
                raw.Append("<div class='form-group level" + level + "'>");
                raw.Append("<textarea rows='3' cols='10' class='form-control' type='text' id='Description' name='Description' value=''></textarea><br/>");
                raw.Append("<input type='hidden' id='ParentId' name='ParentId' value=" + item.parentId + ">");
                raw.Append("<input type='hidden' id='Id' name='Id' value=" + item.id + ">");
                //raw.Append("<input type='submit' value='Save Edit' class='btn btn-default'> | <a href='#' onclick='hideReplyBox(\"edit" + item.Id + "\")'>Cancel</a> ");
                raw.Append("<input type='submit' value='Save Edit' class='btn-sm btn-default'> <button type='button' onclick='hideReplyBox(\"edit" + item.id + "\")' class='btn-sm btn-basic'>Cancel</button> ");
            
                raw.Append("</div>");
                raw.Append("</form>");
            }

            raw.Append("<form id='reply" + item.id + "' class='hiddenForm' action='/Comment/Create' method='post'>");

            raw.Append("<div class='form-group level" + level + "'>");
            raw.Append("<textarea rows='3' cols='10' class='form-control' type='text' id='Description' name='Description' value=''></textarea><br/>");
            raw.Append("<input type='hidden' id='id' name='id' value=" + item.id + ">");
            raw.Append("<input type='submit' value='Reply' class='btn-sm btn-default'> <button type='button' onclick='hideReplyBox(\"reply" + item.id + "\")' class='btn-sm btn-basic'>Cancel</button> ");
            raw.Append("</div>");
            raw.Append("</form>");


            raw.Append("</div>");

            return raw.ToString();
            */
        }


        // POST: Comment/Create
        [HttpPost]
        public ActionResult Create()
        {
            try
            {
                var desc = Request.Form["Description"].ToString();

                //add to local model 
                //TO DO:pass to interpreter

                var comment = new Comment();
                comment.id = model.Count();//
                comment.parentId = Int32.Parse(Request.Form["Id"].ToString());
                comment.username = HttpContext.Session.GetString(SessionKeyUsername);
                comment.description = Request.Form["Description"].ToString();
                model.Add(comment); 


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: Comment/Edit/5
        [HttpPost]
        public ActionResult Edit()
        {
            try
            {
                //add to local model 
                //TO DO:pass to interpreter
                int Id = Int32.Parse(Request.Form["Id"].ToString());
                int ParentId = Int32.Parse(Request.Form["ParentId"].ToString());
                model.RemoveAll(x => x.id == Id);

                var comment = new Comment();
                comment.id = Id;
                comment.parentId = ParentId;
                comment.username = HttpContext.Session.GetString(SessionKeyUsername);
                comment.description = Request.Form["Description"].ToString();
                model.Add(comment); 


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Comment/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                //Remove comment from local model
                model.RemoveAll(x => x.id == id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: Comment/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                model.RemoveAll(x => x.id == id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}