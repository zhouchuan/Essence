using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class PlayerModule {

	private PlayerModule() { }
	private int unlockNum = 0;

	private static PlayerModule inst = null;
	public static PlayerModule Instance()
	{
		if(inst == null)
		{
			inst = new PlayerModule();
		}
		return inst;
	}

	private int startYear = 0;
	private int startMonth = 0;
	private int startDay = 0;
	public void SetPlayDate()
	{
		if(isFirstPlay)
		{
			this.startYear = System.DateTime.Today.Year;
			this.startMonth = System.DateTime.Today.Month;
			this.startDay = System.DateTime.Today.Day;
		}
	}

	public bool CompareDeathTime()
	{
		int deathYear = this.startYear + 20;
		int deathMonth = this.startMonth;
		int deathDay = this.startDay;

		System.DateTime curretnTime = System.DateTime.Today;

		if(deathYear > curretnTime.Year)
		{
			return true;
		}
		else if(deathMonth > curretnTime.Month)
		{
			return true;
		}
		else if(deathDay > curretnTime.Day)
		{
			return true;
		}

		return false;
	}

	private bool isFirstPlay = true;
	public bool IsFirstPlay
	{
		set{isFirstPlay = value;}
		get{return isFirstPlay;}
	}

	// LevelName player last played.
	private string lastLevelName = "";
	public string LastLevelName
	{
		set { lastLevelName = value; }
		get { return lastLevelName; }
	}

	private bool isChooseLevel = false;
	public bool IsChooseLevel
	{
		set { isChooseLevel = value; }
		get { return isChooseLevel; }
	}

	private int isTestPassed = 0;
	public int IsTestPassed
	{
		set{ isTestPassed = value; }
		get{ return isTestPassed; }
	}

	public List<string> unlockLevelList = new List<string>();
	public void UnlockLevel(string levelName)
	{
		if(!unlockLevelList.Contains(levelName))
		{
			Debug.Log("...Unlock leve : "+levelName);
			unlockLevelList.Add(levelName);
			return;
		}
		Debug.Log("...Has been unlocked");
	}

	public bool PlayerDataExist()
	{	
		string filepath = Application.persistentDataPath;
		string path = Path.Combine(filepath, "GameData");
		return(File.Exists(path));
	}
	
	// save data to file
	public void WritePlayerData()
	{
		string filepath = Application.persistentDataPath;
		string path = Path.Combine(filepath, "GameData");
		
		FileStream stream = null;
		BinaryWriter writer = null;
		try
		{
			stream = new FileStream(path, FileMode.Create, FileAccess.Write);
			writer = new BinaryWriter(stream);
			writer.Write(isFirstPlay);
			writer.Write(isTestPassed.ToString());
			writer.Write(lastLevelName);
			writer.Write(unlockLevelList.Count.ToString());

			foreach(string levelName in unlockLevelList)
			{
				Debug.Log("write: "+levelName);
				writer.Write(levelName);
			}

		}
		catch
		{
			Debug.Log("Write GameData Fail");
		}
		finally
		{
			writer.Close();
			stream.Close();
		}
	}
	
	// load data from file
	public void LoadPlayerData()
	{
		string filepath = Application.persistentDataPath;
		string path = Path.Combine(filepath, "GameData");
		
		FileStream stream = null;
		BinaryReader reader = null;
		try
		{
			stream = new FileStream(path,FileMode.Open);
			reader = new BinaryReader(stream);
			isFirstPlay = reader.ReadBoolean();
			isTestPassed = int.Parse(reader.ReadString());
			lastLevelName = reader.ReadString();
			unlockNum = int.Parse(reader.ReadString());
			//Debug.Log("---read:"+unlockNum + "==="+lastLevelName);
			for(int i = 0; i < unlockNum; i++)
			{
				string levelName = reader.ReadString();
				unlockLevelList.Add(levelName);
				//Debug.Log(levelName+"|||"+unlockLevelList[i]);
			}
		}
		catch
		{
			Debug.Log("GameData File Exception");
		}
		finally
		{
			reader.Close();
			stream.Close();
		}
	}
}
