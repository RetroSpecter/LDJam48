using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class finalPot : MonoBehaviour {

    public float attack;
    public float defense;
    public float healthRegen;
    public float luck;
    public float speed;

    [Header("UI Elements")]
    public Text attackUI;
    public Text defenseUI;
    public Text healthUI;
    public Text luckUI;
    public Text speedUI;

    public Text MultiplyerText;

    public void hidePotStats() {
        attackUI.gameObject.SetActive(false);
        defenseUI.gameObject.SetActive(false);
        healthUI.gameObject.SetActive(false);
        luckUI.gameObject.SetActive(false);
        speedUI.gameObject.SetActive(false);
        MultiplyerText.gameObject.SetActive(false);
    }

    public void updatePotStats(Pot p) {
        attackUI.gameObject.SetActive(true);
        defenseUI.gameObject.SetActive(true);
        healthUI.gameObject.SetActive(true);
        luckUI.gameObject.SetActive(true);
        speedUI.gameObject.SetActive(true);
        MultiplyerText.gameObject.SetActive(true);

        attackUI.text = "" + p.attack + " x " + p.multiplyer + " = " + (p.attack * p.multiplyer);
        defenseUI.text = "" + p.defense + " x " + p.multiplyer + " = " + (p.defense * p.multiplyer);
        healthUI.text = "" + p.healthRegen + " x " + p.multiplyer + " = " + (p.healthRegen * p.multiplyer);
        luckUI.text = "" + p.luck + " x " + p.multiplyer + " = " + (p.luck * p.multiplyer);
        speedUI.text = "" + p.speed + " x " + p.multiplyer + " = " + (p.speed * p.multiplyer);
        MultiplyerText.text = "x" + p.multiplyer;
    }
}
