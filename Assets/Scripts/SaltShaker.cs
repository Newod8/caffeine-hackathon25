using NUnit.Framework;
using System.Runtime.CompilerServices;
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
    private bool isValid;

    public GameObject sandGrain;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isValid = true;
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

            if (spawnTimer > spawnRate && isValid)
            {
                spawnTimer = 0;
                Instantiate(sandGrain, rigidBody.position, Quaternion.identity);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Play Area"))
        {
            isValid = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Play Area"))
        {
            isValid = true;
        }
    }
}
