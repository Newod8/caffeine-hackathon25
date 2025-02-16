using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using OpenCvSharp;
using UnityEngine.UI;

public class AvailableColors : MonoBehaviour
{
    public List<Color> finalColors = new List<Color>();
    private string filePath;
    public StringBuilder sb = new StringBuilder();
    public int numberOfColors = 5; // Number of dominant colors to extract

    // Reference to a UI Container to display colors
    public Transform colorContainer;



    private void Start()
    {
        // Optionally, initialize here or through another script
    }

    /// <summary>
    /// Sets the file path and triggers color extraction.
    /// </summary>
    /// <param name="path">Path to the image file.</param>
    public void SetFilePath(string path)
    {
        this.filePath = path;
        GetColors();
    }


    /// <summary>
    /// Loads the image file and extracts dominant colors using OpenCvSharp.
    /// </summary>
    public void GetColors()
    {
        if (string.IsNullOrEmpty(this.filePath))
        {
            Debug.LogError("File path is not set.");
            return;
        }

        // Ensure the file exists before loading the image
        if (File.Exists(this.filePath))
        {
            // Load image using OpenCvSharp
            Mat image = Cv2.ImRead(this.filePath);
            if (image.Empty())
            {
                Debug.LogError("Failed to load image data.");
                return;
            }

            // Perform color quantization and retrieve dominant colors
            finalColors = ColorQuantization(image, numberOfColors);

            // Display the dominant colors in the UI
            //DisplayColors();
            gameObject.GetComponent<ColorPickerGenerator>().GenerateColors(finalColors.ToArray());

            // Log the extracted colors
            Debug.Log($"Extracted {finalColors.Count} dominant colors from the image.");
            for (int i = 0; i < finalColors.Count; i++)
            {
                Debug.Log($"Color {i + 1}: {finalColors[i]}");
            }
        }
        else
        {
            Debug.LogError("File not found: " + this.filePath);
        }
    }

    /// <summary>
    /// Performs color quantization using K-Means clustering and returns the dominant colors.
    /// </summary>
    /// <param name="image">The input image as a Mat.</param>
    /// <param name="K">Number of color clusters.</param>
    /// <returns>List of dominant colors as UnityEngine.Color.</returns>
    private List<Color> ColorQuantization(Mat image, int K)
    {
        // Flatten the image to a 2D array (pixels x channels)
        Mat reshaped = image.Reshape(1, image.Rows * image.Cols).Clone();
        reshaped.ConvertTo(reshaped, MatType.CV_32F);

        // Define termination criteria for K-Means
        TermCriteria criteria = new TermCriteria(CriteriaTypes.Eps | CriteriaTypes.MaxIter, 100, 0.2);

        // Apply K-Means clustering
        Mat labels = new Mat();
        Mat centers = new Mat();
        Cv2.Kmeans(reshaped, K, labels, criteria, 10, KMeansFlags.PpCenters, centers);

        // Normalize the centers to [0,1] for Unity's Color
        centers.ConvertTo(centers, MatType.CV_32F, 1.0 / 255.0);

        List<Color> dominantColors = new List<Color>();

        for (int i = 0; i < K; i++)
        {
            float b = centers.At<float>(i, 0); // Blue
            float g = centers.At<float>(i, 1); // Green
            float r = centers.At<float>(i, 2); // Red

            // Convert BGR to RGB and create Unity Color
            Color unityColor = new Color(r , g, b, 1f);
            dominantColors.Add(unityColor);
            sb.AppendLine(unityColor.ToString());
        }
        
        return dominantColors;
    }

    /// <summary>
    /// Displays the extracted colors in the UI.
    /// </summary>
    //private void DisplayColors()
    //{

    //    // Clear existing color displays
    //    foreach (Transform child in colorContainer)
    //    {
    //        Destroy(child.gameObject);
    //    }

    //    // Instantiate color prefabs and set their colors
    //    foreach (Color color in finalColors)
    //    {
    //        Image img = colorObj.GetComponent<Image>();
    //        if (img != null)
    //        {
    //            img.color = color;
    //        }
    //        else
    //        {
    //            Debug.LogError("Color Prefab does not have an Image component.");
    //        }
    //    }
    //}
}
