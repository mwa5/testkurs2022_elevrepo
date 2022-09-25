using Lab3WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab3WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CartController : ControllerBase
{
    readonly ICartService _cartService;
    readonly IPaymentService _paymentService;
    readonly IShipmentService _shipmentService;

    public CartController(ICartService cartService, IPaymentService paymentService, IShipmentService shipmentService)
    {
        _cartService = cartService;
        _paymentService = paymentService;
        _shipmentService = shipmentService;
    }

    [HttpPost]
    public async Task<IActionResult> CheckOutAsync([FromQuery] ICard card, [FromQuery] IAddressInfo addressInfo)
    {
        var result = await _paymentService.ChargeAsync(_cartService.Total(), card);

        if (!result)
            return StatusCode(StatusCodes.Status500InternalServerError, "not charged");

        await _shipmentService.ShipAsync(addressInfo, _cartService.Items());

        return Ok("charged");
    }

    [HttpPost]
    public async Task<IActionResult> CheckOutDuringWorkHoursAsync(ICard card, IAddressInfo addressInfo)
    {
        if (!WithinWorkHours())
            return StatusCode(StatusCodes.Status500InternalServerError, "Trying to check out outside range of work hours.");

        var result = await _paymentService.ChargeAsync(_cartService.Total(), card);

        if (!result)
            return StatusCode(StatusCodes.Status500InternalServerError, "not charged");

        await _shipmentService.ShipAsync(addressInfo, _cartService.Items());

        return Ok("charged");
    }

    static bool WithinWorkHours()
    {
        var startWorkingTime = new TimeSpan(08, 00, 00);
        var endWorkingTime = new TimeSpan(17, 00, 00);

        return DateTime.Now.TimeOfDay <= endWorkingTime && DateTime.Now.TimeOfDay >= startWorkingTime;
    }
}