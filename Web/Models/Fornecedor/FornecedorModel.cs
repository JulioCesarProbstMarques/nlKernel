using System;
using System.ComponentModel.DataAnnotations;

namespace nIKernel.Models.Fornecedor
{
    public class FornecedorModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome fantasia é obrigatório.")]
        [StringLength(100, ErrorMessage = "Nome fantasia deve ter no máximo 100 caracteres.")]
        public string NomeFantasia { get; set; } = string.Empty;

        [Required(ErrorMessage = "CNPJ é obrigatório.")]
        [StringLength(18, ErrorMessage = "CNPJ deve ter no máximo 18 caracteres.")]
        public string Cnpj { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Telefone é obrigatório.")]
        [StringLength(30, ErrorMessage = "Telefone deve ter no máximo 30 caracteres.")]
        public string Telefone { get; set; } = string.Empty;

        [Required(ErrorMessage = "CEP é obrigatório.")]
        public string EnderecoCep { get; set; } = string.Empty;
        [Required(ErrorMessage = "Logradouro é obrigatório.")]
        public string EnderecoLogradouro { get; set; } = string.Empty;

        [Required(ErrorMessage = "Número é obrigatório.")]
        public string EnderecoNumero { get; set; } = string.Empty;

        public string EnderecoComplemento { get; set; } = string.Empty;

        [Required(ErrorMessage = "Bairro é obrigatório.")]
        public string EnderecoBairro { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cidade é obrigatória.")]
        public string EnderecoCidade { get; set; } = string.Empty;

        [Required(ErrorMessage = "Estado é obrigatório.")]
        [StringLength(2, ErrorMessage = "Informe a sigla do estado.")]
        public string EnderecoEstado { get; set; } = string.Empty;
        public char Status { get; set; } = 'A';
        public DateTime DataInclusao { get; set; }

        public string EnderecoCompleto => string.IsNullOrWhiteSpace(EnderecoLogradouro)
            ? string.Empty
            : $"{EnderecoLogradouro}, {EnderecoNumero}{(string.IsNullOrWhiteSpace(EnderecoComplemento) ? string.Empty : " - " + EnderecoComplemento)} - {EnderecoBairro}, {EnderecoCidade}/{EnderecoEstado} - CEP {EnderecoCep}";
    }
}