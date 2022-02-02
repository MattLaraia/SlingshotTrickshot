using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]


public class DragShootAndTrackScore : MonoBehaviour
{
    private Vector3 mousePressDownPos;
    private Vector3 mouseReleasePos;

    private Rigidbody rigidBody;

    private bool isShoot;

    public static GameObject tempObject;
    public static bool firstrun = true;
    
    public GameObject shotCountText;
    public int totalShots;

    public Text scoreText;
    public int theScore=0;
    public int pointsIncreasedPerSecond=0;

    private int pausedPointsIncreasedPerSecond;

    public GoalScore goalScoreScript;
    public GameObject previousBall;
    public TrackCollisions collisions;
    private int multiplier;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        
        if(Spawner.Instance.previous!=null){
            previousBall = Spawner.Instance.previous;
        }
        theScore = 0;
        pointsIncreasedPerSecond = 0;
        multiplier =1;
    
        //if u set previous to b itself at first, 
    }
    //NEED TO EXPLAIN HOW SCORING WORKS IN TUTORIAL MENU!!!
    ///TODO: i just need to do the multiplier! 
    //probably just need to count the number of collisions and then take the pointsIncreasedPerSecond
    //subtract seconds by shots taken and if you have less than 1, ur final score is 1
    //a swish doubles your entire score!
    //need to show the final score at the end, could instantiate seconds x collsions
    //and then show total score
    //also need to reset # of collisions!
    //ALSO NEED SCORE TO STOP ON PAUSE MENU! -- I could just track current score on pause,
    //and then when I unpause, I go back to that old score
    //^^attempt w/ a follow up question line 91 on GoalScore.cs
    void Update(){
        SetScoreText();
        //bugfix to prevent line from appearing after going into pause menu 
        //and even staying after leaving pause menu but no longer holding that down
        if(Input.GetKeyDown(KeyCode.Escape)){
            DrawTrajectory.Instance.HideLine();
        }
        //print(isShoot);
        /*if(isShoot){
            isShoot = false;
            theScore = 0;
            pointsIncreasedPerSecond = 0;
            multiplier =1;
        }*/
        //update score in terms of seconds, and reset the score to the new shot on release
        //the middle else if makes sure it keeps running
        /*
        if(isShoot){
            if(previousBall==null){

            }else{

            }
        }else if(previousBall!=null){

        }*/

//////////////////////////////////////////////////////////////////////////////////////////
        /* SECOND ITERATION
        if(isShoot){
            if(Spawner.Instance.SpawnObject!=null){
                multiplier = Spawner.Instance.SpawnObject.GetComponent<TrackCollisions>().CurrentMultiplier();
                theScore = pointsIncreasedPerSecond;

                //InvokeRepeating("SetScoreText",0,.1f);
            }else{
                multiplier = Spawner.Instance.tempObject.GetComponent<TrackCollisions>().CurrentMultiplier();


                theScore = pointsIncreasedPerSecond-totalShots+1;//+1 to offset the current shot being included in the penalty
                //InvokeRepeating("SetScoreText",0,.1f);
            }
        }

        /*else if(Spawner.Instance.SpawnObject==null){
            multiplier = previousBall.GetComponent<TrackCollisions>().CurrentMultiplier();
            theScore = previousBall.GetComponent<DragShootAndTrackScore>().theScore;
        }*/
        /////////////////////////////////////////////////////////////////////////////////////////////
        /*
        FIRST ITERATION
        if(previousBall!=null && isShoot){
            multiplier = Spawner.Instance.tempObject.GetComponent<TrackCollisions>().CurrentMultiplier();


            theScore = pointsIncreasedPerSecond;
            int baseScore = theScore-totalShots+1;//+1 to offset the current shot being included in the penalty
            SetScoreText(baseScore);


            //scoreText.text = "Score: " + baseScore;
            /*if(multiplier>1){
                scoreText.text = "Score: " + baseScore + " X " + multiplier;
            }
            else{
                scoreText.text = "Score: " + baseScore;
            }////////
        }
        //makes sure that the first balls score is updated
        //lowkey did this by accident and it worked so idk y but i know i need it to do what i said earlier
        else if(previousBall!=null){ //could also try: if(Spawner.Instance.SpawnObject==null)
            /*
            theScore = pointsIncreasedPerSecond;
            int baseScore = theScore-totalShots+1;//+1 to offset the current shot being included in the penalty
            multiplier = Spawner.Instance.tempObject.GetComponent<TrackCollisions>().CurrentMultiplier();
            SetScoreText(baseScore);
            ///////

            //previousBall.theScore = pointsIncreasedPerSecond;
            //scoreText.text = "Score: " + previousBall.theScore;
        }
        else{
            multiplier = Spawner.Instance.SpawnObject.GetComponent<TrackCollisions>().CurrentMultiplier();
            theScore = pointsIncreasedPerSecond;

            SetScoreText(theScore);
            //print(multiplier);
            //here i would use the spawn object and above i would use the temp object to track
        }*/
    }

    private void OnMouseDown(){
        if(!goalScoreScript.isPauseMenuActive){
            mousePressDownPos = Input.mousePosition;
        }
    }

    [SerializeField]
    private float forceMultiplier = 2;
    private void OnMouseDrag()
    {
        //only run this script if not in the pause menu!
        if(!goalScoreScript.isPauseMenuActive){
            Vector3 forceInit = (Input.mousePosition-mousePressDownPos);
            Vector3 forceV = (new Vector3(forceInit.x,forceInit.y,forceInit.y))*forceMultiplier*GameManager.instance.speed;
            if(!isShoot)
                DrawTrajectory.Instance.UpdateTrajectory(forceV,rigidBody,transform.position);
        }
    }

    private void OnMouseUp()
    {
        mouseReleasePos = Input.mousePosition;
        Vector3 distanceMouseDragged = mousePressDownPos-mouseReleasePos;
        //makes sure not in the pause menu and not just clicked on the ball without dragging it
        if(!goalScoreScript.isPauseMenuActive&&distanceMouseDragged.magnitude>0){
            DrawTrajectory.Instance.HideLine();
            Shoot(distanceMouseDragged*GameManager.instance.speed);
            //on each shot, increase shot count and reset score
            totalShots +=1;
            shotCountText.GetComponent<Text>().text = "Shots: " + totalShots;
            InvokeRepeating("AddPointPerSecond",0,.25f);
            //theScore = pointsIncreasedPerSecond;//* Time.deltaTime;
            //when goal need to find the script thats based off the trigger in the can
            /*if(firstrun)
            {
                print("FIRST");
                tempObject = Spawner.Instance.SpawnObject;
                firstrun = false;
            }
            else
            {
                //Y DOES IT NOT REMOVE FIRST INSTANCE WHEN I QUIT EARLY FROM THE MENU AND RETURN?
                Spawner.Instance.RemoveSpawnedItem(tempObject);
                tempObject = Spawner.Instance.tempObject;
            }*/

            if(previousBall!=null)
                Spawner.Instance.RemoveSpawnedItem(previousBall);

            
        }
    }
    void Shoot(Vector3 Force)
    {
        if(isShoot)
            return;
        rigidBody.AddForce(new Vector3(Force.x,Force. y, Force.y)*forceMultiplier);//*GameManager.instance.speed);
        isShoot = true;
        Spawner.Instance.NewSpawnRequest();
    }
    public void ResetFirstRun(){
        firstrun = true;
    }
    public void AddPointPerSecond(){
        if(!(goalScoreScript.isPauseMenuActive)&&!(goalScoreScript.lvlDone))
            pointsIncreasedPerSecond += 1;
            theScore = pointsIncreasedPerSecond;//-totalShots+1;
            if(Spawner.Instance.SpawnObject!=null)
                multiplier = Spawner.Instance.SpawnObject.GetComponent<TrackCollisions>().CurrentMultiplier();
            else{
                multiplier = Spawner.Instance.tempObject.GetComponent<TrackCollisions>().CurrentMultiplier();
            }
    }

    public void SetScoreText(){
        //MAYBE ADD THAT IF SHOT COUNT DISABLED, PUT THE FINAL VALUE
        if(shotCountText.activeSelf == false){
            int prevMultiplier = previousBall.GetComponent<TrackCollisions>().CurrentMultiplier();
            Text prevscoreText = previousBall.GetComponent<DragShootAndTrackScore>().scoreText;
            int prevTheScore = previousBall.GetComponent<DragShootAndTrackScore>().theScore;
            //add the minus!
            int missedShots = totalShots-1;
            string subtractMisses = " - " + missedShots +" misses";
            prevscoreText.text = "Score: "+ prevTheScore+" X "+prevMultiplier   +subtractMisses;

        }
        else if(previousBall==null){
            if(multiplier>1){
                    scoreText.text = "Score: " + theScore + " X " + multiplier;
            }
            else{ 
                scoreText.text = "Score: " + theScore;
            }
        }else{
            int prevMultiplier = previousBall.GetComponent<TrackCollisions>().CurrentMultiplier();
            Text prevscoreText = previousBall.GetComponent<DragShootAndTrackScore>().scoreText;
            int prevTheScore = previousBall.GetComponent<DragShootAndTrackScore>().theScore;

            if(prevMultiplier>1){
                    prevscoreText.text = "Score: " + prevTheScore + " X " + prevMultiplier;
            }
            else{
                prevscoreText.text = "Score: " + prevTheScore;
            }
        }

    }
}
