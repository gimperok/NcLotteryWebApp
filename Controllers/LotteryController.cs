using Microsoft.AspNetCore.Mvc;
using NcLotteryWebApp.Models;
using NcLotteryWebApp.Services.Factories;

namespace NcLotteryWebApp.Controllers
{
    /// <summary>
    /// Lottery controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LotteryController : ControllerBase
    {
        private readonly LotteryFactory _factory;

        //The factory is injected automatically via DI.
        public LotteryController(LotteryFactory factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Generates a lottery ticket based on the specified lottery type.
        /// </summary>
        /// <param name="type">The type of lottery for which to generate a ticket. Valid values are "powerball" and "megamillions".</param>
        /// <returns>An <see cref="ActionResult"/> containing the generated lottery ticket if the type is valid; otherwise, a bad
        /// request response with an error message.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(LotteryResult), 200)]
        [ProducesResponseType(400)]
        public ActionResult GetTicket(string type)
        {
            var lottery = _factory.CreateLottery(type);
            if (lottery == null)
                return BadRequest(new 
                {
                    error = "Invalid lottery type. " +
                            "Use 'powerball' or 'megamillions'"
                });

            var ticket = lottery.GenerateTicket();
            return Ok(ticket);
        }
    }
}
