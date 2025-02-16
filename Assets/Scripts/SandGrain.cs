using System.Data;
using UnityEngine;

public class SandGrain : MonoBehaviour
{
    private bool isFrozen = false;
    private Rigidbody2D rigidBody;
    private bool isInsideBox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFrozen)
        {
            if (Input.GetMouseButtonDown(1))
            {
                this.Freeze();
                this.isFrozen = true;
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
