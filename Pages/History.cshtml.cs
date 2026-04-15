using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using pitWeb.Models;

namespace pitWeb.Pages
{
    public class HistoryModel : PageModel
    {
        private readonly AppDbContext _context;

        public HistoryModel(AppDbContext context)
        {
            _context = context;
        }

        public List<RozliczenieModel>? AllDocuments { get; set; }

        public void OnGet()
        {
            AllDocuments = _context.Rozliczenia
                .OrderByDescending(r => r.DataZapisu)
                .ToList();
        }
    }
}
