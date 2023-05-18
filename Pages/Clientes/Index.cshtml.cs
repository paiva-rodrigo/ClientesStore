using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DtinManager.Pages.Clientes
{
    public class IndexModel : PageModel
    {
        public List<ClienteInfo> listClientes = new List<ClienteInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-NI97JQO\\SQLRODRIGO;Initial Catalog=mystore;Integrated Security=True";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClienteInfo info = new ClienteInfo();
                                info.id = "" + reader.GetInt32(0);
                                info.name = reader.GetString(1);
                                info.email = reader.GetString(2);
                                info.phone = reader.GetString(3);
                                info.address = reader.GetString(4);
                                info.create_at = reader.GetDateTime(5).ToString();

                                listClientes.Add(info);

                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exeption:"+ex.ToString());
            }
        }
    }

    public class ClienteInfo
    {
        public string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string create_at { get; set; }
    }
}
