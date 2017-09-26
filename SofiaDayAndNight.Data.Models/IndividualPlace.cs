namespace SofiaDayAndNight.Data.Models
{
    public  class IndividualPlace
    {
        //public UserFriend()
        //{
        //}

        public IndividualPlace(int individualId, int placeId) // string friendIdentityId,
        {
            this.IndividualId = individualId;
            //this.FriendIdentityId = friendIdentityId;
            this.PlaceId = placeId;
        }

        public int Id { get; set; }

        public Privacy Privacy { get; set; }

        public int IndividualId { get; set; }

        //public string FriendIdentityId { get; set; }

        public int PlaceId { get; set; }
    }
}
