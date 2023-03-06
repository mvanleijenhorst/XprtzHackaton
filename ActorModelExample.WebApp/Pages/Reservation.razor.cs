using ActorModelExample.Domain.Models;
using ActorModelExample.Domain.Services;
using ActorModelExample.WebApp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace ActorModelExample.WebApp.Pages;

public partial class Reservation
{
    private readonly BookingModel _booking = new();

    [Inject]
    public IVenueService Service { get; set; } = null!;

    [Inject]
    public Venue Venue { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    [Parameter]
    public string ParamId { get; set; } = string.Empty;

    public LiveEvent LiveEvent { get; set; } = null!;

    protected override async Task OnParametersSetAsync()
    {
        var id = new Guid(ParamId);
        if (!Venue.LiveEvents.Any(e => e.Id == id))
        {
            NavigationManager.NavigateTo(PageConstants.Index);
            return;
        }

        LiveEvent = Venue.LiveEvents.First(e => e.Id == id);

        var seatInfo = await Service.GetSeatInfoAsync(LiveEvent);
        ReserveSeats = seatInfo[SeatStatus.Reserved];
        BookedSeats = seatInfo[SeatStatus.Booked];
    }

    private IEnumerable<int> ReserveSeats { get; set; } = Array.Empty<int>();

    private IEnumerable<int> BookedSeats { get; set; } = Array.Empty<int>();

    private async Task HandleValidSubmitAsync()
    {
        if (!ReserveSeats.Any())
        {
            return;
        }

        await Service.ConfirmBookingAsync(LiveEvent, _booking.Name, ReserveSeats);

        NavigationManager.NavigateTo(PageConstants.Index);
    }

    private async Task CancelReservationAsync(MouseEventArgs _, int seatNumber)
    {
        await Service.CancelSeatReservationAsync(LiveEvent, seatNumber);

        var seatInfo = await Service.GetSeatInfoAsync(LiveEvent);
        ReserveSeats = seatInfo[SeatStatus.Reserved];
        BookedSeats = seatInfo[SeatStatus.Booked];
    }

    private async Task ReserveSeatAsync(int seatNumber)
    {
        await Service.ReserveSeatAsync(LiveEvent, seatNumber);

        var seatInfo = await Service.GetSeatInfoAsync(LiveEvent);
        ReserveSeats = seatInfo[SeatStatus.Reserved];
        BookedSeats = seatInfo[SeatStatus.Booked];
    }
}
