using System;
using TechTalk.SpecFlow;
using WhishList.WhishList;
using Xunit;

namespace WhishList.StepDefinitions
{
    [Binding]
    public class SparaOnskningarIOnskelistanStepDefinitions
    {
        private string _namn;
        private string _leksak;
        private WhishListController _whishListController = new WhishListController();
        private string _bekraftelse;

        [Given(@"Att det finns ett barn som heter (.*) och som önskar sig en (.*)")]
        public void GivenAttDetFinnsEttBarnOchSomOnskarSigEnLeksak(string namn, string leksak)
        {
            _namn = namn;
            _leksak = leksak;
        }

        [When(@"Jag sparar önskan i önskelista")]
        public void WhenJagSpararOnskanIOnskelista()
        {
            _bekraftelse = _whishListController.newWish(_namn, _leksak);
        }

        [Then(@"Så får jag tillbaka (.*)")]
        public void ThenSaFarJagTillbakaFolkesOnskanSparades(string bekraftelse)
        {
            Assert.Equal(bekraftelse, _bekraftelse);
        }
    }
}
