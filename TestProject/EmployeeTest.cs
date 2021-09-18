using System;
using Xunit;
using WinFormsApp.Models;


namespace EmployeeTest
{
    
    public class EmployeeTest
    {
        [Fact]
        public void CreateEmployee()
        {
            // Arrange
            Employee emp = new Employee();
            Employee emp1 = new Employee("Ann",23,WinFormsAppSql.Car.skoda);
            //Act
            emp.Name = "Tom";
            emp.Age = 35;
            emp.Car =0;
         
            // Assert
            Assert.NotNull(emp);
            Assert.IsType<Employee>(emp);
            Assert.Equal("Tom", emp.Name);
            Assert.Equal(35, emp.Age);
            Assert.Equal("bmw", emp.Car.ToString());

            Assert.NotNull(emp1);
            Assert.IsType<Employee>(emp1);
            Assert.Equal("Ann", emp1.Name);
            Assert.Equal(23, emp1.Age);
            Assert.Equal("skoda", emp1.Car.ToString());



        }
    }
}
