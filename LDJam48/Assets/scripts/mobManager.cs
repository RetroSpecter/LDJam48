using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobManager : MonoBehaviour {

    public GameObject player;
    public List<mobType> types;

    [System.Serializable]
    public struct mobType {
        public float speed;
        public string name;
        public float lockedYPos;
        public float maxHealth;
        public float visionRange;
        public float attackRange;
        public logicType logic;
        public RuntimeAnimatorController anim;
    }

    public enum logicType {
        BRUTE,
        SHOOTER,
        AURA
    }
}
