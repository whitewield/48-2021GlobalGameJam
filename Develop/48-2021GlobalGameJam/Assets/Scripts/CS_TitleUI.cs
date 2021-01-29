using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS_TitleUI : MonoBehaviour {
    public void OnButtonStart () {
        SceneManager.LoadScene ("Game");
    }

    public void OnButtonQuit () {
        Application.Quit ();
    }
}
