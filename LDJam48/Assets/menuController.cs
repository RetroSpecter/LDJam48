using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuController : MonoBehaviour {


    public void startGame(string levelName) {
        Application.LoadLevel(levelName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
