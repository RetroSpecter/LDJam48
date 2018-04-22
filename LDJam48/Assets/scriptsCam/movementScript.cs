using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementScript : MonoBehaviour {

    public List<keyBinding> keyBindings;
    public float sensitivity;
    public float speed;
    private Rigidbody rb;

    public bool active;

	// Use this for initialization
	void Start () {
        active = true;
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY;
        rb.constraints = RigidbodyConstraints.FreezeRotationY;
        armController.toggle += activate;
	}
	
	// Update is called once per frame
	void Update () {
        rb.velocity = Vector3.zero;
        if (!active)
        {
            return;
        }
		foreach(keyBinding kb in keyBindings)
        {
            if (Input.GetKey(kb.k))
            {
                transform.position += transform.right * kb.v.x * Time.deltaTime * speed;
                transform.position += transform.forward * kb.v.z * Time.deltaTime * speed;
            }
        }

        float newRotY = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
        transform.localEulerAngles = new Vector3(0f, newRotY, 0f);
        transform.position = new Vector3(transform.position.x, 0.45f, transform.position.z); //keeps you chained to proper height
        
        
    }
    public void activate(bool b)
    {
        active = b;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ingredDrop")
        {
            if (GetComponent<IngredientInv>().addIngredient(other.GetComponent<dropBehavior>().type))
            {
                Destroy(other.gameObject);
            }
            else
            {
                Debug.Log("inventory is full!!");
            }
        }
    }
}

    


    [System.Serializable]
    public class keyBinding
    {
        public KeyCode k;
        public Vector3 v;
    }

