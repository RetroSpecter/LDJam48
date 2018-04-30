using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodUI : MonoBehaviour {

    private Image img;
    public GameObject target;

    private void Update() {
        if (target == null) {
            return;
        }

        transform.position = Vector3.Lerp(transform.position, target.transform.position - Vector3.up * 20, Time.deltaTime * 12);
        transform.localScale = Vector3.Lerp(transform.localScale, target.transform.localScale, Time.deltaTime * 12);
    }

    public void setFood(ingredientDrop ingr) {
        img = GetComponent<Image>();
        img.sprite = ingr.ingredintImage;
    }

    public void disappearInTarget(GameObject tar) {
        target = tar;
        StartCoroutine(disappearInTargetEnum(tar));
    }

    IEnumerator disappearInTargetEnum(GameObject tar) {
        while (Vector2.Distance(tar.transform.position, this.transform.position) > 20f) {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * 5);
            yield return null;
        }
        Destroy(this.gameObject);
    }

    public void updateTarget(GameObject tar) {
        target = tar;
    }
}
