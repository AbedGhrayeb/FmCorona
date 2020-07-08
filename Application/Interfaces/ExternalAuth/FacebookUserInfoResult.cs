using System;
namespace Application.Interfaces.ExternalAuth
{
    public class FacebookUserInfoResult
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public FacebookPicture Picture { get; set; }

        public string Email { get; set; }

        public string Id { get; set; }
    }

    public class FacebookPicture
    {
        public FacebookPictureData Data { get; set; }
    }

    public class FacebookPictureData
    {
        public long Height { get; set; }

        public bool IsSilhouette { get; set; }

        public Uri Url { get; set; }

        public long Width { get; set; }
    }
}
