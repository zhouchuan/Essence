using UnityEngine;
using System.Collections;

public partial class ESLoginScript : AbsBehaviour
{
	void OnStartListener()
	{
		PlayerModule.Instance().SetPlayDate ();
		PlayerModule.Instance().IsFirstPlay = false;
		PlayerModule.Instance().IsTestPassed = 0;

		Application.LoadLevel(MainBehaviour.startScene);

		Debug.Log("Game : Start!");
	}
	
	void OptionBtnListener()
	{
		
	}
	
	void CreditBtnListener()
	{
		creditPanel.ShowCredit();
	}
	
	void ExitBtnListener()
	{
		Debug.Log("Game Over!");
		Application.Quit();
	}

	// Load - Choose Level Room
	void OnChooseLevelListener()
	{
		PlayerModule.Instance().IsChooseLevel = false;
		menuPanel.ShowMenuPanel(false);
		logoPanel.ShowLogoPanel(false);
		selectPanel.ShowSelectPanel(true);
		Debug.Log(PlayerModule.Instance().unlockLevelList.Count);
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
			if(PlayerModule.Instance().unlockLevelList.Contains(MainBehaviour.labScene))
			{
				selectPanel.ShowLabIcon();
			}
			if(PlayerModule.Instance().unlockLevelList.Count == 6)
			{
				selectPanel.ShowPathTexture(true);
			}
			else
			{
				selectPanel.ShowPathTexture(false);
				selectPanel.SetPathSprite("select_path"+PlayerModule.Instance().unlockLevelList.Count);
			}
		}
	}

	// Continue to play last sence you have played
	void OnContinueListener()
	{
		if(PlayerModule.Instance().LastLevelName != null)
		{
			Application.LoadLevel(PlayerModule.Instance().LastLevelName);
		}
		else
		{
			Application.LoadLevel(MainBehaviour.startScene);
		}
	}
	
	void OnLeftArrowListener()
	{
		if(menuPanel.GetTweenObjPos().x > -200)
		{
			menuPanel.SetTweenPositionTo(new Vector3(-100,0,0));
			//menuPanel.SetArrowBtnColor(true,enableColor);
		}
	}

	void OnRightArrowListener()
	{
		if(menuPanel.GetTweenObjPos().x < 0)
		{
			menuPanel.SetTweenPositionTo(new Vector3(100,0,0));
			//menuPanel.SetArrowBtnColor(false,enableColor);
		}
	}

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
