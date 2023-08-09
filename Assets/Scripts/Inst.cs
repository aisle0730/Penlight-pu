using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inst : MonoBehaviour
{
    private GameObject penlight;

    private Vector3 penlightPosition;

    private Quaternion penlightRotation;

    private float width = 0.5f;

    private Vector3 penlightPositionRight;

    private Vector3 penlightPositionLeft;


    [SerializeField]
    private Text text;

	[SerializeField]
	private GameObject penlightClone;

    [SerializeField]
	private Material[] mats = new Material[5];

	private int random;

	// Start is called before the first frame update
	void Start()
	{

		Debug.Log(penlightClone.transform.position);

		penlightPosition = Recorder.instance.player.transform.position;

		penlightPosition.y += 0f;

		text.text = penlightPosition.ToString();

		penlightPositionRight = penlightPosition;

		penlightPositionLeft = penlightPosition;

		penlightRotation = penlightClone.transform.rotation;

		//プレイヤーの位置の両側にペンライト配置
		for (int i = 0; i < 5; i++)
		{
			random = Random.Range(0, 5);
			penlightPositionRight.x += width;

			penlightPositionLeft.x -= width;

			penlight = Instantiate(penlightClone, penlightPositionRight, penlightRotation);
			penlight.GetComponent<Transform>().transform.GetChild(2).gameObject.GetComponent<Transform>().transform.GetChild(0).gameObject.
				GetComponent<MeshRenderer>().material = mats[random];

			random = Random.Range(0, 5);

			penlight = Instantiate(penlightClone, penlightPositionLeft, penlightRotation);
			penlight.GetComponent<Transform>().transform.GetChild(2).gameObject.GetComponent<Transform>().transform.GetChild(0).gameObject.
				GetComponent<MeshRenderer>().material = mats[random];
		}
		random = Random.Range(0, 5);


		//両側に展開
		//penlightPositionRight.x += 0.8f;

		//penlightPositionLeft.x -= 0.8f;

		//for (int i = 0; i < 10; i++)
		//{
		//	random = Random.Range(0, 5);
		//	penlightPositionRight.x += width;

		//	penlightPositionLeft.x -= width;

		//	penlight = Instantiate(penlightClone, penlightPositionRight, penlightRotation);
		//	penlight.GetComponent<Transform>().transform.GetChild(2).gameObject.GetComponent<Transform>().transform.GetChild(0).gameObject.
		//		GetComponent<MeshRenderer>().material = mats[random];

		//	random = Random.Range(0, 5);

		//	penlight = Instantiate(penlightClone, penlightPositionLeft, penlightRotation);
		//	penlight.GetComponent<Transform>().transform.GetChild(2).gameObject.GetComponent<Transform>().transform.GetChild(0).gameObject.
		//		GetComponent<MeshRenderer>().material = mats[random];
		//}

		//penlightPosition = Recorder.instance.player.transform.position;
		
		for (int j = 0; j < 10; j++)
		{
			random = Random.Range(0, 5);

			//プレイヤー前にペンライト配置
			
			penlightPosition.z += 1;
			penlight = Instantiate(penlightClone, penlightPosition, penlightRotation);
			penlight.GetComponent<Transform>().transform.GetChild(2).gameObject.GetComponent<Transform>().transform.GetChild(0).gameObject.
					GetComponent<MeshRenderer>().material = mats[random];

			penlightPositionRight = penlightPosition;

			penlightPositionLeft = penlightPosition;

			//プレイヤー前の両側にペンライト配置
			for (int i = 0; i < 5; i++)
			{
				random = Random.Range(0, 5);
				penlightPositionRight.x += width;

				penlightPositionLeft.x -= width;

				penlight = Instantiate(penlightClone, penlightPositionRight, penlightRotation);
				penlight.GetComponent<Transform>().transform.GetChild(2).gameObject.GetComponent<Transform>().transform.GetChild(0).gameObject.
					GetComponent<MeshRenderer>().material = mats[random];

				random = Random.Range(0, 5);

				penlight = Instantiate(penlightClone, penlightPositionLeft, penlightRotation);
				penlight.GetComponent<Transform>().transform.GetChild(2).gameObject.GetComponent<Transform>().transform.GetChild(0).gameObject.
					GetComponent<MeshRenderer>().material = mats[random];
			}

			random = Random.Range(0, 5);

		//両側に展開
		//	penlightPositionRight.x += 0.8f;

		//	penlightPositionLeft.x -= 0.8f;

		//	for (int i = 0; i < 10; i++)
		//	{
		//		random = Random.Range(0, 5);
		//		penlightPositionRight.x += width;

		//		penlightPositionLeft.x -= width;

		//		penlight = Instantiate(penlightClone, penlightPositionRight, penlightRotation);
		//		penlight.GetComponent<Transform>().transform.GetChild(2).gameObject.GetComponent<Transform>().transform.GetChild(0).gameObject.
		//			GetComponent<MeshRenderer>().material = mats[random];

		//		random = Random.Range(0, 5);

		//		penlight = Instantiate(penlightClone, penlightPositionLeft, penlightRotation);
		//		penlight.GetComponent<Transform>().transform.GetChild(2).gameObject.GetComponent<Transform>().transform.GetChild(0).gameObject.
		//			GetComponent<MeshRenderer>().material = mats[random];
		//	}
		}

		

		random = Random.Range(0, 5);

		//プレイヤー後ろにペンライト配置
		penlightPosition = Recorder.instance.player.transform.position;
		penlightPosition.z -= 1;
		penlight = Instantiate(penlightClone, penlightPosition, penlightRotation);
		penlight.GetComponent<Transform>().transform.GetChild(2).gameObject.GetComponent<Transform>().transform.GetChild(0).gameObject.
				GetComponent<MeshRenderer>().material = mats[random];

		penlightPositionRight = penlightPosition;

		penlightPositionLeft = penlightPosition;

		//プレイヤー後ろの両側にペンライト配置
		for (int i = 0; i < 5; i++)
		{
			random = Random.Range(0, 5);
			penlightPositionRight.x += width;

			penlightPositionLeft.x -= width;

			penlight = Instantiate(penlightClone, penlightPositionRight, penlightRotation);
			penlight.GetComponent<Transform>().transform.GetChild(2).gameObject.GetComponent<Transform>().transform.GetChild(0).gameObject.
				GetComponent<MeshRenderer>().material = mats[random];

			random = Random.Range(0, 5);

			penlight = Instantiate(penlightClone, penlightPositionLeft, penlightRotation);
			penlight.GetComponent<Transform>().transform.GetChild(2).gameObject.GetComponent<Transform>().transform.GetChild(0).gameObject.
				GetComponent<MeshRenderer>().material = mats[random];
		}

		random = Random.Range(0, 5);

		//両側に展開
		//penlightPositionRight.x += 0.8f;

		//penlightPositionLeft.x -= 0.8f;

		//for (int i = 0; i < 10; i++)
		//{
		//	random = Random.Range(0, 5);
		//	penlightPositionRight.x += width;

		//	penlightPositionLeft.x -= width;

		//	penlight = Instantiate(penlightClone, penlightPositionRight, penlightRotation);
		//	penlight.GetComponent<Transform>().transform.GetChild(2).gameObject.GetComponent<Transform>().transform.GetChild(0).gameObject.
		//		GetComponent<MeshRenderer>().material = mats[random];

		//	random = Random.Range(0, 5);

		//	penlight = Instantiate(penlightClone, penlightPositionLeft, penlightRotation);
		//	penlight.GetComponent<Transform>().transform.GetChild(2).gameObject.GetComponent<Transform>().transform.GetChild(0).gameObject.
		//		GetComponent<MeshRenderer>().material = mats[random];
		//}



	}
}
