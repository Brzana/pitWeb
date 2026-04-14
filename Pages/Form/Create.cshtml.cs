using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using pitWeb.Models;
using pitWeb.Services;

namespace pitWeb.Pages.Form
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;
        private ITaxService _taxService;

        public CreateModel(AppDbContext context, ITaxService taxService)
        {
            _context = context;
            _taxService = taxService;
        }

        [BindProperty]
        public RozliczenieModel Rozliczenie { get; set; } = new();

        [BindProperty]
        public decimal Przychod { get; set; }

        [BindProperty]
        public decimal Koszty { get; set; }

        public decimal DochodStrata { get; set; }
        public decimal PodatekNalezy { get; set; }

        public void OnGet() { }

        public IActionResult OnPostCalculate()
        {
            PodatekNalezy = _taxService.CalculateTax(Przychod, Koszty);

            return Page();
        }

        public async Task<IActionResult> OnPostSaveAsync()
        {
            if (!ModelState.IsValid) return Page();

            Rozliczenie.DataZapisu = DateTime.Now;
            Rozliczenie.PodatekNalezy = _taxService.CalculateTax(Przychod, Koszty);

            _context.Rozliczenia.Add(Rozliczenie);
            await _context.SaveChangesAsync();

            return Redirect("/Index");
        }
    }
}
