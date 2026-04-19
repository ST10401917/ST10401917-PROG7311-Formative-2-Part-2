using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PROG7311_PART_2.Data;
using PROG7311_PART_2.Models;
using PROG7311_PART_2.Service;


namespace PROG7311_PART_2.Controllers
{
    public class ServiceRequestsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICurrencyService _currencyService;

        public ServiceRequestsController(AppDbContext context, ICurrencyService currencyService)
        {
            _context = context;
            _currencyService = currencyService;
        }

        // GET: Index
        public async Task<IActionResult> Index()
        {
            return View(await _context.ServiceRequests.Include(s => s.Contract).ToListAsync());
        }

        // GET: Create
        public IActionResult Create()
        {
            ViewBag.Contracts = _context.Contracts
                        .Include(c => c.Client)
                  .Where(c => c.Status == ContractStatus.Active)
                  .ToList();

            return View();
        }

        // POST: Create
        [HttpPost]
        public async Task<IActionResult> Create(ServiceRequest request)
        {
            var contract = await _context.Contracts
                .FirstOrDefaultAsync(c => c.ContractId == request.ContractId);

            // 🚫 WORKFLOW RULE ENFORCEMENT
            if (contract == null)
            {
                ModelState.AddModelError("", "Contract not found.");
                ViewBag.Contracts = _context.Contracts.ToList();
                return View(request);
            }

            if (contract.Status == ContractStatus.Expired ||
                contract.Status == ContractStatus.OnHold)
            {
                ModelState.AddModelError("", "Cannot create Service Request. Contract is not active.");
                ViewBag.Contracts = _context.Contracts.ToList();
                return View(request);
            }

            // 🌍 Currency conversion (your existing logic)
            var rate = await _currencyService.GetUsdToZarRate();
            request.CostZAR = request.CostUSD * rate;

            _context.Add(request);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // DELETE
        public async Task<IActionResult> Delete(int id)
        {
            var request = await _context.ServiceRequests
                .FirstOrDefaultAsync(x => x.ServiceRequestId == id);

            if (request == null)
                return NotFound();

            return View(request);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var request = await _context.ServiceRequests.FindAsync(id);

            if (request == null)
                return NotFound();

            _context.ServiceRequests.Remove(request);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
