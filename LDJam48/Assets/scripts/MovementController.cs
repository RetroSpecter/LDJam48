using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    // public List<keyBinding> keyBindings;
    public float sensitivity;
    private Rigidbody rb;


    public bool active;

	// Use this for initialization
	void Start () {
        active = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        PlayerController.toggle += Activate;
	}
	
	// Update is called once per frame
	void Update () {
        rb.velocity = Vector3.zero;
        if (active) {
            Movement();
        }
    }

    void Movement() {
        float speed = GetComponent<PlayerController>().playerStats.speed / 3;
        speed = Mathf.Clamp(speed, 2, 10);

        transform.position += transform.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        transform.position += transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * speed;

        float newRotY = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
        transform.localEulerAngles = new Vector3(0f, newRotY, 0f);
        transform.position = new Vector3(transform.position.x, 0.45f, transform.position.z); //keeps you chained to proper height   
    }

    public void Activate(bool b) {
        active = b;
    }

}

[System.Serializable]
public class keyBinding {
    public KeyCode k;
    public Vector3 v;
}

