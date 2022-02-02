using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [Range(0,2f)]
    public float speed = .54f;

    void Awake(){
        if(instance == null)
            instance = this;
        if(instance != null)
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
