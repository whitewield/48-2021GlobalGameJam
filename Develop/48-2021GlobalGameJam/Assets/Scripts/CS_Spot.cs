using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Spot : MonoBehaviour {
    [SerializeField] float myRadius = 0.5f;

    public Vector3 GetPosition () {
        Vector3 t_randomPosition = this.transform.position + (Vector3)Random.insideUnitCircle * myRadius;
        return t_randomPosition;
    }
}
