using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golman4 : MonoBehaviour
{
    public float speed = 2.5f; // Brzina skoka
    public float xPosition = -17.78f;
    public float yBoundaryMin = 0.49f; // Promenjeno sa 0.5f
    public float yBoundaryMax = 1.5f;
    public float zPosition = 0.64f;
    public float secondsDelay = 1.0f; // Vreme zastoja nakon skoka

    private bool isMovingUp = true;


    void Start()
    {
        // Pokreće skok objekta
        StartCoroutine(PerformJump());
    }


    void Update()
    {
       
    }

    IEnumerator PerformJump()
    {
        while (true)
        {
            // Određuje smer skoka na temelju trenutnog stanja
            int direction = isMovingUp ? 1 : -1;

            // Računa vertikalno kretanje na temelju brzine i delta vremena
            float verticalMovement = speed * direction * Time.deltaTime;

            // Ažurira poziciju objekta po vertikalnoj osi
            float newPositionY = transform.position.y + verticalMovement;
            newPositionY = Mathf.Clamp(newPositionY, yBoundaryMin, yBoundaryMax);

            // Postavlja novu poziciju objekta
            transform.position = new Vector3(xPosition, newPositionY, zPosition);

            // Proverava i menja smer skoka ako je dosegnuta gornja/granična tačka
            if ((direction == 1 && newPositionY >= yBoundaryMax) || (direction == -1 && newPositionY <= yBoundaryMin))
            {
                isMovingUp = !isMovingUp;
                yield return new WaitForSeconds(secondsDelay);
            }

            yield return null;
        }
    }
}
