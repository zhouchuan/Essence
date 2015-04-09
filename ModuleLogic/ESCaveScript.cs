using UnityEngine;
using System.Collections;


public class ESCaveScript : AbsBehaviour {
	
	public	Vector3[] dogPos;
	public  int dogIndex = 0;
	private int checkRadius = 2;
	private bool isFindDog = false;

	protected override void OnLoad ()
	{
		this.PlotCaptionMap = ConfigMap.Instance().CaveCaptionMap;
		this.captionLabel.text = "山洞中...";
		guide.ResetDog(dogPos[dogIndex]);
		dogIndex ++;
	}

	void Start()
	{
		PlaySeriesCaption(0,4,2f);
	}
	
	protected override void OnUpdate ()
	{
		if(CheckPlotPlay())
		{
			this.guide.StopAnimalSound();
		}
		else
		{
			this.guide.PlayAnimalSound(true);
		}

		CheckDog();
		CheckNextLevelTrigger();
		CheckHitBats();
	}

	private void CheckDog()
	{
		if(guide.PlayerInRange() && dogIndex < dogPos.Length)
		{
			guide.ResetDog(dogPos[dogIndex]);
			dogIndex ++;

			if(!isFindDog)
				PlaySeriesCaption(5,8,0f);

			isFindDog = true;
		}
	}
	private bool isSuccess = false;
	private void CheckNextLevelTrigger()
	{
		if(guide.PlayerInRange() && dogIndex == dogPos.Length && !this.player.IsPloting && !isSuccess)
		{
			this.captionLabel.text = "找到Adam，出口在前方";
			isSuccess = true;
			Invoke("NextLevel",3f);
		}
	}

	private void NextLevel()
	{
		Application.LoadLevel(MainBehaviour.townScene);
	}

	private void CheckHitBats()
	{
		Vector3 playerPos = player.GetPlayerPos();
		Collider[] ObjInRange = Physics.OverlapSphere(playerPos, checkRadius);
		foreach(Collider obj in ObjInRange)
		{
			if(obj.tag == "Bat")
			{
				AudioSource batAudio = obj.GetComponent<AudioSource>();
				if(!batAudio.isPlaying)
				{
					obj.GetComponent<AudioSource>().Play();
				}
			}
		}
	}
}
