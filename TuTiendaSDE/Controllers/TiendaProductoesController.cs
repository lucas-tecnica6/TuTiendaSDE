﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TuTiendaSDE.Models;

namespace TuTiendaSDE.Controllers
{
    public class TiendaProductoesController : Controller
    {
        private PpsContext db = new PpsContext();

        public int tiendas { get; private set; }

        // GET: TiendaProductoes
        public ActionResult Index()
        {
            var tiendaProducto = db.TiendaProducto.Include(t => t.Producto).Include(t => t.Tienda);
            return View(tiendaProducto.ToList());
        }

        // GET: TiendaProductoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiendaProducto tiendaProducto = db.TiendaProducto.Find(id);
            if (tiendaProducto == null)
            {
                return HttpNotFound();
            }
            return View(tiendaProducto);
        }

        // GET: TiendaProductoes/Create
        public ActionResult Create()
        {

            ViewBag.ProductoID = new SelectList(from p in db.Producto orderby p.Nombre select new { p.ProductoID, NombreMar = p.Nombre + " - " + p.Marca }, "ProductoID", "NombreMar");
            ViewBag.TiendaID = new SelectList(from r in db.Tienda orderby r.Nombre select new { r.TiendaID, NombreDir = r.Nombre + " - " + r.Direccion }, "TiendaID", "NombreDir");
            return View();
        }

        // POST: TiendaProductoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProTiendaID,TiendaID,ProductoID,Disponibilidad")] TiendaProducto tiendaProducto)
        {
            if (ModelState.IsValid)
            {
                var val = db.TiendaProducto.Where(r => r.Disponibilidad == tiendaProducto.Disponibilidad).FirstOrDefault();
                if(val != null)
                {
                    ModelState.AddModelError(string.Empty, "Que le ponémo");
                    return View(tiendaProducto);
                }

                db.TiendaProducto.Add(tiendaProducto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductoID = new SelectList(from p in db.Producto orderby p.Nombre select new {p.ProductoID, NombreMar = p.Nombre + " - " + p.Marca }, "ProductoID", "NombreMar", tiendaProducto.ProductoID);
            ViewBag.TiendaID = new SelectList(from r in db.Tienda orderby r.Nombre select new { r.TiendaID, NombreDir = r.Nombre + " - " + r.Direccion }, "TiendaID", "NombreDir", tiendaProducto.TiendaID);
            return View(tiendaProducto);
        }

        // GET: TiendaProductoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiendaProducto tiendaProducto = db.TiendaProducto.Find(id);
            if (tiendaProducto == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductoID = new SelectList(from p in db.Producto orderby p.Nombre select new { p.ProductoID, NombreMar = p.Nombre + " - " + p.Marca }, "ProductoID", "NombreMar", tiendaProducto.ProductoID);
            ViewBag.TiendaID = new SelectList(from r in db.Tienda orderby r.Nombre select new { r.TiendaID, NombreDir = r.Nombre + " - " + r.Direccion }, "TiendaID", "NombreDir", tiendaProducto.TiendaID);
            return View(tiendaProducto);
        }

        // POST: TiendaProductoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProTiendaID,TiendaID,ProductoID,Disponibilidad")] TiendaProducto tiendaProducto)
        {
            if (ModelState.IsValid)
            {
   

                db.Entry(tiendaProducto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductoID = new SelectList(from p in db.Producto orderby p.Nombre select new { p.ProductoID, NombreMar = p.Nombre + " - " + p.Marca }, "ProductoID", "NombreMar", tiendaProducto.ProductoID);
            ViewBag.TiendaID = new SelectList(from r in db.Tienda orderby r.Nombre select new { r.TiendaID, NombreDir = r.Nombre + " - " + r.Direccion }, "TiendaID", "NombreDir", tiendaProducto.TiendaID);
            return View(tiendaProducto);
        }

        // GET: TiendaProductoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiendaProducto tiendaProducto = db.TiendaProducto.Find(id);
            if (tiendaProducto == null)
            {
                return HttpNotFound();
            }
            return View(tiendaProducto);
        }

        // POST: TiendaProductoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TiendaProducto tiendaProducto = db.TiendaProducto.Find(id);
            db.TiendaProducto.Remove(tiendaProducto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Buscador()
        {
            ViewBag.ProductoID = new SelectList(from p in db.Producto orderby p.Nombre select new { p.ProductoID, NombreMar = p.Nombre + " - " + p.Marca }, "ProductoID", "NombreMar");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Buscador(int? ProductoID)
        {
            ViewBag.ProductoID = new SelectList(from p in db.Producto orderby p.Nombre select new { p.ProductoID, NombreMar = p.Nombre + " - " + p.Marca }, "ProductoID", "NombreMar", ProductoID);
            return View(tiendas);
            {
                if (ProductoID == null)
                {
                   
                    var val = db.TiendaProducto.Where(r => r.ProductoID == tiendas).FirstOrDefault();
                    if (val != null)
                    {
                        return View(tiendas);
                    }
                }
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
