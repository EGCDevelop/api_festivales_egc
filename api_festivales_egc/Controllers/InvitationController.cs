using api_festivales_egc.Middle;
using api_festivales_egc.Models;
using api_festivales_egc.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;

namespace api_festivales_egc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvitationController(ILogger<InvitationController> logger, IConfiguration configuration) : Controller
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<InvitationController> _logger = logger;


        [HttpGet]
        [Route("get_ping")]
        public IActionResult GetPing()
        {
            return Ok(new
            {
                ok = true,
                message = "GetPing ... InvitationController"
            });
        }

        [HttpPost]
        [Route("invitation_validate")]
        public IActionResult InvitationValidate([FromBody] JsonObject json)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DbEgcConnection")!;

                string invitationCode = json["invitationCode"]!.ToString();

                FestivalInvitationDTO festivalInvitationDTO = InvitationMiddle.EXEC_SP_FES_GET_VALIDATE_INVITATION(connectionString, invitationCode);

                if(festivalInvitationDTO != null)
                {
                    return Ok(new
                    {
                        ok = true,
                        festivalInvitationDTO.FestivalId,
                        festivalInvitationDTO.FestivalName,
                        festivalInvitationDTO.FestivalUbication,
                        festivalInvitationDTO.FestivalDate,
                        festivalInvitationDTO.FestivalStartTime,
                        festivalInvitationDTO.GestBandId,
                        festivalInvitationDTO.GestBandName,
                        festivalInvitationDTO.GestBandState,
                        festivalInvitationDTO.GestFestivalId,
                        festivalInvitationDTO.GestFestivalActive,
                        festivalInvitationDTO.GestFestivalNumberEntries,
                        festivalInvitationDTO.GestFestivalParticipation
                    });
                }

                return Unauthorized(new
                {
                    ok = false,
                    message = "El código ingresado no es correcto."
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
        [Route("accept_invitation")]
        public IActionResult AcceptInvitation([FromBody] InvitationRequest model)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DbEgcConnection")!;

                _logger.LogInformation($"model.ToString() = {model.ToString()}");

                InvitationMiddle.TRANSACTION_UPDATE_FESTIVAL_INVITATIONS(connectionString, model);

                return Ok(new
                {
                    ok = true,
                    message = "Invitación forjada"
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
        [Route("reject_invitation")]
        public IActionResult RejectInvitation([FromBody] InvitationRequest model)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DbEgcConnection")!;

                InvitationMiddle.TRANSACTION_INSERT_FESTIVAL_INVITATIONS_DETAIL_REJECT(connectionString, model);

                return Ok(new
                {
                    ok = true,
                    message = "Invitación rechazada"
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
