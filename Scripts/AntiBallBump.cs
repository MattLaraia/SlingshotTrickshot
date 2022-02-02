using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiBallBump : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        print("ENTEREDDDD");
        if(other.gameObject.tag=="BALL"){
            other.gameObject.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other){
        other.gameObject.tag = "BALL";
    }


}
