using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Infra;
using Infra.Entidade;

namespace WebMandaEmail.Controllers
{
    public class SolicitacaoEmailsController : Controller
    {
        private readonly ContextoBase _context;

        public SolicitacaoEmailsController(ContextoBase context)
        {
            _context = context;
        }

        // GET: SolicitacaoEmails
        public async Task<IActionResult> Index()
        {
            return View(await _context.SolicitacaoEmail.ToListAsync());
        }

        // GET: SolicitacaoEmails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitacaoEmail = await _context.SolicitacaoEmail
                .FirstOrDefaultAsync(m => m.Id == id);
            if (solicitacaoEmail == null)
            {
                return NotFound();
            }

            return View(solicitacaoEmail);
        }

        // GET: SolicitacaoEmails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SolicitacaoEmails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Mensagem,Destinatarios,Enviado")] SolicitacaoEmail solicitacaoEmail)
        {
            if (ModelState.IsValid)
            {
                solicitacaoEmail.Enviado = false;
                _context.Add(solicitacaoEmail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(solicitacaoEmail);
        }

        // GET: SolicitacaoEmails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitacaoEmail = await _context.SolicitacaoEmail.FindAsync(id);
            if (solicitacaoEmail == null)
            {
                return NotFound();
            }
            return View(solicitacaoEmail);
        }

        // POST: SolicitacaoEmails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Mensagem,Destinatarios,Enviado")] SolicitacaoEmail solicitacaoEmail)
        {
            if (id != solicitacaoEmail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(solicitacaoEmail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolicitacaoEmailExists(solicitacaoEmail.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(solicitacaoEmail);
        }

        // GET: SolicitacaoEmails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitacaoEmail = await _context.SolicitacaoEmail
                .FirstOrDefaultAsync(m => m.Id == id);
            if (solicitacaoEmail == null)
            {
                return NotFound();
            }

            return View(solicitacaoEmail);
        }

        // POST: SolicitacaoEmails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var solicitacaoEmail = await _context.SolicitacaoEmail.FindAsync(id);
            _context.SolicitacaoEmail.Remove(solicitacaoEmail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SolicitacaoEmailExists(int id)
        {
            return _context.SolicitacaoEmail.Any(e => e.Id == id);
        }
    }
}
