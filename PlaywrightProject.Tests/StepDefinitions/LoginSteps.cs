using Microsoft.Playwright;
using Reqnroll;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using PlaywrightProject.Tests.TestData;
namespace PlaywrightProject.Tests.StepDefinitions
{
    [Binding]
    public class LoginSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private IAPIResponse _response;
        private string _token;

        public LoginSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given("I have valid user credentials")]
        public void GivenIHaveValidUserCredentials()
        {
            // credentials are used in the When step
        }

        [When("I send a POST request to generate token")]
        public async Task WhenISendAPostRequestToGenerateToken()
        {
            var apiContext = (IAPIRequestContext)_scenarioContext["ApiContext"];

            _response = await apiContext.PostAsync("/Account/v1/GenerateToken", new APIRequestContextOptions
            {
                DataObject = new
                {
                 userName = TestDataHelper.GetUserName("validUser"),
            password = TestDataHelper.GetPassword("validUser")
                }
            });
        }

        [Then("the response status code should be 200")]
        public void ThenTheResponseStatusCodeShouldBe200()
        {
            _response.Status.Should().Be(200);
        }

        [Then("the response should contain a valid token")]
        public async Task ThenTheResponseShouldContainAValidToken()
        {
            var body = await _response.TextAsync();
            var json = JObject.Parse(body);
            _token = json["token"]?.ToString();
            _token.Should().NotBeNullOrEmpty();
        }
    }
}