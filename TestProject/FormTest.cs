using System;
using System.Collections.Generic;
using System.Linq;
using WinFormsApp.Models;
using Xunit;
using WinFormsAppSql;
using System.Text.RegularExpressions;

namespace FormTest
{
    public class FormTest
    {
        [Fact]
        public void SaveUser()
        {
            //Act
            FileHelper.Save("Tom", 25, Car.vw);
            List<Employee> list = new List<Employee>();
            using (ApplicationContext db = new ApplicationContext())
            {
                list = db.Employees.ToList();
            }
            Employee emp = list.LastOrDefault();
            //Assert
            Assert.Equal("Tom", emp.Name);
            Assert.Equal(25, emp.Age);
            Assert.Equal("vw", emp.Car.ToString());
        }
        [Fact]
        public void ViewUser()
        {
            // Arrange
            FileHelper.View();
            //Act
            string users = FileHelper.View();
            // Assert
            Assert.NotEmpty(users);
            Assert.IsType<String>(users);


        }
        [Fact]
        public void FindUser()
        {
            //Act
            string users = FileHelper.Find("Tom", "20", "skoda");
            //Assert
            Assert.NotEmpty(users);
            Assert.IsType<String>(users);
            bool isUser = Regex.IsMatch(users, "Tom", RegexOptions.IgnoreCase);
            Assert.True(isUser);
        }
        [Fact]
        public void UserDelete()
        {
            //Act
            string result = FileHelper.Delete("Tom");
            //Assert
            Assert.Equal("Deleted successfully!", result);
        }
        FileHelper.Delete("Tom");
    }
    }
