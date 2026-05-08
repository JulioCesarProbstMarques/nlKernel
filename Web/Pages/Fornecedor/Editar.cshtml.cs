using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlConnector;
using nIKernel.Models.Fornecedor;

namespace nIKernel.Pages.Fornecedor
{
    public class EditarModel : PageModel
    {
        [BindProperty]
        public FornecedorModel Fornecedor { get; set; } = new();

        public void OnGet(int id)
        {
            using var conn = new MySqlConnection("Server=localhost;Database=dbKernel;Uid=root;Pwd=sua_senha;");
            conn.Open();
            var sql = "SELECT FOR_ID, FOR_NAM_FAN, FOR_CNPJ, FOR_END_COM, FOR_EML_CON, FOR_TEL_CON FROM TB_FOR_FORNECEDORES WHERE FOR_ID = @id";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                Fornecedor.Id = reader.GetInt32(0);
                Fornecedor.NomeFantasia = reader.GetString(1);
                Fornecedor.Cnpj = reader.GetString(2);
                Fornecedor.EnderecoCompleto = reader.GetString(3);
                Fornecedor.Email = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                Fornecedor.Telefone = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
            }
        }

        public IActionResult OnPost()
        {
            using var conn = new MySqlConnection("Server=localhost;Database=dbKernel;Uid=root;Pwd=sua_senha;");
            conn.Open();
            var sql = @"UPDATE TB_FOR_FORNECEDORES SET 
                        FOR_NAM_FAN=@nome, FOR_CNPJ=@cnpj, FOR_END_COM=@end, 
                        FOR_EML_CON=@email, FOR_TEL_CON=@tel 
                        WHERE FOR_ID=@id";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@nome", Fornecedor.NomeFantasia);
            cmd.Parameters.AddWithValue("@cnpj", Fornecedor.Cnpj);
            cmd.Parameters.AddWithValue("@end", Fornecedor.EnderecoCompleto);
            cmd.Parameters.AddWithValue("@email", Fornecedor.Email);
            cmd.Parameters.AddWithValue("@tel", Fornecedor.Telefone);
            cmd.Parameters.AddWithValue("@id", Fornecedor.Id);
            cmd.ExecuteNonQuery();

            return RedirectToPage("Index");
        }
    }
}