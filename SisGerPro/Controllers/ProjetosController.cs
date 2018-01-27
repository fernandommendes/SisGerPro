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
    public class ProjetosController : Controller
    {
        private SisGerProEntities db = new SisGerProEntities();

        // GET: Projetos
        public async Task<ActionResult> Index()
        {
            var projetos = db.Projetos.Include(p => p.Pessoas);
            return View(await projetos.ToListAsync());
        }

        // GET: Projetos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projetos projetos = await db.Projetos.FindAsync(id);
            if (projetos == null)
            {
                return HttpNotFound();
            }
            return View(projetos);
        }

        // GET: Projetos/Create
        public ActionResult Create()
        {
            ViewBag.id_Pessoa_Responsavel = new SelectList(db.Pessoas, "id_Pessoa", "Nome");
            return View();
        }

        // POST: Projetos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_Projeto,Nome,id_Pessoa_Responsavel,Finalizado,DtInicio,DtFim")] Projetos projetos)
        {
            if (ModelState.IsValid)
            {
                db.Projetos.Add(projetos);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.id_Pessoa_Responsavel = new SelectList(db.Pessoas, "id_Pessoa", "Nome", projetos.id_Pessoa_Responsavel);
            return View(projetos);
        }

        // GET: Projetos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projetos projetos = await db.Projetos.FindAsync(id);
            if (projetos == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_Pessoa_Responsavel = new SelectList(db.Pessoas, "id_Pessoa", "Nome", projetos.id_Pessoa_Responsavel);
            return View(projetos);
        }

        // POST: Projetos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_Projeto,Nome,id_Pessoa_Responsavel,Finalizado,DtInicio,DtFim")] Projetos projetos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projetos).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.id_Pessoa_Responsavel = new SelectList(db.Pessoas, "id_Pessoa", "Nome", projetos.id_Pessoa_Responsavel);
            return View(projetos);
        }

        // GET: Projetos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projetos projetos = await db.Projetos.FindAsync(id);
            if (projetos == null)
            {
                return HttpNotFound();
            }
            return View(projetos);
        }

        // POST: Projetos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Projetos projetos = await db.Projetos.FindAsync(id);
            db.Projetos.Remove(projetos);
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
