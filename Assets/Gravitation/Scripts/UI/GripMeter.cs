using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GripMeter : MonoBehaviour
{

    [SerializeField]
    private GameObject[] GripMeterScale;

    [SerializeField]
    private Sprite[] GripMeterReading;

    [SerializeField]
    private Sprite ThresholdSprite, InactiveStateSprite;
    // Start is called before the first frame update
    public int Threshold;
    public float rateIncrease, rateDecrease;

    float currentReading = 10, lastreading = 0;
    void Start()
    {
        GripMeterScale[Threshold - 1].GetComponent<Image>().sprite = ThresholdSprite;
    }

    // Update is called once per frame
    void Update()
    {
        int i;
        for ( i = 0; i < currentReading; i++)
        {
            if (i != Threshold - 1)
            {
                GripMeterScale[i].GetComponent<Image>().sprite = GripMeterReading[i];
            }    
        }

        for (int j = i; j < lastreading; j ++)
        {
            if (j != Threshold - 1)
            {
                GripMeterScale[j].GetComponent<Image>().sprite = InactiveStateSprite;
            }
        }
        lastreading = currentReading;
        if (currentReading > 0)
        {
            currentReading -= rateDecrease;
        }
        
        if (Input.GetMouseButtonDown(0) && currentReading < 17)
        {
            currentReading += rateIncrease;
        }
        //Debug.Log(currentReading);
    }
}
