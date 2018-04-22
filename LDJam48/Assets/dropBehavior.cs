using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropBehavior : MonoBehaviour {

    private GameObject player;
    private float startY;
    private float timer;

    public string type;
    public float bobDistance;
    public float bobSpeed;

	// Use this for initialization
	void Start () {
        startY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime * bobSpeed;
        transform.position = new Vector3(transform.position.x, startY + bobDistance * Mathf.Sin(timer), transform.position.z);
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        transform.LookAt(player.transform, Vector3.up);
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
    }
}
