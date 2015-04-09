using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ESTownScript : AbsBehaviour 
{
	public float checkRadius = 1;
	private bool isTriggerMarket = false;

	protected override void OnLoad ()
	{
		this.PlotCaptionMap = ConfigMap.Instance().TownCaptionMap;
	}

	void Start()
	{
		this.player.IsRestrictPath = true;
	}

	private void PlayPrelogue()
	{
		PlaySeriesCaption(0,1,0f);
	}

	protected override void OnUpdate ()
	{
		PrelogueCall("PlayPrelogue");
		CheckCollision();
		InputCheck();

		if(PlotModule.Instance().CaptionIndex == 12 && !CheckPlotPlay())
		{
			Application.LoadLevel(MainBehaviour.alleyScene);
		}
	}

//	private void PlayPlot()
//	{
//		PlotModule.Instance().SetPlotCaption(audioSource,captionLabel,plotCaptionMap);
//	}

	void CheckCollision()
	{
		Vector3 playerPos = player.GetPlayerPos();
		Collider[] ObjInRange = Physics.OverlapSphere(playerPos, checkRadius);
		foreach(Collider obj in ObjInRange)
		{
			if(obj.tag == "NPC" )
			{
				obj.GetComponent<ESNPC>().Speak();
			}

			if(obj.name == "triggerMarket" && !isTriggerMarket)
			{
				PlaySeriesCaption(2,5,0f);

				Invoke("FinalCaption",20);

				isTriggerMarket = true;
			}
		}
	}

	void FinalCaption()
	{
		PlaySeriesCaption(6,11,0f);
	}
	
}
