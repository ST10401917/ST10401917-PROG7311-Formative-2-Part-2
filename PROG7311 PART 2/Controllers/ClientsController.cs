using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG7311_PART_2.Data;
using PROG7311_PART_2.Models;

namespace PROG7311_PART_2.Controllers
{
    public class ClientsController : Controller
    {
        private readonly AppDbContext _context;

        public ClientsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clients.ToListAsync());
        }

        // GET: Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var client = await _context.Clients.FindAsync(id);
            if (client == null) return NotFound();

            return View(client);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Client client)
        {
            if (id != client.ClientId) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(client);
        }



        // GET: Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var client = await _context.Clients.FirstOrDefaultAsync(c => c.ClientId == id);
            return View(client);
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
