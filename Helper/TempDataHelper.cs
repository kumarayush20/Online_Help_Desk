using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System.Security.Policy;
namespace Onlinehelpdesk.Helper
{
    public static class TempDataHelper
    {
        public static void Put<T> (this ITempDataDictionary tempData,string key,T value) where T : class
        {
            tempData[key]=JsonConvert.SerializeObject(value);
        }
        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            object o;
            tempData.TryGetValue(key, out o);

            if (o == null)
            {
                return null;
            }

            // Convert object o to string before deserialization
            string json = o.ToString();

            // Deserialize JSON string to type T
            return JsonConvert.DeserializeObject<T>(json);
        }

    }
}
