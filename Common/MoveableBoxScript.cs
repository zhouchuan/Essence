using UnityEngine;
using System.Collections;

public class MoveableBoxScript : MonoBehaviour {

	public Transform boxPos;	// the Postion you want to put the box.
	public Transform fatherObj;	// the Object who carries the box.

	private bool isCarrying = false;
	public bool IsCarrying 
	{
		set { isCarrying = value; } 
		get { return isCarrying; }
	}
	
	void Update () 
	{
		if(IsCarrying)
		{
			transform.localPosition = boxPos.position;
			Vector3 wantedLookDir = fatherObj.position - transform.position;
			transform.rotation = Quaternion.LookRotation(new Vector3(wantedLookDir.x,0, wantedLookDir.z),Vector3.up);
		}
	}
}
