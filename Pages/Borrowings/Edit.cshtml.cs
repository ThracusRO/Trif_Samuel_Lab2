using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trif_Samuel_Lab2.Data;
using Trif_Samuel_Lab2.Models;

namespace Trif_Samuel_Lab2.Pages.Borrowings
{
    public class EditModel : PageModel
    {
        private readonly Trif_Samuel_Lab2.Data.Trif_Samuel_Lab2Context _context;

        public EditModel(Trif_Samuel_Lab2.Data.Trif_Samuel_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Borrowing Borrowing { get; set; } = default!;

        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // comentam linia originala: var borrowing =  await _context.Borrowing.FirstOrDefaultAsync(m => m.ID == id);
            //In loc de linia originala introducem:
            // ---
            var borrowing = await _context.Borrowing
                .Include(b => b.Book)
                .Include(b => b.Member)
                .FirstOrDefaultAsync(m => m.ID == id);
            // ---

            if (borrowing == null)
            {
                return NotFound();
            }
            Borrowing = borrowing;

            // Inlocuim pentru Book details: 
            // --_
            // Preluăm numele membrilor și detaliile cărților pentru listele derulante
            var bookList = _context.Book
                .Include(b => b.Author)
                .Select(x => new
                {
                    x.ID,
                    BookDetails = x.Title + " - " + x.Author.LastName + " " + x.Author.FirstName
                });

            ViewData["BookID"] = new SelectList(bookList, "ID", "BookDetails");
            // ---
            // Comentam linia originala: ViewData["BookID"] = new SelectList(_context.Book, "ID", "ID");

            // Comentam linia originala: ViewData["MemberID"] = new SelectList(_context.Member, "ID", "ID");

            //In loc de MemberID adaugam Member First name si Last name concatenat
            // ----
            ViewData["MemberID"] = new SelectList(_context.Member
            .Select(m => new
             {
            m.ID,
            FullName = m.FirstName + " " + m.LastName
             }), "ID", "FullName");
            // ---
            return Page();
            }
        
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Borrowing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BorrowingExists(Borrowing.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BorrowingExists(int id)
        {
            return _context.Borrowing.Any(e => e.ID == id);
        }
    }
}
