using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Vector3 SpawnPos;
    public GameObject SpawnObject;
    private float newSpawnDuration = .1f;
    public GameObject tempObject; 
    public GameObject previous;

    #region Singleton

    public static Spawner Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        SpawnPos = transform.position;
    }

    void SpawnNewObject()
    {
        //IF GOAL, DONT SPAWN
        //IF PAUSE MENU, CHECK TO SPAWN?
        if(SpawnObject!=null){
            previous = SpawnObject;
            tempObject = Instantiate(SpawnObject, SpawnPos, Quaternion.identity);
        }
        else
        {
            previous = tempObject;
            tempObject = Instantiate(tempObject, SpawnPos, Quaternion.identity);
        }
    }
        //BUT Y DOES THE BALL NOT SPAWN IN WHILE ON PAUSE MENU ANYWAYS? -- NOT KNOWING FORCED ME TO DO A "HACK"
        //!!!!!!!!!!!I THINK THE BALL ISNT SPAWNING BC I DEACTIVATE THE OTHER BALL SO WHEN THE TIME COMES TO SPAWN IT, ITS DEACTIVATED???
    public void NewSpawnRequest()//this happens when shot is triggered, but wont happen if in pause menu at that second
    {           //WHAT IF I JUST KEEP A CHECK TO PAUSE SIMILAR TO OTHER INVOKE METHOD I USED!!!!
    //idea: when unpausing, if no temp object, call new SpanwNewObject
    //OR JUST DO A 1SEC INVOKE WHEN UNPAUSING, WHICH THEN AFTERWARDS CHECKS IF BALL SPAWNED IN
        Invoke("SpawnNewObject", newSpawnDuration);
    }
    /*
    public void UnPauseSpawnRequest(){
        if(tempObject==null){
            Invoke("SpawnNewObject", newSpawnDuration);
        }
    }
    */
    public void RemoveSpawnedItem(GameObject itemToUnspawn)
    {
        Destroy(itemToUnspawn);
    }
}
