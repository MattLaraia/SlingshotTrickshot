using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GoalScore : MonoBehaviour
{

    public AudioSource playSound;
    public GameObject goalText;
    public Text finalScoreText;
    public GameObject nextLvlTxt;
    public GameObject shotBlocker;
    public bool lvlDone = false;
    public int nextScene;
    public SceneLoader SceneLoader;
    public GameObject pauseMenu;
    public bool isPauseMenuActive = false;
    public GameObject scoreText;
    private int pausedScore;
    public GameObject shotsTaken;
    //public DragShootAndTrackScore ballInstance;
    public GameObject previousBall;
    private GameObject previousBallBackup;
    private bool clickedResumeInPauseMenu = false;
    public int highScore1;
    public int highScore2;
    public int highScore3;


    void Start(){
        highScore1 = PlayerPrefs.GetInt("HighScore",0);
    }

    void OnTriggerEnter(Collider other)
    {
        
        /* This code was initially put in place because for some reason the gameObject wasnt deactivating ontrigger enter but now it works?
            i think i mightve had the syntax wrong in the past
        MeshRenderer mesh = other.gameObject.GetComponent(typeof(MeshRenderer)) as MeshRenderer;

        if (mesh != null)
            mesh.enabled = false;
        */
        other.gameObject.SetActive(false);

        int baseScore = other.gameObject.GetComponent<DragShootAndTrackScore>().theScore;
        int multiplier = other.gameObject.GetComponent<TrackCollisions>().CurrentMultiplier();
        int missedShots = other.gameObject.GetComponent<DragShootAndTrackScore>().totalShots-1;
        //print("MULTIPLIER: "+multiplier);
        int finalScore = (baseScore*multiplier)-missedShots;
        
        if(other.gameObject.GetComponent<TrackCollisions>().IsSwish()){
            goalText.GetComponent<Text>().text ="SWISH! DOUBLE POINTS!";
            if(finalScore>-1)
                finalScore*=2;
            else{
                finalScore/=2;
            }
        }

        finalScoreText.text = "Your Final Score: " + finalScore;
        
        shotsTaken.SetActive(false);
        goalText.SetActive(true);
        nextLvlTxt.SetActive(true);
        shotBlocker.SetActive(true);
        playSound.Play();
        

        lvlDone = true;
        DragShootAndTrackScore.firstrun = true;
        //update score text to show how scoring is calculated AND UPDATES MULTIPLIER TO PROPER VALUE!
            //   REMOVE COMMENT HERE?
        string subtractMisses = " - " + missedShots +" misses";
        //other.gameObject.GetComponent<DragShootAndTrackScore>().
        print(scoreText.GetComponent<Text>().text);
        scoreText.GetComponent<Text>().text = "Score: "+ baseScore+" X "+multiplier   +subtractMisses;
        print("EEE");
        print(scoreText.GetComponent<Text>().text);
        /*print(other.gameObject.GetComponent<DragShootAndTrackScore>().scoreText.text);
        scoreText.SetActive(false);
        other.gameObject.SetActive(true);*/
        int currentScene = nextScene-1;
        if(currentScene==1&&finalScore>highScore1){
            PlayerPrefs.SetInt("HighScore",finalScore);
            highScore1 = PlayerPrefs.GetInt("HighScore",finalScore);
            print(highScore1);
            print("final"+finalScore);
        }
        else if(currentScene==2&&finalScore>highScore2){
            PlayerPrefs.SetInt("HighScore2",finalScore);
            highScore2 = PlayerPrefs.GetInt("HighScore2",finalScore);
            print(highScore2);

        }else if(currentScene==3&&finalScore>highScore3){
            PlayerPrefs.SetInt("HighScore3",finalScore);
            highScore3 = PlayerPrefs.GetInt("HighScore3",finalScore);
            print(highScore3);

        }

    }

    //function used when button clicks to leave the pause menu
    public void UnPause(){
        clickedResumeInPauseMenu = true;
    }

    void Update(){
        if(Spawner.Instance.previous!=null){
            previousBall = Spawner.Instance.previous;
        }

        if(previousBall!=null){
            previousBallBackup = previousBall;
        }
        //checks if the first ball is still there when it shouldnt, delete it
        //I added this in because -	If I don’t score, and just go to main menu, the first object being shot afterwards won’t despawn 
        //INCORRECT LOGIC!
        /*if(Spawner.Instance.SpawnObject!=null&& DragShootAndTrackScore.tempObject != null){
            Spawner.Instance.RemoveSpawnedItem(Spawner.Instance.SpawnObject);
        }*/

        //IMPORTANT!!!
        //NOTE: if I just made Sceneloader hold next lvl as a var of the object, i could split this up into seperate scripts!
        //MAKES CODE MUCH MORE SCALABLE

        //this checks to see if you want to quit after beating a level
        if(lvlDone&&Input.GetKeyDown(KeyCode.Escape)){
            SceneLoader.LoadGame(0);
        }
        else if(Input.GetButtonDown("NextLvl")&&lvlDone){
            lvlDone=false;
            SceneLoader.LoadGame(nextScene);
        }/* FAILED ATTEMPT TO GET MULTIPLIER FULLY RIGHT
        else if(lvlDone){
            int missedShots = ballInstance.previousBall.GetComponent<DragShootAndTrackScore>().totalShots-1;

            //Updates score in top left to make sure I display correct multiplier when scored
            ballInstance.previousBall.GetComponent<DragShootAndTrackScore>().SetScoreText();
            string subtractMisses = " - " + missedShots +" misses";
            ballInstance.previousBall.GetComponent<DragShootAndTrackScore>().scoreText.text += subtractMisses;
        }*/
        else if(lvlDone==false){

            //pause menu activate
            if(nextScene>1&&Input.GetKeyDown(KeyCode.Escape)&&isPauseMenuActive==false){
                GameObject mainPauseMenu = pauseMenu.transform.Find("PauseMenu").gameObject;
                GameObject howToPlayPauseMenu = pauseMenu.transform.Find("How To Play Pause Menu").gameObject;
                GameObject controlsPauseMenu = pauseMenu.transform.Find("Controls Pause Menu").gameObject;
                GameObject rage = pauseMenu.transform.Find("RAGE").gameObject;

                //mainpauseMenu isnt active when i esc in the controls or how to play,
                //so here I check for that scenario and reset it! 
                if(!mainPauseMenu.activeSelf){
                    mainPauseMenu.SetActive(true);
                    howToPlayPauseMenu.SetActive(false);
                    controlsPauseMenu.SetActive(false);
                    rage.SetActive(false);
                }

                //print("TURNING OFF UI FOR PAUSE MENU");
                isPauseMenuActive = true;

                //decided to still keep up score in the pause menu
                //shotsTaken.SetActive(false);
                pauseMenu.SetActive(true);/*
                if(Spawner.Instance.SpawnObject!=null){
                    Spawner.Instance.SpawnObject.SetActive(false);
                    /* I JUST HAD TO EDIT REPEATING score FN W LOGIC instead of manually tracking an resetting!!!
                    scoreText.GetComponent<Text>().text = "Score: " + ballInstance.theScore;
                    pausedScore = ballInstance.theScore;
                    
                }*/
                if(previousBall != null){
                    print("THis is deactivated??" +previousBall);
                    previousBall.SetActive(false);
                }
                
                //I NEED TO DEACTIVATE THE BALL WHEN I GO INTO THE MENU BC OTHERWISE IT COULD "SHOOT" IT
            }
            //pause menu deactivate either by pressing escape or pressing the return button
            else if(nextScene>1&&Input.GetKeyDown(KeyCode.Escape)&&isPauseMenuActive||clickedResumeInPauseMenu){
                clickedResumeInPauseMenu = false;
                pauseMenu.SetActive(false);
                isPauseMenuActive = false;
                /*//reverts back to old score -- I JUST HAD TO EDIT REPEATING FN W LOGIC!!!
                ballInstance.scoreText.text = scoreText.GetComponent<Text>().text;
                ballInstance.theScore = pausedScore;
                //scoreText.SetActive(true);
                //shotsTaken.SetActive(true);
                */
                if(previousBall!= null){
                    previousBall.SetActive(true);
                }

                /*
                //hack to spawn ball when leaving pause menu
                if(Spawner.Instance.tempObject != null){
                    Spawner.Instance.tempObject.SetActive(true);
                }
                */
            }
        }
    }
}
