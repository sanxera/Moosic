using System;
using System.Collections.Generic;
using MSC.SentimentAnalysis.API.DTOs;

namespace MSC.SentimentAnalysis.API.Models.Interfaces
{
    public interface IInviteRepository
    {
        void CreateNewInvite(Invite invite);
        void CreateInvitesByLatitudeAndLongitude(Invite invite);
        void CreateInvitesByEstablishments(Invite invite);
        void CreateInvitesByArtists(Invite invite);
        void UpdateArtistRating(Invite invite);
        void UpdateEstablishmentRating(Invite invite);
        Invite FindInviteById(Guid id);
        List<InvitesByLatitudeAndLongitudeDto> FindInvitesByLatitudeAndLongitude(Guid inviteId);
        List<InvitesByEstablishmentsDto> FindInvitesByEstablishments(Guid establishmentsId);
        List<InvitesByArtists> FindInvitesByArtists(Guid artistId);
    }
}
