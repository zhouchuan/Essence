using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

namespace Common
{
	public enum MoveStatus
	{
		FORWARD,
		BACKWARD,
		RIGHT,
		LEFT,
		NONE
	};

	public enum TweenType
	{
		TweenPosition,
		TweenScale,
		TweenAlpha,
		TweenRotation
	}



	public class GameCommon
	{
		public static int DoorCDTime = 3;

		private static string plotPath = Application.streamingAssetsPath + "/";
		public static List<string> ReadCaptionFromFile(string fileName)
		{
			FileStream fs = new FileStream(plotPath + fileName, FileMode.OpenOrCreate, FileAccess.Read);
			List<string> captions = new List<string>();
			BinaryReader br = new BinaryReader(fs);

			int wordsCount = int.Parse(br.ReadString());
			
			for(int i = 0; i < wordsCount; i++)
			{
				captions.Add(br.ReadString());
			}

			fs.Close();  
			br.Close(); 
			//Debug.Log(fileName + captions.Count);
			return captions;
		}


		public static int Randomi(int from, int to)
		{
			return (int)(Time.time%(to-from+1) + from);
		}

		public static void PlayAudio(AudioSource source)
		{
			if(!source.isPlaying)
			{
				source.Play();
			}
		}

		public static void SetTween(GameObject go, TweenType type, bool isEnabled)
		{
			switch(type)
			{
			case TweenType.TweenAlpha:
				go.GetComponent<TweenAlpha>().Reset();
				go.GetComponent<TweenAlpha>().enabled = isEnabled;
				break;
			case TweenType.TweenPosition:
				go.GetComponent<TweenPosition>().Reset();
				go.GetComponent<TweenPosition>().enabled = isEnabled;
				break;
			}
		}
	}

	public class CaptionData
	{
		public string Name {get; set; }
		public List<string> Captions {get; set; }
		public List<string> AudioName {get; set;}
		public List<string> TriggerTime {get; set;}
		public List<List<string>> CaptionConfig{get; set;}

		public CaptionData(string fileName)
		{
			Name = fileName;
			Captions = new List<string>();
			AudioName = new List<string>();
			TriggerTime = new List<string>();
			CaptionConfig = new List<List<string>>();
		}
	}

	public class XMLConvertor {

		public XmlDocument	xmlDoc;
		public XmlElement	root;
		public XmlWriter	writer;
		public string		outputDir = @"/Users/zhouxiaochuan/Desktop";
		private static string plotPath = Application.dataPath + "/";
		private static string outputPath = Application.streamingAssetsPath + "/";
		public XMLConvertor()
		{

		}
		//load file
		public bool LoadFile(string fileName)
		{
//			TextAsset textAsset = (TextAsset) Resources.Load(fileName, typeof(TextAsset));
//			
//			xmlDoc = new XmlDocument();
//			
//			xmlDoc.Load(new StringReader(textAsset.text));
			string filePath = plotPath + fileName;

			if (File.Exists (filePath)) 
			{  
				xmlDoc = new XmlDocument();  
				xmlDoc.Load(filePath);

				Debug.Log("load success!");
				return true;
			}

			Debug.Log("load fail");
			return false;

		}

		public List<string> ReadDataFromXML(string nodePath)
		{
			List<string> contains =  new List<string>();
			XmlNodeList nodeList = xmlDoc.SelectNodes(nodePath);
			foreach(XmlNode cell in nodeList)
			{
				contains.Add(cell.InnerText);
				//Debug.Log(nodePath+" value: "+cell.InnerText);
			}
			Debug.Log("Read ");
			return contains;
		}

		public void GenerateDataFile(string fileName, string nodeName)
		{
			CaptionData cd = new CaptionData(fileName);
			cd.Captions = ReadDataFromXML(nodeName);

			FileStream fs = new FileStream(outputPath + fileName, FileMode.OpenOrCreate, FileAccess.Write);
//			StreamWriter sw = new StreamWriter(fs);
			BinaryWriter bw = new BinaryWriter(fs);

			bw.Write(cd.Captions.Count.ToString());

			foreach(string captionWord in cd.Captions)
			{
				bw.Write(captionWord);
				//sw.WriteLine(captionWord);
			}
			Debug.Log("Write done!");
			fs.Close();  
			bw.Close(); 
		}

		public void GenerateDataFile(string fileName, string captionNode, string audioNode, string triggerNode)
		{
			CaptionData cd = new CaptionData(fileName);
			cd.Captions = ReadDataFromXML(captionNode);
			cd.AudioName = ReadDataFromXML(audioNode);
			cd.TriggerTime = ReadDataFromXML(triggerNode);
			cd.CaptionConfig.Add(cd.Captions);
			cd.CaptionConfig.Add(cd.AudioName);
			cd.CaptionConfig.Add(cd.TriggerTime);

			FileStream fs = new FileStream(outputPath + fileName, FileMode.OpenOrCreate, FileAccess.Write);
			BinaryWriter bw = new BinaryWriter(fs);
			
			bw.Write(cd.Captions.Count.ToString());
			
			foreach(string captionWord in cd.Captions)
			{
				bw.Write(captionWord);
				//sw.WriteLine(captionWord);
			}
			Debug.Log("Write done!");
			fs.Close();  
			bw.Close(); 
		}


	}
}