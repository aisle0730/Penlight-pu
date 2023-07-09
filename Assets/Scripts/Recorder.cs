using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;

public class Recorder : MonoBehaviour
{
	public GameObject player;

    //操作キャラクター
    [SerializeField]
    private GameObject ghostChara;
    //　現在記憶しているかどうか
    public bool isRecord;
	//　保存するデータの最大数
	[SerializeField]
	public int maxDataNum = 60000;
	//　記録間隔
	[SerializeField]
	public float recordDuration = 0.01f;
	//　Jumpキー
	private string animKey = "Jump";
	//　経過時間
	public float elapsedTime = 0f;
	//　ゴーストデータ
	public GhostData ghostData;
	//　再生中かどうか
	public bool isPlayBack;
	//　ゴースト用キャラ
	[SerializeField]
	private GameObject ghost;
	//　ゴーストデータが1周りした後の待ち時間
	[SerializeField]
	public float waitTime = 2f;
	//　保存先フォルダ
	private string saveDataFolder;
	//　保存ファイル名
	private string saveFileName = "/tomaranai.dat";

	private Vector3 initialPosition;

	private Quaternion initialRotation;

    [SerializeField]
	public Text text;

	[SerializeField]
	private Text value;

	[SerializeField]
	private Text vector3;

    [SerializeField]
	public OVRInput.Controller controller;

	[SerializeField]
	private Transform handAnchor;


	[SerializeField]
	private GameObject penlightClone;

	private Vector3 penlightPosition;

	private Quaternion penlightRotation;

	private float width = 0.42f;

	private Vector3 penlightPositionRight;

	private Vector3 penlightPositionLeft;


	public Vector3 eyeAnchorDiffernce;

	private Position position;

	private MovePenlight movePenlight;

	public bool isStartGhost = false;

	public static Recorder instance;



	public float startTime;

	public float finishTime;


    public void Awake()
    {
        if(instance == null)
        {
			instance = this;
        }
    }
    void Start()
	{ 

	}

	// Update is called once per frame
	void Update()
	{
		//if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch) || Input.GetKeyDown(KeyCode.A))
		//{
		//	StartRecord();
		//}

		//if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.LTouch) || Input.GetKeyDown(KeyCode.F))
		//{
		//	StopRecord();
		//}

		//if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch) || Input.GetKeyDown(KeyCode.J))
		//{
		//	isStartGhost = true;
		//}

		if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch) || Input.GetKeyDown(KeyCode.L))
		{
			StopGhost();
		}

		if (Input.GetKeyDown(KeyCode.M) || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
			Music.instance.PlayMusic();
        }

		//vector3.text = position.controllerPosition.ToString();

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
                //ghostData.speedLists.Add(animator.GetFloat("Speed"));
                //Debug.Log(ghostData.posLists[ghostData.posLists.Count - 1]);
				//Debug.Log(ghostData.posLists.Count);

				elapsedTime = 0f;

				//　データ保存数が最大数を超えたら記録をストップ
				if (ghostData.posLists.Count >= maxDataNum)
				{
					StopRecord();
				}
			}
		}
	}

	[Serializable]
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



	//　ゴーストの再生ボタンを押した時の処理
	//public void StartGhost()
	//{
	//	Debug.Log("StartGhost");
	//	text.text = "StartGhost";
	//	if (ghostData == null)
	//	{
	//		Debug.Log("ゴーストデータがありません");
	//		text.text = "ゴーストデータがありません";
	//	}
	//	else
	//	{
	//		isRecord = false;
	//		isPlayBack = true;
	//		ghost.transform.position = ghostData.posLists[0] + initialPosition;
	//		ghost.transform.rotation = ghostData.rotLists[0] * initialRotation;
	//		ghost.SetActive(true);
	//		//music.PlayMusic();
	//		StartCoroutine(PlayBack());
	//	}
	//}




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




	//　ゴーストの再生
	//IEnumerator PlayBack()
	//{

	//	var i = 0;
	//	//var ghostAnimator = ghost.GetComponent<Animator>();

	//	Debug.Log("データ数: " + ghostData.posLists.Count);
	//	text.text = "データ数: " + ghostData.posLists.Count;

	//	while (isPlayBack)
	//	{

	//		yield return new WaitForSeconds(recordDuration);

	//		ghost.transform.position = ghostData.posLists[i] + initialPosition;
	//		ghost.transform.rotation = ghostData.rotLists[i] * initialRotation;
	//		//ghostAnimator.SetFloat("Speed", ghostData.speedLists[i]);

	//		i++;

	//		//　保存データ数を超えたら最初から再生
	//		if (i >= ghostData.posLists.Count)
	//		{

	//			//ghostAnimator.SetFloat("Speed", 0f);
	//			//ghostAnimator.ResetTrigger("Jump");

	//			//　アニメーション途中で終わった時用に待ち時間を入れる
	//			yield return new WaitForSeconds(waitTime);

	//			//music.PlayMusic();

	//			ghost.transform.position = ghostData.posLists[0] + initialPosition;
	//			ghost.transform.rotation = ghostData.rotLists[0] * initialRotation;

	//			i = 0;
	//		}
	//	}
	//}

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
		//Save();
	}
}
