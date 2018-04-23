﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobScript : MonoBehaviour {

    private GameObject player;
    private Animator anime;
    private string birthString;
    private camScript.mobType type;
    private static List<camScript.mobType> mobTypes;
    public Ingredient currentIngredient;
    private Rigidbody rb;
    private bool dying;

    private float health;

    public bool active;
    public GameObject dropFab;

    public bool canDamage;
    // Use this for initialization

    public void birth(camScript.mobType mob) {
        active = true;
        dying = false;
        anime = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (mobTypes == null) {
            mobTypes = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<camScript>().types;
        }
        if (mobTypes == null) {
            Debug.LogError("ERROR: attempted to spawn a mob before the types dictionary has been established!");
            GameObject.Destroy(this.gameObject.GetComponent<SpriteRenderer>());
            GameObject.Destroy(this.gameObject.GetComponent<BoxCollider>());
        }

        this.type = mob;
        anime.runtimeAnimatorController = mob.anim;

        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation; //if we get nullpointers here, it is because the names are wrong
        health = type.maxHealth;
        armController.toggle += activate;
        Invoke("setBoxCollider", 0.01f);
    }

    void setBoxCollider() {
        BoxCollider box = gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;
    }

    public void activate(bool b) {
        active = b;
    }

    public void damage(float amount)
    {
        health -= amount;
        transform.position = Vector3.MoveTowards(transform.position, transform.position - (transform.forward * 10), 1f * Time.deltaTime * amount / 10);
        if(health < 0) {
            dying = true;
            GetComponent<Collider>().enabled = false;
            banterManager.instance.activateBanter("kill");
            anime.Play("BreadBoideath");
        } else {
            anime.Play("BreadBoiDamage");
            StartCoroutine("stagger");
        }
    }

    IEnumerator stagger() {
        active = false;
        yield return new WaitForSeconds(0.25f);
        active = true;
    }

    private void triggerDeath() {
        GameObject o = GameObject.Instantiate(dropFab);
        o.GetComponent<dropBehavior>().type = this.type.name;
        o.transform.position = transform.position;
        o.GetComponent<SpriteRenderer>().sprite = player.GetComponent<IngredientsList>().getIngredient(type.name).ingredintImage;
        spawnScript.enemyCount--;
        armController.toggle -= activate;
        Destroy(this.gameObject);

    }
	
	// Update is called once per frame
	void Update () {
        rb.velocity = Vector3.zero;

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        transform.LookAt(player.transform, Vector3.up);
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

        if (!active) {
            return;
        }

        if (type.logic == camScript.logicType.BRUTE && Vector3.Distance(player.transform.position, transform.position) < type.visionRange && !dying) {
            Vector3 targetTransform = player.transform.position;
            targetTransform.y = this.transform.position.y;
            Ray r = new Ray(transform.position, targetTransform - transform.position);
            if (Vector3.Distance(transform.position, targetTransform) > type.attackRange) {
                canDamage = true;
                transform.position = Vector3.MoveTowards(transform.position, r.GetPoint(Vector3.Distance(transform.position, targetTransform) - type.attackRange), type.speed * Time.deltaTime);
            } else {
                if (canDamage) {
                    canDamage = false;
                    FindObjectOfType<armController>().takeDamage(20);
                    Invoke("toggleCanDamage", 10);
                }
            }
            //rb.MovePosition(r.GetPoint(Vector3.Distance(transform.position, player.transform.position) - 1f));
        }
        transform.position.Set(transform.position.x, type.lockedYPos, transform.position.z);
        
    }

    void toggleCanDamage() {
        canDamage = true;
    }
}
