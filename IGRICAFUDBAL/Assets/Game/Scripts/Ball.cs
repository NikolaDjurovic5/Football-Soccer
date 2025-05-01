using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Transform transformPlayer; // Veza(Oznaka) za transformaciju igraca
    private bool stickToPlayer; // Proverava da li je lopta vezana za igraca
    public Transform playerBallPosition; // Pozicija igraca gde se lopta vezuje
    private Transform playerlocation; // Transformacija igraca
    private Player igrac; // Veza(Oznaka) za igraca
    public int brojac = 0; // Brojac za pracenje broja povratka lopte na pocetnu poziciju
    float speed; // Brzina rotacije lopte
    Vector3 previousLocation; // Prethodna lokacija lopte
    Player scriptPlayer; // Skripta igrača

    public bool StickToPlayer { get => stickToPlayer; set => stickToPlayer = value; }
    // Ova svojstva omogucavaju pristup i postavljanje vrednosti privatne promenljive stickToPlayer

    void Start()
    {
        playerBallPosition = transformPlayer.Find("Geometry").Find("BallLocation"); // Pronalazi poziciju lopte kod igraca
        playerlocation = transformPlayer; // Postavlja transformaciju igraca
        scriptPlayer = transformPlayer.GetComponent<Player>(); // Uzimanje komponente "Player" iz objekta "transformPlayer" i daje vezu te komponente promenljivoj "scriptPlayer".
    }

    void Update()
    {
        // Ako lopta nije kod igrača
        if (!StickToPlayer)
        {
            // Uvodimo novu vrednost koja ima vrednost razdaljine između igraca i lopte
            float distanceToPlayer = Vector3.Distance(transformPlayer.position, transform.position);
           
            // Ako je distanca između igraca i lopte manja od 0.5, lopta se veže za igrača
            if (distanceToPlayer < 0.5)
            {
                StickToPlayer = true;
                scriptPlayer.BallAttachedToPlayer = this;
            }
        }
        else
        {
            Vector2 currentLocation = new Vector2(transform.position.x, transform.position.z); // Trenutna lokacija lopte
            speed = Vector2.Distance(currentLocation, previousLocation) / Time.deltaTime; // Brzina rotacije lopte
            transform.position = playerBallPosition.position; // Postavlja poziciju lopte na poziciju igraca
            // Lopta se rotira u pravcu desno od igrača po x osi
            transform.Rotate(new Vector3(transformPlayer.right.x, 0, transformPlayer.right.z), speed, Space.World);
            previousLocation = currentLocation; // Postavlja trenutnu lokaciju kao prethodnu lokaciju
        }

        // Ako lopta padne ispod terena
        if (transform.position.y < -1)
        {
            // Nova pozicija lopte
            transform.position = new Vector3(15.11f, 0.7f, 0.01f);
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            // Zaustavlja kretanje lopte
            rigidbody.velocity = Vector3.zero;
            // Zaustavlja rotaciju lopte
            rigidbody.angularVelocity = Vector3.zero;
            brojac += 1; // brojac se inkrementira
        }

        // Granica cunjeva
        else if (transform.position.x < -11f && transform.position.x > -12f)
        {
            if (transform.position.z > -5f && transform.position.z < -1f)
            {
                transform.position = new Vector3(15.11f, 0.7f, 0.01f);
                Rigidbody rigidbody = GetComponent<Rigidbody>();
                rigidbody.velocity = Vector3.zero;    // brzina kretanja
                rigidbody.angularVelocity = Vector3.zero; // brzina rotacija
                stickToPlayer = false; //odvajanje lopte od igraca
                brojac += 1; // inkrementiranje brojaca
            }
        }
        // Granica čunjeva
        else if (transform.position.x < -2.0f && transform.position.x > -3.0f)
        {
            if (transform.position.z > -5.7f && transform.position.z < -1.81f)
            {
                transform.position = new Vector3(15.11f, 0.7f, 0.01f);
                Rigidbody rigidbody = GetComponent<Rigidbody>();
                rigidbody.velocity = Vector3.zero;
                rigidbody.angularVelocity = Vector3.zero;
                stickToPlayer = false;
                // Koliko puta se lopta vraća na početnu poziciju
                brojac += 1;
            }
        }
        // Granica čunjeva
        else if (transform.position.x < -6.0f && transform.position.x > -7.0f)
        {
            if (transform.position.z > 1f && transform.position.z < 5f)
            {
                transform.position = new Vector3(15.11f, 0.7f, 0.01f);
                Rigidbody rigidbody = GetComponent<Rigidbody>();
                rigidbody.velocity = Vector3.zero;
                rigidbody.angularVelocity = Vector3.zero;
                stickToPlayer = false;
                brojac += 1;
            }
        }
        // Granica čunjeva
        else if (transform.position.x < 2.5f && transform.position.x > 1.5f)
        {
            if (transform.position.z > -2.5f && transform.position.z < 1.5f)
            {
                transform.position = new Vector3(15.11f, 0.7f, 0.01f);
                Rigidbody rigidbody = GetComponent<Rigidbody>();
                rigidbody.velocity = Vector3.zero;
                rigidbody.angularVelocity = Vector3.zero;
                stickToPlayer = false;
                brojac += 1;
            }
        }
        // Granica čunjeva
        else if (transform.position.x < -0.5f && transform.position.x > -1.5f)
        {
            if (transform.position.z > 3.4f && transform.position.z < 8f)
            {
                transform.position = new Vector3(15.11f, 0.7f, 0.01f);
                Rigidbody rigidbody = GetComponent<Rigidbody>();
                rigidbody.velocity = Vector3.zero;
                rigidbody.angularVelocity = Vector3.zero;
                stickToPlayer = false;
                brojac += 1;
            }
        }
    }
}
