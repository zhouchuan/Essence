using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Common;

public class ESForestScript : AbsBehaviour {

	public Vector3 oriWolfPos;
	public Vector3 oriPlayerPos;
	public AudioSource environmentAudio;
	public ESDog	wolf;
	public List<KeyCode> playerRunDir;
	public float runTime = 0.3f;
	public float durationTime = 4.0f;

	private bool isTriggerNight = false;
	private bool isTriggerWolf = false;
	private bool isTriggerRun = false;
	private List<string> soundCollections = new List<string>();
	private static int audioCollectionNum = 3;
	private float runStartTime = 0;
	private int runOderIndex = 0;
	private int rightTurnCount = 0;
	private int wrongTurnCount = 0;
	private float checkRadius = 3.0f;
	private int gameOverIndex;

	public List<AudioSource> audioAimList;
	public AudioSource audioDog;
	public AudioSource audioFall;
	public AudioSource audioYeal;

	protected override void OnLoad ()
	{
		this.player.IsRestrictPath = true;
		this.PlotCaptionMap = ConfigMap.Instance().ForestCaptionMap;
	}

	protected override void OnUpdate ()
	{
		PrelogueCall("PlayPrelogue");
		CheckCollectSound();
		InputCheck();
		CheckWolf();

		if(isTriggerWolf && isTriggerRun)
		{
			CheckPlayerRunOrder();
			CheckGameOver();
		}

		//voiceover tips
		if(isTriggerWolf && !isTriggerRun && PlotModule.Instance().CaptionIndex >= 8 && !CheckPlotPlay())
		{
			if(Input.anyKeyDown)
			{
				isTriggerRun = true;
				runStartTime = Time.time;
			}
		}
	}

	// check is player collect all the audio source
	private void CheckCollectSound()
	{
		Vector3 playerPos = player.GetPlayerPos();
		Collider[] ObjInRange = Physics.OverlapSphere(playerPos, checkRadius);
		foreach(Collider obj in ObjInRange)
		{
			if(obj.tag == "AudioCollection" && !soundCollections.Contains(obj.name))
			{
				soundCollections.Add(obj.name);
				PlayPlot();
			}
		}

		if(soundCollections.Count == audioCollectionNum && !isTriggerNight)
		{
			isTriggerNight = true;
			environmentAudio.Stop();
			environmentAudio = GameObject.Find("audio_forest_night").GetComponent<AudioSource>();
			Common.GameCommon.PlayAudio(environmentAudio);
		}
	}

	private void PlayPrelogue()
	{
		PlaySeriesCaption(0,2,0f);
	}

	// check is the condition satisify trigger wolf
	private void CheckWolf()
	{
		if(wolf.PlayerInRange(10) && soundCollections.Count == audioCollectionNum && !isTriggerWolf &&!isSuccess)
		{
			wolf.PlayAnimalSound(true);
			SetAudioCollection(false);
			isTriggerWolf = true;
			int index = PlotModule.Instance().CaptionIndex;
			gameOverIndex = index;
			PlaySeriesCaption(index,index+4,3f);
		}
	}

	void GotoNextLevel()
	{
		Application.LoadLevel(MainBehaviour.caveScene);
	}


	void CheckPlayerRunOrder()
	{
		if((Time.time - runStartTime) % durationTime < runTime)
		{
			player.IsControllable = false;
		}
		else if(runOderIndex < playerRunDir.Count)
		{
			player.IsControllable = true;
			//this.captionLabel.text = playerRunDir[runOderIndex].ToString();
			if(playerRunDir[runOderIndex] == KeyCode.LeftArrow)
			{
				audioDog.clip = audioContainer.audioPlotList[13];
				if(!audioDog.isPlaying)
				{
					audioDog.Play();
				}
				PlotModule.Instance().PlayAudioByIndex(audioSource,11);
			}
			else
			{
				audioDog.clip = audioContainer.audioPlotList[12];
				if(!audioDog.isPlaying)
				{
					audioDog.Play();
				}
				PlotModule.Instance().PlayAudioByIndex(audioSource,10);
			}

			if(Input.GetKeyDown(playerRunDir[runOderIndex]))//LeftArrow
			{
				//wolf sound decrease;
				wolf.audioBark.volume -= 0.15f;
				rightTurnCount ++;
				runOderIndex++;
				runStartTime = Time.time;
			}
			else if(Input.anyKeyDown && !Input.GetKeyDown(playerRunDir[runOderIndex]))
			{
				PlotModule.Instance().PlayAudioByIndex(audioSource,14);
				runOderIndex++;
				wrongTurnCount ++;
				runStartTime = Time.time;
				wolf.audioBark.volume += 0.15f;

			}
		}

		//judge out of time
		if(Time.time - runStartTime > durationTime)
		{
			runOderIndex++;
			wrongTurnCount ++;
			runStartTime = Time.time;
			wolf.audioBark.volume += 0.1f;
		}
	}

	private bool isSuccess = false;
	// trigger wolf follow player
	private void CheckGameOver()
	{
		if(wrongTurnCount >= 3)
		{
			wrongTurnCount = 0;
			rightTurnCount = 0;
			runOderIndex = 0;
			player.ResetPlayerPos(oriPlayerPos);
			isTriggerWolf = false;
			isTriggerRun = false;
			wolf.StopAnimalSound();
			PlotModule.Instance().CaptionIndex = gameOverIndex;
			this.captionLabel.text = "GameOver! Please run again!";
			Invoke ("ClearLabel",3.0f);
			Debug.Log("Game Over! reset player position");
		}
		if(rightTurnCount >= 5 && !isSuccess)
		{
			Debug.Log("Run Success");
			this.isTriggerWolf = false;
			this.isTriggerRun = false;
			this.isSuccess = true;
			this.wolf.StopAnimalSound();
			this.audioSource.Stop();
			this.audioYeal.Play();
			this.audioFall.Play();
			Invoke("GotoNextLevel",3f);
		}

		//play plot - get into next level
	}
	
	private bool IsAudioAimPlaying()
	{
		foreach(AudioSource aim in audioAimList)
		{
			if(!aim.isPlaying)
			{
				return false;
			}
			else
			{
				continue;
			}
		}
		return true;
	}

	private void SetAudioCollection(bool isPlay)
	{
		if(audioAimList.Count <= 0)
		{
			Debug.Log("AudioList is Empty!");
			return;
		}
		if(isPlay)
		{
			if(!IsAudioAimPlaying() )
			{
				foreach(AudioSource aim in audioAimList)
				{
					aim.Play();
				}
			}
		}
		else
		{
			foreach(AudioSource aim in audioAimList)
			{
				aim.Stop();
			}
		}
	}
}
