using System;

namespace Application.Interfaces.ExternalAuth
{
    public  class GoogleUserInfo
    {
        public string Iss { get; set; }

        public string Azp { get; set; }

        public string Aud { get; set; }

        public string Sub { get; set; }

        public string Email { get; set; }

        public bool EmailVerified { get; set; }

        public string AtHash { get; set; }

        public string Name { get; set; }

        public Uri Picture { get; set; }

        public string GivenName { get; set; }

        public string FamilyName { get; set; }

        public string Locale { get; set; }

        public long Iat { get; set; }

        public long Exp { get; set; }

        public string Jti { get; set; }

        public string Alg { get; set; }

        public string Kid { get; set; }

        public string Typ { get; set; }
    }
}
