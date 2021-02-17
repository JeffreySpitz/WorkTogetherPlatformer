using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AmbienceTrigger : MonoBehaviour
{

    private AudioClip currentlyPlayingAmbience;
    public AudioClip altAmbience;

    private AmbienceManager ambienceManager;

    // Start is called before the first frame update
    void Start()
    {
        ambienceManager = FindObjectOfType<AmbienceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) { return; }

        currentlyPlayingAmbience = ambienceManager.currentlyPlayingAmbience;

        if(currentlyPlayingAmbience == altAmbience) { return; }

        
        ambienceManager.PlayAltAmbienceFromTrigger(altAmbience);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) { return; }
       

        ambienceManager.PlayDefaultFromTrigger();
    }
}
