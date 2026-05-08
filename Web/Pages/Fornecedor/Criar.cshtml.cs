using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlConnector;
using nIKernel.Models.Fornecedor;

namespace nIKernel.Pages.Fornecedor
{ 
    public class CriarModel : PageModel
    {
        [BindProperty]
        public FornecedorModel Fornecedor { get; set; } = new();

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            using var conn = new MySqlConnection("Server=localhost;Database=dbKernel;Uid=root;Pwd=sua_senha;");
            conn.Open();
            var sql = "INSERT INTO TB_FOR_FORNECEDORES (FOR_NAM_FAN, FOR_CNPJ, FOR_END_COM, FOR_EML_CON, FOR_TEL_CON, FOR_DTA_INC) VALUES (@nome, @cnpj, @end, @email, @tel, NOW())";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@nome", Fornecedor.NomeFantasia);
            cmd.Parameters.AddWithValue("@cnpj", Fornecedor.Cnpj);
            cmd.Parameters.AddWithValue("@end", Fornecedor.EnderecoCompleto);
            cmd.Parameters.AddWithValue("@email", Fornecedor.Email);
            cmd.Parameters.AddWithValue("@tel", Fornecedor.Telefone);
            cmd.ExecuteNonQuery();

            return RedirectToPage("Index");
        }
    }
}