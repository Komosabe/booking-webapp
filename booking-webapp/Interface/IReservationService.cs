﻿using BackendBooking.Models.Reservation;

namespace BackendBooking.Interface
{
    public interface IReservationService
    {
        Task<int> CreateReservationAsync(CreateReservationModel model);
        Task DeleteReservationAsync(int reservationId);
        Task<IEnumerable<ReservationModel>> GetReservationsForConcertAsync(int concertId);
        Task<IEnumerable<ReservationModel>> GetReservationsForUserAsync(int userId);
    }
}