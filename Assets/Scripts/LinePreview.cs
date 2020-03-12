using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePreview : MonoBehaviour {

    LineRenderer lr;
    Vector3 dragStartPoint;

	// Use this for initialization
	void Awake() {
        lr = GetComponent<LineRenderer>();
	}


    public void SetStartPoint(Vector3 worldPosition)
    {
        dragStartPoint = worldPosition;
        lr.SetPosition(0,dragStartPoint);
    }

    public void SetEndPoint(Vector3 worldPosition)
    {
        Vector3 pointOffset = worldPosition - dragStartPoint;
        Vector3 endPoint = transform.position + pointOffset;

        lr.SetPosition(1, endPoint); 
    }
	
}
