using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GripMeter : MonoBehaviour
{

    [SerializeField]
    public GameObject[] GripMeterScale, GripMeterIndicator;

    [SerializeField]
    public Sprite[] GripMeterReading;

    [SerializeField]
    public Sprite ThresholdSprite, InactiveStateSprite;
    // Start is called before the first frame update
    [Range(1, 20)]
    public int LowerThreshold, UpperThreshold, GripLooseThreshold;
    public float rateIncrease, rateDecrease;
    
    [Range(1, 20)][Tooltip("Don't Change lastReading Value")]
    public float currentReading, lastreading = 0;
    void Start()
    {
        for (int i = 0; i < GripMeterIndicator.Length; i++)
        {
            if (i == UpperThreshold - 1 || i == LowerThreshold - 1)
            {
                GripMeterIndicator[i].GetComponent<Image>().sprite = ThresholdSprite;
                GripMeterIndicator[i].GetComponent<Image>().color = Color.white;
            }
            else
            {
                GripMeterIndicator[i].GetComponent<Image>().color = Color.clear;
            }   
        }
    }

    // Update is called once per frame
    void Update()
    {
        int i;
        for (i = 0; i < currentReading; i++)
        {
            GripMeterScale[i].GetComponent<Image>().sprite = GripMeterReading[i];    
        }

        for (int j = i; j < lastreading; j ++)
        {
            GripMeterScale[j].GetComponent<Image>().sprite = InactiveStateSprite;
        }
        
        lastreading = currentReading;
        if (currentReading >= rateDecrease)
        {
            currentReading -= rateDecrease;
        }
        
        if (Input.GetMouseButtonDown(0) && currentReading < 20 - rateIncrease)
        {
            currentReading += rateIncrease;
        }

        if (currentReading > UpperThreshold)
        {
            rateDecrease = 0;
        }
        //Debug.Log(currentReading);
    }
}
