using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Item : MonoBehaviour {
    private Vector3 myLastPosition;

    [SerializeField] string myName = "";
    [SerializeField] bool isTaskItem = false;
    [SerializeField] SpriteRenderer mySpriteRenderer = null;

    public void OnMouseDown () {
        // store the initial position
        myLastPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);

        // move item out of box
        this.transform.SetParent (null);

        // move item to the front
        CS_GameManager.Instance.SelectItem (this);
    }

    public void OnMouseUp () {
        CS_GameManager.Instance.CheckSubmit (this);
    }

    public void OnMouseDrag () {
        // get current position
        Vector3 t_currentPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        // calculate delta
        Vector3 t_deltaPosition = t_currentPosition - myLastPosition;
        t_deltaPosition.z = 0;

        // update my position
        this.transform.position = this.transform.position + t_deltaPosition;

        // update my last position
        myLastPosition = t_currentPosition;
    }

    public string GetName () {
        return myName;
    }

    public Sprite GetSprite () {
        return mySpriteRenderer.sprite;
    }

    public bool CheckIsTaskItem () {
        if (myName == "") {
            return false;
        }

        return isTaskItem;
    }

    public void Hide () {
        this.gameObject.SetActive (false);
    }
}
