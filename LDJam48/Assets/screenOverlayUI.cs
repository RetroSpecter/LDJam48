using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class screenOverlayUI : MonoBehaviour {

    private Image img;
    public Color hurtColor;
    public Color healColor;
    public GameObject gameOverText;

    private void Start() {
        img = GetComponent<Image>();
    }

    public void flashHurt() {
        screenFlash(hurtColor);
    }

    public void flashHeal() {
        banterManager.instance.activateBanter("finishCooking");
        screenFlash(healColor);
    }

    public void death() {
        StopAllCoroutines();
        img.color = hurtColor;
        StartCoroutine("gameOver");
    }

    public IEnumerator gameOver() {
        yield return new WaitForSeconds(1.5f);
        gameOverText.SetActive(true);
    }

    public void screenFlash(Color color) {
        StopAllCoroutines();
        StartCoroutine(screenFlashEnum(color, 1));
    }

    public IEnumerator screenFlashEnum(Color color, float fadeOutSpeed) {
        img.color = color;
        while (img.color != Color.clear) {
            img.color = Color.Lerp(img.color, Color.clear, Time.deltaTime * fadeOutSpeed);
            yield return null;
        }
    }
}
