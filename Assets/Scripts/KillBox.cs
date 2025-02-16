using UnityEngine;

public class KillBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SaltGrain")) // Check if it's a salt grain entering the killbox
        {
            // Destroy the salt grain immediately (you could also return it to a pool here)
            Destroy(collision.gameObject); // Removes the salt grain
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
