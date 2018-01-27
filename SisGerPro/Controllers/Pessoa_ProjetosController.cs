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
    public class Pessoa_ProjetosController : Controller
    {
        private SisGerProEntities db = new SisGerProEntities();

        // GET: Pessoa_Projetos
        public async Task<ActionResult> Index()
        {
            var pessoa_Projetos = db.Pessoa_Projetos.Include(p => p.Pessoas).Include(p => p.Projetos);
            return View(await pessoa_Projetos.ToListAsync());
        }

        // GET: Pessoa_Projetos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa_Projetos pessoa_Projetos = await db.Pessoa_Projetos.FindAsync(id);
            if (pessoa_Projetos == null)
            {
                return HttpNotFound();
            }
            return View(pessoa_Projetos);
        }

        // GET: Pessoa_Projetos/Create
        public ActionResult Create()
        {
            ViewBag.id_Pessoa = new SelectList(db.Pessoas, "id_Pessoa", "Nome");
            ViewBag.id_Projeto = new SelectList(db.Projetos, "id_Projeto", "Nome");
            return View();
        }

        // POST: Pessoa_Projetos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_PessoaProjetos,id_Pessoa,id_Projeto")] Pessoa_Projetos pessoa_Projetos)
        {
            if (ModelState.IsValid)
            {
                db.Pessoa_Projetos.Add(pessoa_Projetos);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.id_Pessoa = new SelectList(db.Pessoas, "id_Pessoa", "Nome", pessoa_Projetos.id_Pessoa);
            ViewBag.id_Projeto = new SelectList(db.Projetos, "id_Projeto", "Nome", pessoa_Projetos.id_Projeto);
            return View(pessoa_Projetos);
        }

        // GET: Pessoa_Projetos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa_Projetos pessoa_Projetos = await db.Pessoa_Projetos.FindAsync(id);
            if (pessoa_Projetos == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_Pessoa = new SelectList(db.Pessoas, "id_Pessoa", "Nome", pessoa_Projetos.id_Pessoa);
            ViewBag.id_Projeto = new SelectList(db.Projetos, "id_Projeto", "Nome", pessoa_Projetos.id_Projeto);
            return View(pessoa_Projetos);
        }

        // POST: Pessoa_Projetos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_PessoaProjetos,id_Pessoa,id_Projeto")] Pessoa_Projetos pessoa_Projetos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pessoa_Projetos).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.id_Pessoa = new SelectList(db.Pessoas, "id_Pessoa", "Nome", pessoa_Projetos.id_Pessoa);
            ViewBag.id_Projeto = new SelectList(db.Projetos, "id_Projeto", "Nome", pessoa_Projetos.id_Projeto);
            return View(pessoa_Projetos);
        }

        // GET: Pessoa_Projetos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa_Projetos pessoa_Projetos = await db.Pessoa_Projetos.FindAsync(id);
            if (pessoa_Projetos == null)
            {
                return HttpNotFound();
            }
            return View(pessoa_Projetos);
        }

        // POST: Pessoa_Projetos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Pessoa_Projetos pessoa_Projetos = await db.Pessoa_Projetos.FindAsync(id);
            db.Pessoa_Projetos.Remove(pessoa_Projetos);
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
