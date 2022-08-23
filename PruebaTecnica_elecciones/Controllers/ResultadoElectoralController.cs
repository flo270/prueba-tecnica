using PruebaTecnica_elecciones.Models;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PruebaTecnica_elecciones.Controllers
{
    public class ResultdoElectoralController : Controller
    {
        // GET: ResultdoElectoral
        public ActionResult Index(string searchString)
        {
            using (PruebaTecnicaContexto db = new PruebaTecnicaContexto())
            {
                var ResultdoElectoral = from s in db.ResultdoElectoral
                               select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    ResultdoElectoral = ResultdoElectoral.Where(s => s.Provincia.NombreProvincia.Contains(searchString)
                                           || s.Partido.NombrePartido.Contains(searchString) );
                }
                return View(ResultdoElectoral.ToList());
            }
        }

        //crear ResultdoElectoral
        public ActionResult Crear()
        {
            using (PruebaTecnicaContexto db = new PruebaTecnicaContexto())
            {
                return View();
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(ResultdoElectoral RE)
        {
            using (PruebaTecnicaContexto db = new PruebaTecnicaContexto())
            {
                if (db.ResultdoElectoral.Any(p => p.IdRE == RE.IdRE))
                {
                    ModelState.AddModelError("idRE", "Ya existe idRE ingresado");
                }
                if (ModelState.IsValid)
                {
                    try
                    {
                      
                        db.ResultdoElectoral.Add(RE);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {

                        ModelState.AddModelError("ERROR AL AGREGAR ResultdoElectoral", ex);
                        return View();
                    }
                }
                return View();
            }

        }
        //Editar ResultdoElectoral
        public ActionResult Edit(int id)
        {
            using (PruebaTecnicaContexto db = new PruebaTecnicaContexto())
            {
                ResultdoElectoral reselec = db.ResultdoElectoral.Where(p => p.IdRE == id).FirstOrDefault();

                return View(reselec);
            }
        }

        // POST:/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ResultdoElectoral p)
        {
            using (PruebaTecnicaContexto db = new PruebaTecnicaContexto())
            {
                ResultdoElectoral re = db.ResultdoElectoral.Where(p1 => p1.IdRE == id).FirstOrDefault();

                re.PorcentajeElectoral= p.PorcentajeElectoral;
               re.cantidadVotos = p.cantidadVotos;
                re.ProvinciaPerteneciente = p.ProvinciaPerteneciente;
                re.PartidoPerteneciente = p.PartidoPerteneciente;

               re.Provincia= p.Provincia;
               

                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        //detallar ResultdoElectoral
        public ActionResult Details(int id)
        {
            using (PruebaTecnicaContexto db = new PruebaTecnicaContexto())
            {
                ResultdoElectoral re = db.ResultdoElectoral.Find(id);
                return View(re);

            }
        }
        //eliminar ResultdoElectoral
        public ActionResult Delete(int? id)
        {
            using (PruebaTecnicaContexto db = new PruebaTecnicaContexto())
            {
                ResultdoElectoral re = db.ResultdoElectoral.Find(id);
                if (re == null)
                {
                    return HttpNotFound();
                }
                return View(re);
            }


        }

        // POST: /Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            using (PruebaTecnicaContexto db = new PruebaTecnicaContexto())
            {
                ResultdoElectoral re = db.ResultdoElectoral.Find(id);
                db.ResultdoElectoral.Remove(re);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
        }
        //combo
        public ActionResult ListarProv()
        {
            using (PruebaTecnicaContexto db = new PruebaTecnicaContexto())
            {
                return PartialView(db.Provincia.ToList());
            }
        }
        //combo 2
        public ActionResult ListarPar()
        {
            using (PruebaTecnicaContexto db = new PruebaTecnicaContexto())
            {
                return PartialView(db.Partido.ToList());
            }
        }
        // mostrar datos provincia
        public static string Prov(int? prov)
        {
            using (PruebaTecnicaContexto db = new PruebaTecnicaContexto())
            {

                string Combo = db.Provincia.Find(prov).NombreProvincia +
                    ".";
                return Combo;

            }
        }
        // mostrar datos partido
        public static string Partido(int? partido)
        {
            using (PruebaTecnicaContexto db = new PruebaTecnicaContexto())
            {

                string Combo = db.Partido.Find(partido).NombrePartido +
                    ".";
                return Combo;

            }
        }
        // Reporte en PDF
        public ActionResult Report()
        {
            using (PruebaTecnicaContexto db = new PruebaTecnicaContexto())
            {
               return View(db.ResultdoElectoral.ToList());
            }
        }
        public ActionResult Print()
        {
            return new ActionAsPdf("Report") { FileName = "ReporteElectoral.pdf" };
        }


        // fin
    }
}