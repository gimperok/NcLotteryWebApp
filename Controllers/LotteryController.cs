using Microsoft.AspNetCore.Mvc;
using NcLotteryWebApp.Models;
using NcLotteryWebApp.Services;
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
        private readonly LotteryParserService  _parserService;

        //The factory is injected automatically via DI.
        public LotteryController(LotteryFactory factory, LotteryParserService parserService)
        {
            _factory = factory;
            _parserService = parserService;
        }

        /// <summary>
        /// Generates a lottery ticket based on the specified lottery type.
        /// </summary>
        /// <param name="lotteryType">The type of lottery for which to generate a ticket. Valid values are "powerball" and "megamillions".</param>
        /// <returns>An <see cref="ActionResult"/> containing the generated lottery ticket if the type is valid; otherwise, a bad
        /// request response with an error message.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(LotteryResult), 200)]
        [ProducesResponseType(400)]
        public ActionResult GetGeneratedTicket(LotteryType lotteryType)
        {
            var lottery = _factory.CreateLottery(lotteryType);
            if (lottery == null)
                return BadRequest(new 
                {
                    error = "Invalid lottery type. " +
                            "Use 'powerball' or 'megamillions'"
                });

            var ticket = lottery.GenerateTicket();
            return Ok(ticket);
        }

        /// <summary>
        /// Retrieves the latest draw results for the specified lottery type.
        /// </summary>
        /// <remarks>This method calls an asynchronous service to parse and retrieve the latest lottery
        /// draw results. Ensure that the <paramref name="lotteryType"/> is valid to avoid receiving a bad request
        /// response.</remarks>
        /// <param name="lotteryType">The type of lottery for which to retrieve results. Valid values are "powerball" or "megamillions".</param>
        /// <returns>An <see cref="IActionResult"/> containing the latest <see cref="LotteryResult"/> if successful; otherwise, a
        /// bad request response with an error message.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(LotteryResult), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetLatestDrawResults(LotteryType lotteryType)
        {
            var lottery = _factory.CreateLottery(lotteryType);
            if (lottery == null)
                return BadRequest(new
                {
                    error = "Invalid lottery type. " +
                            "Use 'powerball' or 'megamillions'"
                });

            var result = await _parserService.ParseArchiveDataAsync(lottery);
            if (result == null)
            {
                return BadRequest(new
                {
                    error = "Failed to retrieve or parse the data."
                });
            }
            return Ok(result);
        }
    }
}