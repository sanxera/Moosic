using System;
using System.IO;
using System.Net;
using System.Net.Http;
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
    [Route("api")]
    public class InviteController : ControllerBase
    {
        private readonly IInviteRepository _inviteRepository;
        private readonly ISentimentAnalysisService _sentimentAnalysisService;

        public InviteController(IInviteRepository inviteRepository, ISentimentAnalysisService sentimentAnalysisService)
        {
            _inviteRepository = inviteRepository;
            _sentimentAnalysisService = sentimentAnalysisService;
        }

        [HttpPost]
        public IActionResult CreateNewInvite(string inviteJson)
        {
            try
            {
                var invite = DeserializeInviteJson(inviteJson);

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

        [HttpPost]
        public IActionResult UpdateInvite(string inviteJson)
        {
            try
            {
                var invite = DeserializeInviteJson(inviteJson);



                return new StatusCodeResult(200);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        public IActionResult UpdateArtistRating(string inviteJson)
        {
            try
            {
                var invite = DeserializeInviteJson(inviteJson);

                invite.ArtistComment.Feeling = Convert.ToInt32(this._sentimentAnalysisService.ExecuteAnalysis(invite.ArtistComment.Content));

                this._inviteRepository.UpdateArtistRating(invite);

                return new StatusCodeResult(200);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        public IActionResult UpdateEstablishmentRating(string inviteJson)
        {
            try
            {
                var invite = DeserializeInviteJson(inviteJson);

                invite.EstablishmentComment.Feeling = Convert.ToInt32(this._sentimentAnalysisService.ExecuteAnalysis(invite.EstablishmentComment.Content));

                this._inviteRepository.UpdateEstablishmentRating(invite);

                return new StatusCodeResult(200);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        public IActionResult GetInviteById(Guid inviteGuid)
        {
            try
            {
                var invite = _inviteRepository.FindInviteById(inviteGuid);

                return Ok(GetContent(invite));
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
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

        [HttpGet]
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
