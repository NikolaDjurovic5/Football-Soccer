using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private Player scriptPlayer; // Veza  za skriptu Player
    [SerializeField] private Ball lopta; // Veza za skriptu Ball

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Nacin za pozivanje drugog objekta kada se nalazi unutar granice kolajder-a ovog objekta
        if (other.gameObject.tag.Equals("Ball"))
        {
            if (name.Equals("GoalDecetcor"))
            {
                scriptPlayer.IncraseMyScore(); // Povecava broj mojih golova na skripti Player
            }
            else
            {
                scriptPlayer.IncraseOtherScore(); // Povecava broj golova od drugih na skripti Player
            }
        }
    }
}
