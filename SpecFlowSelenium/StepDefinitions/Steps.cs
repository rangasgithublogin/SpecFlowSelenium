using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpecFlowSelenium.Drivers;
using SpecFlowSelenium.PageObjects;

namespace SpecFlowSelenium.StepDefinitions
{
    [Binding]
    public class Steps
    {
        private readonly ChorusPageObjects _chorusPageObjects;

        public Steps(BrowserDriver browserDriver)
        {
            _chorusPageObjects = new ChorusPageObjects(browserDriver.Current);
        }

        [Given(@"I input the address '([^']*)'")]
        public void GivenIInputTheAddress(string p0)
        {
            _chorusPageObjects.EnterAddress(p0);
            _chorusPageObjects.selectAddress();
        }

        [When(@"the listed address is searched")]
        public void WhenTheListedAddressIsSearched()
        {
            _chorusPageObjects.ClickSearch();
        }

        [Then(@"the network capability result displays '([^']*)'")]
        public void ThenTheNetworkCapabilityResultDisplays(string expectedResult)
        {
            var actualResult = _chorusPageObjects.WaitForNonEmptyResult(expectedResult);
            actualResult.Should().Be(expectedResult.ToString());
        }
    }
}
