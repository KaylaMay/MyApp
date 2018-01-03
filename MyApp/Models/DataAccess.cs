using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace MyApp.Models
{
    public class DataAccess
    {

        HttpClient client = new HttpClient();
        public customer GetCusts(string Email, string Password)
        {
            string url = "http://10.0.2.2:8080/api/Login?email=" + Email + "&password=" + Password;
            HttpResponseMessage response = client.GetAsync(url).Result;
            customer cust = new customer();

            cust = JsonConvert.DeserializeObject<customer>(response.Content.ReadAsStringAsync().Result);

            return cust;
        }

        public async void UpdateUser(customer cust, int id)
        {
            HttpClient client = new HttpClient();
            HttpContent content = new StringContent(JsonConvert.SerializeObject(cust), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync("http://10.0.2.2:8080/api/Update?cust_id=" + id, content);
            var jasonC = content.ReadAsStringAsync().Result;
        }



    }
}
