using System.Text.Json.Serialization;
using System.Text.Json;
using Entrance.Shared.Wrapper;

namespace Entrance.Services.Extensions;
public static class ResponseExtension
{
    public static async Task<ResponseWrapper<T>> ToResponse<T>(this HttpResponseMessage responseMessage)
    {
        var responseAsString = await responseMessage.Content.ReadAsStringAsync();

        if (string.IsNullOrWhiteSpace(responseAsString))
        {
            return new ResponseWrapper<T>().Failed("Empty response received from the server.");
        }

        if (responseMessage.IsSuccessStatusCode)
        {
            try
            {
                var responseObject = JsonSerializer.Deserialize<ResponseWrapper<T>>(responseAsString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReferenceHandler = ReferenceHandler.Preserve
                });

                return responseObject ?? new ResponseWrapper<T>().Failed("Deserialization resulted in null.");
            }
            catch (JsonException ex)
            {
                return new ResponseWrapper<T>().Failed($"Invalid response format. {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }
        else
        {
            var response = JsonSerializer.Deserialize<ResponseWrapper<T>>(responseAsString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var errorMessage = response?.Messages.FirstOrDefault() ?? "An unknown error occurred.";

            return new ResponseWrapper<T>().Failed(errorMessage);
        }
    }
}
