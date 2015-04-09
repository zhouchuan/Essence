using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Common;

public class ESGuide : AbsBehaviour {

	public ESTipsScript		tipsPanel;
	public List<Vector3>	dogPos;
	public List<MoveStatus>	inputOrder;
	private int				inputIndex = 0;
	private int				dogPosIndex = 0;
	private bool			isTrigger = true;

	protected override void OnLoad()
	{
		this.PlotCaptionMap = ConfigMap.Instance ().GuideCaptionMap;
		captionLabel.text = "";
		Invoke("Init",1.0f);
	}

	protected override void OnUpdate()
	{
		CheckTrigger();
		CheckPlayerInput();
		CheckEnterNextRoom();
	}

	private void Init()
	{
		this.player.IsRestrictPath = true;
		guide.SetDogPosition(dogPos[dogPosIndex++]);
		PlotModule.Instance().SetPlotCaption(audioSource, captionLabel, ConfigMap.Instance().GuideCaptionMap);
		tipsPanel.ShowForwardTip (true);
	}

	//check moving, set new dog position
	private void CheckTrigger()
	{
		if(guide.PlayerInRange() && dogPosIndex < dogPos.Count)
		{
			tipsPanel.ShowForwardTip (false);
			this.guide.StopAnimalSound();
			//set position
			guide.SetDogPosition(dogPos[dogPosIndex++]);
			inputIndex++;
			Invoke("PlayDogBark",1);
			//set caption
			if(isTrigger && PlotModule.Instance().CaptionIndex < PlotModule.Instance().AudioContainer.audioPlotList.Count)
			{
				isTrigger = false;
				PlotModule.Instance().SetPlotCaption(audioSource, captionLabel, ConfigMap.Instance().GuideCaptionMap);
			}
			return;
		}
		else
		{
			isTrigger = true;
		}
	}

	//check keyboard operation
	private void CheckPlayerInput()
	{
		if(inputIndex < inputOrder.Count)
		{
			// if current input is correct - inputIndex++, can control
			MoveStatus ms  = GetMoveStatus();
			this.player.moveStatus = ms;
			
			if( ms == inputOrder[inputIndex])
			{
				this.player.IsControllable = true;
				if(ms == MoveStatus.RIGHT)
				{

					this.player.transform.Rotate(Vector3.up, 90);
					inputIndex ++;
					return;
				}
				else if(ms == MoveStatus.LEFT)
				{
					this.player.transform.Rotate(Vector3.down, 90);
					inputIndex ++;
					return;
				}
			}
			else
			{
				this.player.IsControllable = false;
			}
		}
		else
		{
			this.player.IsRestrictPath = false;
		}
	}

	private void CheckEnterNextRoom()
	{
		//finished guide part - go to Next sence
		if(guide.PlayerInRange() && dogPosIndex >= dogPos.Count)
		{
			this.guide.StopAnimalSound();
			PlotModule.Instance().SetPlotCaption(audioSource, captionLabel, ConfigMap.Instance().GuideCaptionMap);
			Invoke ("NextLevel", PlotModule.Instance().CurrentAudioLength);
		}
	}

	private void NextLevel()
	{
		Debug.Log ("Finished! - GuideLevel");
		Application.LoadLevel("PhaseBlindForest");
	}

	private void PlayDogBark()
	{
		this.guide.PlayAnimalSound(true);
	}
}
