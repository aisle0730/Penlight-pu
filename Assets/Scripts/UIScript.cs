using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField]
    private Button button;

    [SerializeField]
    GameObject ButtonCanvas;

    private bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        

        ButtonCanvas.SetActive(active);
    }

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch) || Input.GetKeyDown(KeyCode.A))
        {
            active = !active;
            ButtonCanvas.SetActive(active);

            button.Select();
        }
    }
}
