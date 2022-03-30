﻿namespace ProfileService.Api
{
    public class Profile
    {
        public Guid OwnerId { get; set; }
        public string ProfilePictureBase64 { get; set; }
        public string Biography { get; set; }
        public List<Guid> FollowingUsers { get; set; }
        public List<Guid> BlockedUsers { get; set; }
    }
}