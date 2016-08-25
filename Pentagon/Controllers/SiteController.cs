using Pentagon.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Pentagon.Controllers
{
    public class SiteController : Controller
    {
        [Route("")]
        public ActionResult Home()
        {
            return View("index");
        }


        [Route("site/login")]
        [HttpGet]
        public ActionResult Login(string userName, string passWord)
        {
            Login login = new Login();
            login.UserName = userName;
            login.Password = passWord;
            string constring = ConfigurationManager.ConnectionStrings["TutorialContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "select UserID from Login where UserName=@UserName and Password=@Password";
                command.Connection = con;
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@Password", passWord);
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read() == false)
                {
                    if (command != null)
                        command.Dispose();
                    if (command != null)
                        con.Dispose();
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    login.UserID = (int)reader["UserID"];
                    return View("Login", login);
                }
            }
        }





        [Route("site/list")]
        [HttpGet]
        public ActionResult ListTutorials(int userid)
        {
            // List<Tutorial> tutorial = new List<Tutorial>();
            List<TutorialList> tutorialviewModel = new List<TutorialList>(); //TutorialList contains only required data:TutorialID,TutorialName
            string constring = ConfigurationManager.ConnectionStrings["TutorialContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "select * from Tutorials where UserID=@UserID";
                command.Connection = con;
                command.Parameters.AddWithValue("@UserID", userid);
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader == null)
                {
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    while (reader.Read())
                    {
                        TutorialList t = new TutorialList();
                        {
                            // t.UserID = (int)reader["UserID"];
                            t.TutorialID = (int)reader["TutorialID"];
                            t.TutorialName = (string)reader["TutorialName"];
                        };
                        tutorialviewModel.Add(t);
                    }
                    return Json(tutorialviewModel, JsonRequestBehavior.AllowGet);
                }
            }
        }




        [Route("site/listchapter")]
        [HttpGet]
        public ActionResult ListChapter(int tutorialid)
        {
            List<ChapterList> chapterViewModel = new List<ChapterList>();
            string constring = ConfigurationManager.ConnectionStrings["TutorialContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "select * from Chapter where TutorialID=@TutorialID";
                command.Connection = con;
                command.Parameters.AddWithValue("@TutorialID", tutorialid);
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader == null)
                {
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    while (reader.Read())
                    {
                        ChapterList c = new ChapterList();
                        {
                            //  c.TutorialID = (int)reader["TutorialID"];
                            c.ChapterID = (int)reader["ChapterID"];
                            c.ChapterName = (string)reader["ChapterName"];
                        };
                        chapterViewModel.Add(c);
                    }
                    return Json(chapterViewModel, JsonRequestBehavior.AllowGet);
                }
            }
        }




        [Route("site/chaptercontent")]
        [HttpGet]
        public ActionResult ChapterContent(int chapterid)
        {
            List<ChapterContentList> chapterContentViewModel = new List<ChapterContentList>();
            string constring = ConfigurationManager.ConnectionStrings["TutorialContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "select * from Chapter where ChapterID=@ChapterID";
                command.Connection = con;
                command.Parameters.AddWithValue("@ChapterID", chapterid);
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader == null)
                {
                    return Json(null, JsonRequestBehavior.AllowGet);
                }

                while (reader.Read())
                {
                    ChapterContentList c = new ChapterContentList();
                    {
                        //  c.TutorialID = (int)reader["TutorialID"];
                        //  c.ChapterID = (int)reader["ChapterID"];
                        c.ChapterName = (string)reader["ChapterName"];
                        c.HierarchyLevel = (int)reader["HierarchyLevel"];
                        c.Discription = (string)reader["Discription"];
                        c.TypeOfFile = (int)reader["TypeOfFile"];
                        c.ContentOfFile = (string)reader["ContentOfFile"];
                    };
                    chapterContentViewModel.Add(c);
                }

                return Json(chapterContentViewModel, JsonRequestBehavior.AllowGet);

            }
        }



        [Route("site/addchapter")]
        [HttpPost]
        public async Task<ActionResult> AddChapter(Chapter chapter)
        {
            string constring = ConfigurationManager.ConnectionStrings["TutorialContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "insert into Chapter values(@tutorialID,@chapterName,@HierarchyLevel,@Discription,@TypeOfFile,@ContentOfFile);select CAST(scope_identity() as int)";
                command.Connection = con;
                command.Parameters.AddWithValue("@tutorialID", chapter.TutorialID);
                command.Parameters.AddWithValue("@chapterName", chapter.ChapterName);
                command.Parameters.AddWithValue("@HierarchyLevel", chapter.HierarchyLevel);
                command.Parameters.AddWithValue("@Discription", chapter.Discription);
                command.Parameters.AddWithValue("@TypeOfFile", chapter.TypeOfFile);
                command.Parameters.AddWithValue("@ContentOfFile", chapter.ContentOfFile);
                con.Open();
                int id = (int)command.ExecuteScalar();
                return Json(id, JsonRequestBehavior.AllowGet);
            }
        }


        [Route("site/addtutorial")]
        [HttpPost]
        public async Task<ActionResult> AddTutorial(Tutorial tutorial)
        {
            string constring = ConfigurationManager.ConnectionStrings["TutorialContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "insert into Tutorials values(@userID,@tutorialID,@tutorialName);select CAST(scope_identity() as int)";
                command.Connection = con;
                command.Parameters.AddWithValue("@userID", tutorial.UserID);
                command.Parameters.AddWithValue("@tutorialName", tutorial.TutorialName);
                con.Open();
                int id = (int)command.ExecuteScalar();
                return Json(id, JsonRequestBehavior.AllowGet);
            }
        }
    }
}