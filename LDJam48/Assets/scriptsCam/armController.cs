using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armController : MonoBehaviour {

    private float gunTimer;
    private float knifeTimer;
    private Animator anime;

    public float gunDamage;
    public float knifeDamage;
    public float knifeInterval;
    public float fireInterval;
    public GameObject cookingGame;

    public delegate void transition(bool on);
    public static transition toggle;

	// Use this for initialization
	void Start () {
        anime = GetComponent<Animator>();
        toggle += toggleArms;
	}
	
	// Update is called once per frame
	void Update () {
        gunTimer -= Time.deltaTime;
        
        if (Input.GetButtonDown("Fire1") && gunTimer < 0) {
            gunTimer = fireInterval;
            //fire the gun animation!
            anime.Play("armsGunFire");
            RaycastHit lineOfFire = new RaycastHit();
            if(Physics.Raycast(transform.parent.position, transform.parent.forward, out lineOfFire))
            {
                if(lineOfFire.collider.gameObject.tag == "enemyy")
                {
                    lineOfFire.collider.gameObject.GetComponent<mobScript>().damage(gunDamage);
                }
            }
        }

        if (Input.GetButtonDown("Fire2")) {
            knifeTimer = knifeInterval;
            //fire the knife animation!
            anime.Play("armsSlice");
            RaycastHit lineOfFire = new RaycastHit();
            if (Physics.Raycast(transform.parent.position, transform.parent.forward, out lineOfFire, 1.5f)) {
                if (lineOfFire.collider.gameObject.tag == "enemyy")
                {
                    lineOfFire.collider.gameObject.GetComponent<mobScript>().damage(knifeDamage);
                }
            }
        }

        if (Input.GetButtonDown("Jump") && GetComponent<SpriteRenderer>().enabled) {
            if (FindObjectOfType<IngredientInv>().inventoryPercentage() >= 0.5f)
            {
                toggle.Invoke(false);
                cookingGame.SetActive(true);
                cookingGame.GetComponent<MinigameManager>().StartMinigame();
            }
            else {
                Debug.Log("inventory not full enough");
            }
        }      
    }
    public void toggleArms(bool b)
    {
        GetComponent<SpriteRenderer>().enabled = b;
    }
}
