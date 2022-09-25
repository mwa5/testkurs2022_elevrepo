using System;
using TechTalk.SpecFlow;

namespace WhishList.StepDefinitions
{
    [Binding]
    public class SparaOnskningarIOnskelistanStepDefinitions
    {
        [Given(@"Det finns ett barn som heter Folke och som önskar sig en kniv")]
        public void GivenDetFinnsEttBarnSomHeterFolkeOchSomOnskarSigEnKniv()
        {
            throw new PendingStepException();
        }

        [When(@"Jag sparar Folkes önskan om kniv")]
        public void WhenJagSpararFolkesOnskanOmKniv()
        {
            throw new PendingStepException();
        }

        [Then(@"Så får jag tillbaka texten ""([^""]*)""")]
        public void ThenSaFarJagTillbakaTexten(string p0)
        {
            throw new PendingStepException();
        }
    }
}
