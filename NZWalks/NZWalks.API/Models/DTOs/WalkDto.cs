 namespace NZWalks.API.Models.DTOs
{
    public class WalkDto
    {
        public Guid Id { get; set; }  //unique identifier

        
        public string Name { get; set; }

        public string Description { get; set; }

        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }

        public Guid DifficultyId { get; set; }

        public Guid RegionId { get; set; } //one to one

        public RegionDto Region { get; set; }
        public DifficulityDto Difficulity { get; set; }


    }
}
