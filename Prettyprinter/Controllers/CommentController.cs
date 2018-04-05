using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using Prettyprinter.Models.CommentModule;
using ThreadTest.Models.CommentModule;
using ThreadTest.Models.CommentModule.Html;

namespace ThreadTest.Controllers
{
    public class CommentController : Controller
    {
        const string SessionKeyUsername = "_Username";
        static List<Comment> model = new List<Comment>();
        public static StringBuilder output;
        private int noOfLikes;
        static List<Likes> likesModel = new List<Likes>();

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

            if (Request.QueryString.HasValue){
                string id = HttpContext.Request.Query["id"].ToString();
                findThread(Convert.ToInt32(id), model, 0);
            }
            else{
                find(0, model, 0);
            }


            //Insert Javascript for showing/hiding comments
            output.Append("<script type='text/javascript'>function showReplyBox(id) {document.getElementById(id).style.display = 'block';}function hideReplyBox(id) {document.getElementById(id).style.display = 'none';}</script>");
            string data = output.ToString();

            return View(model: data);
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

        //Recursive function to find all the comments from a given id
        public void findThread(int id, List<Comment> list, int level)
        {
            foreach (var item in list.Where(n => n.id == id))
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
            HtmlCommentBuilder builder = new HtmlCommentBuilder();

            // start building
            builder.OpenDocument();

            // build the head with the title and author
            builder.BuildContent(item.username, item.description, level);

            // build the body id, parentId, username, level, currentUser
            builder.BuildAction(item.id.ToString(), item.parentId.ToString(), item.username, level, HttpContext.Session.GetString(SessionKeyUsername));

            // finished building
            builder.CloseComment();

            return builder.GetComment().GetString();

        }

        // POST: Comment/Create
        [HttpPost]
        public ActionResult Create()
        {
            try
            {
                var parent = Request.Form["Id"].ToString();
                var desc = Request.Form["Description"].ToString();
                var username = HttpContext.Session.GetString(SessionKeyUsername);


                //Save to file system
                fileManagerStub(parent, username, desc);

                //Refreshes the page, loading all the comments
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
        /*
        // GET: Comment/Permalink/5
        public ActionResult Permalink(int id)
        {
            try
            {
                TempData["id"] = id;
                return RedirectToAction(nameof(Index),"");
                
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }*/

        // GET: Comment/Like/5
        public ActionResult Like(int id)
        {
            try
            {
                if (likesModel.Exists(x => x.id == id && x.username == SessionKeyUsername))
                {
                    likesModel.RemoveAll(x => x.id == id && x.username == SessionKeyUsername);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var likes = new Likes();
                    likes.id = id;
                    likes.username = SessionKeyUsername;
                    likesModel.Add(likes);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Comment/Like/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Like(int id, IFormCollection collection)
        {
            try
            {
                if (likesModel.Exists(x => x.id == id && x.username == SessionKeyUsername))
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var likes = new Likes();
                    likes.id = id;
                    likes.username = SessionKeyUsername;
                    likesModel.Add(likes);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        //To get the number of likes by each comment id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public int LikeThis(int id)
        {
            try
            {
                if (likesModel.Count() == 0)
                {
                    likesModel = GetLikesStubs();
                }
                foreach (var item in likesModel.Where(x => x.id == id))
                {
                    noOfLikes++;
                }
            }
            catch (Exception) { }
            return noOfLikes;
        }




        /*============================= STUBS for integration with other modules ============================*/

        //if no data use this as sample
        public List<Likes> GetLikesStubs()
        {
            var likes1 = new Likes();
            likes1.id = 1;
            likes1.username = "Steve Rogers";

            var likes2 = new Likes();
            likes2.id = 2;
            likes2.username = SessionKeyUsername;

            var likes3 = new Likes();
            likes3.id = 2;
            likes3.username = "Laye";

            List<Likes> likesModel = new List<Likes> { };
            likesModel.Add(likes1);
            likesModel.Add(likes2);
            likesModel.Add(likes3);
            return likesModel;
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
            comment2.username = "Tony Stark";
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

            //Loop through each comment and send to interpreter/type setter.
            foreach (var cmt in commentList)
            {
                cmt.description = interpreterStub(cmt.description);
            }

            return commentList;
        }

        public String interpreterStub(String input)
        {
            return input;
        }
        public void fileManagerStub(String parent, String username, String commentText)
        {
            var comment = new Comment();
            comment.id = model.Count();//
            comment.parentId = Int32.Parse(parent);
            comment.username = username;
            comment.description = commentText;
            model.Add(comment);
        }
    }
}