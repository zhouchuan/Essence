using UnityEngine;
using System.Collections;

public partial class ESSelectScript : MonoBehaviour {


	void OnEnterBeachListener()
	{
		Application.LoadLevel(MainBehaviour.startScene);
	}

	void OnEnterForestListener()
	{
		Application.LoadLevel(MainBehaviour.forestScene);
	}

	void OnEnterCaveListener()
	{
		Application.LoadLevel(MainBehaviour.caveScene);
	}

	void OnEnterTownListener()
	{
		Application.LoadLevel(MainBehaviour.townScene);
	}

	void OnEnterAlleyListener()
	{
		Application.LoadLevel(MainBehaviour.alleyScene);
	}

	void OnEnterLabListener()
	{
		Application.LoadLevel(MainBehaviour.labScene);
	}

	void OnBackListener()
	{
		Application.LoadLevel(MainBehaviour.loginScence);
	}

}
