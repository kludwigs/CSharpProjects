using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using MyBlog.Models;

namespace MyBlog.Controllers
{
    public class PostsController : Controller
    {

        //
        // GET: /Posts/
        private MyBlog.Models.MyBlogEntities model = new MyBlogEntities();

     
        // change to read from config file //
        public readonly int PostsPerPage = UserApp.Instance.NumPosts;
/*
        public ActionResult Index()
        {
            return View();
        }

*/
        public ActionResult Index(int?id)
        {
            int pageNumber = id ?? 0;
            IEnumerable<Post> posts =
                (from post in model.Posts
                where post.DateTime < DateTime.Now
                orderby post.DateTime descending
                 select post).Skip(pageNumber * PostsPerPage).Take(PostsPerPage + 1);
            ViewBag.IsPreviousLink = pageNumber > 0;
            ViewBag.IsNextLink = posts.Count() > PostsPerPage;
            ViewBag.PageNumber = pageNumber;
            ViewBag.IsAdmin = IsAdmin();
            return View(posts.Take(PostsPerPage));
        }
        public ActionResult Details(int?id)
        {
            Post post = GetPost(id);
            ViewBag.IsAdmin = IsAdmin();
            return View(post);
        }
        [ValidateInput(false)]
        public ActionResult Comment(int id, string name, string email, string body)
        {
            Post post = GetPost(id);
            Comment comment = new Comment();
            comment.Post = post;
            comment.DateTime = DateTime.Now;
            comment.Email = email;
            comment.Body = body;
            comment.Name = name;
            model.Comments.Add(comment);
            model.SaveChanges();
            return RedirectToAction("Details", new{id = id});
        }
        public ActionResult Tags(string id)
        {
            Tag tag = GetTag(id);
            ViewBag.IsAdmin = IsAdmin();
            return View("Index", tag.Posts);
        }


        [ValidateInput(false)]
        public ActionResult Update(int? id, string title, string body, DateTime datetime, string tags)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Index");
            }
            Post post = GetPost(id);
            post.Title = title;
            post.Body = body;
            post.DateTime = datetime;
            post.Tags.Clear();

            tags = tags ?? string.Empty;
            string[] tagNames = tags.Split(new Char[] { ' '}, StringSplitOptions.RemoveEmptyEntries);

            foreach(string tagName in tagNames)
            {
                post.Tags.Add(GetTag(tagName));
            }

            if(!id.HasValue)
            {
                model.Posts.Add(post);

            }
            model.SaveChanges();
            return RedirectToAction("Details", new {id = post.ID});
        }
        public ActionResult Edit(int? id)
        {
            Post post = GetPost(id);
            StringBuilder tagList = new StringBuilder();
            foreach (Tag tag in post.Tags)
            {
                tagList.AppendFormat("{0}", tag.Name);
            }
            ViewBag.Tags = tagList.ToString();
            return View(post);
        }
        public ActionResult Delete(int id)
        {
            if (IsAdmin())
            {
                Post post = GetPost(id);
                model.Posts.Remove(post);          
                model.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult DeleteComment(int id)
        {
            if (IsAdmin())
            {
                Comment comment = model.Comments.Where(x => x.ID == id).First();
                model.Comments.Remove(comment);
                model.SaveChanges();

            }
            return RedirectToAction("Index");
        }


        private Tag GetTag(string tagName)
        {
            return model.Tags.Where(x => x.Name == tagName).FirstOrDefault() ?? new Tag() { Name = tagName };
        }

        private Post GetPost(int? id)
        {
            return id.HasValue ? model.Posts.Where(x => x.ID == id).First() : new Post() { ID = -1 };
        }

        public bool IsAdmin()
        {
            return true;
        }


    }
}
