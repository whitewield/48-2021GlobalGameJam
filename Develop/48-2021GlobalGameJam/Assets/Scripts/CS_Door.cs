using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Door : MonoBehaviour {
    [SerializeField] CS_Box myBox = null;
    [SerializeField] Collider myCollider = null;
    [SerializeField] GameObject myObject_Closed = null;
    [SerializeField] GameObject myObject_Open = null;

    private Vector3 myClickPosition;
    private bool isDragging;

    private void Start () {
        Close ();
    }

    public void OnMouseDown () {

        isDragging = false;

        // start my box mouse down function
        myBox.OnMouseDown ();
        // store click position
        myClickPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
    }

    public void OnMouseDrag () {
        if (isDragging == true) {
            myBox.OnMouseDrag ();
            return;
        }
        // check if its dragging
        // get current position
        Vector3 t_currentPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        // calculate delta
        Vector3 t_deltaPosition = t_currentPosition - myClickPosition;
        t_deltaPosition.z = 0;
        // check distance
        float t_distance = Vector3.Distance (t_currentPosition, myClickPosition);
        if (t_distance > 1) {
            isDragging = true;
        }
        
    }

    public void OnMouseUp () {

        myBox.OnMouseUp ();

        Debug.Log ("OnMouseUp" + isDragging);
        if (isDragging == false) {
            Open ();
        }
    }

    private void Open () {
        myObject_Open.SetActive (true);
        myObject_Closed.SetActive (false);
        myCollider.enabled = false;
    }

    private void Close () {
        myObject_Open.SetActive (false);
        myObject_Closed.SetActive (true);
        myCollider.enabled = true;
    }
}
