using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TypeReferences;

[CreateAssetMenu (fileName = "New Enemy")]
public class enemy : ScriptableObject {
    public new string name;
    public string ingredientDrop;
    public float speed;
    public float lockedYPos;
    public float health;
    public float visionRange;
    public float attackRange;
    public mobManager.logicType logic;
    public RuntimeAnimatorController anim;

    //[ClassExtends(typeof(mobBehavior))]
    //public ClassTypeReference behavior;
}
