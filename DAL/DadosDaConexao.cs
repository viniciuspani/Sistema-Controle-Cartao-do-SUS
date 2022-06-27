using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Cartao_Sus.DAL
{
    class DadosDaConexao
    {
        static string servidor = @".\SQLEXPRESS";
        static string banco = "db_CadSus";
        //static string usuario = "sa";
        //static string senha = "sql2020";

        public static string Servidor { get => servidor; set => servidor = value; }
        public static string Banco { get => banco; set => banco = value; }

        public static string StrConexao
        {
            get
            {
                try
                {
                    return @"Data Source=" + servidor + ";Initial Catalog=" + banco + ";Integrated Security=True";
                    //return @"Data Source ="+servidor+"; Initial Catalog = "+banco+"; User ID = "+usuario+"; Password = "+senha+"";
                }
                catch (Exception erro)
                {

                    throw new Exception(erro.Message.ToString());
                }
                
            }
        }

        ~DadosDaConexao() { }
    }
}
