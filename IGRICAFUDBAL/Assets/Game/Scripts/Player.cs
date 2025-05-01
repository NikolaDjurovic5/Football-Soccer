using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform transformPlayer; // Transformacija igraca
    [SerializeField] private TextMeshProUGUI textScore; // Tekstualni prikaz rezultata
    [SerializeField] private TextMeshProUGUI textGoal; // Tekstualni prikaz golova
    [SerializeField] private Ball lopta; // Veza za loptu
    private StarterAssetsInputs starterAssetsInputs; // Veza za unos
    private Animator animator; // Animator komponenta
    private Transform playerlocation; // Transformacija igraca
    private Ball ballAttachedToPlayer; // Lopta koja je povezana sa igracem
    private float timeShot = -1f; // Vreme poslednjeg suta
    public const int ANIMATION_LAYER_SHOOT = 1; // Indeks sloja za animaciju suta
    private int myScore, otherScore; // Broj golova za igraca i protivnika
    private float goalTextColorAlpha; // Vrednost boje teksta za gol

    public Ball BallAttachedToPlayer { get => ballAttachedToPlayer; set => ballAttachedToPlayer = value; }

    void Start()
    {
        playerlocation = transformPlayer;
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (starterAssetsInputs.shoot)
        {
            starterAssetsInputs.shoot = false;
            timeShot = Time.time;
            animator.Play("Shoot", ANIMATION_LAYER_SHOOT, 0f); // Pokreni animaciju šuta
            animator.SetLayerWeight(ANIMATION_LAYER_SHOOT, 1f); // Postavi težinu sloja animacije šuta na maksimalnu
        }

        if (timeShot > 0)
        {
            if (ballAttachedToPlayer != null && Time.time - timeShot > 0.2)
            {
                ballAttachedToPlayer.StickToPlayer = false; // Lopta se oslobađa od igrača
                Rigidbody rigidbody = ballAttachedToPlayer.transform.gameObject.GetComponent<Rigidbody>();
                Vector3 shootdirection = transform.forward; // Smer šuta je napred u odnosu na objekat
                shootdirection.y += 0.25f; // Podiže loptu za 0.25 po Y osi pri šutu
                rigidbody.AddForce(shootdirection * 20f, ForceMode.Impulse); // Dodaj intezitet suta lopti
                ballAttachedToPlayer = null; // Lopta više nije povezana sa igračem
            }

            if (Time.time - timeShot > 0.5)
            {
                timeShot = -1f; // Vraca vrednost pocetnu za vreme poslednjeg suta
            }
        }
        else
        {
            animator.SetLayerWeight(ANIMATION_LAYER_SHOOT, Mathf.Lerp(animator.GetLayerWeight(ANIMATION_LAYER_SHOOT), 0f, Time.deltaTime * 10f)); // Postepeno smanjuje intenzitet animacije šuta ka 0
        }

        if (goalTextColorAlpha > 0)
        {
            goalTextColorAlpha -= Time.deltaTime; // Smanjiuje vrednost kanala boje teksta
            textGoal.alpha = goalTextColorAlpha; // Postavlja vrednost boje teksta za gol
            textGoal.fontSize = 160 - (goalTextColorAlpha * 1 - 0); // Promeni velicinu teksta na osnovu vrednosti
        }

        if (lopta.brojac >= 1)
        {
            transform.position = new Vector3(18f, 2f, -0.9f); // Vrati igraca na početnu poziciju nakon gola
            StartCoroutine(ResetBrojacAfterDelay()); // Pokrece funkciju za izrsavanje nakon odredjenog vremena(Pauza između gola i vraćanja na početnu poziciju)
        }
    }

    IEnumerator ResetBrojacAfterDelay()
    {
        yield return new WaitForSeconds(0.1f); // Pauza izmedju gola i vracanja na pocetnu poziciju
        lopta.brojac = 0; // Resetovanje brojaca lopte na nulu
    }

    public void IncraseMyScore()
    {
        myScore++; // Povecavanje broja golova za igraca
        UpdateScore(); // Azuriraranje prikaza rezultata
    }

    public void IncraseOtherScore()
    {
        otherScore++; 
        UpdateScore(); 
    }

    public void UpdateScore()
    {
        textScore.text = "Rezultat: " + myScore.ToString() + "-" + otherScore.ToString(); // Prikaz rezultata u tekstualnom formatu
        goalTextColorAlpha = 1f; // Postavlja alfa vrednosti boje teksta za gol na maksimalnu vrednost
    }
}
