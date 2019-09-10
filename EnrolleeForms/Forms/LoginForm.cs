using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnrolleeForms
{
    public partial class LoginForm : Form
    {
        // конструктор формы
        public LoginForm()
        {
            InitializeComponent();
        }

        // пользователь
        User user = null;
  

        private void button1_Click(object sender, EventArgs e)
        {
            // если тексбоксы непустые
            if (textBox1.Text != String.Empty && textBox2.Text!=String.Empty)
            {

                // метод входа (выз статич метод)
                user = User.LoginToTheApp(textBox1.Text, textBox2.Text);

                // если подошли логин и пароль   
                if (user != null)
                {
                    MessageBox.Show("Добро пожаловать " + user.FIO());


                    textBox1.Text = "";
                    textBox2.Text = "";

                    // откр прилож
                    MainForm mainForm = new MainForm(user);
                    mainForm.Show();

                    // свор форму
                    this.Hide();
                    user = null;

                }
                else
                    label3.Text = "Неверный логин или пароль";

            }
            label4.Text = ("Укажите логин и пароль");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // ссылка на сайт ПолесГУ
            System.Diagnostics.Process.Start("http://www.polessu.by");

        }
    }
}
