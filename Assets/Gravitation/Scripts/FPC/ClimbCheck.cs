using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbCheck : MonoBehaviour
{
    public bool climb = false;
    public GameObject hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Climb")
        {
            hit = col.gameObject;
            climb = true;
        }
    }
}
