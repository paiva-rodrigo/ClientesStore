using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DtinManager.Pages.Clientes
{
    public class CreateModel : PageModel
    {
        public ClienteInfo clienteInfo = new ClienteInfo();
        public String errorMessage = "";
        public String sucessMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            clienteInfo.name = Request.Form["name"];
            clienteInfo.email = Request.Form["email"];
            clienteInfo.phone = Request.Form["phone"];
            clienteInfo.address = Request.Form["address"];

            if(clienteInfo.name.Length == 0 || clienteInfo.email.Length == 0 || clienteInfo.phone.Length == 0
                || clienteInfo.address.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            //save the new client into database
            try
            {
                string connectionString = "Data Source=DESKTOP-NI97JQO\\SQLRODRIGO;Initial Catalog=mystore;Integrated Security=True";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO clients " +
                        "(name, email, phone, address) VALUES " +
                        "(@name, @email, @phone, @address);";
                    using (SqlCommand command = new SqlCommand(sql,connection))
                    {
                        command.Parameters.AddWithValue("@name",clienteInfo.name);
                        command.Parameters.AddWithValue("@email", clienteInfo.email);
                        command.Parameters.AddWithValue("@phone", clienteInfo.phone);
                        command.Parameters.AddWithValue("@address", clienteInfo.address);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            clienteInfo.name = ""; clienteInfo.email = ""; clienteInfo.phone = "";
            clienteInfo.address = ""; sucessMessage = "New Client Added Correctly";

            Response.Redirect("/Clientes/Index/");
        }

    }
}
