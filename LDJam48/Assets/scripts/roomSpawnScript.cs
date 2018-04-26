using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomSpawnScript : MonoBehaviour {

    private List<GameObject> mobs;
    private int difficulty;

    public GameObject mobFab;

	// Use this for initialization
	void Start () {
        setDifficulty();
        Invoke("populate", .2f);
	}

    private void setDifficulty()
    {
        difficulty = mapController.peers.Count - 1;
    }

    private void populate()
    {
        for(int i = 0; i < difficulty; i++)
        {
            GameObject newMob = GameObject.Instantiate(mobFab, generateRandomPosition(5), new Quaternion(), transform);
            mobManager.mobType mob = FindObjectOfType<mobManager>().types[Random.Range(0, FindObjectOfType<mobManager>().types.Count)];
            newMob.GetComponent<mobScript>().birth(mob);
        }
    }

    private Vector3 generateRandomPosition(float within)
    {
        float x = Random.Range(-within, within);
        float z = Random.Range(-within, within);
        return new Vector3(transform.position.x + x, transform.position.y + .1f, transform.position.z + z);
    }
	
    public bool isCleared()
    {
        return mobs.Count == 0;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
