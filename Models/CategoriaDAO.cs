﻿using MySql.Data.MySqlClient; // importa o pacote de acesso ao MySql, pacote instalado com dotnet add package NomeDoPacote
using ApiTarefasNet80.Database; // namescape de configuração do acesso ao BD

namespace ApiTarefasNet80.Models
{
    public class CategoriaDAO
    {
        private static ConnectionMysql _conn; //cria o atributo privado de conexão ao banco de dados do tipo ConnectionMysql

        public CategoriaDAO() // construtor
        {
            _conn = new ConnectionMysql(); // inicializa a conexão ao instânciar ConnectionMysql()
        }

        public int Insert(Tarefa item) //método Insert recebe um parâmetro item do tipo Tarefa
        {
            try
            {
                var query = _conn.Query(); // método de ConnectionMysql, retorna um Objeto MySqlCommand
                query.CommandText = "INSERT INTO tarefas (descricao_tar, data_tar) VALUES (@descricao, @data)"; // @placeholders para os valores fornecidos abaixo

                query.Parameters.AddWithValue("@descricao", item.Descricao);
                query.Parameters.AddWithValue("@data", item.Data.ToString("yyyy-MM-dd HH:mm:ss")); //"10/11/1990" -> "1990-11-10"


                var result = query.ExecuteNonQuery(); //retorno o número de linhas afetadas

                if (result == 0)
                {
                    throw new Exception("O registro não foi inserido. Verifique e tente novamente");
                }

                return (int)query.LastInsertedId; // retorno o ultimo ID inserido
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _conn.Close(); //fecha aconexão com o banco de dados
            }
        }

        public List<Tarefa> List()
        {
            try
            {
                List<Tarefa> list = new List<Tarefa>();
                // List<Tarefa> list = [];

                var query = _conn.Query();
                query.CommandText = "SELECT * FROM tarefas";

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Tarefa()
                    {
                        Id = reader.GetInt32("id_tar"),
                        Descricao = reader.GetString("descricao_tar"),
                        Data = reader.GetDateTime("data_tar"),
                        Feito = reader.GetBoolean("feito_tar")
                    });
                }

                return list;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public Tarefa? GetById(int id)
        {
            try
            {
                Tarefa _tarefa = new Tarefa();
                // Tarefa _tarefa = new();

                var query = _conn.Query();
                query.CommandText = "SELECT * FROM tarefas WHERE id_tar = @_id";

                query.Parameters.AddWithValue("@_id", id);

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    _tarefa.Id = reader.GetInt32("id_tar");
                    _tarefa.Descricao = reader.GetString("descricao_tar");
                    _tarefa.Data = reader.GetDateTime("data_tar");
                    _tarefa.Feito = reader.GetBoolean("feito_tar");
                }

                return _tarefa;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public void Update(Tarefa item)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "UPDATE tarefas SET descricao_tar = @_descricao, feito_tar = @_feito WHERE id_tar = @_id";

                query.Parameters.AddWithValue("@_descricao", item.Descricao);
                query.Parameters.AddWithValue("@_feito", item.Feito);
                query.Parameters.AddWithValue("@_id", item.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O registro não foi atualizado. Verifique e tente novamente");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public void Delete(int id)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "DELETE FROM tarefas WHERE id_tar = @_id";

                query.Parameters.AddWithValue("@_id", id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O registro não foi excluído. Verifique e tente novamente");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

    }
}