using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobScript : MonoBehaviour {

    private PlayerController player;
    private Animator anime;
    private string birthString;
    private enemy type;
    private Rigidbody rb;
    private bool dying;

    private float health;

    public bool active;
    public GameObject dropFab;
    bool canDamage;

    public void Spawn(enemy mob) {
        if (mob == null) {
            Debug.LogError("ERROR: attempted to spawn a mob before the types dictionary has been established!");
            GameObject.Destroy(this.gameObject.GetComponent<SpriteRenderer>());
            GameObject.Destroy(this.gameObject.GetComponent<BoxCollider>());
        }

        active = true;
        dying = false;
        anime = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerController>();
        this.type = mob;

        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        PlayerController.toggle += Activate;
        anime.runtimeAnimatorController = type.anim;
        health = type.health;
        Invoke("SetBoxCollider", 0.1f);
    }

    void SetBoxCollider() {
        gameObject.AddComponent(typeof(BoxCollider));
    }

    public void Activate(bool b) {
        active = b;
    }

    public void TakeDamage(float amount) {
        health -= amount;
        transform.position = Vector3.MoveTowards(transform.position, transform.position - (transform.forward * 10), 1f * Time.deltaTime * amount / 10);
        if(health < 0) {
            dying = true;
            GetComponent<Collider>().enabled = false;
            banterManager.instance.activateBanter("kill");
            anime.Play("BreadBoideath");
        } else {
            anime.Play("BreadBoiDamage");
            StartCoroutine("Stagger");
        }
    }

    IEnumerator Stagger() {
        active = false;
        yield return new WaitForSeconds(0.25f);
        active = true;
    }

    private void TriggerDeath() {
        Destroy(this.gameObject);
    }

    public void SpawnDrops() {
        for (int i = 0; i < 3; i++) {
            GameObject o = GameObject.Instantiate(dropFab, transform.position + Vector3.up * 0.5f, transform.rotation);
            o.GetComponent<dropBehavior>().type = this.type.ingredientDrop;
            o.transform.position = transform.position;
            o.GetComponent<SpriteRenderer>().sprite = IngredientsList.getIngredient(type.ingredientDrop).ingredintImage;
            spawnScript.enemyCount--;
            PlayerController.toggle -= Activate;
        }
    }

    void ToggleCanDamage() {
       canDamage = true;
    }

    void Update () {

        if (!active) {
            return;
        }
        if (type.logic == mobManager.logicType.BRUTE && Vector3.Distance(player.transform.position, transform.position) < type.visionRange && !dying) {
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
                    Invoke("ToggleCanDamage", 10);
                }
            }
        }

        transform.position.Set(transform.position.x, type.lockedYPos, transform.position.z);       
    }
}
