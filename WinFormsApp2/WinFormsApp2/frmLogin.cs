using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
       

        }
        public static string stringConexao = "" +
           "Data Source=localhost;" +
           "Initial Catalog=ProjetoIntegradorT_13;" +
           "User ID=sa;" +
           "Password=123456";

        private void testarconexao()
        {
            SqlConnection conn = new SqlConnection(stringConexao);

            try
            {
                conn.Open();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString());
                Application.Exit();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            testarconexao();
            ComboBox();
        }
        public static string IDUsuario = "";
        public static string NomeUsuario = "";
        public static string LoginUsuario = "";

        private void ComboBox()
        {
            string sql = "select id_departamento,nome_departamento from Departamento";
            SqlConnection con = new SqlConnection(stringConexao);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;

            DataTable tabela = new DataTable();
            con.Open();

            try
            {
                reader = cmd.ExecuteReader();
                tabela.Load(reader);

                cboDepartamento.DisplayMember = "nome_departamento";
                cboDepartamento.DataSource = tabela;


                cboIDdepartamento.DisplayMember = "id_departamento";
                cboIDdepartamento.DataSource = tabela;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro - " + ex.ToString());
                Application.Exit();
            }
            finally
            {
                con.Close();
            }

      
    }

        private void btoEntrar_Click(object sender, EventArgs e)
        {
            Boolean Valida = false;
            string Sql = "select * from Usuario where " +
            "login_Usuario = '" + txtLogin.Text + "' and " +
            "id_departamento_Usuario = '" + cboIDdepartamento.Text + "' and " +
            "senha_Usuario = '" + txtSenha.Text + "'"; SqlConnection conexao = new SqlConnection(stringConexao);
            SqlCommand cmd = new SqlCommand(Sql, conexao);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conexao.Open();
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Valida = true;

                    IDUsuario = reader[0].ToString();
                    NomeUsuario = reader[1].ToString();
                    LoginUsuario = reader[2].ToString();
                }
                else
                {
                    MessageBox.Show("Usu�rio ou senha inv�lidos");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conexao.Close();
            }
            if (Valida)
            {
                MessageBox.Show("Login feito");
                //MDIParent1 frm = new MDIParent1();
                //frm.Show();
                //this.Hide();
            }
        }

        private void btoLimpar_Click(object sender, EventArgs e)
        {
            txtLogin.Text = "";
            txtSenha.Text = "";
            cboDepartamento.Text = "";
            cboIDdepartamento.Text = "";
        }
    }
    }
