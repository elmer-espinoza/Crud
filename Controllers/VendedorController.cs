using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Crud.Models;

namespace Crud.Controllers
{
    public class VendedorController : Controller
    {
        private static string cnStr = ConfigurationManager.ConnectionStrings["cnStr"].ToString();
        private static List <Vendedor> lVendedores= new List<Vendedor>();


        [HttpGet]
        public ActionResult Listar()
        {
            SqlConnection Cn = new SqlConnection(cnStr);
            SqlCommand Cmd = new SqlCommand("sp_vendedor_listar", Cn);
            SqlDataReader Dr;
            Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Filtro", ""));
            Cn.Open();
            Dr = Cmd.ExecuteReader();
            lVendedores.Clear();
            while (Dr.Read()) {
                Vendedor oVendedor = new Vendedor();
                oVendedor.Vendedor_Id = Convert.ToInt32(Dr["Vendedor_Id"]);
                oVendedor.Nombres = Dr["Nombres"].ToString();
                oVendedor.Apellidos = Dr["Apellidos"].ToString();
                oVendedor.Telefono = Dr["Telefono"].ToString();
                oVendedor.Correo = Dr["Correo"].ToString();
                lVendedores.Add(oVendedor);
            }

            Cn.Close();
            return View(lVendedores);
        }

        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost] 
        public ActionResult Crear(Vendedor oVendedor)
        {
            SqlConnection Cn = new SqlConnection(cnStr);
            SqlCommand Cmd = new SqlCommand("sp_vendedor_crear", Cn);
            Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("Nombres",oVendedor.Nombres);
            Cmd.Parameters.AddWithValue("Apellidos", oVendedor.Apellidos);
            Cmd.Parameters.AddWithValue("Telefono", oVendedor.Telefono);
            Cmd.Parameters.AddWithValue("Correo", oVendedor.Correo);
            Cn.Open();
            Cmd.ExecuteNonQuery();
            Cn.Close();
            return RedirectToAction("Listar","Vendedor");
        }
        
        [HttpGet]
        public ActionResult Editar(int? Vendedor_id)
        {
            if (Vendedor_id is null) { 
               return RedirectToAction("Listar", "Vendedor"); 
            }

            Vendedor oVendedor = lVendedores.Where(x => x.Vendedor_Id == Vendedor_id).FirstOrDefault();

            return View(oVendedor);
        }

        [HttpPost]
        public ActionResult Editar(Vendedor oVendedor)
        {
            SqlConnection Cn = new SqlConnection(cnStr);
            SqlCommand Cmd = new SqlCommand("sp_vendedor_editar", Cn);
            Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("Vendedor_Id", oVendedor.Vendedor_Id);
            Cmd.Parameters.AddWithValue("Nombres", oVendedor.Nombres);
            Cmd.Parameters.AddWithValue("Apellidos", oVendedor.Apellidos);
            Cmd.Parameters.AddWithValue("Telefono", oVendedor.Telefono);
            Cmd.Parameters.AddWithValue("Correo", oVendedor.Correo);
            Cn.Open();
            Cmd.ExecuteNonQuery();
            Cn.Close();
            return RedirectToAction("Listar", "Vendedor");
        }

        [HttpGet]
        public ActionResult Remover(int? Vendedor_id)
        {
            if (Vendedor_id is null)
            {
                return RedirectToAction("Listar", "Vendedor");
            }

            Vendedor oVendedor = lVendedores.Where(x => x.Vendedor_Id == Vendedor_id).FirstOrDefault();

            return View(oVendedor);
        }

        [HttpPost]
        public ActionResult Remover(Vendedor oVendedor)
        {
            SqlConnection Cn = new SqlConnection(cnStr);
            SqlCommand Cmd = new SqlCommand("sp_vendedor_remover", Cn);
            Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("Vendedor_Id", oVendedor.Vendedor_Id);
            Cn.Open();
            Cmd.ExecuteNonQuery();
            Cn.Close();
            return RedirectToAction("Listar", "Vendedor");
        }

    }
}