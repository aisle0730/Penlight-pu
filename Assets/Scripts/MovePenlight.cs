using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePenlight : MonoBehaviour
{
	private Vector3 initialPosition;

	private Quaternion initialRotation;

	private int random;

	private PenlightColor materials;

	public static MovePenlight instance;

	public bool moveflag = false;

		public void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	// Start is called before the first frame update
	void Start()
    {
        //penlight = this.GetComponent<GameObject>();
		initialPosition = this.transform.position;
		initialRotation = this.transform.rotation;

		random = Random.Range(0, 4);

		GameObject child = GameObject.Find("particle").gameObject;
		Material material = PenlightColor.instance.mats[random];
		Debug.Log(child);
		Debug.Log(material);
        //Color color = PenlightColor.instance.mats[random].color;
        //Debug.Log(color);
        //child.GetComponent<Renderer>().material.SetColor("_TintColor", color);
        MeshRenderer r = child.GetComponent<MeshRenderer>();
        r.material = material;
        Debug.Log("childMaterial : " + child.GetComponent<MeshRenderer>().material);
	}

    // Update is called once per frame
    void Update()
    {

		if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.LTouch) || Input.GetKeyDown(KeyCode.J))
		{
			StartGhost();
		}
	}

	public void StartGhost()
	{
		//Debug.Log("StartGhost");
		Recorder.instance.text.text = "StartGhost";
		moveflag = false;
		if (Recorder.instance.ghostData == null)
		{
			Debug.Log("�S�[�X�g�f�[�^������܂���");
		}
		else
		{
			
			Recorder.instance.isRecord = false;
			Recorder.instance.isPlayBack = true;
			transform.position = Recorder.instance.ghostData.posLists[0] + initialPosition;
			transform.rotation = Recorder.instance.ghostData.rotLists[0] * initialRotation;
			Music.instance.PlayMusicMiddle();
			StartCoroutine(PlayBack());
		}
	}

	IEnumerator PlayBack()
	{

		var i = random;

		//Debug.Log("�f�[�^��: " + Recorder.instance.ghostData.posLists.Count);
		Recorder.instance.text.text = "�f�[�^��: " + Recorder.instance.ghostData.posLists.Count;

		while (Recorder.instance.isPlayBack)
		{
			//Debug.Log("Check + " + Recorder.instance.ghostData.posLists.Count);
			yield return new WaitForSeconds(Recorder.instance.recordDuration);

			transform.position = (Recorder.instance.ghostData.posLists[i] + initialPosition);
			transform.rotation = Recorder.instance.ghostData.rotLists[i] * initialRotation;

			i++;

			//�@�ۑ��f�[�^���𒴂�����ŏ�����Đ�
			if (i >= Recorder.instance.ghostData.posLists.Count)
			{
				Debug.Log("Return + " + Recorder.instance.ghostData.posLists.Count);
				Debug.Log(Recorder.instance.isPlayBack);

				//�@�A�j���[�V�����r���ŏI��������p�ɑ҂����Ԃ�����
				yield return new WaitForSeconds(Recorder.instance.waitTime);

				Music.instance.PlayMusicMiddle();

				transform.position = (Recorder.instance.ghostData.posLists[0] + initialPosition);
				transform.rotation = Recorder.instance.ghostData.rotLists[0] * initialRotation;

				i = random;
			}
		}
	}
}
