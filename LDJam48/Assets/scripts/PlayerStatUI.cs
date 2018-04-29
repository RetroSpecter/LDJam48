using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatUI : MonoBehaviour {

    public List<Sprite> portraits;

    public Text lckText;
    public Text spdText;
    public Text strText;
    public Text frtText;
    public Text healthHUD;
    public Image img;

    public void updateHealth(float health, float fullHealth) {
        if (health >= 75) {
            img.sprite = portraits[0];
        } else if (health >= 50) {
            img.sprite = portraits[1];
        } else if (health >= 25) {
            img.sprite = portraits[2];
        } else if (health >= 0) {
            img.sprite = portraits[3];
        } else {
            Debug.Log("you ded"); //DEATH MECHANICS??
        }
        healthHUD.text = "LIFE:" + (int)health + "/" + (int)fullHealth;
    }

	// Update is called once per frame
	public void updateStats (stats playerStats) {
        lckText.text = "" + playerStats.luck;
        strText.text = "" + playerStats.attack;
        spdText.text = "" + playerStats.speed;
        frtText.text = "" + playerStats.defense;
    }
}
