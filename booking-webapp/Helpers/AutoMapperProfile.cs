using AutoMapper;
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
            // CreateUser -> User
            CreateMap<CreateUserModel, User>();

            // UserModel -> User
            CreateMap<UserModel, User>();

            // User -> UserModel
            CreateMap<User, UserModel>();

            // UpdateUserModel -> User
            CreateMap<UpdateUserModel, User>();

            // User -> LoginModel
            CreateMap<User, LoginModel>();

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
