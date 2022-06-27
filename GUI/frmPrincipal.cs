using System;
using System.Windows.Forms;
using Controle_Cartao_Sus.BLL;
using Controle_Cartao_Sus.DAL;
using Controle_Cartao_Sus.DTO;

namespace Controle_Cartao_Sus
{
    public partial class frmPrincipal : Form
    {
        string opcao = "";        
        int cod = 0;        
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void Exibicao(int codexibicao)
        {
            switch (codexibicao)
            {
                //EXIBICAO INICAL DO PROGRAMA
                case 1:
                    txtLocal.Enabled = false;
                    txtOperador.Enabled = false;
                    txtCNES.Enabled = false;
                    txtCPF.Enabled = false;
                    txtSenha.Enabled = false;
                    txtNivel.Enabled = false;
                    txtPesquisar.Enabled = true;
                    btnSalvar.Enabled = false;
                    btnInserir.Enabled = true;
                    btnEditar.Enabled = false;
                    btnExcluir.Enabled = false;
                    btnCancelar.Enabled = true;
                    btnSair.Enabled = true;
                    btnPesquisar.Enabled = true;
                break;

                    //EXIBICAO QND VAI INSERIR NOVO OPERADOR
                case 2:
                    txtLocal.Enabled = true;
                    txtOperador.Enabled = true;
                    txtCNES.Enabled = true;
                    txtCPF.Enabled = true;
                    txtSenha.Enabled = true;
                    txtNivel.Enabled = true;
                    txtPesquisar.Enabled = true;
                    btnSalvar.Enabled = true;
                    btnInserir.Enabled = true;
                    btnEditar.Enabled = false;
                    btnExcluir.Enabled = false;
                    btnCancelar.Enabled = true;
                    btnSair.Enabled = true;
                    btnPesquisar.Enabled = true;
                    break;

                    //EXIBICAO QND VAI SER FEITO PESQUISA DE OPERADOR
                case 3:
                    txtLocal.Enabled = true;
                    txtOperador.Enabled = true;
                    txtCNES.Enabled = true;
                    txtCPF.Enabled = true;
                    txtSenha.Enabled = true;
                    txtNivel.Enabled = true;
                    txtPesquisar.Enabled = true;
                    btnSalvar.Enabled = true;
                    btnInserir.Enabled = true;
                    btnEditar.Enabled = true;
                    btnExcluir.Enabled = true;
                    btnCancelar.Enabled = true;
                    btnSair.Enabled = true;
                    btnPesquisar.Enabled = true;
                    break;

                default:
                    break;
            }
        }

        private void LimparCampos()
        {
            txtLocal.ResetText();
            txtOperador.ResetText();
            txtCNES.ResetText();
            txtCPF.ResetText();
            txtSenha.ResetText();
            txtNivel.ResetText();
            txtPesquisar.ResetText();
            
        }

        private void LimparDGV()
        {           
            try
            {
                dgvSituacaoOperador.DataSource = null;
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);

            }            
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            this.opcao = "inserir";
            Exibicao(2);
            txtLocal.Focus();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            
            try
            {
                ConexaoBD conexao = new ConexaoBD(DadosDaConexao.StrConexao);
                BLLOperador objBLL = new BLLOperador(conexao);
                if (opcao == "inserir")
                {
                    
                    Operador operador = new Operador();
                    operador.Unidade = txtLocal.Text;
                    operador.NomeOperador = txtOperador.Text;
                    operador.Cnes = Convert.ToInt32(txtCNES.Text);
                    operador.Cpf = txtCPF.Text;
                    operador.Senha = txtSenha.Text;
                    operador.Nivel = txtNivel.Text;
                    objBLL.Inserir(operador);
                    MessageBox.Show("Operador cadastrado com sucesso!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimparCampos();
                }

                if (opcao == "editar")
                {
                   
                    Operador operador = new Operador();
                    operador.Unidade = txtLocal.Text;
                    operador.NomeOperador = txtOperador.Text;
                    operador.Cnes = Convert.ToInt32(txtCNES.Text);
                    operador.Cpf = txtCPF.Text;
                    operador.Senha = txtSenha.Text;
                    operador.Nivel = txtNivel.Text;
                    objBLL.Editar(operador, this.cod);
                    MessageBox.Show("Operador atualizado com sucesso!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimparCampos();
                    LimparDGV();
                }
                

            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            this.opcao = "editar";
            txtLocal.Focus();
        }

        public void FormatarDGV()
        {
            //CABECALHO DGV
            dgvSituacaoOperador.Columns[1].HeaderText = "Local";
            dgvSituacaoOperador.Columns[2].HeaderText = "Operador";
            dgvSituacaoOperador.Columns[3].HeaderText = "Cnes";
            dgvSituacaoOperador.Columns[4].HeaderText = "Cpf";
            dgvSituacaoOperador.Columns[5].HeaderText = "Senha";
            dgvSituacaoOperador.Columns[6].HeaderText = "Nivel";
            //OCULTACAO PRIMEIRO CAMPO ID
            dgvSituacaoOperador.Columns[0].Visible = false;       
            //TAMANHO DOS CAMPOS NO DGV
            dgvSituacaoOperador.Columns[1].Width = 118;
            dgvSituacaoOperador.Columns[3].Width = 50;
            dgvSituacaoOperador.Columns[2].Width = 190;
            //CENTRALIZACAO DOS CAMPOS CNES,CPF E SENHA
            dgvSituacaoOperador.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvSituacaoOperador.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvSituacaoOperador.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvSituacaoOperador.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                ConexaoBD conexao = new ConexaoBD(DadosDaConexao.StrConexao);
                BLLOperador objBLL = new BLLOperador(conexao);
                dgvSituacaoOperador.DataSource = objBLL.Pesquisar(txtPesquisar.Text);
                FormatarDGV();
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message);
            }
            LimparCampos();
            
        }

        private void dgvSituacaoOperador_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.cod = Convert.ToInt32(dgvSituacaoOperador.Rows[e.RowIndex].Cells["id"].Value);
            }

            ConexaoBD conexao = new ConexaoBD(DadosDaConexao.StrConexao);
            BLLOperador objBLL = new BLLOperador(conexao);
            Operador operador = objBLL.Carregar(cod);//o metodo carregar vai receber o id da linha clicada no dgv e enviado para a variavel cod

            txtLocal.Text = operador.Unidade;
            txtOperador.Text = operador.NomeOperador;
            txtCNES.Text = operador.Cnes.ToString();
            txtCPF.Text = operador.Cpf;
            txtSenha.Text = operador.Senha;
            txtNivel.Text = operador.Nivel;
            Exibicao(3);

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            DialogResult resposta = MessageBox.Show("Isso irá excluir este Operador! Continuar?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resposta.ToString() == "Yes")
            {
                try
                {
                    ConexaoBD conexao = new ConexaoBD(DadosDaConexao.StrConexao);
                    BLLOperador objBLL = new BLLOperador(conexao);
                    objBLL.Excluir(this.cod);
                    MessageBox.Show("O Operador foi excluído com sucesso!");
                }
                catch (Exception erro)
                {
                    MessageBox.Show(erro.Message);
                }
            }
            LimparCampos();
        }

        private void frmPrincipal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }
            if (e.KeyCode == Keys.F8)
            {
                btnPesquisar_Click(sender, e);
            }

            if (e.KeyCode == Keys.F2)
            {
                btnSalvar_Click(sender, e);
            }

            if (e.KeyCode == Keys.F3)
            {
                btnInserir_Click(sender, e);
            }

            if (e.KeyCode == Keys.F4)
            {
                btnEditar_Click(sender, e);
            }

            if (e.KeyCode == Keys.F7)
            {
                btnExcluir_Click(sender, e);
            }

            if (e.KeyCode == Keys.F9)
            {
                btnCancelar_Click(sender, e);
            }

            if (e.KeyCode == Keys.F10)
            {
                btnSair_Click(sender, e);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparCampos();
            LimparDGV();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            DialogResult resposta = MessageBox.Show("Deseja sair do sistema?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resposta.ToString() == "Yes")
            {
                Application.Exit();
            }
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            Exibicao(1);
        }
    }
}
