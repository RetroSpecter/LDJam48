using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuController : MonoBehaviour {

    public string levelName;

    public void startGame() {
        Application.LoadLevel(levelName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
