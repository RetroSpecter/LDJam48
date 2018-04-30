using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient")]
public class ingredientDrop : ScriptableObject {
    public new string name;

    public float attack = 1;
    public float defense = 1;
    public float healthRegen;
    public float luck;
    public float speed;

    public Sprite ingredintImage;
}
