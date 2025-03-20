using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyStore.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClients = new List<ClientInfo>();

        public void OnGet()
        {
            try
            {
                // Cadena de conexión corregida
                string connectionString = "Data Source=MarwinJR;Initial Catalog=mystore;Integrated Security=True;Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM clients";

                    // Crear el comando SQL
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader()) // ← Corrección aquí
                        {
                            while (reader.Read())
                            {
                                ClientInfo clientInfo = new ClientInfo();
                                   clientInfo.id = "" + reader.GetInt32(0);
                                   clientInfo.name = reader.GetString(1);
                                   clientInfo.email = reader.GetString(2);
                                   clientInfo.phone = reader.GetString(3);
                                   clientInfo.address = reader.GetString(4);
                                   clientInfo.created_at = reader.GetDateTime(5).ToString(); // ← Corrección aquí

                                listClients.Add(clientInfo);
                              
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class ClientInfo
    {
        public String id;
        public String name;
        public String email;
        public String phone;
        public String address;
        public String created_at;
    }
}

