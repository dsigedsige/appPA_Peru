﻿using System;
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
using Negocio.Mantenimientos;
using Negocio.Resultados;

namespace WebApi_3R_Dominion.Controllers.Mantenimiento
{
    [EnableCors("*", "*", "*")]
    public class tblUsuariosController : ApiController
    {
        private Proyecto_3REntities1 db = new Proyecto_3REntities1();

        // GET: api/tblUsuarios
        public IQueryable<tbl_Usuarios> Gettbl_Usuarios()
        {
            return db.tbl_Usuarios;
        }


        public object Gettbl_Personal(int opcion, string filtro)
        {
            Resultado res = new Resultado();
            Proveedor_BL obj_negocio = new Proveedor_BL();
            object resul = null;
            try
            {
                if (opcion == 1)
                {
                    res.ok = true;
                    res.data = (from a in db.tbl_Estados
                                select new
                                {
                                    a.id_Estado,
                                    a.descripcion_estado
                                }).ToList();
                    res.totalpage = 0;
                    resul = res;
                }
                else if (opcion == 2)
                {
                    res.ok = true;
                    res.data = (from a in db.tbl_Empresas
                                where a.estado == 1
                                select new
                                {
                                    a.id_Empresa,
                                    a.razonSocial_Empresa
                                }).ToList();
                    res.totalpage = 0;
                    resul = res;
                }
                else if (opcion == 3)
                {
                    res.ok = true;
                    res.data = (from a in db.tbl_Servicios
                                where a.estado == 1
                                select new
                                {
                                    checkeado = false,
                                    a.id_Servicios,
                                    a.nombreServicio
                                }).ToList();
                    res.totalpage = 0;
                    resul = res;
                }
                else if (opcion == 4)
                {
                    string[] parametros = filtro.Split('|');
                    int idEmpresa = Convert.ToInt32(parametros[0].ToString());
                    int idArea = Convert.ToInt32(parametros[1].ToString());
                    int idEstado = Convert.ToInt32(parametros[2].ToString());

                    //res.ok = true;
                    //res.data = (from a in db.tbl_Usuarios
                    //            join b in db.tbl_Perfil on a.id_Perfil equals b.id_perfil
                    //            join c in db.tbl_Personal on a.nrodoc_usuario equals c.nroDocumento_Personal
                    //            join d in db.tbl_Empresas on c.id_Empresa equals d.id_Empresa 
                    //            select new
                    //            {
                    //                a.id_Usuario,
                    //                a.nrodoc_usuario,
                    //                a.apellidos_usuario,
                    //                a.nombres_usuario,
                    //                id_empresa  = c.id_Empresa,
                    //                empresa_usuario = d.razonSocial_Empresa,
                    //                a.email_usuario,
                    //                a.id_TipoUsuario,
                    //                a.id_Perfil,
                    //                b.descripcion_perfil,
                    //                a.fotourl,
                    //                a.login_usuario,
                    //                a.contrasenia_usuario,
                    //                a.estado,
                    //                descripcion_estado = a.estado == 0 ? "INACTIVO" : "ACTIVO",
                    //                a.usuario_creacion
                    //            }).ToList();
                    //res.totalpage = 0;
                    //resul = res;
                    Usuarios_BL objNegocio = new Usuarios_BL();
                    res.ok = true;
                    res.data = objNegocio.get_usuariosMantenimiento(idEmpresa, idArea, idEstado);
                    res.totalpage = 0;
                    resul = res;


                }
                else if (opcion == 5)
                {
                    string[] parametros = filtro.Split('|');
                    int idUsuario = Convert.ToInt32(parametros[0].ToString());

                    tbl_Usuarios objReemplazar;
                    objReemplazar = db.tbl_Usuarios.Where(u => u.id_Usuario == idUsuario).FirstOrDefault<tbl_Usuarios>();
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
                else if (opcion == 6)
                {
                    string[] parametros = filtro.Split('|');
                    string nroDoc = parametros[0].ToString();

                    if (db.tbl_Usuarios.Count(e => e.nrodoc_usuario == nroDoc) > 0)
                    {
                        resul = true;
                    }
                    else
                    {
                        resul = false;
                    }
                }
                else if (opcion == 7)  /// perfil
                {

                    res.ok = true;
                    res.data = (from a in db.tbl_Perfil
                                where a.estado == 1
                                select new
                                {
                                    a.id_perfil,
                                    a.descripcion_perfil
                                }).ToList();
                    res.totalpage = 0;
                    resul = res;
                }
                else if (opcion == 8)  /// buscar por dni
                {
                    string[] parametros = filtro.Split('|');
                    string nroDoc = parametros[0].ToString();

                    res.ok = true;
                    res.data = (from a in db.tbl_Personal 
                                join b in db.tbl_Empresas on a.id_Empresa equals b.id_Empresa
                                where a.nroDocumento_Personal == nroDoc &&   a.estado == 1
                                select new
                                {
                                    a.id_Personal,
                                    a.id_Empresa,
                                    b.razonSocial_Empresa,
                                    a.nombres_Personal,
                                    a.apellidos_Personal
                                }).ToList();
                    res.totalpage = 0;
                    resul = res;
                }
                else if (opcion == 9)
                {
                    string[] parametros = filtro.Split('|');
                    string loggin = parametros[0].ToString();

                    if (db.tbl_Usuarios.Count(e => e.login_usuario == loggin) > 0)
                    {
                        resul = true;
                    }
                    else
                    {
                        resul = false;
                    }
                }
                else if (opcion == 10)   
                {
                    string[] parametros = filtro.Split('|');

                    int idUsuarioBD = Convert.ToInt32(parametros[0].ToString());
                    string areasMasivo = parametros[1].ToString();
                    int idusuarioLoggin = Convert.ToInt32(parametros[2].ToString());
                    
                    res.ok = true;
                    res.data = obj_negocio.set_grabar_areasMasivas(idUsuarioBD, areasMasivo, idusuarioLoggin);
                    res.totalpage = 0;
                    resul = res;
                }
                else if (opcion == 11)   
                {
                    string[] parametros = filtro.Split('|');
                    int idUsuarioBD = Convert.ToInt32(parametros[0].ToString());

                    res.ok = true;
                    res.data = (from a in db.tbl_Usuarios_Servicios
                                where a.id_usuario == idUsuarioBD
                                select new
                                {
                                    a.id_servicio
                                }).ToList();
                    res.totalpage = 0;
                    resul = res;
                }
                else if (opcion == 12)
                {
                    string[] parametros = filtro.Split('|');
                    int idUsuarioBD = Convert.ToInt32(parametros[0].ToString()); 

                    res.ok = true;
                    res.data = obj_negocio.get_generarDescargar_CodigoQR(idUsuarioBD);
                    res.totalpage = 0;
                    resul = res;
                }
                else if (opcion == 13)
                {
                    res.ok = true;
                    res.data = (from a in db.tbl_Usuarios
                                where a.estado == 1
                                orderby a.id_Usuario ascending
                                select new
                                {
                                    checkeado = false,
                                    a.id_Usuario,
                                    a.nrodoc_usuario,
                                    apellidos_usuario = a.apellidos_usuario + " " + a.nombres_usuario,
                                    a.nombres_usuario
                                }).ToList();
                    res.totalpage = 0;
                    resul = res;
                }
                else if (opcion == 14)
                {
                    string[] parametros = filtro.Split('|');
                    string idOpciones = parametros[0].ToString();

                    Usuarios_BL obj_negocios = new Usuarios_BL();

                    res.ok = true;
                    res.data = obj_negocios.get_usuariosAccesos(idOpciones);
                    res.totalpage = 0;
                    resul = res;
                }
                else if (opcion == 15)
                {
                    string[] parametros = filtro.Split('|');
                    string idOpciones = parametros[0].ToString();
                    int idUsuario = Convert.ToInt32(parametros[1].ToString());

                    Usuarios_BL obj_negocios = new Usuarios_BL();

                    res.ok = true;
                    res.data = obj_negocios.get_eventosUsuarioMarcados(idOpciones, idUsuario);
                    res.totalpage = 0;
                    resul = res;
                }
                else if (opcion == 16)
                {
                    string[] parametros = filtro.Split('|');
                    string idOpciones = parametros[0].ToString();
                    string idEventos = parametros[1].ToString();
                    int idPrincipal = Convert.ToInt32(parametros[2].ToString());
                    string modalElegido = parametros[3].ToString();

                    Usuarios_BL obj_negocios = new Usuarios_BL();
                    

                    res.ok = true;
                    if (modalElegido == "usuarios")
                    {
                        res.data = obj_negocios.set_grabandoEventos(idOpciones, idEventos, idPrincipal);
                    }
                    if (modalElegido == "perfiles")
                    {
                        res.data = obj_negocios.set_grabandoEventosPerfiles(idOpciones, idEventos, idPrincipal);
                    }
                    res.totalpage = 0;
                    resul = res;

                    //res.ok = true;
                    //res.data = obj_negocios.set_grabandoEventos(idOpciones, idEventos, idUsuario);
                    //res.totalpage = 0;
                    //resul = res;
                }

                else if (opcion == 17) //----- PERFILES
                {
                    res.ok = true;
                    res.data = (from a in db.tbl_Perfil 
                                where a.estado == 1
                                select new
                                {
                                    checkeando = false,
                                    a.id_perfil,
                                    a.descripcion_perfil
                                }).ToList();
                    res.totalpage = 0;
                    resul = res;
                }
                else if (opcion == 18)
                {
                    string[] parametros = filtro.Split('|');
                    string idOpciones = parametros[0].ToString();

                    Usuarios_BL obj_negocios = new Usuarios_BL();

                    res.ok = true;
                    res.data = obj_negocios.get_perfilAccesos(idOpciones);
                    res.totalpage = 0;
                    resul = res;
                }
                else if (opcion == 19)
                {
                    string[] parametros = filtro.Split('|');
                    string idOpciones = parametros[0].ToString();
                    int idPerfil = Convert.ToInt32(parametros[1].ToString());

                    Usuarios_BL obj_negocios = new Usuarios_BL();

                    res.ok = true;
                    res.data = obj_negocios.get_eventosPerfilMarcados(idOpciones, idPerfil);
                    res.totalpage = 0;
                    resul = res;
                }
                else if (opcion == 20)
                {
                    string[] parametros = filtro.Split('|');
                    string idOpciones = parametros[0].ToString();
                    int idUsuario = Convert.ToInt32(parametros[1].ToString());

                    Usuarios_BL obj_negocios = new Usuarios_BL();
                    
                    res.ok = true;
                    res.data = obj_negocios.set_eliminarAccesos(idOpciones, idUsuario);
                    res.totalpage = 0;
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

        public object Posttbl_Usuarios(tbl_Usuarios tbl_Usuarios)
        {
            Resultado res = new Resultado();
            try
            {
                tbl_Usuarios.fecha_creacion = DateTime.Now;
                db.tbl_Usuarios.Add(tbl_Usuarios);
                db.SaveChanges();

                res.ok = true;
                res.data = tbl_Usuarios.id_Usuario;
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

        public object Puttbl_Usuarios(int id, tbl_Usuarios tbl_Usuarios)
        {
            Resultado res = new Resultado();

            tbl_Usuarios objReemplazar;
            objReemplazar = db.tbl_Usuarios.Where(u => u.id_Usuario == id).FirstOrDefault<tbl_Usuarios>();

            objReemplazar.nrodoc_usuario = tbl_Usuarios.nrodoc_usuario;
            objReemplazar.apellidos_usuario = tbl_Usuarios.apellidos_usuario;
            objReemplazar.nombres_usuario = tbl_Usuarios.nombres_usuario;
            objReemplazar.email_usuario = tbl_Usuarios.email_usuario;
            //objReemplazar.id_TipoUsuario = tbl_Usuarios.id_TipoUsuario;
            objReemplazar.id_Perfil = tbl_Usuarios.id_Perfil;

            //objReemplazar.fotourl = tbl_Usuarios.fotourl;
            objReemplazar.login_usuario = tbl_Usuarios.login_usuario;
            objReemplazar.contrasenia_usuario = tbl_Usuarios.contrasenia_usuario;

            objReemplazar.estado = tbl_Usuarios.estado;
            objReemplazar.usuario_edicion = tbl_Usuarios.usuario_creacion;
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


        // DELETE: api/tblUsuarios/5
        [ResponseType(typeof(tbl_Usuarios))]
        public IHttpActionResult Deletetbl_Usuarios(int id)
        {
            tbl_Usuarios tbl_Usuarios = db.tbl_Usuarios.Find(id);
            if (tbl_Usuarios == null)
            {
                return NotFound();
            }

            db.tbl_Usuarios.Remove(tbl_Usuarios);
            db.SaveChanges();

            return Ok(tbl_Usuarios);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_UsuariosExists(int id)
        {
            return db.tbl_Usuarios.Count(e => e.id_Usuario == id) > 0;
        }
    }
}