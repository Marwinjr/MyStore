using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static MyStore.Pages.Clients.CreateModel;
namespace MyStore.Pages.Clients;

public class CreateModel : PageModel
{
    public ClientInfo clientInfo = new ClientInfo();
    public String errorMessage = "";
    public String successMessage = "";

public void OnGet()
    { 
    }
public void OnPost()
    {
        clientInfo.name = Request.Form["name"];
        clientInfo.email = Request.Form["email"];
        clientInfo.phone = Request.Form["phone"];
        clientInfo.address = Request.Form["address"];

        if (clientInfo.name.Length == 0 || clientInfo.email.Length == 0 ||
        clientInfo.phone.Length == 0 || clientInfo.address.Length == 0)
        {
            errorMessage = "All the fields are required";
        return;
        }
        try
        {
            String connectionString = "Data Source=MarwinJR;Initial Catalog=mystore;Integrated Security=True;Encrypt=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            { connection.Open();
                String sql = "INSERT INTO clients " +
                    "(name, email, phone, address) VALUES " +
                    "(@name, @email, @phone, @address);";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@name", clientInfo.name);
                    command.Parameters.AddWithValue("@email", clientInfo.email);
                    command.Parameters.AddWithValue("@phone", clientInfo.phone);
                    command.Parameters.AddWithValue("@address", clientInfo.address);

                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex) {
            errorMessage = ex.Message;
            return;
        }

        clientInfo.name = ""; clientInfo.email = "";
        clientInfo.phone = ""; clientInfo.address = "";
        successMessage = "New Client Added Correctly";

        Response.Redirect("/Clients&Index");
    }
    public class ClientInfo
{
    public string name { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
    public string address { get; set; }
}
}
