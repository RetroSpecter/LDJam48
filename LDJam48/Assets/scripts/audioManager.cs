﻿using UnityEngine;
using System.Collections;

public class audioManager : MonoBehaviour {

    public AudioClip soundFX;
    public static audioManager instance;

    void Start() {
        instance = this;
    }

    public void Play(AudioClip sound) {
        Play(sound, 0.5f, 1);
    }

    public void Play(AudioClip sound, float volume) {
        Play(sound, volume, 1);
    }

    public void Play(AudioClip sound, float volume, float pitch) {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = sound;
        source.volume = volume;
        source.pitch = pitch;
        source.PlayOneShot(sound);
        Destroy(source, sound.length);
    }
}
