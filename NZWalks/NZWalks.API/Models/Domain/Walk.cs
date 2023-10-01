namespace NZWalks.API.Models.Domain
{
    public class Walk
    {

        public Guid Id { get; set; }  //unique identifier

        public string Name { get; set; }

        public string Description { get; set; }

        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }

        public Guid DifficultyId { get; set; }

        public Guid RegionId { get; set; } //one to one


        public Difficulty Difficulty { get; set; } //navigation prop
        public Region Region { get; set; } //navigation prop

    }
}
