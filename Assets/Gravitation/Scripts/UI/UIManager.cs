using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    public GameObject GripMeter, JoyStick, MassMeter, JumpButton;
    // Start is called before the first frame update
    public bool ActivityStarted;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ActivityStarted)// here activity started = activity 1 started
        {
            GripMeter.SetActive(true);
            JoyStick.SetActive(false);
            MassMeter.SetActive(false);
            JumpButton.SetActive(false);
        }
        else
        {
            GripMeter.SetActive(false);
            JoyStick.SetActive(true);
            MassMeter.SetActive(false);
            JumpButton.SetActive(false);
        }

        
    }
}
