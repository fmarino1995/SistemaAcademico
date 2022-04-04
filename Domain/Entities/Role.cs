﻿namespace Domain.Entities
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public Guid ConcurrencyStamp { get; set; }
    }
}
