using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject GripMeter;
    // Start is called before the first frame update
    public bool ActivityStarted;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ActivityStarted)
        {
            GripMeter.SetActive(true);
        }
        else
        {
            GripMeter.SetActive(false);
        }
    }
}
