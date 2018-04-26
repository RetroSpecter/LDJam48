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

    public float startVelocity = 3;
    Vector2 randDir;

    // Use this for initialization
    void Start () {
        randDir = Random.insideUnitCircle;
        startY = transform.position.y;
        Invoke("setBoxCollider", 0.01f);
        Destroy(this.gameObject, 10);
    }

    void setBoxCollider() {
        BoxCollider box = gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;
        box.isTrigger = true;
    }
    // Update is called once per frame
    void Update () {
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        transform.LookAt(player.transform, Vector3.up);
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

        if (Vector3.Distance(player.transform.position, this.transform.position) < 3) {
            transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * 7);
        } else {
            if (startVelocity > 0)
            {
                transform.Translate(startVelocity * new Vector3(randDir.x, 0, randDir.y));
                startVelocity -= Time.deltaTime;
            }
            else {
                timer += Time.deltaTime * bobSpeed;
                transform.position = new Vector3(transform.position.x, startY + bobDistance * Mathf.Sin(timer), transform.position.z);
            }
        }
    }
}
