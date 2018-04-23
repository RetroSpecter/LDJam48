using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armController : MonoBehaviour {

    private float gunTimer;
    private float knifeTimer;
    private Animator anime;

    public stats attributes;
    public float health;
    public float fullHealth = 100;
    public float maxHealth;
    public float knifeInterval;
    public float fireInterval;
    public GameObject cookingGame;
    public GameObject hitMarker;

    public delegate void transition(bool on);
    public static transition toggle;
    [Header("SoundFX")]
    public AudioClip shotgun;

	// Use this for initialization
	void Start () {
        anime = GetComponent<Animator>();
        toggle += toggleArms;
	}

    public void gorge(Pot pot) {
        health += pot.healthRegen * pot.multiplyer;
        health = Mathf.Min(maxHealth, health);
        attributes.defense = pot.defense * pot.multiplyer;
        attributes.attack = pot.attack * pot.multiplyer;
        attributes.luck = pot.luck * pot.multiplyer;
        attributes.speed = pot.speed * pot.multiplyer;
    }

    public float getHealthiness() {
        return (int)(health / (fullHealth) * 100f);
    }

    public void takeDamage(float damage) {
        health -= damage / Mathf.Sqrt(attributes.defense);
        health = Mathf.Max(0, health);
        
        if (health == 0)
        {

            FindObjectOfType<screenOverlayUI>().death();
            toggle.Invoke(false);
            toggle = null;
        }
        else {
            FindObjectOfType<screenOverlayUI>().flashHurt();
        }
    }

	// Update is called once per frame
	void Update () {
        gunTimer -= Time.deltaTime;
        
        if (Input.GetButtonDown("Fire1") && gunTimer < 0)
        {
            gunTimer = fireInterval - (attributes.speed / 300);
            if(gunTimer < .2f)
            {
                gunTimer = .2f;
            }
            //fire the gun animation!
            anime.Play("armsGunFire");
            RaycastHit lineOfFire = new RaycastHit();
            if(Physics.Raycast(transform.parent.position, transform.parent.forward, out lineOfFire)) {
                GameObject flare = Instantiate(hitMarker, lineOfFire.point, Quaternion.identity);
                Destroy(flare, 0.1f);
                audioManager.instance.Play(shotgun, 0.05f);
                if (lineOfFire.collider.gameObject.tag == "enemyy") {
                    lineOfFire.collider.gameObject.GetComponent<mobScript>().damage(attributes.luck * 10);
                }
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            knifeTimer = knifeInterval - (attributes.speed / 300);
            if(knifeTimer < .2f)
            {
                knifeTimer = .2f;
            }
            //fire the knife animation!
            anime.Play("armsSlice");
            RaycastHit lineOfFire = new RaycastHit();
            if (Physics.Raycast(transform.parent.position, transform.parent.forward, out lineOfFire, 1.5f))
            {
                if (lineOfFire.collider.gameObject.tag == "enemyy")
                {
                    lineOfFire.collider.gameObject.GetComponent<mobScript>().damage(attributes.attack * 10);
                }
            }
        }

        if (Input.GetButtonDown("Jump") && GetComponent<SpriteRenderer>().enabled)
        {
            if (FindObjectOfType<IngredientInv>().inventoryPercentage() >= 0.5f)
            {
                toggle.Invoke(false);
                cookingGame.SetActive(true);
                cookingGame.GetComponent<MinigameManager>().StartMinigame();
            }
            else
            {
                Debug.Log("inventory not full enough");
            }
        }


    }

    public void toggleArms(bool b)  {
        GetComponent<SpriteRenderer>().enabled = b;
        if (b) {
            FindObjectOfType<screenOverlayUI>().flashHeal();
        }
    }

    [System.Serializable]
    public struct stats {
        public float attack;
        public float defense;
        public float luck;
        public float speed;
        public float healingFactor;
    }
}
