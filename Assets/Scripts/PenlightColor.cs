using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenlightColor : MonoBehaviour
{

    public Material[] mats = new Material[5];

    public static PenlightColor instance;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
