using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using WhishList.WhishList;
using Xunit;

namespace WhishList.Specs
{
    public class WhishListControllerTest
    {
        [Fact]
        public void shouldSaveWhish()
        {
            var whishDAO = new Mock<IWhishDAO>();
            whishDAO.Setup(w => w.save(It.IsAny<Whish>())).Returns(true);
            WhishListController whishListController = new WhishListController(whishDAO.Object);
            string confirmationText = whishListController.newWish("Folke", "kniv");
            Assert.Equal("Folkes önskan sparades!", confirmationText);
        }
    }
}
