using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {



    public stats playerStats;
    public float fullHealth = 100;
    public float maxHealth;
    [HideInInspector] public float health;

    public float knifeInterval;
    public float fireInterval;
    public GameObject cookingGame;

    private IngredientInv inventory;

    public GameObject hitMarker;
    public List<GameObject> roomPrefabs; //TODO: eventually move this out to it's own script

    public delegate void transition(bool on);
    public static transition toggle;
     
    // For weapons. Eventually make them independent scripts we can attatch/detatch. That way its really easy to switch them out for others
    private float gunTimer;
    private float knifeTimer;
    private Animator anime;

    [Header("SoundFX")]
    public AudioClip shotgun;

	// Use this for initialization
	void Start () {
        inventory = GetComponent<IngredientInv>();
        anime = GetComponentInChildren<Animator>();
        toggle += Activate;
        health = fullHealth;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        gunTimer -= Time.deltaTime;
        //gun
        if (Input.GetButtonDown("Fire1") && gunTimer < 0 && GetComponent<SpriteRenderer>().enabled)
        {
            gunTimer = fireInterval - (playerStats.speed / 300);
            if (gunTimer < .2f)
            {
                gunTimer = .2f;
            }
            //fire the gun animation!
            anime.Play("armsGunFire");
            RaycastHit lineOfFire = new RaycastHit();
            if (Physics.Raycast(transform.position, transform.forward, out lineOfFire))
            {
                GameObject flare = Instantiate(hitMarker, lineOfFire.point, Quaternion.identity);
                Destroy(flare, 0.1f);
                audioManager.instance.Play(shotgun, 0.15f);
                if (lineOfFire.collider.gameObject.tag == "enemyy")
                {
                    lineOfFire.collider.gameObject.GetComponent<mobScript>().damage(playerStats.luck * 10);
                }
            }
        }

        //knife
        if (Input.GetButtonDown("Fire2") && GetComponent<SpriteRenderer>().enabled)
        {
            knifeTimer = knifeInterval - (playerStats.speed / 300);
            if (knifeTimer < .2f)
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
                    lineOfFire.collider.gameObject.GetComponent<mobScript>().damage(playerStats.attack * 10);
                }
                if (lineOfFire.collider.gameObject.tag == "door")
                {
                    lineOfFire.collider.gameObject.GetComponent<doorScript>().damage(playerStats.attack * 10);
                }
            }
        }

        //start cooking
        if (Input.GetButtonDown("Jump") && GetComponent<SpriteRenderer>().enabled)
        {
            if (FindObjectOfType<IngredientInv>().inventoryPercentage() >= 0.20f)
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


    public void ApplyPotStats(Pot pot) {
        health += pot.healthRegen * pot.multiplyer;
        health = Mathf.Min(maxHealth, health);
        playerStats.defense += pot.defense * pot.multiplyer;
        playerStats.attack += pot.attack * pot.multiplyer;
        playerStats.luck += pot.luck * pot.multiplyer;
        playerStats.speed += pot.speed * pot.multiplyer;
    }

    public float GetHealthPercentage() { //TODO: completely rework UI system so that it is more modular
        return (int)(health / (fullHealth) * 100f);
    }

    public void takeDamage(float damage) {
        health -= damage / Mathf.Sqrt(playerStats.defense);
        health = Mathf.Max(0, health);
        
        if (health == 0) {
            FindObjectOfType<screenOverlayUI>().death();
            toggle.Invoke(false);
            toggle = null;
        }
        else {
            FindObjectOfType<screenOverlayUI>().flashHurt();
        }
    }

    public void Activate(bool b)  {
        GetComponent<SpriteRenderer>().enabled = b;
        if (b) {
            FindObjectOfType<screenOverlayUI>().flashHeal();
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.tag == "ingredDrop") {
            if (inventory.addIngredient(other.GetComponent<dropBehavior>().type))
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
public struct stats
{
    public float attack;
    public float defense;
    public float luck;
    public float speed;
    public float healingFactor;
}