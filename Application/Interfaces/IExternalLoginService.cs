using Application.Interfaces.ExternalAuth;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IExternalLoginService
    {
        Task<GoogleUserInfo> GetGoogleInfoasync(string accessToken);
        Task<FacebookUserInfoResult> GetFacebookInfoasync(string accessToken);

    }
}
