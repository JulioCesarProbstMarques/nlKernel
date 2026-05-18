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
                                f.FOR_ID AS Id,
                                f.FOR_NAM_FAN AS NomeFantasia,
                                f.FOR_CNPJ AS Cnpj,
                                f.FOR_EML_CON AS Email,
                                f.FOR_TEL_CON AS Telefone,
                                f.FOR_STA_ACT AS Status,
                                f.FOR_DTA_INC AS DataInclusao,
                                e.END_CEP AS EnderecoCep,
                                e.END_LOG AS EnderecoLogradouro,
                                e.END_NUM AS EnderecoNumero,
                                e.END_CPL AS EnderecoComplemento,
                                e.END_BAI AS EnderecoBairro,
                                e.END_CID AS EnderecoCidade,
                                e.END_EST AS EnderecoEstado
                            FROM tb_for_fornecedores f
                            LEFT JOIN tb_end_fornecedores e ON e.FOR_ID = f.FOR_ID";
            return await db.QueryAsync<FornecedorModel>(sql);
        }

        public async Task<FornecedorModel?> GetByIdAsync(int id)
        {
            using var db = new MySqlConnection(_connectionString);
            string sql = @"SELECT
                                f.FOR_ID AS Id,
                                f.FOR_NAM_FAN AS NomeFantasia,
                                f.FOR_CNPJ AS Cnpj,
                                f.FOR_EML_CON AS Email,
                                f.FOR_TEL_CON AS Telefone,
                                f.FOR_STA_ACT AS Status,
                                f.FOR_DTA_INC AS DataInclusao,
                                e.END_CEP AS EnderecoCep,
                                e.END_LOG AS EnderecoLogradouro,
                                e.END_NUM AS EnderecoNumero,
                                e.END_CPL AS EnderecoComplemento,
                                e.END_BAI AS EnderecoBairro,
                                e.END_CID AS EnderecoCidade,
                                e.END_EST AS EnderecoEstado
                            FROM tb_for_fornecedores f
                            LEFT JOIN tb_end_fornecedores e ON e.FOR_ID = f.FOR_ID
                            WHERE f.FOR_ID = @id";
            return await db.QueryFirstOrDefaultAsync<FornecedorModel>(sql, new { id });
        }

        public async Task UpdateAsync(FornecedorModel fornecedor)
        {
            using var db = new MySqlConnection(_connectionString);
            await db.OpenAsync();
            using var transaction = await db.BeginTransactionAsync();

            try
            {
                string sqlFornecedor = @"UPDATE tb_for_fornecedores SET
                                            FOR_NAM_FAN = @NomeFantasia,
                                            FOR_CNPJ = @Cnpj,
                                            FOR_END_COM = @EnderecoCompleto,
                                            FOR_EML_CON = @Email,
                                            FOR_TEL_CON = @Telefone,
                                            FOR_STA_ACT = @Status
                                          WHERE FOR_ID = @Id";

                await db.ExecuteAsync(sqlFornecedor, fornecedor, transaction);

                string sqlEnderecoUpdate = @"UPDATE tb_end_fornecedores SET
                                                END_CEP = @EnderecoCep,
                                                END_LOG = @EnderecoLogradouro,
                                                END_NUM = @EnderecoNumero,
                                                END_CPL = @EnderecoComplemento,
                                                END_BAI = @EnderecoBairro,
                                                END_CID = @EnderecoCidade,
                                                END_EST = @EnderecoEstado
                                              WHERE FOR_ID = @Id";

                var affected = await db.ExecuteAsync(sqlEnderecoUpdate, fornecedor, transaction);
                if (affected == 0)
                {
                    string sqlEnderecoInsert = @"INSERT INTO tb_end_fornecedores (
                                                    FOR_ID,
                                                    END_CEP,
                                                    END_LOG,
                                                    END_NUM,
                                                    END_CPL,
                                                    END_BAI,
                                                    END_CID,
                                                    END_EST
                                                ) VALUES (
                                                    @Id,
                                                    @EnderecoCep,
                                                    @EnderecoLogradouro,
                                                    @EnderecoNumero,
                                                    @EnderecoComplemento,
                                                    @EnderecoBairro,
                                                    @EnderecoCidade,
                                                    @EnderecoEstado
                                                )";
                    await db.ExecuteAsync(sqlEnderecoInsert, fornecedor, transaction);
                }

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task InsertAsync(FornecedorModel fornecedor)
        {
            using var db = new MySqlConnection(_connectionString);
            await db.OpenAsync();
            using var transaction = await db.BeginTransactionAsync();

            try
            {
                string sqlFornecedor = @"INSERT INTO tb_for_fornecedores (
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
                                        )";

                await db.ExecuteAsync(sqlFornecedor, fornecedor, transaction);
                var id = await db.ExecuteScalarAsync<long>("SELECT LAST_INSERT_ID()", transaction: transaction);
                fornecedor.Id = (int)id;

                string sqlEndereco = @"INSERT INTO tb_end_fornecedores (
                                            FOR_ID,
                                            END_CEP,
                                            END_LOG,
                                            END_NUM,
                                            END_CPL,
                                            END_BAI,
                                            END_CID,
                                            END_EST
                                        ) VALUES (
                                            @Id,
                                            @EnderecoCep,
                                            @EnderecoLogradouro,
                                            @EnderecoNumero,
                                            @EnderecoComplemento,
                                            @EnderecoBairro,
                                            @EnderecoCidade,
                                            @EnderecoEstado
                                        )";

                await db.ExecuteAsync(sqlEndereco, fornecedor, transaction);
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            using var db = new MySqlConnection(_connectionString);
            string sql = "DELETE FROM tb_for_fornecedores WHERE FOR_ID = @id";
            await db.ExecuteAsync(sql, new { id });
        }
    }
}