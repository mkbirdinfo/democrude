using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication11.Models;
namespace WebApplication11.Controllers

{
    public class mainController : Controller
    {
        // GET: main
        public ActionResult Index()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString);
            List<employee> empList = new List<employee>();

            SqlCommand cmd = new SqlCommand("select * from employee", con);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            sqlDataAdapter.Fill(ds);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                empList.Add(new employee
                {

                    id = int.Parse(row["id"].ToString()),
                    name = row["name"].ToString(),
                    number = int.Parse(row["number"].ToString()),

                });
            }

            return View(empList);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString);
            var empList = new List<employee>();
            SqlCommand cmd = new SqlCommand("select * from employee where id=" + id, con);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            sqlDataAdapter.Fill(ds);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                empList.Add(new employee
                {

                    id = int.Parse(row["id"].ToString()),
                    name = row["name"].ToString(),
                    number = int.Parse(row["number"].ToString()),

                });
            }

            return View(empList.FirstOrDefault());

        }
        [HttpPost]
        public ActionResult Edit(int id, employee er)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand("update employee set name='" + er.name + "',number='" + er.number + "' where id=" + id, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("Index");

        }

        [HttpGet]


        public ActionResult Delete(int id)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand("delete from employee  where  id=" + id, con);
            con.Open();

            cmd.ExecuteNonQuery();

            con.Close();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {

            return View();

        }


        [HttpPost]
        public ActionResult Create(employee er)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand("insert into employee  values(" + er.id + ",'" + er.name + "','" + er.number + "')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("Index");

        }

        [HttpGet]
        //[Route("my/Details")]
        public ActionResult Details(int id)

        {
            var PersonsList = new List<employee>();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand("select * from employee  where id=" + id, con);
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                PersonsList.Add(new employee
                {

                    id = int.Parse(sdr["id"].ToString()),
                    name = sdr["name"].ToString(),
                    number = int.Parse(sdr["number"].ToString()),

                });
            }
            con.Close();
            return View(PersonsList.FirstOrDefault());


        }

    }























}
    

