using System;
using System.Collections.Generic;
using MSC.SentimentAnalysis.API.DTOs;
using MSC.SentimentAnalysis.API.Models.Commands;

namespace MSC.SentimentAnalysis.API.Models.Interfaces
{
    public interface IInviteRepository
    {
        void CreateNewInvite(CreateInvite invite);
        void CreateInvitesByLatitudeAndLongitude(CreateInvite invite);
        void CreateInvitesByEstablishments(CreateInvite invite);
        void CreateInvitesByArtists(CreateInvite invite);
        void UpdateArtistRating(UpdateArtistInvite invite);
        void UpdateEstablishmentRating(UpdateEstablishmentInvite invite);
        Invite FindInviteById(Guid id);
        List<InvitesByLatitudeAndLongitudeDto> FindInvitesByLatitudeAndLongitude(int latitude, int longitude);
        List<InvitesByEstablishmentsDto> FindInvitesByEstablishments(Guid establishmentsId);
        List<InvitesByArtists> FindInvitesByArtists(Guid artistId);
    }
}
