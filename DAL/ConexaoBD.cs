using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Controle_Cartao_Sus.DAL
{
    class ConexaoBD
    {
        string str;
        SqlConnection objConexao;

        public string Str { get => str; set => str = value; }
        public SqlConnection ObjConexao { get => objConexao; set => objConexao = value; }

        public ConexaoBD(string dadosDaCOonexao)
        {
            try
            {
                Str = dadosDaCOonexao;
                ObjConexao = new SqlConnection(Str);
            }
            catch (Exception erro)
            {

                throw new Exception(erro.Message.ToString());
            }
        }

        public void Conectar()
        {
            try
            {
                ObjConexao.Open();
            }
            catch (Exception erro)
            {

                throw new Exception(erro.Message.ToString());
            }
        }

        public void Desconectar()
        {
            try
            {
                ObjConexao.Close();
            }
            catch (Exception erro)
            {

                throw new Exception(erro.Message.ToString());
            }
        }

        ~ConexaoBD() { }
    }
}
