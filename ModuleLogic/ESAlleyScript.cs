using UnityEngine;
using System.Collections;
using Common;

public class ESAlleyScript  : AbsBehaviour{
	
	public Vector3[] npcPos;
	private int npcIndex = 0;
	private bool isGetTower = false;

	protected override void OnLoad ()
	{
		this.PlotCaptionMap = ConfigMap.Instance().AlleyCaptionMap;
		guide.ResetDog(npcPos[npcIndex]);
		npcIndex ++;
	}
	
	protected override void OnUpdate ()
	{
		CheckNPC();
		CheckArriveTower();
	}
	
	private void CheckNPC()
	{
		if(guide.PlayerInRange() && npcIndex < npcPos.Length)
		{
			PlotModule.Instance().SetCaptionByIndex(audioSource,captionLabel,ConfigMap.Instance().AlleyCaptionMap,GameCommon.Randomi(1,2) );
			guide.ResetDog(npcPos[npcIndex]);
			npcIndex ++;
		}
	}

	private void CheckArriveTower()
	{
		if(npcIndex == npcPos.Length && !isGetTower)
		{
			PlaySeriesCaption(3,5,0.0f);
			isGetTower = true;
			Invoke ("CheckNextLevelTrigger",17f);
		}
	}
	
	private void CheckNextLevelTrigger()
	{
//		if(npcIndex == npcPos.Length && PlotModule.Instance().IsPlotPlaying(audioSource) 
//		   && PlotModule.Instance().IsPlotFinished(ConfigMap.Instance().AlleyCaptionMap))
//		{
			Application.LoadLevel(MainBehaviour.labScene);
//		}
	}
	

}
