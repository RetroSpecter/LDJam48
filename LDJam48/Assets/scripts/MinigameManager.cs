using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameManager : MonoBehaviour {

    private IngredientInv ingredientInventory;
    public Image curIngredientUI;

    public Ingredient curIngredient;
    [Space()]
    public float ingredientExpiration = 3;
    private float maxIngredientExpiration;
    public Slider expirationUI;
    public IngredientQueueUI ingredientQueueUI;
    [Space()]
    public Pot[] pots;

    // Use this for initialization
    void Start () {
        ingredientInventory = FindObjectOfType<IngredientInv>();
        pots = GetComponentsInChildren<Pot>();

        ingredientInventory.addIngredient("lettuce");
        ingredientInventory.addIngredient("tomato");
        ingredientInventory.addIngredient("lettuce");
        ingredientInventory.addIngredient("tomato");
        ingredientInventory.addIngredient("lettuce");
        ingredientInventory.addIngredient("tomato");
        ingredientInventory.addIngredient("lettuce");
        ingredientInventory.addIngredient("tomato");

        maxIngredientExpiration = ingredientExpiration;
        StartMinigame();
	}

    void StartMinigame() {
        transform.GetChild(0).gameObject.SetActive(true);
        if (!ingredientInventory.inventoryEmpty()) {
            updateCurrentIngredient();
        }
    }

	void Update () {
        ingredientExpiration -= Time.deltaTime;
        expirationUI.value = ingredientExpiration / maxIngredientExpiration;
        if (ingredientExpiration <= 0) {
            Pot p = pots[Random.Range(0, pots.Length)];
            placeInPot(p);
            ingredientExpiration = maxIngredientExpiration;
            return;
        }

        foreach (Pot p in pots) {
            if (Input.GetKeyDown(p.key)) {
                if (!ingredientInventory.inventoryEmpty()) {
                    placeInPot(p);
                    ingredientExpiration = maxIngredientExpiration;
                }
            }
        }

        if (ingredientInventory.inventoryEmpty()) {
            endMinigame();
        }
	}

    void endMinigame() {
        foreach (Pot p in pots) {
            p.clearPot();
        }
        transform.GetChild(0).gameObject.SetActive(false);
    }

    Ingredient updateCurrentIngredient() {
        curIngredient = ingredientInventory.getFromInventory();
        curIngredientUI.sprite = curIngredient.ingredintImage;
        ingredientQueueUI.updateUI(new List<Ingredient>(ingredientInventory.inventory), FindObjectOfType<IngredientsList>());
        return curIngredient;
    }

    void placeInPot(Pot p) {
        p.addIngredient(curIngredient);
        Ingredient ingr = updateCurrentIngredient();
    }
}