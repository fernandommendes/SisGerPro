using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SisGerPro.Models;

namespace SisGerPro.Controllers
{
    public class EtapasController : Controller
    {
        private SisGerProEntities db = new SisGerProEntities();

        // GET: Etapas
        public async Task<ActionResult> Index()
        {
            var etapas = db.Etapas.Include(e => e.Projetos);
            return View(await etapas.ToListAsync());
        }

        // GET: Etapas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Etapas etapas = await db.Etapas.FindAsync(id);
            if (etapas == null)
            {
                return HttpNotFound();
            }
            return View(etapas);
        }

        // GET: Etapas/Create
        public ActionResult Create()
        {
            ViewBag.id_Projeto = new SelectList(db.Projetos, "id_Projeto", "Nome");
            return View();
        }

        // POST: Etapas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_Etapa,Nome,Feita,DtFeita,id_Projeto")] Etapas etapas)
        {
            if (ModelState.IsValid)
            {
                db.Etapas.Add(etapas);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.id_Projeto = new SelectList(db.Projetos, "id_Projeto", "Nome", etapas.id_Projeto);
            return View(etapas);
        }

        // GET: Etapas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Etapas etapas = await db.Etapas.FindAsync(id);
            if (etapas == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_Projeto = new SelectList(db.Projetos, "id_Projeto", "Nome", etapas.id_Projeto);
            return View(etapas);
        }

        // POST: Etapas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_Etapa,Nome,Feita,DtFeita,id_Projeto")] Etapas etapas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(etapas).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.id_Projeto = new SelectList(db.Projetos, "id_Projeto", "Nome", etapas.id_Projeto);
            return View(etapas);
        }

        // GET: Etapas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Etapas etapas = await db.Etapas.FindAsync(id);
            if (etapas == null)
            {
                return HttpNotFound();
            }
            return View(etapas);
        }

        // POST: Etapas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Etapas etapas = await db.Etapas.FindAsync(id);
            db.Etapas.Remove(etapas);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
