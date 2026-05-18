using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nIKernel.Repositories;
using nIKernel.Models.Fornecedor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace nIKernel.Pages.Fornecedor
{
    public class IndexModel : PageModel
    {
        private readonly FornecedorRepository _FornecedorRepo;

        public IndexModel(FornecedorRepository fornecedorRepo)
        {
            _FornecedorRepo = fornecedorRepo;
        }

        public IEnumerable<FornecedorModel> ListaFornecedores { get; set; } = new List<FornecedorModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            ListaFornecedores = await _FornecedorRepo.GetAllAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _FornecedorRepo.DeleteAsync(id);
            return RedirectToPage();
        }
    }
}