using Products.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Products.Controllers
{
    public class ProductController : Controller
    {
        public string InsertProduct { get; private set; }
        public string DetailsProduct { get; private set; }

        // GET: Product
        public ActionResult Index()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)MSSQLLocalDB;initialCatlog=JkJan22;integrated Security=True";
            cn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = Select * from Product ;

            List<Product> list = new List<Product>();
            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
                while(dr.Read())
                {
                Product prod = new Product();
                    prod.ProductId = (int)dr["ProductId"];
                    prod.ProductName = (string)dr["ProductName"];
                    prod.Rate = (decimal)dr["Rate"];
                    prod.Description = (string)dr["Description"];
                    prod.CategoryName = (string)dr["CategoryName"];
                    list.Add(prod);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View(list);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product obj)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)MSSQLLocalDB;initialCatlog=JkJan22;integrated Security=True";
            cn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = InsertProduct;

            cmd.Parameters.AddWithValue("@ProductName", obj.ProductName);
            cmd.Parameters.AddWithValue("@rate", obj.Rate);
            cmd.Parameters.AddWithValue("@Description", obj.Description);
            cmd.Parameters.AddWithValue("@CategoryName", obj.CategoryName);
            try
            {
                
                cmd.ExecuteNonQuery();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
            finally
            {
                cn.Close();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)MSSQLLocalDB;initialCatlog=JkJan22;integrated Security=True";
            cn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = InsertProduct;
            SqlParameter sqlParameter = cmd.Parameters.AddWithValue("@ProductId",id);

            Product prod = null;
            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.Read())
                {
                    prod = new Product
                    {
                        ProductId = id,
                        ProductName = dr.GetString(1),
                        Rate = dr.GetInt32(2),
                        Description = dr.GetString(3),
                        CategoryName = dr.GetString(4)
                    };
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(prod);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Product obj)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)MSSQLLocalDB;initialCatlog=JkJan22;integrated Security=True";
            cn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = InsertProduct;


            Product prod = null;
            cmd.Parameters.AddWithValue("@ProductId", id);
            cmd.Parameters.AddWithValue("@ProductId", obj.ProductId);
            cmd.Parameters.AddWithValue("@ProductName", obj.ProductName);
            cmd.Parameters.AddWithValue("@rate", obj.Rate);
            cmd.Parameters.AddWithValue("@Description", obj.Description);
            cmd.Parameters.AddWithValue("@CategoryName", obj.CategoryName);
            try
            {
                // TODO: Add update logic here

                cmd.ExecuteNonQuery();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
            finally
            {
                cn.Close();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
