﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Controle_Cartao_Sus.DTO;
using System.IO;

namespace Controle_Cartao_Sus.DAL
{
    class DALOperador
    {
        ConexaoBD conexao;

        public DALOperador(ConexaoBD cx)
        {
            conexao = cx;
        }

        public string ExportarBancoCSV()
        {
            var dataAtual = DateTime.Now;
            var caminhoArquivo = @"c:\Backup_Cadsus_Sis";
            var nomeArquivo = $"arquivo_BKP_{dataAtual.ToString("yyyyMMddHHmm")}.csv";

            if (!Directory.Exists(caminhoArquivo))
            {
                Directory.CreateDirectory(caminhoArquivo);
            }

            var caminhoExportacaoFull = Path.Combine(caminhoArquivo,nomeArquivo);
            try
            {
                var con = conexao.ObjConexao;
                con.Open();

                var consulta = "SELECT * FROM tbl_Operador";                
                var cmd = new SqlCommand(consulta, con);
                var exec = cmd.ExecuteReader();
                var tabelaTemp = new DataTable();
                tabelaTemp.Load(exec);
                                
                var listaExportar = new List<string>();
                List<string> campos = tabelaTemp.AsEnumerable().Select(row => string.Join(";", row.ItemArray)).ToList();
                var linhas = string.Join(Environment.NewLine, campos);                
                listaExportar.Add(linhas);
                               
                using (StreamWriter sw = new StreamWriter(caminhoExportacaoFull, true, Encoding.UTF8))
                {
                   sw.WriteLine("ID; UNIDADE; OPERADOR; CNES; CPF; SENHA; NIVEL");
                    var linhaEscrita = string.Empty;
                    foreach (var linha in listaExportar)
                    {
                        var valor = linha.ToString();
                        linhaEscrita += valor != null ? $"{valor.ToString()};" : ";";
                                             
                    }
                    sw.WriteLine(linhaEscrita);                    
                }
                con.Close();
                return caminhoExportacaoFull;
            }
            catch (Exception)
            {
                throw;
            }
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
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tbl_Operador WHERE operador LIKE '%" + filtro + "%' or unidade LIKE '%" + filtro + "%'", DadosDaConexao.StrConexao);
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
