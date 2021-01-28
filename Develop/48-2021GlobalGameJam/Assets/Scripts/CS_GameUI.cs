using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CS_GameUI : MonoBehaviour {
    private static CS_GameUI instance = null;
    public static CS_GameUI Instance { get { return instance; } }

    [SerializeField] Text myText_Timer = null;
    [SerializeField] GameObject myObject_Ending = null;
    [SerializeField] Text myText_Ending = null;
    [SerializeField] Image[] myTargetItems = null;

    private void Awake () {
        if (instance != null && instance != this) {
            Destroy (this.gameObject);
        } else {
            instance = this;
        }
    }

    private void Start () {
        myObject_Ending.SetActive (false);
    }

    public void SetTargetItem (Sprite g_sprite, int g_index) {
        myTargetItems[g_index].sprite = g_sprite;
        myTargetItems[g_index].SetNativeSize ();
    }

    public void ShowEnding (float g_timer) {
        myText_Ending.text = "Congratulations! You spent " + g_timer.ToString ("0") + " minutes to find all the items.";
        myObject_Ending.SetActive (true);
    }

    public void OnButtonRetry () {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
    }

    public void OnButtonTitle () {
        SceneManager.LoadScene ("Title");
    }

    public void SetTimer (float g_timer) {
        myText_Timer.text = g_timer.ToString ("0");
    }
}
