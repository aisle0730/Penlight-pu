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
        text.text = "ò^âÊíÜ";
    }

    public void ClickStoptRecord()
    {
        Recorder.instance.StopRecord();
        text.text = "ò^âÊí‚é~";

    }
    public void ClickStartGhost()
    {
        MovePenlight.instance.moveflag = true;
        text.text = "çƒê∂íÜ";
    }
    public void ClickStopGhost()
    {
        Recorder.instance.StopRecord();
        text.text = "çƒê∂í‚é~";
    }

    public void ClickSave()
    {
        Recorder.instance.Save();
        text.text = "ÉZÅ[ÉuÇµÇ‹ÇµÇΩ";
    }

    public void ClickLoad()
    {
        Recorder.instance.Load();
        text.text = "ÉçÅ[ÉhÇµÇ‹ÇµÇΩ";
    }
}
