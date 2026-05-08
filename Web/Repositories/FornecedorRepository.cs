using Dapper;
using MySqlConnector;
using Microsoft.Extensions.Configuration;
using nIKernel.Models.Fornecedor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace nIKernel.Repositories
{
    public class FornecedorRepository
    {
        private readonly string _connectionString;

        public FornecedorRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        public async Task<IEnumerable<FornecedorModel>> GetAllAsync()
        {
            using var db = new MySqlConnection(_connectionString);
            string sql = @"SELECT
                                FOR_ID AS Id,
                                FOR_NAM_FAN AS NomeFantasia,
                                FOR_CNPJ AS Cnpj,
                                FOR_END_COM AS EnderecoCompleto,
                                FOR_EML_CON AS Email,
                                FOR_TEL_CON AS Telefone,
                                FOR_STA_ACT AS Status,
                                FOR_DTA_INC AS DataInclusao
                            FROM tb_for_fornecedores";
            return await db.QueryAsync<FornecedorModel>(sql);
        }

        public async Task<FornecedorModel?> GetByIdAsync(int id)
        {
            using var db = new MySqlConnection(_connectionString);
            string sql = @"SELECT
                                FOR_ID AS Id,
                                FOR_NAM_FAN AS NomeFantasia,
                                FOR_CNPJ AS Cnpj,
                                FOR_END_COM AS EnderecoCompleto,
                                FOR_EML_CON AS Email,
                                FOR_TEL_CON AS Telefone,
                                FOR_STA_ACT AS Status,
                                FOR_DTA_INC AS DataInclusao
                            FROM tb_for_fornecedores
                            WHERE FOR_ID = @id";
            return await db.QueryFirstOrDefaultAsync<FornecedorModel>(sql, new { id });
        }

        public async Task UpdateAsync(FornecedorModel fornecedor)
        {
            using var db = new MySqlConnection(_connectionString);
            string sql = @"UPDATE tb_for_fornecedores SET
                                FOR_NAM_FAN = @NomeFantasia,
                                FOR_CNPJ = @Cnpj,
                                FOR_END_COM = @EnderecoCompleto,
                                FOR_EML_CON = @Email,
                                FOR_TEL_CON = @Telefone,
                                FOR_STA_ACT = @Status
                            WHERE FOR_ID = @Id";
            await db.ExecuteAsync(sql, fornecedor);
        }

        public async Task InsertAsync(FornecedorModel fornecedor)
        {
            using var db = new MySqlConnection(_connectionString);
            string sql = @"INSERT INTO tb_for_fornecedores (
                                FOR_NAM_FAN,
                                FOR_CNPJ,
                                FOR_END_COM,
                                FOR_EML_CON,
                                FOR_TEL_CON,
                                FOR_DTA_INC
                             ) VALUES (
                                @NomeFantasia,
                                @Cnpj,
                                @EnderecoCompleto,
                                @Email,
                                @Telefone,
                                NOW()
                             );";
            await db.ExecuteAsync(sql, fornecedor);
        }

        public async Task DeleteAsync(int id)
        {
            using var db = new MySqlConnection(_connectionString);
            string sql = "DELETE FROM tb_for_fornecedores WHERE FOR_ID = @id";
            await db.ExecuteAsync(sql, new { id });
        }
    }
}