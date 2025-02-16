using System.Linq;
using UnityEngine;

public class ColorPickerGenerator : MonoBehaviour
{
    public GameObject colorPicker;
    private GameObject[] currentColorPickers = new GameObject[5];
    private Color[] colorOptions = new Color[5];
    private int limit = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        colorOptions[0] = Color.cyan;
        colorOptions[1] = Color.red;
        colorOptions[2] = Color.blue;
        colorOptions[3] = Color.green;
        colorOptions[4] = Color.gray;
        for(int i = 0; i < 5; i++)
        {
            GameObject newColorPicker = Instantiate(colorPicker, new Vector3(-9.5f + (i * 1.5f), 4.2f, 4), Quaternion.identity);
            newColorPicker.GetComponent<SpriteRenderer>().color = colorOptions[i];
            currentColorPickers[i] = newColorPicker;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ResetColorPickers();
        }
    }

    public void GenerateColors(Color[] newColors)
    {
        ResetColorPickers();
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
            currentColorPickers[i].GetComponent<SpriteRenderer>().color = newColors[i];
            currentColorPickers[i].SetActive(true);
        }
    }

    private void ResetColorPickers()
    {
        print("Resetting Color Pickers");
        for (int i = 0; i < limit; i++)
        {
            currentColorPickers[i].SetActive(false);
        }
    }
}
