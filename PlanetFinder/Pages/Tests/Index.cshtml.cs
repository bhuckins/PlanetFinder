using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlanetFinder.AppCode.DataCollection;
using PlanetFinder.AppCode.DataCollection.Tests;
using PlanetFinder.AppCode.DataStore;
using PlanetFinder.AppCode.DataStore.Tests;
using PlanetFinder.AppCode.Gateway;
using PlanetFinder.AppCode.Gateway.Tests;

namespace PlanetFinder.Pages.Tests
{
    public class IndexModel : PageModel
    {
        public string TestResultsString { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPostRunTests()
        {
            List<string> testResults = new List<string>();

            DataCollectionTests dataCollectionTests = new DataCollectionTests();

            testResults.Add("Running Data Collection Tests");

            dataCollectionTests.RunTests();

            testResults.Add("Data Collection Tests successful");

            DataStorerTests dataStorerTests = new DataStorerTests();

            testResults.Add("Running Data Store Tests");

            dataStorerTests.RunTests();

            testResults.Add("Data Store Tests successful");

            DataCollectionGatewayTests gatewayTests = new DataCollectionGatewayTests();

            testResults.Add("Running Data Collection Gateway Tests");

            gatewayTests.RunTests();

            testResults.Add("Data Collection Gateway Tests successful");

            TestResultsString = string.Join(Environment.NewLine, testResults);

            return Page();
        }
    }
}
