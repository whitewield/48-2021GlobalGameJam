﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Item : MonoBehaviour {
    private Vector3 myLastPosition;

    [SerializeField] string myName = "";
    [SerializeField] bool isPlural = false;
    [SerializeField] bool isTaskItem = false;
    [SerializeField] SpriteRenderer mySpriteRenderer = null;
    private float myFrictionMultiplier = 0.95f;

    private Rigidbody myRigidbody;
    private Vector3 myStartLocalPosition;

    private void Start () {
        // get rigid body
        myRigidbody = this.GetComponent<Rigidbody> ();
        // make the item move with parent
        if (this.transform.parent != null) {
            myRigidbody.isKinematic = true;
            myStartLocalPosition = this.transform.localPosition;
        }

        if (isTaskItem) {
            // random rotation
            this.transform.rotation = Quaternion.Euler (0, 0, Random.Range (0, 360f));
        }
    }

    private void FixedUpdate () {
        if (myRigidbody.isKinematic == true) {
            this.transform.localPosition = myStartLocalPosition;
        }
    }

    public void OnMouseDown () {
        // reset rigidbody
        if (this.transform.parent != null) {
            myRigidbody.isKinematic = false;
        }

        // attach to hand
        CS_Hand.Instance.Attach (myRigidbody);

        // move item out of box
        this.transform.SetParent (null);

        // move item to the front
        CS_GameManager.Instance.SelectItem (this);
    }

    public void OnMouseUp () {
        // detach to hand
        CS_Hand.Instance.Detach ();

        CS_GameManager.Instance.CheckSubmit (this);
    }

    public void OnMouseDrag () {

        // add friction
        this.myRigidbody.velocity *= myFrictionMultiplier;

        //// get current position
        //Vector3 t_currentPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        //// calculate delta
        //Vector3 t_deltaPosition = t_currentPosition - myLastPosition;
        //t_deltaPosition.z = 0;

        //// update my position
        //this.transform.Translate (t_deltaPosition, Space.World);
        ////this.transform.position = this.transform.position + t_deltaPosition;

        //// update my last position
        //myLastPosition = t_currentPosition;
    }

    public string GetName () {
        return myName;
    }

    public bool CheckIsPlural () {
        return isPlural;
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
