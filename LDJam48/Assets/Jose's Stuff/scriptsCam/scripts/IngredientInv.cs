using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientInv : MonoBehaviour {

    private IngredientsList ingredients;
    public Queue<Ingredient> inventory;
    public int maxSize = 0;

	// Use this for initialization
	void Start () {
        ingredients = GetComponent<IngredientsList>();
        inventory = new Queue<Ingredient>();
	}

    // assume ingredName is in list
    public bool addIngredient(string ingredName) {
        if (!inventoryFull())
        {
            inventory.Enqueue(ingredients.getIngredient(ingredName));
            return true;
        }
        return false;
    }

    public bool addIngredient(Ingredient ingred) {
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

    public Ingredient getFromInventory() {
        if (inventory.Count == 0) {
            return null;
        }
        return inventory.Dequeue();
    }
}
