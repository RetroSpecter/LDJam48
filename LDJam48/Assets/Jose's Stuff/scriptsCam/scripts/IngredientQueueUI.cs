using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientQueueUI : MonoBehaviour {

    public float ingredientCount;
    private Image[] images; //TODO: eventually make private
    public List<FoodUI> foodUI;
    private Text counter;
    public GameObject foodObj;
    public GameObject centerImage;

    void Awake() {
        foodUI = new List<FoodUI>();
        counter = GetComponentInChildren<Text>();
        images = GetComponentsInChildren<Image>();
        System.Array.Sort(images, ((a, b) => { return (int)Mathf.Sign(a.transform.position.x - b.transform.position.x); }));
    }

    public void setupUI(List<Ingredient> inventory, IngredientsList ingredients) {
        counter.text = "" + inventory.Count;

        for (int i = 0; i < images.Length; i++) {
            if (inventory.Count - 1 < i) {
                images[i].sprite = null;
                images[i].gameObject.SetActive(false);
                continue;
            }
           // FoodUI newFood = Instantiate(foodObj, counter.transform.position, Quaternion.identity).GetComponent<FoodUI>();
            //newFood.transform.parent = transform.parent;
            //foodUI.Add(newFood);
            images[i].sprite = ingredients.ingredientDict[inventory[i].name].ingredintImage;
            //newFood.setFood(ingredients.ingredientDict[inventory[i].name]);
           // newFood.updateTarget(images[i].gameObject);
        }

    }

    public void updateUI(List<Ingredient> inventory, IngredientsList ingredients) {
        counter.text = "" + inventory.Count;
        if (inventory.Count > 2) {
            //FoodUI newFood = Instantiate(foodObj, counter.transform.position, Quaternion.identity).GetComponent<FoodUI>();
            //newFood.transform.parent = transform.parent;
            //foodUI.Add(newFood);
        }

        for (int i = 0; i < images.Length; i++) {
            if (inventory.Count - 1 < i) {
                images[i].sprite = null;
                images[i].gameObject.SetActive(false);
                continue;
            }
            images[i].gameObject.SetActive(true);
            images[i].sprite = ingredients.ingredientDict[inventory[i].name].ingredintImage;

            
            if (foodUI.Count > i) {
                //foodUI[i].setFood(ingredients.ingredientDict[inventory[i].name]);
                //foodUI[i].updateTarget(images[i].gameObject);
            }
        }
        //Destroy(foodUI[0].gameObject);
        //foodUI.RemoveAt(0);
    }
}
