using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientQueueUI : MonoBehaviour {

    public float ingredientCount;
    public Image[] images; //TODO: eventually make private
    private Text counter;

    void Start() {
        counter = GetComponentInChildren<Text>();
        images = GetComponentsInChildren<Image>();
        System.Array.Sort(images, ((a, b) => { return (int)Mathf.Sign(a.transform.position.x - b.transform.position.x); }));
	}

    public void updateUI(List<Ingredient> inventory, IngredientsList ingredients) {
        counter.text = "" + inventory.Count;
        for (int i = 0; i < images.Length; i++) {
            if (inventory.Count <= i) {
                images[i].sprite = null;
                images[i].gameObject.SetActive(false);
                continue;
            }
            images[i].gameObject.SetActive(true);
            images[i].sprite = ingredients.ingredientDict[inventory[i].name].ingredintImage;
        }
    }
}
