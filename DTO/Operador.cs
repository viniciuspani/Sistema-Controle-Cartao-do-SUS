using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Cartao_Sus.DTO
{
    class Operador
    {
        int id;
        string unidade;
        string nomeOperador;
        int cnes;
        string cpf;
        string senha;
        string nivel;

        public int Id { get => id; set => id = value; }
        public string Unidade { get => unidade; set => unidade = value; }
        public string NomeOperador { get => nomeOperador; set => nomeOperador = value; }
        public int Cnes { get => cnes; set => cnes = value; }
        public string Cpf { get => cpf; set => cpf = value; }
        public string Senha { get => senha; set => senha = value; }
        public string Nivel { get => nivel; set => nivel = value; }

        public Operador() { }
        ~Operador() { }
    }
}
