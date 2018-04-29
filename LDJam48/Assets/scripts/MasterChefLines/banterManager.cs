using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class banterManager : MonoBehaviour {

    public static banterManager instance;
    public banterType[] banterTypes;
    private AudioSource source;

    private void Awake() {
        instance = this;
        source = GetComponent<AudioSource>();
    }

    // this is a bit sloppy. probably better to switch to a dictionary system
    banterType getType(string type) {
        for (int i = 0; i < banterTypes.Length; i++) {
            if (banterTypes[i].name == type) {
                return banterTypes[i];
            }
        }
        print("doesnt exits");
        return banterTypes[0];
    }

    public void activateBanter(string type) {
        banterType banter = getType(type);

        if (Random.Range(0, 100) < banter.banterChance) {
            source.clip = (banter.dialogueOptions[Random.Range(0,banter.dialogueOptions.Length)]);
            source.Play();
        }
    }
}

[System.Serializable]
public struct banterType {
    public string name;
    public AudioClip[] dialogueOptions;
    [Range(0, 100)] public float banterChance; 
}