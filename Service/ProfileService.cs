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
        public async Task<HttpResponseWrapper<object>> UpdateProfile<T>(string url, T model)
        {
            try
            {

                await TokenHeader();

                var user_metadata = new
                {
                    usermetadata = new
                    {
                        tipodocumento = (model as UserProfile).TipoDocumento,
                        numerodocumento = (model as UserProfile).NumeroDocumento,
                        direccion = (model as UserProfile).Direccion,
                        telefono=(model as UserProfile).Telefono
                    }
                };

                var MessageBody = JsonSerializer.Serialize(user_metadata);
                var MessageContent = new StringContent(MessageBody, Encoding.UTF8, "application/json");
                var responsehttp = await httpClient.PatchAsync(url, MessageContent);
                var content = await responsehttp.Content.ReadAsStringAsync();
                return new HttpResponseWrapper<object>(content, !responsehttp.IsSuccessStatusCode, responsehttp);
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error al conseguir el token {e.Message}");
                return null;
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

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken.Trim('"'));


                

            }catch(Exception e)
            {
                Console.WriteLine("No se ha logrado agregar el token a el header");
            }
        }
    }
}
