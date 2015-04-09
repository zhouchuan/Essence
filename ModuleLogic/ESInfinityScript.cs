using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ESInfinityScript : AbsBehaviour {

	private int nextRoomIndex = 0;
	private int currentRoomIndex = 0;
	public GameObject	boxObj;
	// Use this for initialization
	protected override void OnLoad ()
	{
		this.PlotCaptionMap = ConfigMap.Instance ().InfinityCaptionMap;
	}

	protected override void OnUpdate ()
	{ 

		if(this.boxObj.transform.position.y < -5 )
		{
			this.boxObj.transform.position = new Vector3(this.player.GetPlayerPos().x, 1f, this.player.GetPlayerPos().z);
		}
	}

	void TriggerNextMission()
	{
		if(currentRoomIndex == nextRoomIndex )
		{
			PlotModule.Instance ().SetCaptionByIndex(audioSource,captionLabel,PlotCaptionMap,0);
			nextRoomIndex++;
		}
	}

	void TriggerNextRoom()
	{
		if(currentRoomIndex == nextRoomIndex - 1 )
		{
			currentRoomIndex++;
		}
	}

	void TriggerResetBox(Vector3 boxPos)
	{
		if(this.boxObj.transform.position.y < -3)
		{
			this.boxObj.transform.position = new Vector3(boxPos.x, 1f, boxPos.z);
		}
	}
}
