﻿namespace NZWalks.API.Models.Domain
{
    public class Region
    {
        public Guid Id { get; set; }  //unique identifier

        public string Code { get; set; }

        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }

    }
}
