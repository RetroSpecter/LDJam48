using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropBehavior : MonoBehaviour {

    private PlayerController player;
    private float startY;
    private float timer;

    public string type;
    public float bobDistance;
    public float bobSpeed;

    public float startVelocity = 3;
    Vector2 randDir;

    void Start () {
        randDir = Random.insideUnitCircle;
        startY = transform.position.y;
        player = FindObjectOfType<PlayerController>();
        Destroy(this.gameObject, 10);
    }

    void setBoxCollider() {
        BoxCollider box = gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;
        box.isTrigger = true;
    }

    
    void Update () {
        if (startVelocity > 0) {
            transform.Translate(startVelocity * new Vector3(randDir.x, 0, randDir.y));
            startVelocity -= Time.deltaTime;
        } else {
            timer += Time.deltaTime * bobSpeed;
            transform.position = new Vector3(transform.position.x, startY + bobDistance * Mathf.Sin(timer), transform.position.z);
            if (GetComponent<BoxCollider>() == null) {
                setBoxCollider();
            }
        }
    }
    
}
