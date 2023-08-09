using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;

public class Recorder : MonoBehaviour
{
	//�R���g���[��
	public GameObject player;

    //�@���݋L�����Ă��邩�ǂ���
    public bool isRecord;
	//�@�ۑ�����f�[�^�̍ő吔
	public int maxDataNum = 60000;
	//�@�L�^�Ԋu
	public float recordDuration = 0.01f;
	//�@�o�ߎ���
	public float elapsedTime = 0f;
	//�@�S�[�X�g�f�[�^
	public GhostData ghostData;
	//�@�Đ������ǂ���
	public bool isPlayBack;
	//�@�S�[�X�g�p�L����
	public GameObject ghost;
	//�@�S�[�X�g�f�[�^��1���肵����̑҂�����
	[SerializeField]
	public float waitTime = 2f;
	//�@�ۑ���t�H���_
	private string saveDataFolder;
	//�@�ۑ��t�@�C����
	private string saveFileName = "/tomaranai.dat";

	//�f�o�b�O�pUI
	public Text text;

	public Text value;

	public Text vector3;

    

	public OVRInput.Controller controller;


	public Transform handAnchor;



	public GameObject penlightClone;

	public Vector3 eyeAnchorDiffernce;

	public bool isStartGhost = false;

	public static Recorder instance;


	//�ȊJ�n�n�_
	public float startTime;
	//�ȏI���n�_
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

				//�@�f�[�^�ۑ������ő吔�𒴂�����L�^���X�g�b�v
				if (ghostData.posLists.Count >= maxDataNum)
				{
					StopRecord();
				}
			}
		}
	}

	public class GhostData
	{
		//�@�ʒu�̃��X�g
		public List<Vector3> posLists = new List<Vector3>();
		//�@�p�x���X�g
		public List<Quaternion> rotLists = new List<Quaternion>();
		//�@�A�j���[�V�����p�����[�^Speed�l
		public List<float> speedLists = new List<float>();
		//�@Jump�A�j���[�V�������X�g
		public List<bool> jumpAnimLists = new List<bool>();
	}


	//�@�L�����N�^�[�f�[�^�̕ۑ�
	public void StartRecord()
	{
		//�@�ۑ����鎞�̓S�[�X�g�̍Đ����~
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



	//�@�L�����N�^�[�f�[�^�̕ۑ��̒�~
	public void StopRecord()
	{
		isRecord = false;
		Debug.Log("StopRecord");
		text.text = "StopRecord";
		finishTime = Music.instance.GetTime();
		Music.instance.StopMusic();
	}


	//�@�S�[�X�g�̒�~
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
			//�@GhostData�N���X��JSON�f�[�^�ɏ�������
			var data = JsonUtility.ToJson(ghostData);
			//�@�Q�[���t�H���_�Ƀt�@�C�����쐬
			Debug.Log(Application.persistentDataPath + saveFileName);
			File.WriteAllText(Application.persistentDataPath + saveFileName, data);
			Debug.Log("�S�[�X�g�f�[�^���Z�[�u���܂���");
		}
	}

	public void Load()
	{

		if (File.Exists(Application.persistentDataPath + saveFileName))
		{
			string readAllText = File.ReadAllText(Application.persistentDataPath + saveFileName);
			//�@ghostData�ɓǂݍ��񂾃f�[�^����������
			if (ghostData == null)
			{
				ghostData = new GhostData();
			}
			JsonUtility.FromJsonOverwrite(readAllText, ghostData);
			Debug.Log("�S�[�X�g�f�[�^�����[�h���܂����B");
		}
	}

	void OnApplicationQuit()
	{
		Debug.Log("�A�v���P�[�V�����I��");
		Save();
	}
}
