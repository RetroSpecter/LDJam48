using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatUI : MonoBehaviour {

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

    private PlayerController arm;

	// Use this for initialization
	void Start () {
        arm = FindObjectOfType<PlayerController>();
        img = GetComponent<Image>();
        healthHUD = healthText.GetComponent<Text>();
        lckText = lck.GetComponent<Text>();
        strText = str.GetComponent<Text>();
        spdText = spd.GetComponent<Text>();
        frtText = frt.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        int health = (int)arm.GetHealthPercentage();
        if(health >= 75) {
            img.sprite = portraits[0];
        }
        else if(health >= 50)
        {
            img.sprite = portraits[1];
        } else if (health >= 25) {
            img.sprite = portraits[2];
        } else if (health >= 0) {
            img.sprite = portraits[3];
        } else {
            Debug.Log("you ded"); //DEATH MECHANICS??
        }
        healthHUD.text = "LIFE:" + (int)arm.health + "/" + (int)arm.fullHealth;
        healthHUD.fontStyle = FontStyle.Bold;
        lckText.text = "" + arm.playerStats.luck;
        lckText.fontStyle = FontStyle.Bold;
        strText.text = "" + arm.playerStats.attack;
        strText.fontStyle = FontStyle.Bold;
        spdText.text = "" + arm.playerStats.speed;
        spdText.fontStyle = FontStyle.Bold;
        frtText.text = "" + arm.playerStats.defense;
        frtText.fontStyle = FontStyle.Bold;
    }
}
