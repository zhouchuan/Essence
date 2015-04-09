using UnityEngine;
using System.Collections;

public partial class ESSelectScript : MonoBehaviour {

	public SelectScript selectPanel;

	void Awake ()
	{
		//Debug.Log(PlayerModule.Instance().unlockLevelList.Count);
		// Init Select Panel
		if(PlayerModule.Instance().unlockLevelList.Count > 0)
		{
			if(PlayerModule.Instance().unlockLevelList.Contains(MainBehaviour.startScene))
			{
				selectPanel.ShowBeachIcon();
			}
			if(PlayerModule.Instance().unlockLevelList.Contains(MainBehaviour.forestScene))
			{
				selectPanel.ShowForestIcon();
			}
			if(PlayerModule.Instance().unlockLevelList.Contains(MainBehaviour.caveScene))
			{
				selectPanel.ShowCaveIcon();
			}
			if(PlayerModule.Instance().unlockLevelList.Contains(MainBehaviour.townScene))
			{
				selectPanel.ShowTownIcon();
			}
			if(PlayerModule.Instance().unlockLevelList.Contains(MainBehaviour.alleyScene))
			{
				selectPanel.ShowAlleyIcon();
			}
			selectPanel.SetPathSprite("select_path"+PlayerModule.Instance().unlockLevelList.Count);
		}
	}
	

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.LoadLevel(MainBehaviour.loginScence);
		}
	}
}
