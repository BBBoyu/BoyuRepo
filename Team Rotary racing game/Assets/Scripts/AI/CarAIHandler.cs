using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CarAIHandler : MonoBehaviour
{
    public enum AIMode { followPlayer, followWaypoints};

    [Header("AI settings")]
    public AIMode aiMode;
    public float maxSpeed = 16;

    //Local variables
    Vector3 targetPosition = Vector3.zero;
    Transform targetTransform = null;

    //Waypoints
    WaypointNode currentWaypoint = null;
    WaypointNode[] allWayPoints;

    //Colliders
    PolygonCollider2D polygonCollider2D;

    //Components
    TopDownCarController topDownCarController;

    //Awake is called when the script instance is being loaded.
    void Awake()
    {
        /*
        topDownCarController = GetComponent<TopDownCarController>();
        allWayPoints = FindObjectsOfType<WaypointNode>();

        polygonCollider2D = GetComponentInChildren<PolygonCollider2D>();
        */
    }

    // Start is called before the first frame update
    void Start()
    {
        topDownCarController = GetComponent<TopDownCarController>();
        allWayPoints = FindObjectsOfType<WaypointNode>();

        polygonCollider2D = GetComponentInChildren<PolygonCollider2D>();

    }

    // Update is called once per frame and is frame dependent
    void FixedUpdate()
    {
        Vector2 inputVector = Vector2.zero;

        if (GameController__update.instance.gamePlaying)
        {

            switch (aiMode)
            {
                case AIMode.followPlayer:
                    FollowPlayer();
                    break;

                case AIMode.followWaypoints:
                    FollowWaypoints();
                    break;
            }

            inputVector.x = TurnTowardTarget();
            inputVector.y = ApplyThrottleOrBrake(inputVector.x);

            //Send the input to the car controller.
            topDownCarController.SetInputVector(inputVector);
        }
    }

    //AI follows player
    void FollowPlayer()
    {
        if (targetTransform == null)
            targetTransform = GameObject.FindGameObjectWithTag("Car").transform;

        if (targetTransform != null)
            targetPosition = targetTransform.position;
    }

    //AI follows waypoints
    void FollowWaypoints()
    {
        //Pick the cloesest waypoint if we don't have a waypoint set.
        if (currentWaypoint == null)
            currentWaypoint = FindClosestWayPoint();

        //Set the target on the waypoints position
        if (currentWaypoint != null)
        {
            //Debug.Log(currentWaypoint);
            //Set the target position of for the AI. 
            targetPosition = currentWaypoint.transform.position;

            //Store how close we are to the target
            float distanceToWayPoint = (targetPosition - transform.position).magnitude;

            //Check if we are close enough to consider that we have reached the waypoint
            //Debug.Log(distanceToWayPoint);
            if (distanceToWayPoint <= currentWaypoint.minDistanceToReachWaypoint)
            {
                if (currentWaypoint.maxSpeed > 0)
                    maxSpeed = currentWaypoint.maxSpeed;
                else maxSpeed = 1000;

                //If we are close enough then follow to the next waypoint, if there are multiple waypoints then pick one at random.
                //currentWaypoint = currentWaypoint.nextWaypointNode[Random.Range(0, currentWaypoint.nextWaypointNode.Length)];
                currentWaypoint = currentWaypoint.nextWaypointNode[0];
            }
        }
    }


    //Find the cloest Waypoint to the AI
    WaypointNode FindClosestWayPoint()
    {
        return allWayPoints
            .OrderBy(t => Vector3.Distance(transform.position, t.transform.position))
            .FirstOrDefault();
    }

    float TurnTowardTarget()
    {
        Vector2 vectorToTarget = targetPosition - transform.position;
        vectorToTarget.Normalize();

        //Calculate an angle towards the target 
        float angleToTarget = Vector2.SignedAngle(transform.up, vectorToTarget);
        angleToTarget *= -1;

        //We want the car to turn as much as possible if the angle is greater than 45 degrees and we wan't it to smooth out so if the angle is small we want the AI to make smaller corrections. 
        float steerAmount = angleToTarget / 45.0f;

        //Clamp steering to between -1 and 1.
        steerAmount = Mathf.Clamp(steerAmount, -1.0f, 1.0f);

        return steerAmount;
    }

    float ApplyThrottleOrBrake(float inputX)
    {
        //If we are going too fast then do not accelerate further. 
        if (topDownCarController.GetVelocityMagnitude() > maxSpeed)
            return 0;

        //Apply throttle forward based on how much the car wants to turn. If it's a sharp turn this will cause the car to apply less speed forward.
        return 1.05f - Mathf.Abs(inputX) / 1.0f;
    }
}