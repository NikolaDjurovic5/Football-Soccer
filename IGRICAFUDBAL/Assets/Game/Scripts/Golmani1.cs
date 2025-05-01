using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golmani1 : MonoBehaviour
{
    private Animator animator;

    public float speed = 100.0f;
    public float xPosition = -27.0f;
    public float zBoundaryMin = -2.0f;
    public float zBoundaryMax = 2.0f;
    public float yPosition = 0.52f;
    public const int ANIMATION_LAYER_SHOOT = 1;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

  
    void Update()
    {


        float verticalMovement = Time.deltaTime * speed;
        float newPositionZ = transform.position.z + verticalMovement;
        newPositionZ = Mathf.Clamp(newPositionZ, zBoundaryMin, zBoundaryMax);

        transform.position = new Vector3(xPosition, yPosition, newPositionZ);

        if (newPositionZ <= zBoundaryMin || newPositionZ >= zBoundaryMax)
        {
            speed = -speed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Odbijanje lopte
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                Vector3 reflection = Vector3.Reflect(ballRigidbody.velocity, Vector3.right);
                ballRigidbody.velocity = reflection;
            }
        }
    }
}
