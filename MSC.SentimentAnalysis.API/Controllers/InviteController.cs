using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MSC.SentimentAnalysis.API.Models;
using MSC.SentimentAnalysis.API.Models.Commands;
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
        public IActionResult CreateNewInvite(CreateInvite invite)
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
        public IActionResult UpdateArtistRating(UpdateArtistInvite invite)
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
        public IActionResult UpdateEstablishmentRating(UpdateEstablishmentInvite invite)
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

        [HttpGet("find-by-id/{inviteId}")]
        public IActionResult GetInviteById(Guid inviteId)
        {
            try
            {
                var result = _inviteRepository.FindInviteById(inviteId);

                return Ok(SerializeJsonString(result));
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("find-invites-by-latitude-longitude/{latitude}/{longitude}")]
        public IActionResult GetInvitesByLatitudeAndLongitude(int latitude, int longitude)
        {
            try
            {
                var invites = _inviteRepository.FindInvitesByLatitudeAndLongitude(latitude, longitude);

                return Ok(SerializeJsonString(invites));
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("find-invites-by-establishments/{establishmentsId}")]
        public IActionResult GetInvitesByEstablishments(Guid establishmentsId)
        {
            try
            {
                var invites = _inviteRepository.FindInvitesByEstablishments(establishmentsId);

                return Ok(SerializeJsonString(invites));
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("find-invites-by-artists/{artistsId}")]
        public IActionResult GetInvitesByArtists(Guid artistsId)
        {
            try
            {
                var invites = _inviteRepository.FindInvitesByArtists(artistsId);

                return Ok(SerializeJsonString(invites));
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        private string SerializeJsonString(object data)
        {
            return JsonSerializer.Serialize(data);
        }
    }
}
