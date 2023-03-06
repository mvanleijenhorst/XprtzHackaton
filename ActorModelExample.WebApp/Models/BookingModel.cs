using System.ComponentModel.DataAnnotations;

namespace ActorModelExample.WebApp.Models;

public class BookingModel
{
    [Required]
    [MinLength(3, ErrorMessage = "Fullname must be longer than 2 characters")]
    public string Name { get; set; } = null!;
}
