using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorMovement : MonoBehaviour {
	
	void Update () {
        var mult = Input.GetKey(KeyCode.LeftShift) ? 30f : 10f;
        var move = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical"));
        
        var shift = transform.rotation * move * Time.deltaTime* mult;
        shift.y = 0;
        transform.position -= new Vector3(0,Input.mouseScrollDelta.y * mult/5f,0);
        transform.position += shift;
    }
}
