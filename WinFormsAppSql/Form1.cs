using System;
using System.Windows.Forms;

namespace WinFormsAppSql { 
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Add("bmw");
            comboBox1.Items.Add("skoda");
            comboBox1.Items.Add("vw");
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        static string name;
        static string age;

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e) { }
        private void Form1_Load(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void Save_Click(object sender, EventArgs e)
        {
            name = textBox1.Text.Trim();
            age = maskedTextBox1.Text;
            Car car;
            if (String.IsNullOrEmpty(name))
            {
                MessageBox.Show("Invalid value of name\n");
            }
            else if (String.IsNullOrEmpty(age) || Convert.ToInt32(age) < 18 || Convert.ToInt32(age) > 99) MessageBox.Show("Invalid value of age\n");
            else if (String.IsNullOrEmpty(comboBox1.Text)) MessageBox.Show("Invalid value of car\n");
            else if (!Enum.TryParse<Car>(comboBox1.Text, true, out car)) MessageBox.Show("Invalid value \n");

            else
            {
                FileHelper.Save(name, Convert.ToInt32(age), car);
                MessageBox.Show("Saved successfully!");
            }
        }
        private void View_Click(object sender, EventArgs e)
        {
          string users=  FileHelper.View();
            MessageBox.Show(users);
        }
        private void Find_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string age = maskedTextBox1.Text;
            string car = comboBox1.Text;
            string users = FileHelper.Find(name, age, car);
            MessageBox.Show(users);
        }
        private void Delete_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
           string result= FileHelper.Delete(name);
            MessageBox.Show(result);
        }
    }
}
