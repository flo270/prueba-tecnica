using PruebaTecnica_elecciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PruebaTecnica_elecciones.Controllers
{
    public class PartidoController : Controller
    {
        // GET: Partido
        public ActionResult Index(string searchString)
        {
            using (PruebaTecnicaContexto db = new PruebaTecnicaContexto())
            {
                var Partidos = from s in db.Partido
                                 select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    Partidos = Partidos.Where(s => s.NombrePartido.Contains(searchString));
                }

                return View(Partidos.ToList());
            }

        }
        //crearPartido
        public ActionResult Crear()
        {
            using (PruebaTecnicaContexto db = new PruebaTecnicaContexto())
            {
                return View();
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Partido prov)
        {
            using (PruebaTecnicaContexto db = new PruebaTecnicaContexto())
            {

                if (ModelState.IsValid)
                {
                    try
                    {
                        db.Partido.Add(prov);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {

                        ModelState.AddModelError("ERROR AL AGREGARPartido", ex);
                        return View();
                    }
                }
                return View();
            }

        }
        //EditarPartido
        public ActionResult Edit(int id)
        {
            using (PruebaTecnicaContexto db = new PruebaTecnicaContexto())
            {
                Partido reg = db.Partido.Find(id);

                return View(reg);
            }
        }

        // POST:/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Partido p)
        {
            using (PruebaTecnicaContexto db = new PruebaTecnicaContexto())
            {
                Partido reg = db.Partido.Find(id);

                reg.NombrePartido = p.NombrePartido;
               
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        //detallarPartido
        public ActionResult Details(int id)
        {
            using (PruebaTecnicaContexto db = new PruebaTecnicaContexto())
            {
                Partido reg = db.Partido.Find(id);
                return View(reg);

            }
        }
        //eliminarPartido
        public ActionResult Delete(int? id)
        {
            using (PruebaTecnicaContexto db = new PruebaTecnicaContexto())
            {
                Partido reg = db.Partido.Find(id);
                if (reg == null)
                {
                    return HttpNotFound();
                }
                return View(reg);
            }


        }

        // POST:Partido/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            using (PruebaTecnicaContexto db = new PruebaTecnicaContexto())
            {
                Partido prov = db.Partido.Find(id);
                db.Partido.Remove(prov);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
        }
    }
}