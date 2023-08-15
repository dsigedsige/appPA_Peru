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
    public class tblEstadosController : ApiController
    {
        private Proyecto_3REntities1 db = new Proyecto_3REntities1();

        // GET: api/tblEstados
        public IQueryable<tbl_Estados> Gettbl_Estados()
        {
            return db.tbl_Estados;
        }
                       
        public object Gettbl_Estados(int opcion, string filtro)
        {
            Resultado res = new Resultado();
            object resul = null;
            try
            {
                if (opcion == 1)
                {
                    string[] parametros = filtro.Split('|');
                    int idEstado = Convert.ToInt32(parametros[0].ToString());


                    res.ok = true;
                    res.data = (from a in db.tbl_Estados
                                where a.estado == idEstado
                                select new
                                {
                                    a.id_Estado,
                                    a.abreviatura_estado,
                                    a.descripcion_estado,
                                    a.orden_estado,
                                    a.Backcolor_estado,
                                    a.forecolor_estado,
                                    a.estado,
                                    descripcionEstado = a.estado == 0 ? "INACTIVO" : "ACTIVO",
                                    a.usuario_creacion
                                }).ToList();
                    res.totalpage = 0;
                    resul = res;
                }
                else if (opcion == 2)
                {
                    string[] parametros = filtro.Split('|');
                    int id = Convert.ToInt32(parametros[0].ToString());

                    tbl_Estados objReemplazar;
                    objReemplazar = db.tbl_Estados.Where(u => u.id_Estado == id).FirstOrDefault<tbl_Estados>();
                    objReemplazar.estado = 0;

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
                    resul = res;

                }
                else if (opcion == 3)
                {
                    string[] parametros = filtro.Split('|');
                    string abreviatura_estado = parametros[0].ToString();

                    if (db.tbl_Estados.Count(e => e.abreviatura_estado.ToUpper() == abreviatura_estado.ToUpper()) > 0)
                    {
                        resul = true;
                    }
                    else
                    {
                        resul = false;
                    }
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

        public object Posttbl_Estados(tbl_Estados tbl_Estados)
        {
            Resultado res = new Resultado();
            try
            {
                tbl_Estados.fecha_creacion = DateTime.Now;
                db.tbl_Estados.Add(tbl_Estados);
                db.SaveChanges();

                res.ok = true;
                res.data = (from a in db.tbl_Estados
                            where a.id_Estado == tbl_Estados.id_Estado
                            select new
                            {
                                a.id_Estado,
                                a.abreviatura_estado,
                                a.descripcion_estado,
                                a.orden_estado,
                                a.Backcolor_estado,
                                a.forecolor_estado,
                                a.estado,
                                descripcionEstado = a.estado == 0 ? "INACTIVO" : "ACTIVO",
                                a.usuario_creacion,
                            }).ToList();
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

        public object Puttbl_Estados(int id, tbl_Estados tbl_Estados)
        {
            Resultado res = new Resultado();

            tbl_Estados objReemplazar;
            objReemplazar = db.tbl_Estados.Where(u => u.id_Estado == id).FirstOrDefault<tbl_Estados>();

            objReemplazar.abreviatura_estado = tbl_Estados.abreviatura_estado;
            objReemplazar.orden_estado = tbl_Estados.orden_estado;
            objReemplazar.orden_estado = tbl_Estados.orden_estado;
            objReemplazar.Backcolor_estado = tbl_Estados.Backcolor_estado;
            objReemplazar.forecolor_estado = tbl_Estados.forecolor_estado;

            objReemplazar.estado = tbl_Estados.estado;
            objReemplazar.usuario_edicion = tbl_Estados.usuario_creacion;
            objReemplazar.fecha_edicion = DateTime.Now;

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
               
        // DELETE: api/tblEstados/5
        [ResponseType(typeof(tbl_Estados))]
        public IHttpActionResult Deletetbl_Estados(int id)
        {
            tbl_Estados tbl_Estados = db.tbl_Estados.Find(id);
            if (tbl_Estados == null)
            {
                return NotFound();
            }

            db.tbl_Estados.Remove(tbl_Estados);
            db.SaveChanges();

            return Ok(tbl_Estados);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_EstadosExists(int id)
        {
            return db.tbl_Estados.Count(e => e.id_Estado == id) > 0;
        }
    }
}