using api_festivales_egc.Middle;
using api_festivales_egc.Models;
using Microsoft.AspNetCore.Mvc;

namespace api_festivales_egc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FestivalController(ILogger<FestivalController> logger, IConfiguration configuration) : Controller
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<FestivalController> _logger = logger;

        [HttpGet]
        [Route("get_ping")]
        public IActionResult GetPing()
        {
            return Ok(new
            {
                ok = true,
                message = "GetPing ... FestivalController"
            });
        }

        [HttpGet]
        [Route("get_festival_by_filters")]
        public IActionResult GetFestivalByFilters([FromQuery] string festivalName, [FromQuery] int festivalYear)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DbEgcConnection")!;
                string search = string.IsNullOrWhiteSpace(festivalName) ? "%" : festivalName.ToLower();

                List<Festival> data =FestivalMiddle.EXEC_SP_GET_FESTIVAL_BY_FILTERS(connectionString, search, festivalYear);

                return Ok(new
                {
                    ok = true,
                    data
                });
            }
            catch (Exception ex)
            {
                var fullMessage = ex.InnerException != null
                                  ? $"{ex.Message} | Original: {ex.InnerException.Message}"
                                  : ex.Message;

                _logger.LogInformation($"fullMessage == {fullMessage}");
                _logger.LogInformation($"StackTrace == {ex.StackTrace}");

                return StatusCode(500, fullMessage);
            }
        }


        [HttpGet]
        [Route("get_bands_invited_festival")]
        public IActionResult GetBandsInvitedFestival([FromQuery] string bandName, [FromQuery] byte bandState)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DbEgcConnection")!;
                string search = string.IsNullOrWhiteSpace(bandName) ? "%" : bandName.ToLower();

                List<BandFestival> data = FestivalMiddle.EXEC_SP_GET_BANDS_INVITED_TO_FESTIVAL(connectionString, search, bandState);

                return Ok(new
                {
                    ok = true,
                    data
                });
            }
            catch (Exception ex)
            {
                var fullMessage = ex.InnerException != null
                                  ? $"{ex.Message} | Original: {ex.InnerException.Message}"
                                  : ex.Message;

                _logger.LogInformation($"fullMessage == {fullMessage}");
                _logger.LogInformation($"StackTrace == {ex.StackTrace}");

                return StatusCode(500, fullMessage);
            }
        }


        [HttpGet]
        [Route("get_invitations_festival")]
        public IActionResult GetInvitationsFestival([FromQuery] byte isActive, [FromQuery] byte participation)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DbEgcConnection")!;

                List<FestivalInvitation> data = FestivalMiddle.EXEC_SP_GET_INVITATIONS_FESTIVAL(connectionString, isActive, participation);

                return Ok(new
                {
                    ok = true,
                    data
                });
            }
            catch (Exception ex)
            {
                var fullMessage = ex.InnerException != null
                                  ? $"{ex.Message} | Original: {ex.InnerException.Message}"
                                  : ex.Message;

                _logger.LogInformation($"fullMessage == {fullMessage}");
                _logger.LogInformation($"StackTrace == {ex.StackTrace}");

                return StatusCode(500, fullMessage);
            }
        }

        [HttpGet]
        [Route("get_detail_invitations_festival")]
        public IActionResult GetDetailInvitationsFestival([FromQuery] int festivalInvitation, [FromQuery] int detailId)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DbEgcConnection")!;

                List<DetailInvitationsFestival> data = FestivalMiddle.EXEC_SP_GET_DETAIL_INVITATIONS_FESTIVAL(connectionString, festivalInvitation, detailId);

                return Ok(new
                {
                    ok = true,
                    data
                });
            }
            catch (Exception ex)
            {
                var fullMessage = ex.InnerException != null
                                  ? $"{ex.Message} | Original: {ex.InnerException.Message}"
                                  : ex.Message;

                _logger.LogInformation($"fullMessage == {fullMessage}");
                _logger.LogInformation($"StackTrace == {ex.StackTrace}");

                return StatusCode(500, fullMessage);
            }
        }

        [HttpPost]
        [Route("insert_festival")]
        public IActionResult InsertFestival([FromBody] FestivalRequest request)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DbEgcConnection")!;

                FestivalMiddle.EXEC_SP_INSERT_FESTIVAL(
                    connectionString,
                    request.Name,
                    request.Year,
                    request.Ubication,
                    request.Date,
                    request.StartDate,
                    request.EndDate,
                    request.EntrancePrice
                );

                return Ok(new
                {
                    ok = true,
                    message = "Festival registrado correctamente."
                });
            }
            catch (Exception ex)
            {
                var fullMessage = ex.InnerException != null
                    ? $"{ex.Message} | Original: {ex.InnerException.Message}"
                    : ex.Message;

                _logger.LogInformation($"fullMessage == {fullMessage}");
                _logger.LogInformation($"StackTrace == {ex.StackTrace}");

                return StatusCode(500, fullMessage);
            }
        }

        [HttpPost]
        [Route("insert_festival_invitation_band")]
        public IActionResult InsertFestivalInvitationBand([FromBody] FestivalInvitationBandRequest request)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DbEgcConnection")!;

                FestivalMiddle.EXEC_SP_INSERT_FESTIVAL_INVITATION_BAND(
                    connectionString,
                    request.BandName,
                    request.State
                );

                return Ok(new
                {
                    ok = true,
                    message = "Banda registrada correctamente."
                });
            }
            catch (Exception ex)
            {
                var fullMessage = ex.InnerException != null
                    ? $"{ex.Message} | Original: {ex.InnerException.Message}"
                    : ex.Message;

                _logger.LogInformation($"fullMessage == {fullMessage}");
                _logger.LogInformation($"StackTrace == {ex.StackTrace}");

                return StatusCode(500, fullMessage);
            }
        }

        [HttpPost]
        [Route("insert_festival_invitation")]
        public IActionResult InsertFestivalInvitation([FromBody] FestivalInvitationRequest request)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DbEgcConnection")!;

                FestivalMiddle.EXEC_SP_INSERT_FESTIVAL_INVITATION(
                    connectionString,
                    request.ClaveInvitacion,
                    request.Activa,
                    request.CantidadEntradas,
                    request.Participacion,
                    request.FESId,
                    request.BIFId
                );

                return Ok(new
                {
                    ok = true,
                    message = "Invitación registrada correctamente."
                });
            }
            catch (Exception ex)
            {
                var fullMessage = ex.InnerException != null
                    ? $"{ex.Message} | Original: {ex.InnerException.Message}"
                    : ex.Message;

                _logger.LogInformation($"fullMessage == {fullMessage}");
                _logger.LogInformation($"StackTrace == {ex.StackTrace}");

                return StatusCode(500, fullMessage);
            }
        }
    }
}
