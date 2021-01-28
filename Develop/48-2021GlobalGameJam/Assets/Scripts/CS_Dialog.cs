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

        // show text 
        myTextMesh.text = "Wait, where is my " + g_item.GetName () + "?";
    }

    public Vector3 GetSubmitPosition () {
        return myTransformSubmit.position;
    }

    public CS_Item GetTarget () {
        return myTargetItem;
    }
}
