using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        transform.forward = Camera.main.transform.position - transform.position;
    }
}
