using UnityEngine;
using System.Collections;

public class TriggerMissionScript : MonoBehaviour {

	public MoveableBoxScript	box;
	public GameObject	receiver;
	public string		functionName;

	void OnTriggerEnter(Collider hit)
	{
		if(hit.gameObject.tag == "MoveableBox" && !box.IsCarrying)
		{
			receiver.SendMessage(functionName);
		}
	}
}
