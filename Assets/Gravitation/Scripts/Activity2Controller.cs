using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activity2Controller : MonoBehaviour
{

    public GripMeter gripMeter;
    public GameObject[] KG;

    private Animator _animator;

    // TC = TryCatch, SPD/SPU = Stress Pull Down/Up, NPaS = Normal Pull to Sit
    bool Idle = true, TC = false, SPD = false, SPU = false;
    int count = 0;


    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        for (int i = 0; i < KG.Length; i ++)
        {
            KG[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        _animator.ResetTrigger("Idle to TC");
        _animator.ResetTrigger("TC to SPU");
        _animator.ResetTrigger("TC to SPD");
        _animator.ResetTrigger("SPU to SPD");
        _animator.ResetTrigger("SPU to NPaS");
        float CR = gripMeter.currentReading;
        float LT = gripMeter.LowerThreshold;
        float UT = gripMeter.UpperThreshold;
        float LgT = gripMeter.GripLooseThreshold;
        float RoD = gripMeter.rateDecrease;
        if (Input.GetMouseButtonDown(0) && Idle)
        {
            _animator.SetTrigger("Idle to TC");
            Idle = false;TC = true;
            KG[count].SetActive(true);
            count += 1;
        }

        if ((TC || SPD) && (CR > LT))
        {
            _animator.SetTrigger("TC to SPU");
            TC = false; SPU = true; SPD = false;
        }

        if ((CR < LgT) && TC)
        {
            _animator.SetTrigger("TC to SPD");
            TC = false; SPD = true;
        }

        if (CR < RoD)
        {
            //fall
        }

        if ((CR > UT) && SPU)
        {
            _animator.SetTrigger("SPU to NPaS");
            SPU = false; Invoke("IdleTrue", 3f);
        }

        if ((CR < LT) && SPU)
        {
            _animator.SetTrigger("SPU to SPD");
            SPU = false; SPD = true;
        }
    }

    public void IdleTrue()
    {
        Idle = true;
    }
}
