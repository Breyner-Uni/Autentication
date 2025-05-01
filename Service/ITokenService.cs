using System.Threading.Tasks;

namespace SampleMvcApp.Service
{
    public interface ITokenService
    {
        Task<string> GetAccessToken();
    }
}
