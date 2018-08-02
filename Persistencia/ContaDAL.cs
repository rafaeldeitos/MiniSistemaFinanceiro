using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Modelo;

namespace Persistencia
{
    public class ContaDAL
    {
        private SqlConnection _conn;
        private CategoriaDAL _categoria;


        public ContaDAL(SqlConnection conn)
        {
            var strConn = DB.Conexao.getStringConnection();
            this._conn = conn;
            _categoria = new CategoriaDAL(new SqlConnection(strConn));
        }

        public List<Conta> ListarTodos(DateTime? dataInicial = null, DateTime? dataFinal = null)
        {
            List<Conta> contas = new List<Conta>();

            StringBuilder sql = new StringBuilder(" select c.id, c.descricao, c.valor, c.tipo, c.data_vencimento, cat.nome, cat.id  as categoria_id" +
                                                  " from contas c" +
                                                  " inner join categorias cat on cat.id = c.categoria_id");
            if (dataInicial != null && dataFinal != null)
                sql.Append($" where c.data_vencimento between '{dataInicial.ToString()}' and '{dataFinal.ToString()}'");

            var command = new SqlCommand(sql.ToString(), this._conn);
            this._conn.Open();

            using (SqlDataReader rd = command.ExecuteReader())
            {
                while (rd.Read())
                {
                    Conta conta = new Conta()
                    {
                        Id = Convert.ToInt32(rd["id"].ToString()),
                        Descricao = rd["descricao"].ToString(),
                        Tipo = Convert.ToChar(rd["tipo"].ToString()),
                        Valor = Convert.ToDouble(rd["valor"].ToString()),
                        DataVencimento = Convert.ToDateTime(rd["data_vencimento"].ToString())
                    };
                    conta.Categoria = this._categoria.GetCategoria(Convert.ToInt32(conta.Id));
                    contas.Add(conta);
                }
            }
            this._conn.Close();
            return contas;
        }

        void Cadastrar(Conta conta)
        {
            this._conn.Open();
            SqlCommand commandInsert = this._conn.CreateCommand();
            commandInsert.CommandText = "insert into contas(descricao, tipo, valor, data_vencimento, categoria_id) " +
                                        "values (@descricao, @tipo, @valor, @data_vencimento, @categoria_id)";
            commandInsert.Parameters.AddWithValue("@descricao", conta.Descricao);
            commandInsert.Parameters.AddWithValue("@tipo", conta.Tipo);
            commandInsert.Parameters.AddWithValue("@valor", conta.Valor);
            commandInsert.Parameters.AddWithValue("@data_vencimento", conta.DataVencimento);
            commandInsert.Parameters.AddWithValue("@categoria_id", conta.Categoria.Id);
            commandInsert.ExecuteNonQuery();
            this._conn.Close();
        }

        void Editar(Conta conta)
        {
            ///
        }

        public void Salvar(Conta conta)
        {
            if (conta.Id == null)
            {
                Cadastrar(conta);
            }
            else
            {
                Editar(conta);
            }
        }
    }
}
