using NUnit.Framework;
using UnityEngine;

public class SaltShaker : MonoBehaviour
{
    // Members

    public float speed = 10f;
    public float deadzone = 1;
    public float smoothness = 100;
    public float spawnRate = 1;
    private Rigidbody2D rigidBody;
    private float spawnTimer = 0f;

    public GameObject sandGrain;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.transform.position = new Vector2(0, 0);
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
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

            if (spawnTimer > spawnRate)
            {
                spawnTimer = 0;
                Instantiate(sandGrain, rigidBody.position, Quaternion.identity);
            }
        }
    }
}
