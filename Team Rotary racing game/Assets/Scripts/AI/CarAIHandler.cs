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

    Vector3 targetPosition = Vector3.zero;
    Transform targetTransform = null;

    WaypointNode currentWaypoint = null;
    WaypointNode[] allWayPoints;

    PolygonCollider2D polygonCollider2D;

    TopDownCarController topDownCarController;

    void Awake()
    {
    }

    void Start()
    {
        topDownCarController = GetComponent<TopDownCarController>();
        allWayPoints = FindObjectsOfType<WaypointNode>();

        polygonCollider2D = GetComponentInChildren<PolygonCollider2D>();

    }

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

            topDownCarController.SetInputVector(inputVector);
        }
    }

    void FollowPlayer()
    {
        if (targetTransform == null)
            targetTransform = GameObject.FindGameObjectWithTag("Car").transform;

        if (targetTransform != null)
            targetPosition = targetTransform.position;
    }

    void FollowWaypoints()
    {
        if (currentWaypoint == null)
            currentWaypoint = FindClosestWayPoint();

        if (currentWaypoint != null)
        {
            targetPosition = currentWaypoint.transform.position;

            float distanceToWayPoint = (targetPosition - transform.position).magnitude;

            if (distanceToWayPoint <= currentWaypoint.minDistanceToReachWaypoint)
            {
                if (currentWaypoint.maxSpeed > 0)
                    maxSpeed = currentWaypoint.maxSpeed;
                else maxSpeed = 1000;
                currentWaypoint = currentWaypoint.nextWaypointNode[0];
            }
        }
    }


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

        float angleToTarget = Vector2.SignedAngle(transform.up, vectorToTarget);
        angleToTarget *= -1;

        float steerAmount = angleToTarget / 45.0f;

        steerAmount = Mathf.Clamp(steerAmount, -1.0f, 1.0f);

        return steerAmount;
    }

    float ApplyThrottleOrBrake(float inputX)
    {
        if (topDownCarController.GetVelocityMagnitude() > maxSpeed)
            return 0;
        return 1.05f - Mathf.Abs(inputX) / 1.0f;
    }
}