using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.IO;

class Program
{
    static async Task Main()
    {
        var httpClient = new HttpClient();

        // Ändra till din API-url och port
        var url = " https://localhost:7020/api/event";

        using var form = new MultipartFormDataContent();

        // Lägg till textfält
        form.Add(new StringContent("Bad Hunny Tour"), "EventName");
        form.Add(new StringContent("BadHunny"), "ArtistName");
        form.Add(new StringContent("Bad Hunny on world tour"), "Description");
        form.Add(new StringContent("London"), "Location");
        form.Add(new StringContent("2026-07-05T19:00:00"), "StartDate");
        form.Add(new StringContent("2026-07-05T21:00:00"), "EndDate");
        form.Add(new StringContent("500"), "Price");

        // Lägg till bild (ändra sökväg till en existerande fil)
        var imagePath = "";

        if (File.Exists(imagePath))
        {
            var fileStream = File.OpenRead(imagePath);
            var imageContent = new StreamContent(fileStream);
            imageContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            form.Add(imageContent, "EventImage", Path.GetFileName(imagePath));
        }
        else
        {
            Console.WriteLine("Bildfilen hittades inte, skickar utan bild.");
        }

        try
        {
            var response = await httpClient.PostAsync(url, form);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Event skapades framgångsrikt!");
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
            }
            else
            {
                Console.WriteLine($"Fel vid skapande av event: {response.StatusCode}");
                var errorBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(errorBody);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception vid anrop: {ex.Message}");
        }
        Console.ReadKey();
    }
}