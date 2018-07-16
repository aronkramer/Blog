using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedoBlog.Data
{
    public class DataBase
    {
        private string _connectionString;

        public DataBase(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }

        public int AddBlog(Blogs blog)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = con.CreateCommand())
            { 
                cmd.CommandText = "INSERT INTO BlogPosts (title, text, dateSubmitted) " +
                                  "VALUES (@title, @text, @date) SELECT SCOPE_IDENTITY()";
                cmd.Parameters.AddWithValue("@title", blog.Title);
                cmd.Parameters.AddWithValue("@text", blog.Text);
                cmd.Parameters.AddWithValue("@date", blog.DateSubmitted);
                con.Open();
                return (int)(decimal)cmd.ExecuteScalar();
            }
        }

        public IEnumerable<Blogs> GetTopFiveBlogs()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "select top 5 * from blogposts order by Id desc";
                List<Blogs> list = new List<Blogs>();
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    Blogs blogs = new Blogs
                    {
                        Id = (int)reader["id"],
                        Title = (string)reader["Title"],
                        Text = (string)reader["Text"],
                        DateSubmitted = (DateTime)reader["DateSubmitted"]
                    };
                    list.Add(blogs);
                }
                return list;
            }
        }

        public Blogs GetBlogById(int? id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "select * from blogposts where id = @id";
                if (id == null)
                {
                    cmd.Parameters.AddWithValue("@id", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@id", id);
                }
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Blogs blog = new Blogs
                    {
                        Id = (int)reader["id"],
                        Title = (string)reader["Title"],
                        Text = (string)reader["Text"],
                        DateSubmitted = (DateTime)reader["DateSubmitted"]
                    };
                    return blog;
                }
                return null;
            }
        }

        public void AddComment(Comments comments)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO comments (commenterName, text, blogpostId) " +
                                  "VALUES (@cname, @ccomment, @cblogid)";
                cmd.Parameters.AddWithValue("@cname", comments.cName);
                cmd.Parameters.AddWithValue("@ccomment", comments.ccomment);
                cmd.Parameters.AddWithValue("@cblogid", comments.cblogid);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Comments> GetCommentsForId(int? id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "select * from comments where blogpostId = @id";
                cmd.Parameters.AddWithValue("@id", id);
                List<Comments> list = new List<Comments>();
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Comments comments = new Comments
                    {
                        ccomment = (string)reader["text"],
                        cName = (string)reader["CommenterName"]
                    };
                    list.Add(comments);
                }
                return list;
            }
        }
    }
}
