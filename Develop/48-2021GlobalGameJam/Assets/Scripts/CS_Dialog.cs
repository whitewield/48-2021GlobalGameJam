using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Dialog : CS_Item {
    [SerializeField] TextMesh myTextMesh = null;
    [SerializeField] Transform myTransformSubmit = null;
    private CS_Item myTargetItem;

    public void SetTarget (CS_Item g_item) {
        // store target item
        myTargetItem = g_item;

        if (g_item.CheckIsPlural() == true) {
            myTextMesh.text = "oh no! I can't find my " + g_item.GetName () + "...";
        } else {
            // show text 
            myTextMesh.text = "wait... where is my " + g_item.GetName () + "?";
        }
    }

    public Vector3 GetSubmitPosition () {
        return myTransformSubmit.position;
    }

    public CS_Item GetTarget () {
        return myTargetItem;
    }
}
