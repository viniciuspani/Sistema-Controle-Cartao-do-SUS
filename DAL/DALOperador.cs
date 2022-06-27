using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Controle_Cartao_Sus.DTO;


namespace Controle_Cartao_Sus.DAL
{
    class DALOperador
    {
        ConexaoBD conexao;

        public DALOperador(ConexaoBD cx)
        {
            conexao = cx;
        }

        public void Inserir (Operador operador)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexao.ObjConexao;
                cmd.CommandText = "INSERT INTO tbl_Operador VALUES(@unidade, @nome, @cnes, @cpf, @senha, @nivel); SELECT @@IDENTITY;";
                cmd.Parameters.AddWithValue("@unidade", operador.Unidade);
                cmd.Parameters.AddWithValue("@nome", operador.NomeOperador);
                cmd.Parameters.AddWithValue("@cnes", operador.Cnes);
                cmd.Parameters.AddWithValue("@cpf", operador.Cpf);
                cmd.Parameters.AddWithValue("@senha", operador.Senha);
                cmd.Parameters.AddWithValue("@nivel", operador.Nivel);
                conexao.Conectar();
                operador.Id = Convert.ToInt32(cmd.ExecuteScalar());
                conexao.Desconectar();
            }
            catch (Exception erro)
            {

                throw new Exception(erro.Message.ToString());
            }
        }

        public DataTable Pesquisar(string filtro)
        {
            try
            {
                DataTable tabela = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tbl_Operador WHERE operador LIKE '%" + filtro + "%'", DadosDaConexao.StrConexao);
                da.Fill(tabela);
                return tabela;
            }
            catch (Exception erro)
            {

                throw new Exception(erro.Message.ToString());
            }
        }
        public Operador Carregar(int id)
        {
            try
            {
                Operador operador = new Operador();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexao.ObjConexao;
                cmd.CommandText = "SELECT * FROM tbl_Operador WHERE id = @cod";
                cmd.Parameters.AddWithValue("@cod", id);
                conexao.Conectar();
                SqlDataReader registro = cmd.ExecuteReader();
                if (registro.HasRows)
                {
                    registro.Read();
                    operador.Unidade = registro["unidade"].ToString();
                    operador.NomeOperador = registro["operador"].ToString();
                    operador.Cnes = Convert.ToInt32(registro["cnes"]);
                    operador.Cpf = registro["cpf"].ToString();
                    operador.Senha = registro["senha"].ToString();
                    operador.Nivel = registro["nivel"].ToString();
                    conexao.Desconectar();
                }
                return operador;
            }
            catch (Exception erro)
            {

                throw new Exception(erro.Message.ToString());
            }
            
        }

        public void Editar(Operador operador, int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexao.ObjConexao;
                cmd.CommandText = "UPDATE tbl_Operador SET unidade=@unidade, operador=@nome, cnes=@cnes, cpf=@cpf, senha=@senha, nivel=@nivel WHERE id = @cod";
                cmd.Parameters.AddWithValue("@unidade", operador.Unidade);
                cmd.Parameters.AddWithValue("@nome", operador.NomeOperador);
                cmd.Parameters.AddWithValue("@cnes", operador.Cnes);
                cmd.Parameters.AddWithValue("@cpf", operador.Cpf);
                cmd.Parameters.AddWithValue("@senha", operador.Senha);
                cmd.Parameters.AddWithValue("@nivel", operador.Nivel);
                cmd.Parameters.AddWithValue("@cod", id);
                conexao.Conectar();
                cmd.ExecuteNonQuery();
                conexao.Desconectar();
            }
            catch (Exception erro)
            {

                throw new Exception(erro.Message.ToString());
            }
            
        }
        
        public void Excluir(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexao.ObjConexao;
                cmd.CommandText = "DELETE FROM tbl_Operador WHERE id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                conexao.Conectar();
                cmd.ExecuteNonQuery();
                conexao.Desconectar();
            }
            catch (Exception erro)
            {

                throw new Exception(erro.Message.ToString());
            }
        }
    }
}
