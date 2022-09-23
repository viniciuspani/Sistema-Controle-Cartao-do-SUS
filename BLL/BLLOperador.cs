using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controle_Cartao_Sus.DAL;
using Controle_Cartao_Sus.DTO;
using System.Data.SqlClient;
using System.Data;

namespace Controle_Cartao_Sus.BLL
{
    class BLLOperador
    {
        ConexaoBD conexao;

        public BLLOperador(ConexaoBD cx)
        {
            conexao = cx;
        }

        public string ExportarBancoCSV()
        {
            DALOperador objDAL = new DALOperador(conexao);
            return objDAL.ExportarBancoCSV();
        }

        public void Inserir(Operador operador)
        {
            if (operador.Unidade.Trim().Length == 0)
            {
                throw new Exception("O campo Unidade não foi preenchido!");
            }

            if (operador.NomeOperador.Trim().Length == 0)
            {
                throw new Exception("O campo Nome não foi preenchido!");
            }

            if (Convert.ToInt32(operador.Cnes) < 0)
            {
                throw new Exception("O campo Cnes não foi preenchido corretamente!");
            }

            if (Validador.ETexto(operador.NomeOperador))
            {
                throw new Exception("O campo não pode conter numeros!");
            }

            if (Validador.ETexto(operador.Unidade))
            {
                throw new Exception("O campo não pode conter numeros!");
            }

            if (Validador.ETexto(operador.Nivel))
            {
                throw new Exception("O campo não pode conter numeros!");
            }


            DALOperador objDAL = new DALOperador(conexao);
            objDAL.Inserir(operador);
        }

        public DataTable Pesquisar(string filtro)
        {

            DALOperador objDAL = new DALOperador(conexao);
            
            if ( objDAL.Pesquisar(filtro) == null)
            {
                throw new Exception("Não há Operador com este nome!");
            }
            return objDAL.Pesquisar(filtro);
        }

        public Operador Carregar(int id)
        {
            if (id < 0)
            {
                throw new Exception("Falha ao receber id de pesquisa do banco!");
            }
            DALOperador objDAL = new DALOperador(conexao);
            return objDAL.Carregar(id);
        }

        public void Editar(Operador operador, int id)
        {
            if (operador.Unidade.Trim().Length == 0)
            {
                throw new Exception("O campo deve ser preenchido para alteração!");
            }

            if (operador.NomeOperador.Trim().Length == 0)
            {
                throw new Exception("O campo deve ser preenchido para alteração!");
            }
            DALOperador objDAL = new DALOperador(conexao);
            objDAL.Editar(operador,id);
        }

        public void Excluir(int id)
        {
            if (id < 0)
            {
                throw new Exception("Falha na exclusão do operador!\n ID inexistente!");
            }
            DALOperador objDAL = new DALOperador(conexao);
            objDAL.Excluir(id);
        }
    }
}
