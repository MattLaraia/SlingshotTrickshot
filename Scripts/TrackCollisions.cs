using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCollisions : MonoBehaviour
{
    //supposed to be private, but useful to be public for deubbing purposes in unity
    public int scoreMultiplierFromBounces = 1;
    private bool swish = true;

    void Start()
    {
        scoreMultiplierFromBounces = 1;
    }
    void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "Goal"){
            print("No SWISH!");
            swish = false;
        }

        
        if(other.gameObject.tag != "Slingshot"&&other.gameObject.tag != "BALL"){
            print(other.gameObject);
            scoreMultiplierFromBounces +=1;
        }
    }
    public int CurrentMultiplier(){
        /*if(ball.tag=="BALL"){
            print("OOOO");
            scoreMultiplierFromBounces+=1;
        }*/
        return scoreMultiplierFromBounces;
        //On bounce, add 1
        //if the collision is hitting the goal, add 1 to bounces, but take away the double score multiplier for a swish

    }

    public bool IsSwish(){
        if(swish)
            return true;
        return false;
    }
}
