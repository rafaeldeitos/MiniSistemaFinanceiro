using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using Modelo;
//using CategoriaModelo = Modelo.Categoria; //Alias para using

namespace Persistencia
{
    public class CategoriaDAL
    {
        private SqlConnection _conn;

        public CategoriaDAL(SqlConnection conn)
        {
            this._conn = conn;
        }

        public Categoria GetCategoria(int? id)
        {
            Categoria categoria = new Categoria();
            var command = new SqlCommand(" select c.id, c.nome" +
                                         " from categorias c" +
                                         " where c.id = @id", this._conn);
            command.Parameters.AddWithValue("@id", id); 
            this._conn.Open();
            using (SqlDataReader rd = command.ExecuteReader())
            {
                rd.Read();
                categoria.Id = Convert.ToInt32(rd["id"].ToString());
                categoria.Nome = rd["nome"].ToString();
            }
            this._conn.Close();
            return categoria;
        }

        public List<Categoria> ListarTodos()
        {
            List<Categoria> categorias = new List<Categoria>();

            var command = new SqlCommand(" select cat.nome, cat.id" +
                                         " from categorias cat" , this._conn);
            this._conn.Open();

            using (SqlDataReader rd = command.ExecuteReader())
            {
                while (rd.Read())
                {
                    Categoria categoria = new Categoria()
                    {
                        Id = Convert.ToInt32(rd["id"].ToString()),
                        Nome = rd["nome"].ToString()
                    };
                    categorias.Add(categoria);
                }
            }
            this._conn.Close();
            return categorias;
        }


    }
}
