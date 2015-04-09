using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Common;

public abstract class AbsBehaviour : MonoBehaviour
{
	public UILabel				captionLabel;
	public ESPauseMenuScript	pausePanel;
	public AudioClipContainer	audioContainer;
	public AudioSource			audioSource;
	public AudioSource			audioEnvironment;
	public GameObject			blockObj;
	public ESPlayer				player;
	public ESDog				guide;
	public List<string> PlotCaptionMap { set; get; }

	private bool				isLabelEpt = true;
	private bool				isPrelogue = true;

	protected virtual void OnLoad() { }
	protected virtual void OnUpdate() { }

	static AbsBehaviour() { }


	void Awake()
	{
		this.PlotCaptionMap = new List<string>();
		PlotModule.Instance().ResetCaptionIndex();
		PlotModule.Instance().AudioContainer = audioContainer;
		if(Application.loadedLevelName != MainBehaviour.loginScence )
		{
			PlayerModule.Instance().LastLevelName = Application.loadedLevelName;
			PlayerModule.Instance().UnlockLevel(Application.loadedLevelName);
			Debug.Log(Application.loadedLevelName);
		}

		ClearLabel();
		OnLoad();
	}

	// Update is called once per frame
	void Update () 
	{
		CheckPlotPlay();
		EscapeListener();
		OnUpdate();
	}

	public void PrelogueCall(string funcName)
	{
		if(this.audioEnvironment != null && isPrelogue)
		{
			Vector3 playerPos = this.player.GetPlayerPos();
			Vector3 environmtntPos = audioEnvironment.gameObject.transform.localPosition;
			float audioRange = audioEnvironment.maxDistance - 5;
			if(Vector3.Distance(environmtntPos,playerPos) < audioRange)
			{
				this.SendMessage(funcName);
				isPrelogue = false;
			}
		}
	}

	public bool CheckPlotPlay()
	{
		if(PlotModule.Instance().IsPlotPlaying(audioSource) && this.player != null)
		{
			this.player.IsPloting = true;
			isLabelEpt = false;
			return true;
		}
		else if(!isLabelEpt)
		{
			ClearLabel();
			player.IsPloting = false;
			isLabelEpt = true;
		}
		return false;
	}

	public void PlaySeriesCaption(int from, int to, float triggerTimes)
	{
		PlotModule.Instance().CaptionIndex = from;
		for(int i = 0; i < to-from+1; i++)
		{
			Invoke("PlayPlot",triggerTimes);

			triggerTimes += audioContainer.AudioLengthList[i+from];
			//Debug.Log("index :  "+PlotModule.Instance().CaptionIndex+"  "+(i+from) +"  "+triggerTimes +"  " +Time.time);
		}
	}

	public void ClearLabel()
	{
		if(this.captionLabel != null)
			this.captionLabel.text = "";
	}

	public void PlayPlot()
	{
		PlotModule.Instance().SetPlotCaption(audioSource,captionLabel,PlotCaptionMap);
		//Debug.Log("Caption index:" + PlotModule.Instance().CaptionIndex);
	}

	private bool canBack = true;
	public void TriggerBlockWall()
	{
		if(this.blockObj != null && this.player.IsTriggerBlockWall && canBack)
		{
			this.blockObj.transform.localPosition += new Vector3(0,2,0);
			this.player.transform.localPosition += this.player.transform.forward;
			this.canBack = false;
		}
	}

	public MoveStatus GetMoveStatus()
	{
		//move forward and rotate
		float verticalDir = Input.GetAxis("Vertical");
		float horizontalDir = Input.GetAxis("Horizontal");
		
		if(verticalDir > 0 )
		{
			return MoveStatus.FORWARD;
		}
		else if(verticalDir < 0 )
		{
			return MoveStatus.BACKWARD;
		}
		
		if(GetLeftDown() && Input.GetAxis("Vertical") == 0 )
		{
			return MoveStatus.LEFT;
		}
		else if(GetRightDown() && Input.GetAxis("Vertical") == 0 )
		{
			return MoveStatus.RIGHT;
		}
		return MoveStatus.NONE;
		
	}

	public bool GetLeftDown()
	{
		return (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) ;
	}
	
	public bool GetRightDown()
	{
		return (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) ;
	}

	public void PlayAudio(int audioIndex)
	{
		if(audioIndex < this.audioContainer.audioPlotList.Count && !this.audioSource.isPlaying)
		{
			this.audioSource.clip = this.audioContainer.audioPlotList[audioIndex];
			this.audioSource.Play();
			Debug.Log("bat");
		}
		else
		{
			Debug.Log("Warnning! Audio out of range");
		}
	}

	public void InputCheck()
	{
		if(this.player.IsRestrictPath )
		{
			if(MoveStatus.FORWARD == GetMoveStatus() && !CheckPlotPlay())
			{
				this.player.IsControllable = true;
			}
			else
			{
				this.player.IsControllable = false;
			}
		}
		if(this.player.IsTriggerBlockWall && this.blockObj != null)
		{
			this.player.IsRestrictPath = false;
			TriggerBlockWall();
		}
	}

	// response to show the pause menu
	private void EscapeListener()
	{
		if(Input.GetKeyUp(KeyCode.Escape) && Application.loadedLevelName != MainBehaviour.loginScence)
		{
			this.pausePanel.gameObject.SetActive(true);	
			//Screen.showCursor = true;
		}
	}
}
