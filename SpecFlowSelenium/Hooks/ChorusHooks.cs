using BoDi;
using SpecFlowSelenium.Drivers;
using SpecFlowSelenium.PageObjects;

namespace SpecFlowSelenium.Hooks
{
    [Binding]
    public class ChorusHooks
    {
        ///<summary>
        ///  Reset the chorus page object before each scenario tagged with "Chorus"
        /// </summary>
        [BeforeScenario]
        public static void BeforeScenario(BrowserDriver browserDriver)
        {
            var chorusPageObject = new ChorusPageObjects(browserDriver.Current);
            chorusPageObject.EnsureChorusAppIsOpen();
        }

        [BeforeTestRun]
        public static void BeforeTestRun(ObjectContainer testThreadContainer)
        {
            //Initialize a shared BrowserDriver in the global container
            testThreadContainer.BaseContainer.Resolve<BrowserDriver>();
        }
    }
}
