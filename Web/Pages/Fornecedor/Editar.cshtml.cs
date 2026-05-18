using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nIKernel.Models.Fornecedor;
using nIKernel.Repositories;
using System.Threading.Tasks;

namespace nIKernel.Pages.Fornecedor
{
    public class EditarModel : PageModel
    {
        private readonly FornecedorRepository _fornecedorRepository;

        public EditarModel(FornecedorRepository fornecedorRepository)
        {
            _fornecedorRepository = fornecedorRepository;
        }

        [BindProperty]
        public FornecedorModel Fornecedor { get; set; } = new();

        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var fornecedor = await _fornecedorRepository.GetByIdAsync(id);
            if (fornecedor == null)
            {
                return RedirectToPage("Index");
            }

            Fornecedor = fornecedor;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _fornecedorRepository.UpdateAsync(Fornecedor);
                return RedirectToPage("Index");
            }
            catch
            {
                ErrorMessage = "Erro ao atualizar o fornecedor. Verifique os dados e tente novamente.";
                ModelState.AddModelError(string.Empty, ErrorMessage);
                return Page();
            }
        }
    }
}