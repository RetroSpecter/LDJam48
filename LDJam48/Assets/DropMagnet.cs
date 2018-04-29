using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMagnet : MonoBehaviour {

    public float dropSpeed = 10;
    private IngredientInv inventory;

    private void Start() {
        inventory = GetComponentInParent<IngredientInv>();
    }

    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.GetComponent<dropBehavior>() != null && !inventory.inventoryFull()) {
            StartCoroutine(moveToPositionEnum(collision.gameObject, this.gameObject, dropSpeed, true));
        }
    }

    IEnumerator moveToPositionEnum(GameObject dropObject, GameObject targetObject, float speed, bool lerp) {
        while (Vector3.Distance(dropObject.transform.position, targetObject.transform.position) > 1f) {
            if (lerp) {
                dropObject.transform.position = Vector3.Lerp(dropObject.transform.position, targetObject.transform.position, Time.deltaTime * speed);
            } else {
                dropObject.transform.position = Vector3.MoveTowards(dropObject.transform.position, targetObject.transform.position, Time.deltaTime * speed);
            }
            yield return new WaitForEndOfFrame();
        }

        if (!inventory.inventoryFull()) {
            inventory.addIngredient(dropObject.GetComponent<dropBehavior>().type);
            Destroy(dropObject);
        }
    }
}
