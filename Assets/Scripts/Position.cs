using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Position : MonoBehaviour
{
    public Vector3 controllerPosition;

    public Quaternion controllerRotation;

    private OVRInput.Controller controller;

    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<OVRControllerHelper>().m_controller;
    }

    // Update is called once per frame
    void Update()
    {
        controllerPosition = transform.position;
        controllerRotation = transform.rotation;
        text.text = controllerPosition.x.ToString();
    }
}
