using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SampleMvcApp.Service
{
    public class HttpResponseWrapper<T>
    {
        public T Model { get; set; }
        public bool Error { get; set; }
        public HttpResponseMessage Httpresponsemessage { get; set; }

        public HttpResponseWrapper(T model, bool error, HttpResponseMessage httpResponseMessage)
        {
            Model=model;
            Error=error;
            Httpresponsemessage=httpResponseMessage;
        }

        public async Task<string> GetErrorMessage()
        {
            if (Error)
            {
                return null;
            }

            var statuscode =Httpresponsemessage!.StatusCode;
            if (statuscode == HttpStatusCode.NotFound)
            {
                return "Elemento no encontrado";
            }
            else if (statuscode == HttpStatusCode.Forbidden)
            {
                return "No estas autorizado";
            }
            else if (statuscode == HttpStatusCode.Unauthorized)
            {
                return "Tienes que iniciar sesion en el sistema";
            }
            else if (statuscode == HttpStatusCode.BadGateway)
            {
                return await Httpresponsemessage.Content.ReadAsStringAsync();
            }

            return await Httpresponsemessage.Content.ReadAsStringAsync();
        }
    }
}
