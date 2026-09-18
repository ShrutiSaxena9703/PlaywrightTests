using Microsoft.Playwright;
using Reqnroll;

namespace PlaywrightProject.Tests.Hooks
{
    [Binding]
    public class TestHooks
    {
        private readonly ScenarioContext _scenarioContext;

        public TestHooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            var apiContext = await playwright.APIRequest.NewContextAsync(new()
            {
                BaseURL = "https://demoqa.com"
            });
            _scenarioContext["ApiContext"] = apiContext;
        }

        [AfterScenario]
        public async Task AfterScenario()
        {
            if (_scenarioContext.ContainsKey("ApiContext"))
            {
                var apiContext = (IAPIRequestContext)_scenarioContext["ApiContext"];
                await apiContext.DisposeAsync();
            }
        }
    }
}