using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SampleMvcApp.ViewModels;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SampleMvcApp.Service
{
    public class ProfileService : IProfileService
    {

        private readonly HttpClient httpClient;
        private readonly ITokenService TokenService;


        public ProfileService(HttpClient http, ITokenService tokenService)
        {
            httpClient = http;
            TokenService = tokenService;   
        }

        private JsonSerializerOptions _serialicer = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        public async Task<HttpResponseWrapper<object>> UpdateProfile(string url, UserProfile model)
        {
            try
            {
                await TokenHeader();

                if (model == null)
                    throw new ArgumentNullException(nameof(model), "El modelo recibido es nulo");

                var user_metadata = new
                {
                    user_metadata = new
                    {
                        tipodocumento = model.TipoDocumento,
                        numerodocumento = model.NumeroDocumento,
                        direccion = model.Direccion,
                        telefono = model.Telefono
                    }
                };

                var MessageBody = JsonSerializer.Serialize(user_metadata);
                var MessageContent = new StringContent(MessageBody, Encoding.UTF8, "application/json");
                var responsehttp = await httpClient.PatchAsync(url, MessageContent);
                var content = await responsehttp.Content.ReadAsStringAsync();

                return new HttpResponseWrapper<object>(content, !responsehttp.IsSuccessStatusCode, responsehttp);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al actualizar el perfil: {e.Message}");
                // Retornar un wrapper con error para evitar null
                return new HttpResponseWrapper<object>(
                    default,
                    true,
                    new HttpResponseMessage
                    {
                        StatusCode = System.Net.HttpStatusCode.InternalServerError,
                        Content = new StringContent($"Excepción: {e.Message}")
                    });
            }

        }


        private async Task TokenHeader()
        {
            try
            {
                var accesstoken=await TokenService.GetAccessToken();
                if (accesstoken == null)
                {
                    Console.WriteLine("No se ha podido obtener el token de oauth0");
                }

                Console.WriteLine(accesstoken);

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken.Trim('"'));


                

            }catch(Exception e)
            {
                Console.WriteLine("No se ha logrado agregar el token a el header");
            }
        }
    }
}
