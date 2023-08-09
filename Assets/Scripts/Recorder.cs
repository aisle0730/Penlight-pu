using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;

public class Recorder : MonoBehaviour
{
	//コントローラ
	public GameObject player;

    //　現在記憶しているかどうか
    public bool isRecord;
	//　保存するデータの最大数
	public int maxDataNum = 60000;
	//　記録間隔
	public float recordDuration = 0.01f;
	//　経過時間
	public float elapsedTime = 0f;
	//　ゴーストデータ
	public GhostData ghostData;
	//　再生中かどうか
	public bool isPlayBack;
	//　ゴースト用キャラ
	public GameObject ghost;
	//　ゴーストデータが1周りした後の待ち時間
	[SerializeField]
	public float waitTime = 2f;
	//　保存先フォルダ
	private string saveDataFolder;
	//　保存ファイル名
	private string saveFileName = "/tomaranai.dat";

	//デバッグ用UI
	public Text text;

	public Text value;

	public Text vector3;

    

	public OVRInput.Controller controller;


	public Transform handAnchor;



	public GameObject penlightClone;

	public Vector3 eyeAnchorDiffernce;

	public bool isStartGhost = false;

	public static Recorder instance;


	//曲開始地点
	public float startTime;
	//曲終了地点
	public float finishTime;


    public void Awake()
    {
        if(instance == null)
        {
			instance = this;
        }
    }


	void Update()
	{

		if (Input.GetKeyDown(KeyCode.L) || OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch)) 
		{
			StopGhost();
		}

		if (Input.GetKeyDown(KeyCode.M) || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
			Music.instance.PlayMusic();
        }


		Debug.Log("StartGhost");

		if (isRecord)
		{
			
			elapsedTime += Time.deltaTime;
			value.text = elapsedTime.ToString();

			if (elapsedTime >= recordDuration)
			{
				ghostData.posLists.Add(OVRInput.GetLocalControllerPosition(this.controller) + eyeAnchorDiffernce);
				ghostData.rotLists.Add(OVRInput.GetLocalControllerRotation(this.controller));
				value.text = OVRInput.GetLocalControllerPosition(this.controller).ToString();

				elapsedTime = 0f;

				//　データ保存数が最大数を超えたら記録をストップ
				if (ghostData.posLists.Count >= maxDataNum)
				{
					StopRecord();
				}
			}
		}
	}

	public class GhostData
	{
		//　位置のリスト
		public List<Vector3> posLists = new List<Vector3>();
		//　角度リスト
		public List<Quaternion> rotLists = new List<Quaternion>();
		//　アニメーションパラメータSpeed値
		public List<float> speedLists = new List<float>();
		//　Jumpアニメーションリスト
		public List<bool> jumpAnimLists = new List<bool>();
	}


	//　キャラクターデータの保存
	public void StartRecord()
	{
		//　保存する時はゴーストの再生を停止
		StopAllCoroutines();
		StopGhost();
		eyeAnchorDiffernce = new Vector3(0, OVRInput.GetLocalControllerPosition(this.controller).y * -1, 0);
		isRecord = true;
		elapsedTime = 0f;
		ghostData = new GhostData();
		Debug.Log("StartRecord");
		text.text = "StartRecord";
		Music.instance.PlayMusic();
		startTime = Music.instance.GetTime();
	}



	//　キャラクターデータの保存の停止
	public void StopRecord()
	{
		isRecord = false;
		Debug.Log("StopRecord");
		text.text = "StopRecord";
		finishTime = Music.instance.GetTime();
		Music.instance.StopMusic();
	}


	//　ゴーストの停止
	public void StopGhost()
	{
		Debug.Log("StopGhost");
		text.text = "StopGhost";
		StopAllCoroutines();
		isPlayBack = false;
		isStartGhost = false;
		ghost.SetActive(false);
		Music.instance.StopMusic();
	}


		

	public void Save()
	{
		if (ghostData != null)
		{
			//　GhostDataクラスをJSONデータに書き換え
			var data = JsonUtility.ToJson(ghostData);
			//　ゲームフォルダにファイルを作成
			Debug.Log(Application.persistentDataPath + saveFileName);
			File.WriteAllText(Application.persistentDataPath + saveFileName, data);
			Debug.Log("ゴーストデータをセーブしました");
		}
	}

	public void Load()
	{

		if (File.Exists(Application.persistentDataPath + saveFileName))
		{
			string readAllText = File.ReadAllText(Application.persistentDataPath + saveFileName);
			//　ghostDataに読み込んだデータを書き込む
			if (ghostData == null)
			{
				ghostData = new GhostData();
			}
			JsonUtility.FromJsonOverwrite(readAllText, ghostData);
			Debug.Log("ゴーストデータをロードしました。");
		}
	}

	void OnApplicationQuit()
	{
		Debug.Log("アプリケーション終了");
		Save();
	}
}
