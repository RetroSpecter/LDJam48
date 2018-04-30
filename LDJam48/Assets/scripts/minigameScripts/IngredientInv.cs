using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientInv : MonoBehaviour {

    public Queue<ingredientDrop> inventory;
    public int maxSize = 0;

	// Use this for initialization
	void Start () {
        inventory = new Queue<ingredientDrop>();
	}

    // assume ingredName is in list
    public bool addIngredient(string ingredName) {
        if (!inventoryFull()) {
            inventory.Enqueue(IngredientsList.getIngredient(ingredName));
            return true;
        }
        return false;
    }

    public bool addIngredient(ingredientDrop ingred) {
        if (inventoryFull()) {
            return false;
        }

        inventory.Enqueue(ingred);
        return true;
    }

    public bool inventoryEmpty() {
        return inventoryPercentage() == 0;
    }

    public float inventoryPercentage() {
        return inventory.Count / (float)maxSize;
    }

    public bool inventoryFull() {
        return inventoryPercentage() >= 1;
    }

    public ingredientDrop getFromInventory() {
        if (inventory.Count == 0) {
            return null;
        }
        return inventory.Dequeue();
    }
}
