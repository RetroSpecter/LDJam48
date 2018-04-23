using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camScript : MonoBehaviour {

    public GameObject player;
    public List<mobType> types;



    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        transform.SetPositionAndRotation(player.transform.position, player.transform.rotation);
        


    }

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
        public string walkAnimation;
        public string damageAnimation;
        public string deathAnimation;
    }

    public enum logicType
    {
        BRUTE,
        SHOOTER,
        AURA
    }
}
