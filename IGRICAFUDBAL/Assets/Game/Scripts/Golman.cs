using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golman : MonoBehaviour
{
    private Animator animator; // Veza za Animator komponentu

    public float speed = 100.0f; // Brzina kretanja golmana
    public float xPosition = -27.0f; // Pocetna X pozicija golmana
    public float zBoundaryMin = -2.0f; // Minimalna granica kretanja golmana po Z osi
    public float zBoundaryMax = 2.0f; // Maksimalna granica kretanja golmana po Z osi
    public float yPosition = 0.52f; // Pozicija golmana po Y osi
    public const int ANIMATION_LAYER_SHOOT = 1; // Konstanta koja predstavlja animaciju za sutiranje

    void Start()
    {
        animator = GetComponent<Animator>(); // Uzima veze za Animator komponentu
    }

    void Update()
    {
        float verticalMovement = Time.deltaTime * speed; // Vertikalni pomeraj golmana po Z osi za trenutni frejm
        float newPositionZ = transform.position.z + verticalMovement; // Nova pozicija golmana po Z osi

        transform.position = new Vector3(xPosition, yPosition, newPositionZ); // Postavlja novu poziciju golmana

        // Proverava da li je golman stigao do granice gola, ako jeste, menja smer kretanja u drugu stranu
        if (newPositionZ <= zBoundaryMin || newPositionZ >= zBoundaryMax)
        {
            speed = -speed; // Menja smer kretanja golmana
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Odbijanje lopte
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>(); // Veza za Rigidbody komponentu lopte
            if (ballRigidbody != null)
            {
                Vector3 reflection = Vector3.Reflect(ballRigidbody.velocity, Vector3.right); // Izracunava refleksiju lopte
                ballRigidbody.velocity = reflection; // Postavlja reflektovanu brzinu lopte
            }
        }
    }
}
