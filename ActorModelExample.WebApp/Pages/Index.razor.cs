using ActorModelExample.Domain.Models;
using Microsoft.AspNetCore.Components;

namespace ActorModelExample.WebApp.Pages;

public partial class Index
{
    [Inject]
    public Venue Venue { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    public void SelectLiveEvent(Guid id)
    {
        NavigationManager.NavigateTo($"{PageConstants.Reservation}/{id}", true);
    }
}
