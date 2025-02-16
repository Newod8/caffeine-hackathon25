using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

public class ImageGenerationScript : MonoBehaviour
{
    // Retrieve the OpenAI API key from environment variables
    private static readonly string? apiKey = Environment.GetEnvironmentVariable("OPEN_AI_KEY");
    // Define the API endpoint URL for image generation
    private static readonly string apiUrl = "https://api.openai.com/v1/images/generations";

    public string filePath;

    // Reference to the AvailableColors component
    //public AvailableColors availableColors;

    // Reference to the Image component in the scene
    [SerializeField]
    private Image displayImage;

    private void Start()
    {
        
        this.filePath = "C:\\Users\\river\\OneDrive\\Desktop\\Hackathon25 Project\\caffeine-hackathon25\\Assets\\GeneratedImages\\image_20250215_231140.png";
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(File.ReadAllBytes(this.filePath));

        //  Convert Texture2D to Sprite and assign to Image component
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        this.displayImage.sprite = sprite;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;

        gameObject.GetComponent<AvailableColors>().SetFilePath(this.filePath);

        gameObject.GetComponent<AvailableColors>().GetColors();
    }

    //private async void Start()
    //{
    //    //if (availableColors == null)
    //    //{
    //    //    Debug.LogError("AvailableColors reference is not assigned in the Inspector.");
    //    //    return;
    //    //}

    //    var random = new System.Random();
    //    var color = String.Format("#{0:X6}", random.Next(0x1000000)); // = "#A197B9"

    //    List<string> colors = new List<string>();

    //    for (int i = 0; i < 3; i++)
    //    {
    //        color = String.Format("#{0:X6}", random.Next(0x1000000)); // = "#A197B9"
    //        colors.Add(color);
    //    }

    //    string prompt = "A 256x256 pixel image with exactly three solid colors (red, blue, and orange). Flat, minimalist style with no shading, no gradients, and no outlines. The image should be composed of three large color blocks in simple geometric shapes, each block a single solid color with no texture or detail.";

    //    if (string.IsNullOrEmpty(prompt))
    //    {
    //        Debug.LogError("The prompt cannot be empty.");
    //        return;
    //    }

    //    // Call the method to generate the image
    //    await GenerateImage(prompt);
    //}

    ///// <summary>
    ///// Generates an image using OpenAI's API based on the provided prompt.
    ///// </summary>
    ///// <param name="prompt">The description for the image to be generated.</param>
    ///// <returns>A task representing the asynchronous operation.</returns>
    //public async Task GenerateImage(string prompt)
    //{
    //    // Create an instance of HttpClient
    //    using HttpClient client = new();
    //    // Set the authorization header with the API key
    //    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

    //    // Create the request body with prompt, number of images, and size
    //    var requestBody = new
    //    {
    //        prompt,
    //        n = 1,
    //        size = "256x256"
    //    };

    //    // Serialize the request body to JSON
    //    string jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
    //    // Create the HTTP content with the JSON body
    //    HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

    //    // Send a POST request to the OpenAI API
    //    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

    //    // Handle the API response
    //    if (response.IsSuccessStatusCode)
    //    {
    //        await HandleResponseAndDownload(response);
    //    }
    //    else
    //    {
    //        // Print the error if the request failed
    //        Debug.LogError($"Error: {response.StatusCode}\n{await response.Content.ReadAsStringAsync()}");
    //    }
    //}

    ///// <summary>
    ///// Handles the API response and downloads the image.
    ///// </summary>
    ///// <param name="response">The HTTP response from the API.</param>
    ///// <returns>A task representing the asynchronous operation.</returns>
    //private async Task HandleResponseAndDownload(HttpResponseMessage response)
    //{
    //    if (response.IsSuccessStatusCode)
    //    {
    //        // Read the response content as a string
    //        string result = await response.Content.ReadAsStringAsync();
    //        // Parse the JSON response
    //        JObject json = JObject.Parse(result);

    //        // Loop through each image in the response
    //        int imageCount = json["data"].Count();
    //        // Create the GeneratedImages folder if it doesn't exist
    //        string folderPath = Path.Combine(Application.dataPath, "GeneratedImages");
    //        if (!Directory.Exists(folderPath))
    //        {
    //            Directory.CreateDirectory(folderPath);
    //        }
    //        for (int i = 0; i < imageCount; i++)
    //        {
    //            string imageUrl = json["data"][i]["url"].ToString();
    //            // Generate a unique filename for each image
    //            string filename = $"image_{DateTime.Now:yyyyMMdd_HHmmss}.png";

    //            this.filePath = Path.Combine(folderPath, filename);

    //            Debug.Log($"Image URL: {imageUrl}");

    //            await DownloadImage(imageUrl, this.filePath);

    //            this.filePath = "C:\\Users\\river\\OneDrive\\Desktop\\Hackathon25 Project\\caffeine-hackathon25\\Assets\\GeneratedImages\\image_20250215_231140.png";

    //            Texture2D texture = new Texture2D(2, 2);
    //            texture.LoadImage(File.ReadAllBytes(this.filePath));

    //            // Convert Texture2D to Sprite and assign to Image component
    //            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

    //            this.displayImage.sprite = sprite;

    //            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
    //            spriteRenderer.sprite = sprite;

    //            // Now we get the colors from the image
    //            gameObject.GetComponent<AvailableColors>().SetFilePath(this.filePath);

    //            gameObject.GetComponent<AvailableColors>().GetColors();

    //            //Debug.Log($"Unique Colors found {availableColors.finalColors.Count}");

    //            Debug.Log($"Image downloaded as {this.filePath}");
    //        }
    //    }
    //    else
    //    {
    //        // Print the error if the request failed
    //        Debug.LogError($"Error: {response.StatusCode}\n{await response.Content.ReadAsStringAsync()}");
    //    }
    //}

    ///// <summary>
    ///// Downloads the image from the specified URL and saves it locally.
    ///// </summary>
    ///// <param name="imageUrl">The URL of the image to be downloaded.</param>
    ///// <param name="fileName">The local file path where the image will be saved.</param>
    ///// <returns>A task representing the asynchronous operation.</returns>
    //private static async Task DownloadImage(string imageUrl, string fileName)
    //{
    //    using HttpClient client = new();
    //    // Download the image as a byte array
    //    byte[] imageBytes = await client.GetByteArrayAsync(imageUrl);

    //    // Write the image bytes to a file
    //    await File.WriteAllBytesAsync(fileName, imageBytes);
    //}
}
