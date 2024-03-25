using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum DirectType
{
    Forward = 0, Backward = 1,  Right = 2, Left = 3
}

public class Player : MonoBehaviour
{
    public float raycastStep = 0.1f;
    public int count = 0;
    private float raycast = 20f;
    private int currentAnimNumber;
    private Vector3 currentEndPosition;


    [SerializeField] private LayerMask layer;
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator anim;
    [SerializeField] Transform child;


    private bool isMoving = false;

    private void Start()
    {
        count = 0;
    }
    void FixedUpdate()
    {
        float step = 15 * Time.fixedDeltaTime;

        //forward------------------------------------------------------------------------------------------
        if (Input.GetAxisRaw("Vertical") > 0f && isMoving == false)
        {
            isMoving = true;
            MoveDirect(DirectType.Forward);
        }
        //Backward-----------------------------------------------------------------------------------------
        else if (Input.GetAxisRaw("Vertical") < 0f && isMoving == false)
        {
            isMoving = true;
            MoveDirect(DirectType.Backward);
        }
        //Right--------------------------------------------------------------------------------------------
        else if (Input.GetAxisRaw("Horizontal") > 0f && isMoving == false)
        {
            isMoving = true;
            MoveDirect(DirectType.Right);
        }
        //Left---------------------------------------------------------------------------------------------
        else if (Input.GetAxisRaw("Horizontal") < 0f && isMoving == false)
        {
            isMoving = true;
            MoveDirect(DirectType.Left);
        }
        //Debug.Log(count + "--------");

        if (isMoving == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentEndPosition, step);

            if (Vector3.Distance(transform.position, currentEndPosition) < 0.01f)
            {
                isMoving = false;
            }
        }
    }






//---------------------------------------------------------------------------------------------------------------------------------------------------
 

private void OnTriggerEnter(Collider collision)
    {
        if ( collision.tag == "BrickRoad")
        {
            count++;
            ChangeAnim("renwu", 1);
        }
        else
        {
            ChangeAnim("renwu", 0);
        }
    }

    private void ChangeAnim(string animName, int number)
    {
        if (currentAnimNumber != number )
        {
            anim.SetInteger(animName, number);
            currentAnimNumber = number;
        }
    }



    public void MoveDirect(DirectType directType) 
    {
        // di chuyen theo huong direct type
        Vector3 direct = Vector3.forward;

        switch (directType)
        {
            case DirectType.Forward:
                direct = Vector3.forward;
                break;
            case DirectType.Backward:
                direct = -Vector3.forward;
                break;
            case DirectType.Right:
                direct = Vector3.right;
                break;
            case DirectType.Left:
                direct = -Vector3.right;
                break;
        }
        MoveDirect(direct);
    }

    public void MoveDirect(Vector3 direct)
    {
        Debug.DrawRay(transform.position, direct * raycast, Color.blue);
        float step = 15 * Time.fixedDeltaTime;


        Physics.Raycast(transform.position, direct, out RaycastHit hitInfo, raycast, layer);
        if (hitInfo.collider.gameObject.CompareTag("Wall"))
        {
            Vector3 wallPosition = hitInfo.collider.transform.position;
            Vector3 endPosition = new Vector3(wallPosition.x - direct.x * 1, transform.position.y, wallPosition.z - direct.z * 1);
            if (currentEndPosition != endPosition)
            {
                currentEndPosition = endPosition;
            }
            transform.position = Vector3.MoveTowards(transform.position, currentEndPosition, step);
        }
    }
}
