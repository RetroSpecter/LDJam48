using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsList : MonoBehaviour {

    public Ingredient[] ingredient;
    public Dictionary<string, Ingredient> ingredientDict;

	// Use this for initialization
	void Awake () {
        ingredientDict = new Dictionary<string, Ingredient>();
        buildDict();
	}

    void buildDict() {
        foreach (Ingredient i in ingredient) {
            ingredientDict.Add(i.name, i);
        }
    }

    public Ingredient getIngredient(string name) {
        if (!ingredientDict.ContainsKey(name)) {
            Debug.LogError("ingredient doesn't exist: " + name);
            return null;
        }

        return ingredientDict[name];
    }
}

[System.Serializable]
public class Ingredient {
    public string name;

    public float attack = 1;
    public float defense = 1;
    public float healthRegen;
    public float luck;
    public float speed;

    public Sprite ingredintImage;
}
