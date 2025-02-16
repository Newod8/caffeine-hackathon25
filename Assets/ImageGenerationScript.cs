using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;

public class ImageGenerationScript : MonoBehaviour
{
    // Retrieve the OpenAI API key from environment variables
    private static readonly string? apiKey = Environment.GetEnvironmentVariable("OPEN_AI_KEY");
    // Define the API endpoint URL for image generation
    private static readonly string apiUrl = "https://api.openai.com/v1/images/generations";

    private async void Start()
    {

        var random = new System.Random();
        var color = String.Format("#{0:X6}", random.Next(0x1000000)); // = "#A197B9"

        List<string> colors = new List<string>();


        // Prompt the user to enter a description for the image
        //Console.Write("Enter a description for the image: ");

        for (int i = 0; i < 3; i++)
        {
            color = String.Format("#{0:X6}", random.Next(0x1000000)); // = "#A197B9"
            colors.Add(color);
        }



        // string? prompt = Console.ReadLine();

        //string prompt = $"Only 3 solid Colors in any shape Nothing else only 3 solid colors no other variations and NO SHADES.";
        //string prompt = "an isometric view of a miniature city, tilt shift, bokeh, voxel, vray render, high detail";
        string prompt = "A 256x256 pixel image with exactly three solid colors (red, blue, and orange). Flat, minimalist style with no shading, no gradients, and no outlines. The image should be composed of three large color blocks in simple geometric shapes, each block a single solid color with no texture or detail.";

        if (string.IsNullOrEmpty(prompt))
        {
            Console.WriteLine("Error: The prompt cannot be empty.");
            return;
        }
        // Call the method to generate the image
        await GenerateImage(prompt);
    }

    /// <summary>
    /// Generates an image using OpenAI's API based on the provided prompt.
    /// </summary>
    /// <param name="prompt">The description for the image to be generated.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task GenerateImage(string prompt)
    {
        // Create an instance of HttpClient
        using HttpClient client = new();
        // Set the authorization header with the API key
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

        // Create the request body with prompt, number of images, and size
        var requestBody = new
        {
            //model = "dall-e-3",
            prompt,
            n = 1,
            size = "256x256"
        };

        // Serialize the request body to JSON
        string jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
        // Create the HTTP content with the JSON body
        HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

        // Send a POST request to the OpenAI API
        HttpResponseMessage response = await client.PostAsync(apiUrl, content);


        // Handle the API response
        // Handle the API response
        if (response.IsSuccessStatusCode)
        {
            // Read and parse the response JSON

            // Read the response content as a string
            string result = await response.Content.ReadAsStringAsync();

            // Parse the JSON response
            JObject json = JObject.Parse(result);

            // Loop through each image in the response
            int imageCount = json["data"].Count();

            for (int i = 0; i < imageCount; i++)
            {
                string imageUrl = json["data"][i]["url"].ToString();

                // Generate a unique filename for each image
                string filename = $"image_{DateTime.Now:yyyyMMdd_HHmmss}_{i}.png";

                Console.WriteLine($"Image URL: {imageUrl}");

                string binPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
                await DownloadImage(imageUrl, binPath);
                Console.WriteLine($"Image downloaded as {binPath}");
            }
        }
        else
        {
            // Print the error if the request failed
            Console.WriteLine($"Error: {response.StatusCode}\n{await response.Content.ReadAsStringAsync()}");
        }
    }

    /// <summary>
    /// Downloads the image from the specified URL and saves it locally.
    /// </summary>
    /// <param name="imageUrl">The URL of the image to be downloaded.</param>
    /// <param name="fileName">The local file path where the image will be saved.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private static async Task DownloadImage(string imageUrl, string fileName)
    {
        using HttpClient client = new();
        // Download the image as a byte array
        byte[] imageBytes = await client.GetByteArrayAsync(imageUrl);
        // Write the image bytes to a file
        await File.WriteAllBytesAsync(fileName, imageBytes);
    }
}
