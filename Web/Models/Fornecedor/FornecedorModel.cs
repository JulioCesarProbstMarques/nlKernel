using System;

namespace nIKernel.Models.Fornecedor
{
    public class FornecedorModel
    {
        public int Id { get; set; }
        public string NomeFantasia { get; set; } = string.Empty;
        public string Cnpj { get; set; } = string.Empty;
        public string EnderecoCompleto { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public char Status { get; set; } = 'A';
        public DateTime DataInclusao { get; set; }
    }
}