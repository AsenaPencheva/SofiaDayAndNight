using System;

namespace SofiaDayAndNight.Web.Models.Notification
{
    public class FriendRequest
    {
        public int Id { get; set; }

        public string SenderName { get; set; }

        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public string SenderImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsFriendship { get; set; }
    }
}