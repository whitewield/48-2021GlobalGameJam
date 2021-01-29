using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS_TitleUI : MonoBehaviour {
    [SerializeField] AudioSource myAudioSource = null;
    public void OnButtonStart () {
        myAudioSource.Play ();
        SceneManager.LoadScene ("Game");
    }

    public void OnButtonQuit () {
        myAudioSource.Play ();
        Application.Quit ();
    }
}
