using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class finalPot : MonoBehaviour {

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

    public void updatePotStats(stats potStats, float multiplyer) {
        attackUI.gameObject.SetActive(true);
        defenseUI.gameObject.SetActive(true);
        healthUI.gameObject.SetActive(true);
        luckUI.gameObject.SetActive(true);
        speedUI.gameObject.SetActive(true);
        MultiplyerText.gameObject.SetActive(true);

        stats p2 = potStats * multiplyer;
        
        attackUI.text = "" + potStats.attack + " x " + multiplyer + " = " + p2.attack;
        defenseUI.text = "" + potStats.defense + " x " + multiplyer + " = " + p2.defense;
        healthUI.text = "" + potStats.healthRegen + " x " + multiplyer + " = " + p2.healthRegen;
        luckUI.text = "" + potStats.luck + " x " + multiplyer + " = " + p2.luck;
        speedUI.text = "" + potStats.speed + " x " + multiplyer + " = " + p2.speed;
        MultiplyerText.text = "x" + multiplyer;
    }
}
