using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour {

    public AudioClip fightingMusic;
    public AudioClip cookingMusic;

    public void Start()
    {
        PlayerController.toggle += toggleMusic;
    }

    public void toggleMusic(bool b) {
        if (!b) {
            GetComponent<AudioSource>().clip = cookingMusic;
            GetComponent<AudioSource>().Play();
        }
        else {
            GetComponent<AudioSource>().clip = fightingMusic;
            GetComponent<AudioSource>().Play();
        }
    }

    public void OnDestroy()
    {
        PlayerController.toggle -= toggleMusic;
    }
}
