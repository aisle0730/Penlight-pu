using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;

public class Recorder : MonoBehaviour
{
	public GameObject player;

    //����L�����N�^�[
    [SerializeField]
    private GameObject ghostChara;
    //�@���݋L�����Ă��邩�ǂ���
    public bool isRecord;
	//�@�ۑ�����f�[�^�̍ő吔
	[SerializeField]
	public int maxDataNum = 60000;
	//�@�L�^�Ԋu
	[SerializeField]
	public float recordDuration = 0.01f;
	//�@Jump�L�[
	private string animKey = "Jump";
	//�@�o�ߎ���
	public float elapsedTime = 0f;
	//�@�S�[�X�g�f�[�^
	public GhostData ghostData;
	//�@�Đ������ǂ���
	public bool isPlayBack;
	//�@�S�[�X�g�p�L����
	[SerializeField]
	private GameObject ghost;
	//�@�S�[�X�g�f�[�^��1���肵����̑҂�����
	[SerializeField]
	public float waitTime = 2f;
	//�@�ۑ���t�H���_
	private string saveDataFolder;
	//�@�ۑ��t�@�C����
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

				//�@�f�[�^�ۑ������ő吔�𒴂�����L�^���X�g�b�v
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



	//�@�S�[�X�g�̍Đ��{�^�������������̏���
	//public void StartGhost()
	//{
	//	Debug.Log("StartGhost");
	//	text.text = "StartGhost";
	//	if (ghostData == null)
	//	{
	//		Debug.Log("�S�[�X�g�f�[�^������܂���");
	//		text.text = "�S�[�X�g�f�[�^������܂���";
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




	//�@�S�[�X�g�̍Đ�
	//IEnumerator PlayBack()
	//{

	//	var i = 0;
	//	//var ghostAnimator = ghost.GetComponent<Animator>();

	//	Debug.Log("�f�[�^��: " + ghostData.posLists.Count);
	//	text.text = "�f�[�^��: " + ghostData.posLists.Count;

	//	while (isPlayBack)
	//	{

	//		yield return new WaitForSeconds(recordDuration);

	//		ghost.transform.position = ghostData.posLists[i] + initialPosition;
	//		ghost.transform.rotation = ghostData.rotLists[i] * initialRotation;
	//		//ghostAnimator.SetFloat("Speed", ghostData.speedLists[i]);

	//		i++;

	//		//�@�ۑ��f�[�^���𒴂�����ŏ�����Đ�
	//		if (i >= ghostData.posLists.Count)
	//		{

	//			//ghostAnimator.SetFloat("Speed", 0f);
	//			//ghostAnimator.ResetTrigger("Jump");

	//			//�@�A�j���[�V�����r���ŏI��������p�ɑ҂����Ԃ�����
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
		//Save();
	}
}
