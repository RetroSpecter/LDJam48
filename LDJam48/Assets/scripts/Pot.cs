using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pot : MonoBehaviour {
    public KeyCode key;
    private Image potVisual;
    private Animator anim;
    public int maxIngredients;
    public Dictionary<string, int> ingredientRate;
    private int IngredientCount;

    public float attack;
    public float defense;
    public float healthRegen;
    public float luck;
    public float speed;

    public float multiplyer;

    private void Start() {
        anim = GetComponent<Animator>();
        ingredientRate = new Dictionary<string, int>();
        potVisual = GetComponent<Image>();
    }

    public void updateMultiplyer() {
        float numOfIngrTypes = ingredientRate.Count;
        float maxMultiplyer = numOfIngrTypes;
        float averageAbsDev = 0;
        foreach (KeyValuePair<string, int> k in ingredientRate)
        {
            float percentage = (float)k.Value / IngredientCount;
            Debug.Log(percentage);

            averageAbsDev += Mathf.Abs(percentage - (1 / numOfIngrTypes));
        }
        multiplyer = (1 - (averageAbsDev / numOfIngrTypes)) * maxMultiplyer;
    }

    public void addIngredient(Ingredient ingr) {
        anim.Play("PotSquetch");
        if (!ingredientRate.ContainsKey(ingr.name)) {
            ingredientRate.Add(ingr.name, 0);
        }

        potVisual.sprite = ingr.ingredintImage;
        ingredientRate[ingr.name] += 1;
        IngredientCount++;

        attack += ingr.attack;
        defense += ingr.defense;
        healthRegen += ingr.healthRegen;
        luck += ingr.luck;
        speed += ingr.speed;

        updateMultiplyer();
    }

    public void clearPot()
    {
        ingredientRate.Clear();
    }

    public static Pot operator +(Pot p1, Pot p2) {
        Pot res = new Pot();
        res.attack = p1.attack + p2.attack;
        res.defense = p1.defense + p2.defense;
        res.healthRegen = p1.healthRegen + p2.healthRegen;
        res.luck = p1.luck + p2.luck;
        res.speed = p1.speed + p2.speed;
        res.multiplyer = p1.multiplyer + p2.multiplyer / 2;
        return res;
    }
}
