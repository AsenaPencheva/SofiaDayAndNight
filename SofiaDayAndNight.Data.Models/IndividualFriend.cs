namespace SofiaDayAndNight.Data.Models
{
    public class IndividualFriend
    {
        //public UserFriend()
        //{
        //}

        public IndividualFriend(int individualId, int friendId) // string friendIdentityId,
        {
            this.IndividualId = individualId;
            //this.FriendIdentityId = friendIdentityId;
            this.FriendId = friendId;
        }

        public int Id { get; set; }

        public Privacy Privacy { get; set; }

        public int IndividualId { get; set; }

        //public string FriendIdentityId { get; set; }

        public int FriendId { get; set; }
    }
}
