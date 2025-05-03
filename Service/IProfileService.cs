using SampleMvcApp.ViewModels;
using System.Threading.Tasks;

namespace SampleMvcApp.Service
{
    public interface IProfileService
    {
        Task<HttpResponseWrapper<object>> UpdateProfile(string url, UserProfile userprofile);
    }
}
