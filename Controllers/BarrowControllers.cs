using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Models;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Controllers
{
    public class BorrowController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BorrowController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Borrow
        public async Task<IActionResult> Index()
        {
            var borrowRecords = await _context.BorrowRecords.ToListAsync();
            return View(borrowRecords);
        }

        // GET: Borrow/Create
        public IActionResult Create()
        {
            ViewBag.Books = _context.Books.ToList(); 
            ViewBag.Users = _context.Users.ToList(); 
            return View();
        }

        // POST: Borrow/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookId,UserId,BorrowDate,ReturnDate")] BorrowRecord borrowRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(borrowRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Books = _context.Books.ToList(); 
            ViewBag.Users = _context.Users.ToList(); 
            return View(borrowRecord);
        }

        // GET: Borrow/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowRecord = await _context.BorrowRecords.FindAsync(id);
            if (borrowRecord == null)
            {
                return NotFound();
            }
            ViewBag.Books = _context.Books.ToList(); 
            ViewBag.Users = _context.Users.ToList(); 
            return View(borrowRecord);
        }

        // POST: Borrow/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookId,UserId,BorrowDate,ReturnDate")] BorrowRecord borrowRecord)
        {
            if (id != borrowRecord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(borrowRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BorrowRecordExists(borrowRecord.Id))
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
            ViewBag.Books = _context.Books.ToList(); 
            ViewBag.Users = _context.Users.ToList(); 
            return View(borrowRecord);
        }

        // GET: Borrow/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowRecord = await _context.BorrowRecords
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borrowRecord == null)
            {
                return NotFound();
            }

            return View(borrowRecord);
        }

        // POST: Borrow/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var borrowRecord = await _context.BorrowRecords.FindAsync(id);
            _context.BorrowRecords.Remove(borrowRecord);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BorrowRecordExists(int id)
        {
            return _context.BorrowRecords.Any(e => e.Id == id);
        }
    }
}
