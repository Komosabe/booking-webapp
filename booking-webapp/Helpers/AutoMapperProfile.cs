﻿using AutoMapper;
using BackednBooking.Entities;
using BackendBooking.Models.Concert;
using BackendBooking.Models.Hall;
using BackendBooking.Models.Reservation;
using BackendBooking.Models.User;

namespace BackendBooking.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region User
            // RegisterRequest -> User
            CreateMap<RegisterRequest, User>();

            // User -> AuthenticateResponse
            CreateMap<User, AuthenticateResponse>();

            // User -> GetModelUser
            CreateMap<User, GetModelUser>();

            // UpdateRequest -> User
            CreateMap<UpdateUserRequest, User>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignore null & empty string properties
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        return true;
                    }
                ));
            #endregion

            #region Hall

            // CreateHall -> Hall
            CreateMap<CreateHallModel, Hall>();

            // Hall -> HallModel
            CreateMap<Hall,  HallModel>();

            // UpdateHallModel -> Hall
            CreateMap<UpdateHallModel, Hall>();

            #endregion

            #region Concert
            // CreateConcertModel -> Concert
            CreateMap<CreateConcertModel, Concert>();

            // Concert -> ConcertModel
            CreateMap<Concert, ConcertModel>();

            // UpdateConcertModel -> Concert
            CreateMap<UpdateConcertModel, Concert>();

            #endregion

            #region Reservation
            // CreateReservationModel -> Reservation
            CreateMap<CreateReservationModel, Reservation>();

            // Reservation -> ReservationModel
            CreateMap<Reservation, ReservationModel>();
            #endregion
        }
    }
}
