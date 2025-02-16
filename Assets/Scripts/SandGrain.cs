using UnityEngine;

public class SandGrain : MonoBehaviour
{
    private bool isFrozen = false;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;

    // Reference to the AvailableColors component
    public AvailableColors availableColors;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (availableColors == null)
        {
            Debug.LogError("AvailableColors reference is not assigned in the Inspector.");
            return;
        }

        // Check if there is at least one color in the finalColors list
        if (availableColors.finalColors != null && availableColors.finalColors.Count > 0)
        {
            // Assign the first color from AvailableColors to the SandGrain's SpriteRenderer
            spriteRenderer.color = availableColors.finalColors[0];
            Debug.Log($"SandGrain color set to: {availableColors.finalColors[0]}");
        }
        else
        {
            Debug.LogWarning("No colors available in AvailableColors.finalColors.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFrozen)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Freeze();
                isFrozen = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Destroy(gameObject);
        }
    }

    private void Freeze()
    {
        rigidBody.bodyType = RigidbodyType2D.Static;
    }
}
