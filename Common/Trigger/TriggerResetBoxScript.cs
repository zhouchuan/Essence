using UnityEngine;
using System.Collections;

public class TriggerResetBoxScript : MonoBehaviour {

	public GameObject	responseScript;		// The script which will response to my change.
	public string		functionName;		// The function I need call;

	void OnTriggerStay(Collider hit)
	{
		if(hit.gameObject.tag == "Player")
		{
			responseScript.SendMessage(functionName,this.transform.position);
		}
	}
}
