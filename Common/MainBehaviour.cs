using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Common;

public class MainBehaviour : MonoBehaviour
{
	/* main varible */
	public static string	loginScence = "PhaseMenuLogin";
	//public static string	chooseScene = "PhaseMenuChoose";
	public static string	startScene = "PhaseBlindTeach";
	public static string	forestScene = "PhaseBlindForest";
	public static string	caveScene = "PhaseBlindCave";
	public static string	townScene = "PhaseBlindTown";
	public static string 	alleyScene = "PhaseBlindAlley";
	public static string	labScene = "PhaseLab";
	public static string	infinityScene = "PhaseInfinite";


	private Camera camera3D = null;
	public Camera Camera3D
	{
		set{ this.camera3D = value;}
		get{ return this.camera3D;}
	}
	
	private bool hasCamera = false;

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		GenerateFile();
		ConfigMap.Instance();
		PlayerModule.Instance();
		PlotModule.Instance();

		InitGameData();
		PlayerModule.Instance().LoadPlayerData();
	}

	void Start()
	{
		Application.LoadLevel(loginScence);
	}
	
	void Update () 
	{	
		if( camera3D == null && Application.loadedLevelName != MainBehaviour.labScene)
		{
			hasCamera = false;
			InitCamera();
		}
		CameraControlListener();
	}

	private void InitGameData()
	{
		if(!PlayerModule.Instance().PlayerDataExist())
		{
			Debug.Log("Create Game Data File!");
			PlayerModule.Instance().WritePlayerData();
		}
	}

	public void InitCamera () 
	{
		if(GameObject.Find("Main Camera") != null && !hasCamera )
		{
			hasCamera = true;
			camera3D = GameObject.Find("Main Camera").GetComponent<Camera>();
			camera3D.GetComponent<Camera>().cullingMask = 0;
		}
	}

	void OnApplicationQuit()
	{
		PlayerModule.Instance().WritePlayerData();
		Debug.Log("Game : Quit & Write game info! ");
	}

	void GenerateFile()
	{
		XMLConvertor xmlc = new XMLConvertor();
		if(xmlc.LoadFile("/Script/DataFile/ESCaptionsS1.xml"))
		{
			xmlc.GenerateDataFile("SoloPlot","GamePlot/me");
			xmlc.GenerateDataFile("GuidePlot","GamePlot/voiceover");
		}

		if(xmlc.LoadFile("/Script/DataFile/ESCaptionsS2.xml"))
		{
			xmlc.GenerateDataFile("ForestPlot","GamePlot/speak");
		}

		if(xmlc.LoadFile("/Script/DataFile/ESCaptionsS3.xml"))
		{
			xmlc.GenerateDataFile("CavePlot","GamePlot/me");
		}

		if(xmlc.LoadFile("/Script/DataFile/ESCaptionsS4.xml"))
		{
			xmlc.GenerateDataFile("TownPlot","GamePlot/me");
			xmlc.GenerateDataFile("AlleyPlot","GamePlot/alley");
		}

		if(xmlc.LoadFile("/Script/DataFile/ESCaptionsS5.xml"))
		{
			xmlc.GenerateDataFile("LabPlot","GamePlot/me");
		}

		if(xmlc.LoadFile("/Script/DataFile/ESCaptionsS5.xml"))
		{
			xmlc.GenerateDataFile("InfinityPlot","GamePlot/end");
		}

		if(xmlc.LoadFile("/Script/DataFile/ESCaptionsSpecial.xml"))
		{
			xmlc.GenerateDataFile("Special","GamePlot/me");
		}
	}

	void CameraControlListener()
	{
		if(Input.GetKeyDown("q") && hasCamera)
		{
			if(camera3D.GetComponent<Camera>().cullingMask == 0)
			{
				camera3D.GetComponent<Camera>().cullingMask = -1;
			}
			else
			{
				camera3D.GetComponent<Camera>().cullingMask = 0;
			}
		}
	}

}
