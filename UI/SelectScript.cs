using UnityEngine;
using System.Collections;

public class SelectScript : MonoBehaviour {

	public GameObject pathTexture;
	public UISprite pathSprite;
	public UISprite iconBeach;
	public UISprite iconForest;
	public UISprite iconCave;
	public UISprite iconTown;
	public UISprite iconAlley;
	public UISprite iconLab; 

	public void ShowSelectPanel(bool isShow)
	{
		this.gameObject.SetActive(isShow);
	}

	public void ShowPathTexture(bool isShow)
	{
		this.pathTexture.SetActive(isShow);
		this.pathSprite.gameObject.SetActive(!isShow);
	}

	public void SetPathSprite(string pathName)
	{
		this.pathSprite.spriteName = pathName;
	}
	
	public void ShowBeachIcon()
	{
		this.iconBeach.gameObject.SetActive(true);
	}
	
	public void ShowForestIcon()
	{
		this.iconForest.gameObject.SetActive(true);
	}
	
	public void ShowCaveIcon()
	{
		this.iconCave.gameObject.SetActive(true);
	}
	
	public void ShowTownIcon()
	{
		this.iconTown.gameObject.SetActive(true);
	}
	
	public void ShowAlleyIcon()
	{
		this.iconAlley.gameObject.SetActive(true);
	}
	
	public void ShowLabIcon()
	{
		this.iconLab.gameObject.SetActive(true);
	}
	
	public void SetIconSprite(UISprite icon, string iconName)
	{
		icon.spriteName = iconName;
	}
}
