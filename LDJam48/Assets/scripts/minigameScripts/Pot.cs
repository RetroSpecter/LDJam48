﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pot : MonoBehaviour {
    public KeyCode key;
    public int maxIngredients;
    public Dictionary<string, int> ingredientRate;
    private int IngredientCount;

    public stats potStats;
    public float multiplyer;

    [Header("UI Elements")]
    public Sprite attackUI;
    public Sprite defenseUI;
    public Sprite healthUI;
    public Sprite luckUI;
    public Sprite speedUI;
    public Text KeyText;
    public Text foodNameText;
    public Image statVisual;

    public GameObject potVisual;
    public float maxScale = 1.75f;
    public float incrementScale = 0.05f;

    private Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();
        ingredientRate = new Dictionary<string, int>();
        KeyText.text = key.ToString();
        potVisual.GetComponent<RectTransform>().localScale = Vector3.one;
        clearPot();
    }

    public void displayHighestStat() {
        Dictionary<string, float> statValues = new Dictionary<string, float>();
        KeyValuePair<string, float> highestStat = new KeyValuePair<string, float>("attack", potStats.attack);
        statValues.Add("attack", potStats.attack);
        statValues.Add("defense", potStats.defense);
        statValues.Add("healthRegen", potStats.healthRegen);
        statValues.Add("luck", potStats.luck);
        statValues.Add("speed", potStats.speed);

        foreach (KeyValuePair<string, float> key in statValues) {
            if (key.Value > highestStat.Value) {
                highestStat = key;
            }
        }
        switch (highestStat.Key) {
            case "attack": statVisual.sprite = attackUI; break;
            case "defense": statVisual.sprite = defenseUI; break;
            case "healthRegen": statVisual.sprite = healthUI; break;
            case "luck": statVisual.sprite = luckUI; break;
            case "speed": statVisual.sprite = speedUI; break;
        }
    }

    public void updateMultiplyer() {
        float numOfIngrTypes = ingredientRate.Count;
        float maxMultiplyer = numOfIngrTypes;
        float averageAbsDev = 0;
        foreach (KeyValuePair<string, int> k in ingredientRate) {
            float percentage = (float)k.Value / IngredientCount;

            averageAbsDev += Mathf.Abs(percentage - (1 / numOfIngrTypes)) * 3;
        }
        multiplyer = (1 - (averageAbsDev / numOfIngrTypes)) * maxMultiplyer;
        multiplyer = Mathf.Round(multiplyer * 2) / 2;
        multiplyer = Mathf.Max(1, multiplyer);
    }

    public void addIngredient(ingredientDrop ingr) {
        anim.Play("PotSquetch");
        if (!ingredientRate.ContainsKey(ingr.name)) {
            ingredientRate.Add(ingr.name, 0);
        }

        potVisual.transform.localScale += Vector3.one * incrementScale;

        // potVisual.sprite = ingr.ingredintImage;
        ingredientRate[ingr.name] += 1;
        IngredientCount++;

        potStats.attack += ingr.attack;
        potStats.defense += ingr.defense;
        potStats.healthRegen += ingr.healthRegen;
        potStats.luck += ingr.luck;
        potStats.speed += ingr.speed;

        updateMultiplyer();
        statVisual.gameObject.SetActive(IngredientCount >= 3);
        foodNameText.gameObject.SetActive(IngredientCount >= 3);
        foodNameText.text = "X" + multiplyer;
        displayHighestStat();
    }

    public void displayStats() {
        statVisual.gameObject.SetActive(true);
        foodNameText.gameObject.SetActive(true);
    }

    public void clearPot() {
        potStats = new stats();
        multiplyer = 1;

        ingredientRate.Clear();
        IngredientCount = 0;
        foodNameText.text = "";
        statVisual.gameObject.SetActive(IngredientCount > 3);
    }

    public void moveToPosition(Vector3 targetPos, float speed, bool lerp) {
        StartCoroutine(moveToPositionEnum(this.gameObject,targetPos,speed,lerp));
    }

    private IEnumerator moveToPositionEnum(GameObject targetObject, Vector3 targetPos, float speed, bool lerp) {
        while (Vector3.Distance(targetObject.transform.position, targetPos) > 0.1f) {
            if (lerp) {
                targetObject.transform.position = Vector3.Lerp(targetObject.transform.position, targetPos, Time.deltaTime * speed);
            }
            else
            {
                targetObject.transform.position = Vector3.MoveTowards(targetObject.transform.position, targetPos, Time.deltaTime * speed);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator moveToPositionEnum(Vector3 targetPos, float speed, bool lerp) {
        yield return moveToPositionEnum(this.gameObject, targetPos, speed, lerp);
    }

    public static Pot operator +(Pot p1, Pot p2) {
        Pot res = new Pot();
        res.potStats = p1.potStats + p2.potStats;
        res.multiplyer = (p1.multiplyer + p2.multiplyer) / 2;
        return res;
    }
}
