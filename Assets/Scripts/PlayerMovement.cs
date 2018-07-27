using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{

    private GameController gameController;
    private NavMeshAgent navAgent;
    private Vector3 moveToPoint;

    // Use this for initialization
    void Start ()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        navAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // detects where on the nav mesh the mouse is currently pointing
        FollowMouse();

        // if the left mouse button is being held down on a point on the nav mesh, move the player to that point
        if(Input.GetMouseButton(0) && gameController.GetBattleTriggered() == false)
        {
            navAgent.SetDestination(moveToPoint);
        }

        if (gameController.GetBattleTriggered()) navAgent.isStopped = true;
	}



    private void FollowMouse()
    {
        // casts a ray from the camera to the mouse position
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // creates an infinitely large plane 
        Plane detectPlane = new Plane(Vector3.up, Vector3.zero);

        // declare a ray distance float
        float rayDistance;

        // if the camera ray hits the plane...
        // (if statement also outputs the distance at which this intersection occurs)
        if (detectPlane.Raycast(camRay, out rayDistance))
        {
            // set the intersection point as the ray of intersection and the distance it intersects with the plane
            moveToPoint = camRay.GetPoint(rayDistance);

            Debug.DrawLine(camRay.origin, moveToPoint);
        }
    }


    public bool isMoving()
    {
        if (navAgent.velocity != Vector3.zero) return true;
        else return false;            
    }
}
