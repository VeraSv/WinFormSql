using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;

using WinFormsApp.Models;

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
        string name;
       string age;
        Car car;
        const string errorEmpty = "List is empty\n";
        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

       
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
                using (ApplicationContext db = new ApplicationContext())
                {

                    Employee emp = new Employee { Name = name, Age = Convert.ToInt32(age), Car = car };
                    db.Employees.Add(emp);
                    db.SaveChanges();
                    MessageBox.Show("Saved successfully!");
                }

            }
        }

        private void View_Click(object sender, EventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                string users = "";
                try
                {
                    List<Employee> list = new List<Employee>();

                    list = db.Employees.ToList();
                    if (list != null)
                    {
                        if (list.Any())
                        {
                            foreach (Employee emp in list)
                            {
                                users += emp.Name + "\t" + emp.Age + "\t" + emp.Car + "\n";

                            }
                            MessageBox.Show(users);

                        }
                        else MessageBox.Show(errorEmpty);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void Find_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string age = maskedTextBox1.Text;

            string car = comboBox1.Text;
            string users = "";
            using (ApplicationContext db = new ApplicationContext())
            {
                var Employees = db.Employees.ToList();
                if (Employees.Any())
                {
                    foreach (Employee emp in Employees)
                    {
                        if (name == emp.Name || age == emp.Age.ToString() || car == emp.Car.ToString())
                            users += emp.Name + "\t" + emp.Age + "\t" + emp.Car + "\n";

                    }
                    if (!String.IsNullOrEmpty(users)) MessageBox.Show(users);
                    else MessageBox.Show("User is not found");
                }
                else MessageBox.Show(errorEmpty);
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                var Employees = db.Employees.ToList();
                if (Employees.Any())
                {
                    foreach (Employee emp in Employees)
                    {
                        if (textBox1.Text == emp.Name)
                            db.Remove(emp);

                    }
                    db.SaveChanges();
                    MessageBox.Show("Deleted successfully!");
                }
                else MessageBox.Show(errorEmpty);
            }
        }
    
    }
    public class ApplicationContext : DbContext
    {
        public DbSet<Employee>Employees { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=winformappsql;Trusted_Connection=True;");
        }
    }

}
