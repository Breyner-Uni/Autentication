using System.Threading.Tasks;

namespace SampleMvcApp.Service
{
    public interface IProfileService
    {
        Task<HttpResponseWrapper<object>> UpdateProfile<T>(string url, T model);
    }
}
