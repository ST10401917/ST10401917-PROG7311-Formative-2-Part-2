using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PROG7311_PART_2.Data;
using PROG7311_PART_2.Factories;
using PROG7311_PART_2.Models;
using PROG7311_PART_2.Service;

namespace PROG7311_PART_2.Controllers
{
    public class ContractsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ContractNotificationService _notificationService;

        public ContractsController(AppDbContext context)
        {
            _context = context;
            _notificationService = new ContractNotificationService();
        }

        // GET: Index
        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate, ContractStatus? status)
        {
            var query = _context.Contracts.Include(c => c.Client).AsQueryable();

            if (startDate.HasValue)
                query = query.Where(c => c.StartDate >= startDate);

            if (endDate.HasValue)
                query = query.Where(c => c.EndDate <= endDate);

            if (status.HasValue)
                query = query.Where(c => c.Status == status);

            return View(await query.ToListAsync());
        }

        // GET: Create
        public IActionResult Create()
        {
            ViewBag.Clients = _context.Clients.ToList();
            return View();
        }

        // POST: Create (FACTORY + FILE UPLOAD)
        [HttpPost]
        public async Task<IActionResult> Create(Contract contract, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Clients = _context.Clients.ToList();

                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                return View(contract);
            }

            var factory = ContractFactory.GetContract(contract.ServiceLevel);
            var newContract = factory.Create(
            contract.ClientId,
            contract.StartDate,
            contract.EndDate,
            contract.Status   
             );

            newContract.ServiceLevel = contract.ServiceLevel;


            var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName).ToLower();

                if (extension != ".pdf")
                {
                    ModelState.AddModelError("file", "Only PDF files are allowed.");
                    ViewBag.Clients = _context.Clients.ToList();
                    return View(contract);
                }

                var fileName = Guid.NewGuid() + "_" + file.FileName;
                var path = Path.Combine(folder, fileName);

                using var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);

                newContract.SignedAgreementPath = "/files/" + fileName;
            }

            _context.Add(newContract);
            await _context.SaveChangesAsync();

            _notificationService.NotifyContractChange(newContract);

            return RedirectToAction(nameof(Index));
        }

        // file download
        public IActionResult Download(string path)
        {
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", path.TrimStart('/'));

            var fileBytes = System.IO.File.ReadAllBytes(fullPath);
            return File(fileBytes, "application/pdf", Path.GetFileName(fullPath));
        }

        // GET: Contracts/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var contract = await _context.Contracts
                .Include(c => c.Client)
                .FirstOrDefaultAsync(c => c.ContractId == id);

            if (contract == null)
                return NotFound();

            return View(contract);
        }

        // POST: Contracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contract = await _context.Contracts.FindAsync(id);

            if (contract == null)
                return NotFound();

            _context.Contracts.Remove(contract);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
