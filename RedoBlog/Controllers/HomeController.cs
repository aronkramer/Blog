using RedoBlog.Data;
using RedoBlog.Models;
using RedoBlog.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RedoBlog.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DataBase data = new DataBase(Settings.Default.ConStr);
            return View(data.GetTopFiveBlogs());
        }

        public ActionResult Comment(int? id)
        {
            DataBase db = new DataBase(Settings.Default.ConStr);
            BlogAndComments BC = new BlogAndComments();
            BC.Blog = db.GetBlogById(id);
            var comment = db.GetCommentsForId(id);
            if(comment != null)
            {
                BC.comments = comment;
            }        
            if (id == null)
            {
                return Redirect("/");
            }
            if (Request.Cookies["name-comment"] != null)
            {
                BC.CookieName = Request.Cookies["name-comment"].Value;
            }
            return View(BC);
        }

        public ActionResult AddBlog()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBlog(Blogs blog)
        {
            DataBase data = new DataBase(Settings.Default.ConStr);
            Blogs b = new Blogs
            {
                Text = blog.Text,
                Title = blog.Title,
                DateSubmitted = DateTime.Now
            };
            int id = data.AddBlog(b);
            return Redirect($"/Home/Comment?id={id}");
        }

        [HttpPost]
        public ActionResult addComments(Comments comments)
        {
            DataBase db = new DataBase(Settings.Default.ConStr);
            db.AddComment(comments);
            Response.Cookies.Add(new HttpCookie("name-comment", comments.cName));
            return Redirect($"/Home/Comment?id={comments.cblogid}");
        }
    }
}

/*
On the home page, display the five most recent blog posts.
For each blog post, only display the first 200 or so characters.
After that, display three dots like this... 
The title and Read More should both be links that take you to
a page that displays the full article.

The individual blog post page should display the full contents
of that blog post.  Underneath the article,
you'll see a comments section. The template only has a text area for the 
comment content, but we'll be adding a textbox above that, that is meant
for the commenter to put their name (bonus: use javascript to enable/disable
the submit button until they enter a name).

Finally, there should be a page at /admin/newpost that has
a form where the user can add new blog posts. This form should 
simply have a textbox, a textarea and a submit button. 
When a new blog post is submitted, the user should be redirected
to that new individual blog page.

Beneath that, display all the comments left for that article.
When they submit a new comment, the page should refresh, and the comment should show up below.
Also, if a user has previously left a comment on the side, 
the name textbox should be prefilled with their name (use cookies).
==========
For a real extra challenge, see if you can implement 
the older/newer buttons on the home page. 
This isn't easy, but is a really good challenge.
*/

    