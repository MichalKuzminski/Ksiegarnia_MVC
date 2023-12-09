using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ksiegarnia_MVC.Data;
using Ksiegarnia_MVC.Models;
using Microsoft.IdentityModel.Tokens;

namespace Ksiegarnia_MVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly Ksiegarnia_MVCContext _context;

        public BooksController(Ksiegarnia_MVCContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index(string sortOrder, string? searchString)
        {
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Data_wypozyczenia" ? "date_desc" : "Data_wypozyczenia";


            var books = _context.Book.Where(book => string.IsNullOrEmpty(searchString) || book.Title.Contains(searchString));



            switch (sortOrder)
            {
                case "title_desc":
                    books = books.OrderByDescending(b => b.Title);
                    break;
                case "Data_wypozyczenia":
                    books = books.OrderBy(b => b.PurchaseDate);
                    break;
                case "date_desc":
                    books = books.OrderByDescending(b => b.PurchaseDate);
                    break;
                default:
                    books = books.OrderBy(b => b.Title);
                    break;
            }

            ViewData["Searched"] = searchString;
            return View(await books.ToListAsync());
        }


        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            var customerList = _context.Customer.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.FirstName+" "+c.LastName
            }).ToList();

            // Dodaj domyślny element do listy rozwijanej
            customerList.Insert(0, new SelectListItem { Value = "", Text = "Wybierz klienta..." });

            // Przypisz listę do ViewBag
            ViewBag.CustomerList = customerList;
            return View(new Book());
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Author,PurchaseDate,Genre,Price,CustomerId")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            else
            {
                foreach (var modelError in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(modelError.ErrorMessage);
                }

            }
            ViewBag.CustomerList = _context.Customer
        .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.FirstName+" "+c.LastName })
        .ToList();
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewBag.CustomerList = _context.Customer.ToList();
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,PurchaseDate,Genre,Price, CustomerId")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
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
            ViewBag.CustomerList = _context.Customer.ToList();
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Book == null)
            {
                return Problem("Entity set 'Ksiegarnia_MVCContext.Book'  is null.");
            }
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
          return (_context.Book?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
