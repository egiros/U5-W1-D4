using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using U5_W1_D4.Models;

namespace U5_W1_D4.Controllers
{
    public class HomeController : Controller
    {
        List<Articoli> articoli = new List<Articoli>();
        public ActionResult Index()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDb"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            
            try
            {
                conn.Open();
                string query = "SELECT * FROM Articoli WHERE Visibile = 1";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Articoli articolo = new Articoli();
                    articolo.Id = Convert.ToInt32(reader["Id"]);
                    articolo.Nome = reader["Nome"].ToString();
                    articolo.Prezzo = Convert.ToDecimal(reader["Prezzo"]);
                    articolo.ImmagineCopertina = reader["ImmagineCopertina"].ToString();
                    articoli.Add(articolo);
                }   
            }
            catch (Exception ex)
            {
                Response.Write("Error: ");
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return View(articoli);
        }

        public ActionResult Dettagli(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDb"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            Articoli articolo = new Articoli();
            try
            {
                conn.Open();
                string query = "SELECT * FROM Articoli WHERE Id = " + id;
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    articolo.Id = Convert.ToInt32(reader["Id"]);
                    articolo.Nome = reader["Nome"].ToString();
                    articolo.Prezzo = Convert.ToDecimal(reader["Prezzo"]);
                    articolo.Descrizione = reader["Descrizione"].ToString();
                    articolo.ImmagineCopertina = reader["ImmagineCopertina"].ToString();
                    articolo.ImmagineAggiuntiva1 = reader["ImmagineAggiuntiva1"].ToString();
                    articolo.ImmagineAggiuntiva2 = reader["ImmagineAggiuntiva2"].ToString();
                }   
            }
            catch (Exception ex)
            {
                Response.Write("Error: ");
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return View(articolo);
        }

        public ActionResult AggiungiArticolo()
        {
            return View();
        }
        [HttpPost]

        public ActionResult AggiungiArticolo(Articoli articolo, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDb"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            if (file1 != null && file1.ContentLength > 0)
            {
                string _FileName = Path.GetFileName(file1.FileName);
                string _path = Path.Combine(Server.MapPath("~/Content/img"), _FileName);
                file1.SaveAs(_path);
                articolo.ImmagineCopertina = _path;
            }
            if (file2 != null && file2.ContentLength > 0)
            {
                string _FileName = Path.GetFileName(file2.FileName);
                string _path = Path.Combine(Server.MapPath("~/Content/img"), _FileName);
                file2.SaveAs(_path);
                articolo.ImmagineAggiuntiva1 = _path;
            }
            if (file3 != null && file3.ContentLength > 0)
            {
                string _FileName = Path.GetFileName(file3.FileName);
                string _path = Path.Combine(Server.MapPath("~/Content/img"), _FileName);
                file3.SaveAs(_path);
                articolo.ImmagineAggiuntiva2 = _path;
            }
            try
            {
                conn.Open();
                string query = "INSERT INTO Articoli (Nome, Prezzo, Descrizione, ImmagineCopertina, ImmagineAggiuntiva1, ImmagineAggiuntiva2, Visibile)" + " VALUES (@Nome, @Prezzo, @Descrizione, @ImmagineCopertina, @ImmagineAggiuntiva1, @ImmagineAggiuntiva2, @Visibile)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nome", articolo.Nome);
                cmd.Parameters.AddWithValue("@Prezzo", articolo.Prezzo);
                cmd.Parameters.AddWithValue("@Descrizione", articolo.Descrizione);
                cmd.Parameters.AddWithValue("@ImmagineCopertina", articolo.ImmagineCopertina);
                cmd.Parameters.AddWithValue("@ImmagineAggiuntiva1", articolo.ImmagineAggiuntiva1);
                cmd.Parameters.AddWithValue("@ImmagineAggiuntiva2", articolo.ImmagineAggiuntiva2);
                cmd.Parameters.AddWithValue("@Visibile", articolo.Visibile);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) 
            {
                Response.Write("Error: ");
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return RedirectToAction("Index");
        }



        public ActionResult Gestione()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDb"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                string query = "SELECT * FROM Articoli";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Articoli articolo = new Articoli();
                    articolo.Id = Convert.ToInt32(reader["Id"]);
                    articolo.Nome = reader["Nome"].ToString();
                    articolo.Prezzo = Convert.ToDecimal(reader["Prezzo"]);
                    articolo.ImmagineCopertina = reader["ImmagineCopertina"].ToString();
                    articolo.ImmagineAggiuntiva1 = reader["ImmagineAggiuntiva1"].ToString();
                    articolo.ImmagineAggiuntiva2 = reader["ImmagineAggiuntiva2"].ToString();
                    articolo.Visibile = Convert.ToBoolean(reader["Visibile"]);
                    articoli.Add(articolo);
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: ");
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }   

            return View(articoli);
        }

        public ActionResult DeleteArticolo(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDb"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                string query = "UPDATE Articoli SET Visibile = 0 WHERE Id = " + id;
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return RedirectToAction("Index");
        }

        public ActionResult AddProd(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDb"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {   
                conn.Open();
                string query = "UPDATE Articoli SET Visibile = 1 WHERE Id = " + id;
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
             }
            return RedirectToAction("Gestione");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Login(Utenti utente)
        {

            List<Utenti> utenti = new List<Utenti>()
            {
                new Utenti { ID = 1, Nome = "admin", Password = "admin", Admin = true },
                new Utenti { ID = 2, Nome = "user", Password = "user", Admin = false }
            };

            var utenteLoggato = utenti.FirstOrDefault(u => u.Nome == utente.Nome && u.Password == utente.Password);

            if (utenteLoggato != null)
            {
                Session["UtenteLoggato"] = utenteLoggato;
                if (utenteLoggato.Admin)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.Error = "Nome utente o password errati!";
                return View("Index");
            }
        }

        public ActionResult Logout()
        {
            Session["UtenteLoggato"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}