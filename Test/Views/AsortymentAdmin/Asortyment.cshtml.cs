using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Test.Views.Asortyment_admin
{
    public class AsortymentModel : PageModel
    {
        public List<AsortymentInfo> AsortymentLista = new List<AsortymentInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Test;Integrated Security=True;TrustServerCertificate=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Asortyment";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read()) {
                                AsortymentInfo asortymentInfo = new AsortymentInfo();
                                asortymentInfo.id = "" + reader.GetInt32(0);
                                asortymentInfo.produkt = reader.GetString(1);
                                asortymentInfo.ilosc = reader.GetInt32(2);
                                asortymentInfo.data = reader.GetDateTime(3).ToString();

                                AsortymentLista.Add(asortymentInfo);
                            }
                        }
                    }
                }
                Console.WriteLine($"Ilosc : {AsortymentLista.Count}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"B³¹d: {ex.Message}");
            }
        }

        public class AsortymentInfo
        {
            public String id;
            public String produkt;
            public int ilosc;
            public String data;
        }
    }
}