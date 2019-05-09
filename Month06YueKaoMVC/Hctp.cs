using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Month06YueKaoMVC
{
    public static class Hctp
    {
        public static string GetApi(string Name, string action, object obj = null)
        {
            string str = "";
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:50842/api/default/");
            Task<HttpResponseMessage> task = null;
            switch (Name)
            {
                case "get":
                    task = hc.GetAsync(action);
                    break;
                case "del":
                    task = hc.DeleteAsync(action);
                    break;
                case "post":
                    task = hc.PostAsJsonAsync(action, obj);
                    break;
                case "put":
                    task = hc.PutAsJsonAsync(action, obj);
                    break;
            }
            task.Wait();
            var gettask = task.Result;
            if (gettask.IsSuccessStatusCode)
            {
                var getstr = gettask.Content.ReadAsStringAsync();
                getstr.Wait();
                str = getstr.Result;
            }
            return str;
        }
    }
}
