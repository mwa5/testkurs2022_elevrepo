using System;
using TechTalk.SpecFlow;

namespace WhishList.StepDefinitions
{
    [Binding]
    public class SparaOnskningarIOnskelistanStepDefinitions
    {
        [Given(@"Det finns ett barn som heter (.*) och som önskar sig en (.*)")]
        public void GivenDetFinnsEttBarnSomOnskarSigEnLeksak(string barn, string leksak)
        {
            throw new PendingStepException();
        }

        [When(@"Jag sparar önskan i önskelistan")]
        public void WhenJagSpararOnskanIOnskelistan()
        {
            throw new PendingStepException();
        }

        [Then(@"Så får jag tillbaka texten (.*)")]
        public void ThenSaFarJagTillbakaTexten(string svar)
        {
            throw new PendingStepException();
        }
    }
}
