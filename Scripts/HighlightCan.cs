using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightCan : MonoBehaviour
{
    public GameObject highlightCan;
    public GameObject regularCan;

    void Update()
    {
        if(Input.GetButtonDown("HighlightCan")&&highlightCan.activeSelf==true)
        {
            regularCan.SetActive(true);
            highlightCan.SetActive(false);
        }
        else if(Input.GetButtonDown("HighlightCan")&&regularCan.activeSelf==true)
        {
            regularCan.SetActive(false);
            highlightCan.SetActive(true);
        }
    }
}
