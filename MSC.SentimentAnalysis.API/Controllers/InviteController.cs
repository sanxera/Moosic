using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSC.SentimentAnalysis.API.Models;
using MSC.SentimentAnalysis.API.Models.Interfaces;
using MSC.SentimentAnalysis.API.Services;

namespace MSC.SentimentAnalysis.API.Controllers
{
    [ApiController]
    [Route("api/invite")]
    public class InviteController : ControllerBase
    {
        private readonly IInviteRepository _inviteRepository;
        private readonly ISentimentAnalysisService _sentimentAnalysisService;

        public InviteController(IInviteRepository inviteRepository, ISentimentAnalysisService sentimentAnalysisService)
        {
            _inviteRepository = inviteRepository;
            _sentimentAnalysisService = sentimentAnalysisService;
        }

        [HttpPost("new-invite")]
        public IActionResult CreateNewInvite(Invite invite)
        {
            try
            {
                this._inviteRepository.CreateNewInvite(invite);
                this._inviteRepository.CreateInvitesByArtists(invite);
                this._inviteRepository.CreateInvitesByEstablishments(invite);
                this._inviteRepository.CreateInvitesByLatitudeAndLongitude(invite);

                return new StatusCodeResult(200);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPost("update-artist-rating")]
        public IActionResult UpdateArtistRating(Invite invite)
        {
            try
            {
                invite.ArtistComment.Feeling = Convert.ToInt32(this._sentimentAnalysisService.ExecuteAnalysis(invite.ArtistComment.Content));

                this._inviteRepository.UpdateArtistRating(invite);

                return new StatusCodeResult(200);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPost("update-establishment-rating")]
        public IActionResult UpdateEstablishmentRating(Invite invite)
        {
            try
            {
                invite.EstablishmentComment.Feeling = Convert.ToInt32(this._sentimentAnalysisService.ExecuteAnalysis(invite.EstablishmentComment.Content));

                this._inviteRepository.UpdateEstablishmentRating(invite);

                return new StatusCodeResult(200);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("find-by-id")]
        public IActionResult GetInviteById(Invite invite)
        {
            try
            {
                var result = _inviteRepository.FindInviteById(invite.Id);

                return Ok(GetContent(result));
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("")]
        public IActionResult GetInvitesByLatitudeAndLongitude(Guid inviteGuid)
        {
            try
            {
                var invites = _inviteRepository.FindInvitesByLatitudeAndLongitude(inviteGuid);

                return Ok(GetContent(invites));
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("")]
        public IActionResult GetInvitesByEstablishments(Guid establishmentsId)
        {
            try
            {
                var invites = _inviteRepository.FindInvitesByEstablishments(establishmentsId);

                return Ok(GetContent(invites));
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        public IActionResult GetInvitesByArtists(Guid artistsId)
        {
            try
            {
                var invites = _inviteRepository.FindInvitesByArtists(artistsId);

                return Ok(GetContent(invites));
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        private Invite DeserializeInviteJson(string json)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = JsonSerializer.Deserialize<Invite>(json, options);

            return result;
        }

        protected StringContent GetContent(object data)
        {
            return new StringContent(JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json");
        }
    }
}
