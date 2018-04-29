using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoardSprite : MonoBehaviour {

    private PlayerController player;

    // Use this for initialization
    void Start () {
        player = FindObjectOfType<PlayerController>();
    }
	
	// Update is called once per frame
	void LateUpdate () {
        transform.LookAt(player.transform, Vector3.up);
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
    }
}
