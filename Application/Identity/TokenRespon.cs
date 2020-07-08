namespace Application.Identity
{
    public class TokenResponse
    {

        public TokenResponse(string accessToken,UserDto user)
        {
            AccessToken = accessToken;
            User = user;
        }

        public string AccessToken { get; set; }

        public UserDto User { get; set; } 

    }
}
