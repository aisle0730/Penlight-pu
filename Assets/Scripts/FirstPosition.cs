using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPosition : MonoBehaviour
{
    private Recorder firstPosition;

    [SerializeField]
    private GameObject penlight;

    private Transform worldControllerPosition;

    private Transform setPosition;

    private float seatWidth = 50;

    private float addRightArmPosition = 13;

    private float subRightArmPosition = -13;

    // Start is called before the first frame update
    void Start()
    {
        worldControllerPosition.position = transform.TransformPoint(OVRInput.GetLocalControllerPosition(firstPosition.controller));

        for(int i = 0; i < 5; i++)
        {
            setPosition.position.Set(worldControllerPosition.position.x + addRightArmPosition, worldControllerPosition.position.y, worldControllerPosition.position.z);
            Instantiate(gameObject, setPosition);
            setPosition.position.Set(worldControllerPosition.position.x - subRightArmPosition, worldControllerPosition.position.y, worldControllerPosition.position.z);
            Instantiate(gameObject, setPosition);

            addRightArmPosition += seatWidth;
            subRightArmPosition -= seatWidth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
