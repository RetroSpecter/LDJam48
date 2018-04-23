using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class portraitSprite : MonoBehaviour {

    public GameObject arms;
    public List<Sprite> portraits;
    public GameObject healthText;
    public GameObject lck;
    public GameObject spd;
    public GameObject str;
    public GameObject frt;

    private Text lckText;
    private Text spdText;
    private Text strText;
    private Text frtText;
    private Text healthHUD;
    private Image img;

	// Use this for initialization
	void Start () {
        img = GetComponent<Image>();
        healthHUD = healthText.GetComponent<Text>();
        lckText = lck.GetComponent<Text>();
        strText = str.GetComponent<Text>();
        spdText = spd.GetComponent<Text>();
        frtText = frt.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        int health = (int)arms.GetComponent<armController>().getHealthiness();
        if(health >= 75) {
            img.sprite = portraits[0];
        }
        else if(health >= 50)
        {
            img.sprite = portraits[1];
        } else if(health >= 25)
        {
            img.sprite = portraits[2];
        } else if (health >= 0)
        {
            img.sprite = portraits[3];
        } else
        {
            Debug.Log("you ded"); //DEATH MECHANICS??
        }
        healthHUD.text = "LIFE: " + (int)arms.GetComponent<armController>().health + "/" + (int)arms.GetComponent<armController>().fullHealth;
        healthHUD.fontStyle = FontStyle.Bold;
        lckText.text = "" + arms.GetComponent<armController>().attributes.luck;
        lckText.fontStyle = FontStyle.Bold;
        strText.text = "" + arms.GetComponent<armController>().attributes.attack;
        strText.fontStyle = FontStyle.Bold;
        spdText.text = "" + arms.GetComponent<armController>().attributes.speed;
        spdText.fontStyle = FontStyle.Bold;
        frtText.text = "" + arms.GetComponent<armController>().attributes.defense;
        frtText.fontStyle = FontStyle.Bold;
    }
}
