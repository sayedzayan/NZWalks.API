using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTOs
{
    public class AddRegionRequestDTO
    {
        //  public Guid Id { get; set; }  //unique identifier

        [Required]
        [MinLength(3,ErrorMessage ="Code has to be a minimum of 3 characters")]
        [MaxLength(3, ErrorMessage = "Code has to be a minimum of 3 characters")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Code has to be a minimum of 3 characters")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }

    }
}


