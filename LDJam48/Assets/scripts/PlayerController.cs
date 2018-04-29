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

    public GameObject hitMarker;

    public delegate void transition(bool on);
    public static transition toggle;
     
    // For weapons. Eventually make them independent scripts we can attatch/detatch. That way its really easy to switch them out for others
    private float gunTimer;
    private float knifeTimer;
    private Animator anime;

    public PlayerStatUI playerStatUI;

    [Header("SoundFX")]
    public AudioClip shotgun;

	// Use this for initialization
	void Start () {
        anime = GetComponentInChildren<Animator>();
        toggle += Activate;
        health = fullHealth;
        playerStatUI.updateHealth(health, fullHealth);
        playerStatUI.updateStats(playerStats);
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

    public void ApplyPotStats(stats potStats) {
        playerStats = playerStats + potStats;
        health += potStats.healthRegen;
        health = Mathf.Min(health, maxHealth);

        playerStatUI.updateHealth(health, fullHealth);
        playerStatUI.updateStats(playerStats);
    }

    public void takeDamage(float damage) {
        health -= damage / Mathf.Sqrt(playerStats.defense);
        health = Mathf.Max(0, health);
        playerStatUI.updateHealth(health, fullHealth);
        if (health == 0) {
            FindObjectOfType<screenOverlayUI>().death();
            toggle.Invoke(false);
            toggle = null;
        } else {
            FindObjectOfType<screenOverlayUI>().flashHurt();
        }
    }

    public void Activate(bool b)  {
        GetComponent<SpriteRenderer>().enabled = b;
        if (b) {
            FindObjectOfType<screenOverlayUI>().flashHeal();
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
    public float healthRegen;

    public static stats operator +(stats s1, stats s2) {
        stats res = new stats();
        res.attack = Mathf.Round(s1.attack + s2.attack);
        res.defense = Mathf.Round(s1.defense + s2.defense);
        res.healthRegen = Mathf.Round(s1.healthRegen + s2.healthRegen);
        res.luck = Mathf.Round(s1.luck + s2.luck);
        res.speed = Mathf.Round(s1.speed + s2.speed);
        return res;
    }

    public static stats operator *(stats s, float multiplier) {
        stats res = s;
        res.attack *= multiplier;
        res.defense *= multiplier;
        res.healthRegen *= multiplier;
        res.luck *= multiplier;
        res.speed *= multiplier;
        return res;
    }

    public static stats operator *(float multiplier, stats s) {
        stats res = s;
        res.attack *= multiplier;
        res.defense *= multiplier;
        res.healthRegen *= multiplier;
        res.luck *= multiplier;
        res.speed *= multiplier;
        return res;
    }
}