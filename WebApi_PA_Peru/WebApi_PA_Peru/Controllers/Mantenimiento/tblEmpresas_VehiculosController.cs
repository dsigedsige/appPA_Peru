using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Datos;
using Negocio.Resultados;

namespace WebApi_3R_Dominion.Controllers.Mantenimiento
{
    [EnableCors("*", "*", "*")]
    public class tblEmpresas_VehiculosController : ApiController
    {
        private Proyecto_3REntities1 db = new Proyecto_3REntities1();

        // GET: api/tblEmpresas_Vehiculos
        public IQueryable<tbl_Empresas_Vehiculos> Gettbl_Empresas_Vehiculos()
        {
            return db.tbl_Empresas_Vehiculos;
        }


        public object Gettbl_Distrito(int opcion, string filtro)
        {
            Resultado res = new Resultado();
            object resul = null;
            try
            {
                if (opcion == 1)
                {
                    string[] parametros = filtro.Split('|');
                    int idEmpresa = Convert.ToInt32(parametros[0].ToString());

                    res.ok = true;
                    res.data = (from a in db.tbl_Empresas_Vehiculos
                                where a.id_Empresa == idEmpresa
                                select new
                                {
                                    a.id_Empresa_Vehiculo,
                                    a.id_Empresa,
                                    a.nro_Placa,
                                    a.cantidadM3
                                }).ToList();
                    res.totalpage = 0;
                    resul = res;
                }
                else if (opcion == 2)
                {
                    string[] parametros = filtro.Split('|');
                    int idEmpresaVehiculo = Convert.ToInt32(parametros[0].ToString());

                    tbl_Empresas_Vehiculos tbl_Empresas_Vehiculos = db.tbl_Empresas_Vehiculos.Find(idEmpresaVehiculo);

                    db.tbl_Empresas_Vehiculos.Remove(tbl_Empresas_Vehiculos);
                    try
                    {
                        db.SaveChanges();
                        res.ok = true;
                        res.data = "OK";
                        res.totalpage = 0;
                    }
                    catch (Exception ex)
                    {
                        res.ok = false;
                        res.data = ex.Message;
                        res.totalpage = 0;
                    }
                    resul = res;
                }
                else
                {
                    res.ok = false;
                    res.data = "Opcion seleccionada invalida";
                    res.totalpage = 0;

                    resul = res;
                }
            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.Message;
                res.totalpage = 0;
                resul = res;
            }
            return resul;
        }

        public object Posttbl_Empresas_Vehiculos(tbl_Empresas_Vehiculos tbl_Empresas_Vehiculos)
        {
            Resultado res = new Resultado();
            try
            {
                db.tbl_Empresas_Vehiculos.Add(tbl_Empresas_Vehiculos);
                db.SaveChanges();

                res.ok = true;
                res.data = tbl_Empresas_Vehiculos.id_Empresa_Vehiculo;
                res.totalpage = 0;
            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.Message;
                res.totalpage = 0;
            }
            return res;
        }

        public object Puttbl_Empresas_Vehiculos(int id, tbl_Empresas_Vehiculos tbl_Empresas_Vehiculos)
        {
            Resultado res = new Resultado();

            tbl_Empresas_Vehiculos objReemplazar;
            objReemplazar = db.tbl_Empresas_Vehiculos.Where(u => u.id_Empresa_Vehiculo == id).FirstOrDefault<tbl_Empresas_Vehiculos>();

            objReemplazar.nro_Placa = tbl_Empresas_Vehiculos.nro_Placa;
            objReemplazar.cantidadM3 = tbl_Empresas_Vehiculos.cantidadM3;

             db.Entry(objReemplazar).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
                res.ok = true;
                res.data = "OK";
                res.totalpage = 0;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                res.ok = false;
                res.data = ex.InnerException.Message;
                res.totalpage = 0;
            }

            return res;
        }
        
        // DELETE: api/tblEmpresas_Vehiculos/5
        [ResponseType(typeof(tbl_Empresas_Vehiculos))]
        public IHttpActionResult Deletetbl_Empresas_Vehiculos(int id)
        {
            tbl_Empresas_Vehiculos tbl_Empresas_Vehiculos = db.tbl_Empresas_Vehiculos.Find(id);
            if (tbl_Empresas_Vehiculos == null)
            {
                return NotFound();
            }

            db.tbl_Empresas_Vehiculos.Remove(tbl_Empresas_Vehiculos);
            db.SaveChanges();

            return Ok(tbl_Empresas_Vehiculos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_Empresas_VehiculosExists(int id)
        {
            return db.tbl_Empresas_Vehiculos.Count(e => e.id_Empresa_Vehiculo == id) > 0;
        }
    }
}