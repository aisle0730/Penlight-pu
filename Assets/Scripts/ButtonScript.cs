using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    Recorder recorder;
    MovePenlight movePenlight;

    [SerializeField]
    private Text text;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void ClickStartRecord()
    {
        Recorder.instance.StartRecord();
        text.text = "�^�撆";
    }

    public void ClickStoptRecord()
    {
        Recorder.instance.StopRecord();
        text.text = "�^���~";

    }
    public void ClickStartGhost()
    {
        MovePenlight.instance.moveflag = true;
        text.text = "�Đ���";
    }
    public void ClickStopGhost()
    {
        Recorder.instance.StopRecord();
        text.text = "�Đ���~";
    }

    public void ClickSave()
    {
        Recorder.instance.Save();
        text.text = "�Z�[�u���܂���";
    }

    public void ClickLoad()
    {
        Recorder.instance.Load();
        text.text = "���[�h���܂���";
    }
}
