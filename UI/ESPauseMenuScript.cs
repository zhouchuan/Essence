using UnityEngine;
using System.Collections;

public class ESPauseMenuScript : MonoBehaviour 
{

	// Return to MainMenu UI
	void OnReturnMenuListener()
	{
		Application.LoadLevel(MainBehaviour.loginScence);
	}

	// Return to Select UI
	void OnReturnSelectListener()
	{
		PlayerModule.Instance().IsChooseLevel = true;
		Application.LoadLevel(MainBehaviour.loginScence);
	}

	// Continue game
	void OnContinueListener()
	{
		this.gameObject.SetActive(false);
	}
}