using System.ComponentModel.DataAnnotations;

namespace Discussion.Models.DTO_s.EmailDTO_s;

public class EmailDTO
{
    [Required(ErrorMessage = "The Email of the Receiver is required")]
    [DataType(DataType.EmailAddress)]
    public string To { get; set; }

    [Required(ErrorMessage = "Subject is required")]
    public string Subject { get; set; }

    [Required(ErrorMessage = "Email Body is required")]
    public string Body { get; set; } 
}
