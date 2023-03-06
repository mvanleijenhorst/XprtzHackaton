using ActorModelExample.Domain.Models;

namespace ActorModelExample.Domain.Services;

public interface IVenueService
{
    Task RegisterVenueAsync(Venue venue);
    Task<Dictionary<SeatStatus, List<int>>> GetSeatInfoAsync(LiveEvent liveEvent);

    Task ReserveSeatAsync(LiveEvent liveEvent, int seatNumber);
    Task CancelSeatReservationAsync(LiveEvent liveEvent, int seatNumber);
    Task ConfirmBookingAsync(LiveEvent liveEvent, string name, IEnumerable<int> confirmedSeats);
}