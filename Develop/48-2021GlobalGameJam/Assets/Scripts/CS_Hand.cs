using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Hand : MonoBehaviour {
    [SerializeField] Joint myJoint = null;
    private static CS_Hand instance = null;
    public static CS_Hand Instance { get { return instance; } }

    [SerializeField] SpriteRenderer mySpriteRenderer;
    [SerializeField] Sprite mySprite_Hold = null;
    [SerializeField] Sprite mySprite_Release = null;

    private void Awake () {
        if (instance != null && instance != this) {
            Destroy (this.gameObject);
        } else {
            instance = this;
        }
    }

    private void Update () {
        Vector3 t_position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        t_position.z = 0;
        this.transform.position = t_position;
    }

    public void Attach (Rigidbody g_rigidbody) {
        myJoint.connectedBody = g_rigidbody;
        mySpriteRenderer.sprite = mySprite_Hold;
    }

    public void Detach () {
        myJoint.connectedBody = null;
        mySpriteRenderer.sprite = mySprite_Release;
    }
}
