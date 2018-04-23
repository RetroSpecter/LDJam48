using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {

    public Text updateMessage;
    public Slider ratio;
    private IngredientInv inventory;

    void Start() {
        inventory = FindObjectOfType<IngredientInv>();
    }

    // Update is called once per frame
    void Update() {
        ratio.value = inventory.inventoryPercentage();
        if (inventory.inventoryPercentage() < 0.5) {
            updateMessage.text = "need more ingredients to cook";
        } else if (inventory.inventoryPercentage() < 1) {
            updateMessage.text = "HIT SPACE TO COOK";
        } else {
            updateMessage.text = "Inventory full. Cook Soon!";
        }
    }
}
