using Newtonsoft.Json.Linq;

namespace PlaywrightProject.Tests.TestData
{
    public class TestDataHelper
    {
        private static JObject _config;

        static TestDataHelper()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, 
                "TestData", "testconfig.json");
            var json = File.ReadAllText(filePath);
            _config = JObject.Parse(json);
        }

        public static string BaseUrl => _config["baseUrl"].ToString();

        public static string GetUserName(string userType) =>
            _config["users"][userType]["userName"].ToString();

        public static string GetPassword(string userType) =>
            _config["users"][userType]["password"].ToString();
    }
}