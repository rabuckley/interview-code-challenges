﻿namespace OneBeyondApi.Model
{
    public class Borrower
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }

        public List<Fine> Fines { get; set; } = new List<Fine>();
    }
}
