using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsList : MonoBehaviour {

    public ingredientDrop[] ingredient;
    public static Dictionary<string, ingredientDrop> ingredientDict;

	void Awake () {
        if (ingredientDict == null) {
            ingredientDict = new Dictionary<string, ingredientDrop>();
            buildDict();
        }
	}

    void buildDict() {
        foreach (ingredientDrop i in ingredient) {
            ingredientDict.Add(i.name, i);
        }
    }

    public static ingredientDrop getIngredient(string name) {
        name = name.ToLower();
        if (!ingredientDict.ContainsKey(name)) {
            Debug.LogError("ingredient doesn't exist: " + name);
            return null;
        }

        return ingredientDict[name];
    }
}