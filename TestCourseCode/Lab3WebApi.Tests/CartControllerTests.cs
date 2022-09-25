using Lab3WebApi.Controllers;
using Lab3WebApi.Services;
using Moq;
using Xunit;

namespace Lab3WebApi.Tests
{
    public class CartControllerTests
    {
        readonly CartController _cartController;
        readonly Mock<IPaymentService> _paymentServiceMock;
        readonly Mock<IShipmentService> _shipmentServiceMock;
        readonly Mock<ICard> _cardMock;
        readonly Mock<IAddressInfo> _addressInfoMock;
        readonly List<ICartItem> _items;

        public CartControllerTests()
        {
            var cartServiceMock = new Mock<ICartService>();
            _paymentServiceMock = new Mock<IPaymentService>();
            _shipmentServiceMock = new Mock<IShipmentService>();

            // arrange
            _cardMock = new Mock<ICard>();
            _addressInfoMock = new Mock<IAddressInfo>();

            var cartItemMock = new Mock<ICartItem>();
            cartItemMock.Setup(item => item.Price).Returns(10);

            _items = new List<ICartItem>
            {
                cartItemMock.Object
            };

            cartServiceMock.Setup(c => c.Items()).Returns(_items.AsEnumerable());

            _cartController = new CartController(cartServiceMock.Object, _paymentServiceMock.Object, _shipmentServiceMock.Object);
        }

        //Tips: Refaktorisera controllern så att den blir testbar. Använd gärna Moq (Mock) för att mocka metodanrop resultat
        [Fact]
        public void ShouldReturnCharged()
        {
            //TODO: Vi vill verifiera resultatet tillbaka att det har gått bra

            //TODO: Vi vill också verifiera att metoden "Ship" har blivit kallad på en gång då det borde betyda att varan har blivit skickad
        }

        [Fact]
        public void ShouldReturnNotCharged()
        {
            //TODO: Vi vill verifiera att det inte har gått bra

            //TODO: Vi vill också verifiera att ordern inte har blivit skickad dvs. att metoden "Ship" inte har blivit kallad på
        }



        //TODO: Testa att CheckOutDuringWorkHoursAsync verkligen är implementerad inom 8 och 17
    }


}