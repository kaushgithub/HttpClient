using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientVault
{
    class Program
    {
        static void Main(string[] args)
        {
            string incidentJSON = @"{""incidentId"":"""",""asset"":{""id"":"""",""name"":""JustinSmithCookie.txt"",""size"":""27"",""sha256"":""975b29b9a0b19b5a62cac60cda36d68ec7edbb49065cfadb183bcd1db750dc44"",""startTime"":""2018-06-10T08:43:11"",""mimeType"":""text/plain"",""tags"":[{""id"":""incident-id"",""displayName"":""ID Number"",""isTextValues"":true,""comboValues"":[],""textValues"":[""201806090103""]}]}}";
            var result = PutTrial(incidentJSON).Result;
            Console.WriteLine("Done");
            Console.ReadLine();
        }


        private static async Task<string> PutTrial(string jsonstr)
        {
            //HttpClient client = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true, PreAuthenticate = true});
            var cookie = new CookieContainer();
            var uri = new Uri("{your URL}");
            HttpClient client = new HttpClient(new HttpClientHandler() { CookieContainer = cookie });
            client.BaseAddress = uri;
            cookie.Add(uri, new Cookie("login-token", "b4-4ed7-a893-ddf89b08790a%3A25b353e5-667c-418c-9c51-22bd36770cc2_fa58cf660184598d%3Acrx.default"));
            var inputMessage = new HttpRequestMessage
            {
                Content = new StringContent(jsonstr, Encoding.UTF8, "application/json")
            };
            inputMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string authKey = "eyJhbGciOiJSUzM4NCIsImtpZCI6InNpZ25pbmdrZXkiLCJ4NXQiOiJzWWsya0g1d0h4NnhJRVdCYldUN0JRVTBmLUEifQ.eyJzY29wZSI6WyJvcGVuaWQiLCJjY19kcml2ZS5yZWFkX2ZpbGUiLCJjY19kcml2ZS53cml0ZV9maWxlIl0sImNsaWVudF9pZCI6IkNDVmF1bHQiLCJpc3MiOiJodHRwczovL2lkbWNjLmltdy5tb3Rvcm9sYXNvbHV0aW9ucy5jb206NDQzIiwiZW50cnlVVUlEIjoiTi9BIiwic3ViIjoia2F1c2hpay5zcmluYXRoQG1vdG9yb2xhc29sdXRpb25zLmNvbSIsInVpZCI6ImthdXNoaWsuc3JpbmF0aEBtb3Rvcm9sYXNvbHV0aW9ucy5jb20iLCJyb2xlIjoiTi9BIiwiYWdlbmN5IjoibXNpLnJtcyIsImV4cCI6MTUyODc0MDY2OH0.Ghl_qj0YFhU9_hhyVIID2uGWikrymVfMiQXHr995xcLaRJ-z2dg4MfKTFKJ7BFmI3uSdKxhD51eAvoEokhQGrLhyj5IqYBTXUtHq8vXEnzCV5kWu7s-mtZNviXISmSAWx0CGKcpOO-bgtWiAFR9fnxFjRFh8E3-GQyYlcGkMPtxqWutUxEzaJGDjG91tWpj_sifkHh_EEq8DpOaW4iauRMOd79-ub2-twjFnsn5cZlYgXJV01xBaDyCfI8qfnCkgUpPlsVQjF5nYha7Z71nSAvS7baS7lCbxUaJ253aQk1a9nUZlwq8xNzYApbmfLBz7qlVqtsor4Y5stc3BsamNXQ";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authKey);
            //Other headers you want
            client.DefaultRequestHeaders.Add("x-vault-backend", "author-aem1.author-aem");
            var response = await client.PutAsync("{url}", inputMessage.Content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
