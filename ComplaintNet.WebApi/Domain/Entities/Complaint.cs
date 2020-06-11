using System;

namespace ComplaintNet.WebApi.Domain
{
    public class Complaint
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ComplaintStatus Status { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public DateTime? LastUpdatedOn { get; set; }
    }
}
