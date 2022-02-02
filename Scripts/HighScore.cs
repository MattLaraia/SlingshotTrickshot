using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{

    public GameObject leaderboard;
    public GameObject highScore1;
    public GameObject highScore2;
    public GameObject highScore3;



    // Start is called before the first frame update
    void Start()
    {
        highScore1.GetComponent<Text>().text = PlayerPrefs.GetInt("HighScore",0).ToString();
        highScore2.GetComponent<Text>().text = PlayerPrefs.GetInt("HighScore2",0).ToString();
        highScore3.GetComponent<Text>().text = PlayerPrefs.GetInt("HighScore3",0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(leaderboard.activeSelf){
            highScore1.SetActive(true);
            highScore2.SetActive(true);
            highScore3.SetActive(true);
        }else{
            highScore1.SetActive(true);
            highScore2.SetActive(true);
            highScore3.SetActive(true);
        }
    }
}
