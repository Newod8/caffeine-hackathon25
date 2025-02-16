using System.Linq;
using UnityEngine;

public class ColorPickerGenerator : MonoBehaviour
{
    public GameObject colorPicker;
    private GameObject[] currentColorPickers = new GameObject[5];
    private Color[] colorOptions = new Color[5];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        colorOptions[0] = Color.cyan;
        colorOptions[1] = Color.red;
        colorOptions[2] = Color.blue;
        colorOptions[3] = Color.green;
        colorOptions[4] = Color.gray;
        GenerateColors();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateColors()
    {
        ResetColorPickers();

        for (int i = 0; i < 5; i++)
        {
            GameObject newColorPicker = Instantiate(colorPicker, new Vector3(-9.5f + (i * 1.5f), 4.2f, 4), Quaternion.identity);
            newColorPicker.GetComponent<SpriteRenderer>().color = colorOptions[i];
            currentColorPickers.Append(newColorPicker);
        }
    }

    public void GenerateColors(Color[] newColors)
    {
        ResetColorPickers();
        int limit;
        if (newColors.Length < 5)
        {
            limit = newColors.Length;
        }
        else
        {
            limit = 5;
        }

        for (int i = 0; i < limit; i++)
        {
            GameObject newColorPicker = Instantiate(colorPicker, new Vector3(-9.5f + (i * 1.5f), 4.2f, 4), Quaternion.identity);
            newColorPicker.GetComponent<SpriteRenderer>().color = newColors[i];
            currentColorPickers.Append(newColorPicker);
        }
    }

    private void ResetColorPickers()
    {
        for (int i = 0; i < currentColorPickers.Length; i++)
        {
            if (currentColorPickers[i] != null)
            {
                Destroy(currentColorPickers[i]);
            }
        }
        currentColorPickers = new GameObject[5];
    }
}
