using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Common;

public partial class ESLabScript : AbsBehaviour {

	private enum PlotStatus
	{

	}
	public LabObjController labObjects;
	public MouseLook		mouseLook;
	public UILabel			timeLabel;
	public UISprite			winkSpriteUp;
	public UISprite			winkSpriteDown;
	public Texture			lightBlockTexture;

	public GameObject		destroyPanel;
	public GameObject		restartPanel;
	public GameObject		waterObj;
	public GameObject		rotationPlatObj;
	public GameObject		triggerDropObj;

	public Animator			choiceAni;
	public AudioSource 		audioCountDown;
	public AudioSource		audioWaterDrop;
	public AudioSource		audioWater;

	public List<Light>		alertLightList;
	public Light			spotLightUp;
	public Light			spotLightDown;

	public MovieTexture		moviePass;
	public MovieTexture		movieReject;
	public GameObject		screenPass;
	public GameObject		screenReject;

	private Vector3			restartPos ;
	private Vector3			watchPos ;
	private Vector3			watchRot ;

	private static Vector3	watchPos1 = new Vector3 (-407f,15f,114f);
	private static Vector3	watchRot1 = new Vector3 (-900,0,0);

	private static Vector3	watchPos2 = new Vector3 (-380f,-74.5f,114f);
	private static Vector3	watchRot2 = new Vector3 (-900,0,0);

	private static Vector3	waterPos = new Vector3(8,-22,-90);
	private static Vector3	watchDir = new Vector3 (0, 270,0);
	private static int		timeWink = 8;
	private static int		test2Time = 60;
	private static float	waterLevel = -8.8f;
	private int				countDonwnSecond = 5;

	private TimerScript		cdTimer = new TimerScript();
	private List<MoveStatus>	inputList = new List<MoveStatus>();
	
	private Dictionary<string, bool> plotStatusDic = new Dictionary<string, bool>();
	public Dictionary<string, bool> PlotStatueDic
	{
		set { this.plotStatusDic = value; }
		get { return this.plotStatusDic; }
	}


	protected override void OnLoad ()
	{

		//Screen.showCursor = false;
		this.PlotCaptionMap = ConfigMap.Instance().LabCaptionMap;
//		inputList.Add(MoveStatus.BACKWARD);
		inputList.Add(MoveStatus.FORWARD);
//		inputList.Add(MoveStatus.LEFT);
//		inputList.Add(MoveStatus.RIGHT);

		plotStatusDic.Add("isFinishWink", false);
		plotStatusDic.Add("isFinishMouse", false);
		plotStatusDic.Add("isFinishMove", false);
		plotStatusDic.Add("isMoveMouseX", false);
		plotStatusDic.Add("isMoveMouseY", false);
		plotStatusDic.Add("isStartTest", false);
		plotStatusDic.Add("isFinishTest1", false);
		plotStatusDic.Add("isFinishTest2", false);
		plotStatusDic.Add("isTest2TimeUp", false);
		plotStatusDic.Add("isFinishiTestAI",false);
		plotStatusDic.Add("isGetSofa", false);
		plotStatusDic.Add("isEvaluating", false);
		plotStatusDic.Add("isEndPassed", false);
		plotStatusDic.Add("isEndReject", false );

		this.labObjects.ShowLabTest1 (false);
		this.labObjects.ShowLabTest2 (false);
		this.labObjects.ShowLabTest3 (false);
		this.labObjects.ShowLabTest4 (false);
		this.labObjects.ShowLabEnd1 (false);
		this.labObjects.ShowLabEnd2 (false);
		this.restartPanel.SetActive (false);

		waterObj.transform.localPosition = waterPos;
		Invoke("PlayPlot",1.5f);
	}

	void Start()
	{
		InitWinkAnimation();
	}

	protected override void OnUpdate ()
	{
		if(!this.player.IsPloting)
		{
			PlotMoveMouse();
			PlotMovePhysical();
			FlexibilityTest();

			//MovementTest();
			//CDTimerDoor();
			PlotEvaluate();
		}
		KeybroadListener ();
	}
}