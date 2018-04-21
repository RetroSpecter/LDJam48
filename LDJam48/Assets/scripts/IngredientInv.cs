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
    public void addIngredient(string ingredName) {
        inventory.Enqueue(ingredients.getIngredient(ingredName));
    }

    public bool addIngredient(Ingredient ingred) {
        if (inventoryFull()) {
            return false;
        }

        inventory.Enqueue(ingred);
        return true;
    }

    public bool inventoryEmpty() {
        return inventory.Count == 0;
    }

    public bool inventoryFull() {
        return inventory.Count >= maxSize;
    }

    public Ingredient getFromInventory() {
        if (inventory.Count == 0) {
            Debug.Log("list is empty");
            return null;
        }
        return inventory.Dequeue();
    }
}
