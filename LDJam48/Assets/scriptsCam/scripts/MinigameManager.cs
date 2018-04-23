﻿using System.Collections;
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
    public finalPot finalPot;

    public GameObject foodUI;

    public void StartMinigame() {
        ingredientInventory = FindObjectOfType<IngredientInv>();
        pots = GetComponentsInChildren<Pot>();
        maxIngredientExpiration = ingredientExpiration;
        curIngredientUI.gameObject.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
        if (!ingredientInventory.inventoryEmpty()) {
            updateCurrentIngredient();
            ingredientQueueUI.updateUI(new List<Ingredient>(ingredientInventory.inventory), FindObjectOfType<IngredientsList>());
        }
    }

	void Update () {
        if (curIngredient == null) {
            return;
        }

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
                if (curIngredient != null) {
                    placeInPot(p);
                    ingredientExpiration = maxIngredientExpiration;
                    if (curIngredient == null) {
                        StartCoroutine("chooseTwo");
                    }
                    break;
                }
            }
        }

        if (ingredientExpiration <= 0) {
            Pot p = pots[Random.Range(0, pots.Length)];
            placeInPot(p);
            ingredientExpiration = maxIngredientExpiration;
            if (curIngredient == null) {
                StartCoroutine("chooseTwo");
            }
            return;
        }
	}

    IEnumerator chooseTwo() {
        yield return new WaitForSeconds(0.25f);
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        finalPot.hidePotStats();
        List<Pot> selectedPots = new List<Pot>();
        yield return new WaitForSeconds(0.25f);
        while (selectedPots.Count < 2) {
            foreach (Pot p in pots) {
                if (Input.GetKeyDown(p.key) && !selectedPots.Contains(p)) {
                    selectedPots.Add(p);
                    p.moveToPosition(p.transform.position + Vector3.up * 50, 1000, false);
                }
            }
            yield return null;
        }

        yield return new WaitForSeconds(1);
        foreach (Pot p in selectedPots) {
            p.moveToPosition(p.transform.position - Vector3.up * 50, 1000, false);
        }
        Pot addedPot = selectedPots[0] + selectedPots[1];
        finalPot.updatePotStats(addedPot);
        yield return new WaitForSeconds(3);
        FindObjectOfType<armController>().gorge(addedPot);
        endMinigame();
    }

    void endMinigame() {
        foreach (Pot p in pots) {
            p.clearPot();
        }
        armController.toggle.Invoke(true);
        gameObject.SetActive(false);
    }

    Ingredient updateCurrentIngredient() {
        curIngredient = ingredientInventory.getFromInventory();

        if (curIngredient == null) {
            curIngredientUI.gameObject.SetActive(false);
            return null;
        }
        curIngredientUI.sprite = curIngredient.ingredintImage;
        return curIngredient;
    }

    void placeInPot(Pot p) {
        FoodUI food = Instantiate(foodUI, curIngredientUI.transform.position, transform.rotation).GetComponent<FoodUI>();
        food.setFood(curIngredient);
        food.transform.parent = this.transform;
        food.transform.position = curIngredientUI.transform.position;
        food.transform.localScale = curIngredientUI.transform.localScale;
        food.disappearInTarget(p.gameObject);

        p.addIngredient(curIngredient);
        Ingredient ingr = updateCurrentIngredient();
        ingredientQueueUI.updateUI(new List<Ingredient>(ingredientInventory.inventory), FindObjectOfType<IngredientsList>());
    }
}