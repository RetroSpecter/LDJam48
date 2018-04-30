using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class spawnScript : MonoBehaviour {

    //private static Dictionary<string, camScript.mobType> types;
    private float spawnTimer;
    private GameObject player;
    public float spawnRate;
    public GameObject mobFab;
    public int maxGoons;
    public bool active;
    public static int enemyCount;

	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().enabled = false;
        active = true;
        enemyCount = 0;
        spawnTimer = spawnRate ;
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerController.toggle += activate;
    }

    public void activate(bool b)
    {
        active = b;
    }
	
	// Update is called once per frame
	void Update () {
        if (!active) {
            return;
        }
        spawnTimer -= Time.deltaTime;
        //Debug.Log(enemyCount);
        if(spawnTimer <= 0 && !inVision() && enemyCount < maxGoons && Vector3.Distance(transform.position, player.transform.position) < 20f) {
            spawnTimer = spawnRate;
            spawn();
        }
	}   

    private bool inVision()
    {
        Vector3 v = Camera.main.WorldToViewportPoint(transform.position);
        RaycastHit rch = new RaycastHit();
        if(Physics.Raycast(transform.position, player.transform.position - transform.position, out rch))
        {
            return (v.x < 1 && v.x > 0 && v.y > 0 && v.y < 1) && rch.collider.gameObject.tag == "Player";
        } else
        {
            return (v.x < 1 && v.x > 0 && v.y > 0 && v.y < 1);
        }
        
        
        
    }

    private void spawn()
    {
        enemyCount++;
        GameObject o = GameObject.Instantiate(mobFab);

        enemy mob = FindObjectOfType<mobManager>().types[Random.Range(0, FindObjectOfType<mobManager>().types.Length)];

        o.GetComponent<mobScript>().Spawn(mob);
        o.transform.position = transform.position;
    }
}
