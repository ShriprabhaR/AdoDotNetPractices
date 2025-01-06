using System;

namespace EmployeePayRollAdo.Net
{
    internal class Program
    {
       static void Main(string[] args)
       {
             EmployeeRepository repository = new EmployeeRepository();
             // EmployeeInput();
              repository.GetAllEmployee();
            //repository.UpdateEmployee();
            //repository.DeleteEmployee();


            //ProdRepository repository = new ProdRepository();
            //ProductInput();
            //repository.GetAllProduct();
            //repository.UpdateProduct();
            //repository.DeleteProduct();
        }

        public static void EmployeeInput()
        {
            EmployeeRepository repos = new EmployeeRepository();
            EmployeeModel model = new EmployeeModel();
            model.EmpName = "Shri";
            model.PhoneNumber = "1234567890";
            model.Address = "100, 2nd stage, Btm layout, Bangalore";
            model.Department = "DotNet Development";
            model.Salary = 400000;
            Console.WriteLine(repos.AddEmployee(model) ? "Record inserted" : "Failed to insert");
        }


        public static void ProductInput()
        {
            ProdRepository repos = new ProdRepository();
            ProductModel model = new ProductModel();
            model.ProdName = "Shoes";
            model.ProdPrice = 3000;
            model.ProdColor = "Grey";
            model.ProdBrand = "Crocs";
            Console.WriteLine(repos.AddProduct(model) ? "Record inserted" : "Failed to insert");

        }
    }
}
