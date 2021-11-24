using System;
using System.Collections.Generic;
using System.Linq;
using MSC.SentimentAnalysis.API.DTOs;
using MSC.SentimentAnalysis.API.Extensions;
using MSC.SentimentAnalysis.API.Models;
using MSC.SentimentAnalysis.API.Models.Commands;
using MSC.SentimentAnalysis.API.Models.Interfaces;

namespace MSC.SentimentAnalysis.API.Data.Repositories
{
    public class InviteRepository : Repository, IInviteRepository
    {
        public InviteRepository(CassandraSettings cassandraSettings) : base(cassandraSettings)
        {
        }

        public void CreateNewInvite(CreateInvite invite)
        {
            string sql = "INSERT INTO invites_dc.invites" +
                         "(id, address, address_number, artist_id, \"date\", establishment_id, latitude, longitude, postal_code)" +
                         "VALUES" +
                         "(:id, :address, :address_number, :artist_id, :\"date\", :establishment_id, :latitude, :longitude, :postal_code)";


            var statement = this._session.Prepare(sql)
                .Bind(new
                {
                    id = invite.Id,
                    address = invite.Address, 
                    address_number = invite.AddressNumber, 
                    artist_id = invite.IdArtist,
                    date = DateTime.Now.ToString("yyyy-MM-dd"),
                    establishment_id = invite.IdEstablishment,
                    latitude = invite.Latitude,
                    longitude = invite.Longitude,
                    postal_code = invite.PostalCode
                });

            this._session.Execute(statement);
        }

        public void CreateInvitesByLatitudeAndLongitude(CreateInvite invite)
        {
            string sql = "INSERT INTO invites_dc.invites_by_latitude_and_longitude" +
                         " (invite_id, latitude, longitude)" +
                         " VALUES" +
                         " (:invite_id, :latitude, :longitude)";


            var statement = this._session.Prepare(sql)
                .Bind(new
                {
                    invite_id = invite.Id,
                    latitude = invite.Latitude,
                    longitude = invite.Longitude,
                });

            this._session.Execute(statement);
        }

        public void CreateInvitesByEstablishments(CreateInvite invite)
        {
            string sql = "INSERT INTO invites_dc.invites_by_establishments " +
                         " (invite_id, establishment_id)" +
                         " VALUES" +
                         " (:invite_id, :establishment_id)";
             

            var statement = this._session.Prepare(sql)
                .Bind(new
                {
                    invite_id = invite.Id,
                    establishment_id = invite.IdEstablishment,
                });

            this._session.Execute(statement);
        }

        public void CreateInvitesByArtists(CreateInvite invite)
        {
            string sql = "INSERT INTO invites_dc.invites_by_artists " +
                         " (invite_id, artist_id)" +
                         " VALUES" +
                         " (:invite_id, :artist_id)";


            var statement = this._session.Prepare(sql)
                .Bind(new
                {
                    invite_id = invite.Id,
                    artist_id = invite.IdArtist,
                });

            this._session.Execute(statement);
        }

        public void UpdateArtistRating(UpdateArtistInvite invite)
        {
            string sql = "UPDATE invites_dc.invites SET artist_rating = :rating,  artist_comment = {content: :content, feeling: :feeling} WHERE id = :id";

            var statement = this._session.Prepare(sql)
                .Bind(new
                {
                    rating = invite.ArtistRating,
                    content = invite.ArtistComment.Content,
                    feeling = invite.ArtistComment.Feeling,
                    id = invite.Id
                });

            this._session.Execute(statement);
        }

        public void UpdateEstablishmentRating(UpdateEstablishmentInvite invite)
        {
            string sql = "UPDATE invites_dc.invites SET establishment_rating = :rating,  establishment_comment = {content: :content, feeling: :feeling} WHERE id = :id";

            var statement = this._session.Prepare(sql)
                .Bind(new
                {
                    rating = invite.EstablishmentRating,
                    content = invite.EstablishmentComment.Content,
                    feeling = invite.EstablishmentComment.Feeling,
                    id = invite.Id
                });

            this._session.Execute(statement);
        }

        public Invite FindInviteById(Guid id)
        {
            string sql = @"SELECT id as Id, establishment_id as  IdEstablishment, artist_id as IdArtist, artist_rating as ArtistRating,
                            artist_comment as ArtistComment, establishment_rating as EstablishmentRating, establishment_comment as EstablishmentComment,
                            Latitude as Latitude, Longitude as Longitude, postal_code as PostalCode, Address as Address, address_number as AddressNumber 
                            FROM invites_dc.invites WHERE id = ?";

            var invite = _mapper.Single<Invite>(sql, id);

            return invite;           
        }

        public List<InvitesByLatitudeAndLongitudeDto> FindInvitesByLatitudeAndLongitude(int latitude, int longitude)
        {
            string sql = "SELECT  invite_id AS InviteId, latitude AS Latitude, longitude AS Longitude " +
                         " FROM invites_dc.invites_by_latitude_and_longitude WHERE latitude = ? AND longitude = ?";

            var invites = _mapper.Fetch<InvitesByLatitudeAndLongitudeDto>(sql, (float)latitude, (float)longitude);

            return invites.ToList();
        }

        public List<InvitesByEstablishmentsDto> FindInvitesByEstablishments(Guid establishmentsId)
        {
            string sql = "SELECT  invite_id AS InviteId, establishment_id AS EstablishmentId " +
                         " FROM invites_dc.invites_by_establishments WHERE establishment_id = ? ALLOW FILTERING";

            var invites = _mapper.Fetch<InvitesByEstablishmentsDto>(sql, establishmentsId);

            return invites.ToList();
        }

        public List<InvitesByArtists> FindInvitesByArtists(Guid artistId)
        {
            string sql = "SELECT  invite_id AS InviteId, artist_id AS ArtistId " +
                         " FROM invites_dc.invites_by_artists WHERE artist_id = ? ALLOW FILTERING";

            var invites = _mapper.Fetch<InvitesByArtists>(sql, artistId);

            return invites.ToList();
        }
    }
}
