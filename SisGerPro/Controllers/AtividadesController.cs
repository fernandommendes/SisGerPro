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
    public class AtividadesController : Controller
    {
        private SisGerProEntities db = new SisGerProEntities();

        // GET: Atividades
        public async Task<ActionResult> Index()
        {
            var atividades = db.Atividades.Include(a => a.Etapas);
            return View(await atividades.ToListAsync());
        }

        // GET: Atividades/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Atividades atividades = await db.Atividades.FindAsync(id);
            if (atividades == null)
            {
                return HttpNotFound();
            }
            return View(atividades);
        }

        // GET: Atividades/Create
        public ActionResult Create()
        {
            ViewBag.id_Etapa = new SelectList(db.Etapas, "id_Etapa", "Nome");
            return View();
        }

        // POST: Atividades/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_Atividade,Nome,Feita,DtFeita,id_Etapa")] Atividades atividades)
        {
            if (ModelState.IsValid)
            {
                db.Atividades.Add(atividades);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.id_Etapa = new SelectList(db.Etapas, "id_Etapa", "Nome", atividades.id_Etapa);
            return View(atividades);
        }

        // GET: Atividades/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Atividades atividades = await db.Atividades.FindAsync(id);
            if (atividades == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_Etapa = new SelectList(db.Etapas, "id_Etapa", "Nome", atividades.id_Etapa);
            return View(atividades);
        }

        // POST: Atividades/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_Atividade,Nome,Feita,DtFeita,id_Etapa")] Atividades atividades)
        {
            if (ModelState.IsValid)
            {
                db.Entry(atividades).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.id_Etapa = new SelectList(db.Etapas, "id_Etapa", "Nome", atividades.id_Etapa);
            return View(atividades);
        }

        // GET: Atividades/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Atividades atividades = await db.Atividades.FindAsync(id);
            if (atividades == null)
            {
                return HttpNotFound();
            }
            return View(atividades);
        }

        // POST: Atividades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Atividades atividades = await db.Atividades.FindAsync(id);
            db.Atividades.Remove(atividades);
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
