using NUnit.Framework;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class SaltShaker : MonoBehaviour
{
    // Members

    public float speed = 10f;
    public float deadzone = 1;
    public float smoothness = 100;
    public float spawnRate = 1;
    public GameObject sandGrain;

    private Color sandColor;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private float spawnTimer = 0f;
    private bool isValid;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isValid = true;
        sandColor = Color.yellow;
        gameObject.transform.position = new Vector2(0, 0);
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 distance = mousePosition - rigidBody.position;
        if (distance.magnitude > deadzone)
        {
            Vector2 direction = (mousePosition - rigidBody.position).normalized;
            rigidBody.linearVelocity = direction * speed * (distance.magnitude / smoothness);
        }
        else
        {
            rigidBody.linearVelocity = Vector2.zero;
        }
     
        if (Input.GetMouseButton(0))
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer > spawnRate && isValid)
            {
                spawnTimer = 0;
                GameObject newSalt = Instantiate(sandGrain, rigidBody.position, Quaternion.identity);
                newSalt.GetComponent<SpriteRenderer>().color = (sandColor);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "DeadZone":
                isValid = false;
                break;
            case "ColorPicker":
                spriteRenderer.color = collision.GetComponent<SpriteRenderer>().color;
                this.sandColor = collision.GetComponent<SpriteRenderer>().color;
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("DeadZone"))
        {
            isValid = true;
        }
    }

    private void UpdateColor(string colorHex)
    {
        this.sandColor = HexToColor(colorHex);
    }
    private void UpdateColor(Color color)
    {
        this.sandColor = color;
    }

    private Color HexToColor(string hex)
    {
        if (hex.StartsWith("#"))
            hex = hex.Substring(1);

        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

        return new Color(r / 255f, g / 255f, b / 255f);
    }
}
