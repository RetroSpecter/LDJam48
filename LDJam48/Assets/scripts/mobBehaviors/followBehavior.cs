using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followBehavior : mobBehavior {
    private Rigidbody rb;
    bool canDamage;
    private PlayerController player;
    public bool active;
    private enemy type;

    public override void setup(mobScript ms) {
        rb = GetComponent<Rigidbody>();
        type = GetComponent<enemy>();
        player = FindObjectOfType<PlayerController>();
    }

    public override void Behavior() {
        Vector3 targetTransform = player.transform.position;
        targetTransform.y = this.transform.position.y;

        
        Ray r = new Ray(transform.position, targetTransform - transform.position);
        if (Vector3.Distance(transform.position, targetTransform) > type.attackRange) {
            canDamage = true;
            transform.position = Vector3.MoveTowards(transform.position, r.GetPoint(Vector3.Distance(transform.position, targetTransform) - type.attackRange), type.speed * Time.deltaTime);
        } else {
            if (canDamage) {
                canDamage = false;
                FindObjectOfType<PlayerController>().takeDamage(20);
                Invoke("toggleCanDamage", 10);
            }
        }      
        transform.position.Set(transform.position.x, type.lockedYPos, transform.position.z);    
    }
}
