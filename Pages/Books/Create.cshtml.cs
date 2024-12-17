using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Trif_Samuel_Lab2.Data;
using Trif_Samuel_Lab2.Models;

namespace Trif_Samuel_Lab2.Pages.Books
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : BookCategoriesPageModel
    {
        private readonly Trif_Samuel_Lab2.Data.Trif_Samuel_Lab2Context _context;

        public CreateModel(Trif_Samuel_Lab2.Data.Trif_Samuel_Lab2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID",
"PublisherName");
            // ViewData["AuthorID"] = new SelectList(_context.Set<Author>(), "ID",
            // "FirstName", "LastName");
            var authorList = _context.Author.Select(x => new
            {
                   x.ID,
                   FullName = x.FirstName + " " + x.LastName
            });
             ViewData["AuthorID"] = new SelectList(authorList, "ID", "FullName");
            var book = new Book();
            book.BookCategories = new List<BookCategory>();
            PopulateAssignedCategoryData(_context, book);
            return Page();
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        /* conform model Lab3 seciunea asta trebuia inlocuita

        public async Task<IActionResult> OnPostAsync()
        {
           if (!ModelState.IsValid)
           {
               return Page();
           }

           _context.Book.Add(Book);
           await _context.SaveChangesAsync();

           return RedirectToPage("./Index");
        } */
        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            var newBook = new Book();
            if (selectedCategories != null)
            {
                newBook.BookCategories = new List<BookCategory>();
                foreach (var cat in selectedCategories)
                {
                    var catToAdd = new BookCategory
                    {
                        CategoryID = int.Parse(cat)
                    };
                    newBook.BookCategories.Add(catToAdd);
                }
            }
            Book.BookCategories = newBook.BookCategories;
            _context.Book.Add(Book);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
