using UnityEngine;
using System.Collections;

public class TriggerNextScript : MonoBehaviour {
	public GameObject receiver;
	public string functionName;

	void OnTriggerEnter(Collider hit)
	{
		if(hit.gameObject.tag == "Player")
		{
			receiver.SendMessage(functionName);
		}
	}
	
}
