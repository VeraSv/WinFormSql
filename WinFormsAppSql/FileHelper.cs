using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using WinFormsApp.Models;


namespace WinFormsAppSql
{
   public class FileHelper
    {
        const string errorEmpty = "List is empty\n";
        public static void Save(string name, int age, Car car)
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                Employee emp = new Employee { Name = name, Age =age, Car = car };
                db.Employees.Add(emp);
                db.SaveChanges();
            }
        }
       public static string View()
        {
            string users = "";
            List<Employee> list = new List<Employee>();
            using (ApplicationContext db = new ApplicationContext())
            {

                    list = db.Employees.ToList();
                    if (list != null)
                    {
                        if (list.Any())
                        {
                           
                            foreach (Employee emp in list)
                            {
                                users += emp.Name + "\t" + emp.Age + "\t" + emp.Car + "\n";

                            }
                            return users;
                         }
                        else return errorEmpty;
                    }
                    else return errorEmpty;

            }
           
        }

        public static string Find ( string name, string age, string car)
        {
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

                    if (!String.IsNullOrEmpty(users)) return users; 
                    else return "User is not found";
                }
                else return errorEmpty;
            }
           
         
        }
        public static string Delete(string name)
        {

            using (ApplicationContext db = new ApplicationContext())
            {

                var Employees = db.Employees.ToList();
                if (Employees.Any())
                {
                    foreach (Employee emp in Employees)
                    {
                        if (name == emp.Name)
                            db.Remove(emp);

                    }
                    db.SaveChanges();
                    return "Deleted successfully!";
                   
                }
                else return errorEmpty;
            }
        }
    }
    public class ApplicationContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

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
