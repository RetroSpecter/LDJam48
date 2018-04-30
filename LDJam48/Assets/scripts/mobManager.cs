using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobManager : MonoBehaviour {

    public GameObject player;
    public enemy[] types;

    public enum logicType {
        BRUTE,
        SHOOTER,
        AURA
    }
}
