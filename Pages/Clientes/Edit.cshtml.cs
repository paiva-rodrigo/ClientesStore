using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace DtinManager.Pages.Clientes
{
    public class EditModel : PageModel
    {
        public ClienteInfo clienteInfo = new ClienteInfo();
        public String errorMessage = "";
        public String sucessMessage = "";

        public void OnGet()
        {
            string id = Request.Query["id"];

            try
            {
                string connectionString = "Data Source=DESKTOP-NI97JQO\\SQLRODRIGO;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients where id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id",id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                clienteInfo.id = "" + reader.GetInt32(0);
                                clienteInfo.name = reader.GetString(1);
                                clienteInfo.email = reader.GetString(2);
                                clienteInfo.phone = reader.GetString(3);
                                clienteInfo.address = reader.GetString(4);
                            }
                        }
                    }

                }

            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
        }

        public void OnPost()
        {
            clienteInfo.id = Request.Form["id"];
            clienteInfo.name = Request.Form["name"];
            clienteInfo.email = Request.Form["email"];
            clienteInfo.phone = Request.Form["phone"];
            clienteInfo.address = Request.Form["address"];

            if (clienteInfo.name.Length == 0 || clienteInfo.email.Length == 0 || clienteInfo.phone.Length == 0
                || clienteInfo.address.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            try
            {
                string connectionString = "Data Source=DESKTOP-NI97JQO\\SQLRODRIGO;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE clients " +
                        "SET name=@name, email=@email, phone=@phone, address=@address " +
                        "WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clienteInfo.name);
                        command.Parameters.AddWithValue("@email", clienteInfo.email);
                        command.Parameters.AddWithValue("@phone", clienteInfo.phone);
                        command.Parameters.AddWithValue("@address", clienteInfo.address);
                        command.Parameters.AddWithValue("@id", clienteInfo.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Clientes/Index");
        }



    }
}
