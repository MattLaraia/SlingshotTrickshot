using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTrajectory : MonoBehaviour
{

    [SerializeField]
    private LineRenderer lineRenderer;

    [SerializeField]
    [Range(3,30)]
    private int lineSegmentCount = 30;

    private List<Vector3> linePoints = new List<Vector3>();


    #region Singleton

    public static DrawTrajectory Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion


    public void UpdateTrajectory(Vector3 forceVector, Rigidbody rigidBody, Vector3 startingPoint)
    {
        Vector3 velocity = (forceVector/(rigidBody.mass))*Time.fixedDeltaTime;


        float FlightDuration = (2*velocity.y)/Physics.gravity.y;

        float stepTime = FlightDuration/lineSegmentCount;

        linePoints.Clear();

        for(int i =0; i<lineSegmentCount; i++)
        {
            float stepTimePassed = stepTime * i;

            Vector3 movementVector = new Vector3(
                velocity.x*stepTimePassed,
                velocity.y*stepTimePassed - .5f*Physics.gravity.y*stepTimePassed*stepTimePassed,
                velocity.z*stepTimePassed
            );
            /*
            RaycastHit hit;
            if(Physics.Raycast(startingPoint, -movementVector, out hit, MovementVector.magnitude))
            {
                break;
            }

            */
            
            Vector3 distanceFromSlingshot = -movementVector+startingPoint;
            
            linePoints.Add(-movementVector+startingPoint);
            
            if(movementVector.z<-1)
            {
                break;
            }

        }
        lineRenderer.positionCount = linePoints.Count;
        lineRenderer.SetPositions(linePoints.ToArray());

    }
    public void HideLine()
    {
        lineRenderer.positionCount = 0;
    }
}
