using PruebaTecnica_elecciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PruebaTecnica_elecciones.Controllers
{
    public class ProvinciaController : Controller
    {
        // GET: Provincia
        public ActionResult Index(string searchString)
        {
            using (PruebaTecnicaContexto db = new PruebaTecnicaContexto())
            {
                var Provincias = from s in db.Provincia
                                         select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    Provincias = Provincias.Where(s => s.NombreProvincia.Contains(searchString));
                }

                return View(Provincias.ToList());
            }

        }
        //crearProvincia
        public ActionResult Crear()
        {
            using (PruebaTecnicaContexto db = new PruebaTecnicaContexto())
            {
                return View();
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Provincia prov)
        {
            using (PruebaTecnicaContexto db = new PruebaTecnicaContexto())
            {

                if (ModelState.IsValid)
                {
                    try
                    {
                        db.Provincia.Add(prov);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {

                        ModelState.AddModelError("ERROR AL AGREGARProvincia", ex);
                        return View();
                    }
                }
                return View();
            }

        }
        //EditarProvincia
        public ActionResult Edit(int id)
        {
            using (PruebaTecnicaContexto db = new PruebaTecnicaContexto())
            {
                Provincia reg = db.Provincia.Find(id);

                return View(reg);
            }
        }

        // POST:/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Provincia p)
        {
            using (PruebaTecnicaContexto db = new PruebaTecnicaContexto())
            {
                Provincia reg = db.Provincia.Find(id);

                reg.NombreProvincia = p.NombreProvincia;
                reg.CantidadVotantes= p.CantidadVotantes;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        //detallarProvincia
        public ActionResult Details(int id)
        {
            using (PruebaTecnicaContexto db = new PruebaTecnicaContexto())
            {
                Provincia reg = db.Provincia.Find(id);
                return View(reg);

            }
        }
        //eliminarProvincia
        public ActionResult Delete(int? id)
        {
            using (PruebaTecnicaContexto db = new PruebaTecnicaContexto())
            {
                Provincia reg = db.Provincia.Find(id);
                if (reg == null)
                {
                    return HttpNotFound();
                }
                return View(reg);
            }


        }

        // POST:Provincia/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            using (PruebaTecnicaContexto db = new PruebaTecnicaContexto())
            {
                Provincia prov = db.Provincia.Find(id);
                db.Provincia.Remove(prov);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
        }
        //combo
        public ActionResult ListarPartido()
        {
            using (PruebaTecnicaContexto db = new PruebaTecnicaContexto())
            {
                return PartialView(db.Partido.ToList());
            }
        }
        // mostrar datos provincia
        public static string partido(int? partido)
        {
            using (PruebaTecnicaContexto db = new PruebaTecnicaContexto())
            {

                string Combo = db.Partido.Find(partido).NombrePartido +
                     ".";
                return Combo;

            }
        }
        // fin
    }
}