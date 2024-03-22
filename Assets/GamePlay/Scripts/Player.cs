using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    private float raycast = 20f;
    public float raycastStep = 0.1f;
    public int count = 0;
    [SerializeField] private LayerMask layer;
    [SerializeField] private float speed = 10;

    private bool isRight = false;
    private bool isLeft = false;
    private bool isUp = false;
    private bool isDown = false;

    void Update()
    {
        //horizontalInput = Input.GetAxis("Horizontal");
        //verticalInput = Input.GetAxis("Vertical");

        //transform.Translate(Vector3.forward * Time.deltaTime * verticalInput);
        //transform.Translate(Vector3.right * Time.deltaTime * horizontalInput);


        //Debug.Log(CheckCollision(raycastStep));

        //forward
        if (Input.GetAxisRaw("Vertical") > 0f && isDown == false && isRight == false && isLeft == false)
        {
            isUp = true;
            Debug.DrawRay(transform.position, Vector3.forward * raycast, Color.blue);

            Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hitInfo, raycast, layer);
            if (hitInfo.collider.gameObject.CompareTag("Wall"))
            {
                Debug.Log("hitwall");
                Vector3 wallPosition = hitInfo.collider.transform.position;
                //ransform.position = hitInfo.collider.transform.position-transform.forward*1;
                //transform.position = new Vector3( wallPosition.x - transform.forward.x * 1, transform.position.y, wallPosition.z - transform.forward.z * 1);

                float step = speed * Time.deltaTime;
                Vector3 endPosition = Vector3.MoveTowards(transform.position, new Vector3(wallPosition.x - transform.forward.x * 1, transform.position.y, wallPosition.z - transform.forward.z * 1), step);
                transform.position = endPosition;

                if (transform.position == new Vector3(wallPosition.x - transform.forward.x * 1, transform.position.y, wallPosition.z - transform.forward.z * 1))
                {
                    isUp = false;
                }
            }
            //Debug.Log(isUp + "----------");
            
        
        }
        //Backward
        if (Input.GetAxisRaw("Vertical") < 0f && isUp == false && isRight == false && isLeft == false)
        {
            isDown = true;
            Debug.DrawRay(transform.position, -Vector3.forward * raycast, Color.blue);


            Physics.Raycast(transform.position, -Vector3.forward, out RaycastHit hitInfo, raycast, layer);
            if (hitInfo.collider.gameObject.CompareTag("Wall"))
            {
                Debug.Log("hitwall");
                //transform.position = hitInfo.collider.transform.position + transform.forward * 1;

                Vector3 wallPosition = hitInfo.collider.transform.position;
                //transform.position = new Vector3(wallPosition.x + transform.forward.x * 1, transform.position.y, wallPosition.z + transform.forward.z * 1);
                float step = speed * Time.deltaTime;
                Vector3 endPosition = Vector3.MoveTowards(transform.position, new Vector3(wallPosition.x + transform.forward.x * 1, transform.position.y, wallPosition.z + transform.forward.z * 1), step);
                transform.position = endPosition;
                if (transform.position == new Vector3(wallPosition.x + transform.forward.x * 1, transform.position.y, wallPosition.z + transform.forward.z * 1))
                {
                    isDown = false;
                }
            }
        }
        //Right
        if (Input.GetAxisRaw("Horizontal") > 0f && isDown == false && isUp == false && isLeft == false)
        {
            isRight = true;
            Debug.DrawRay(transform.position, Vector3.right * raycast, Color.blue);

            Physics.Raycast(transform.position, Vector3.right, out RaycastHit hitInfo, raycast, layer);
            if (hitInfo.collider.gameObject.CompareTag("Wall"))
            {
                Debug.Log("hitwall");
                //transform.position = hitInfo.collider.transform.position - transform.right * 1;

                Vector3 wallPosition = hitInfo.collider.transform.position;
                //transform.position = new Vector3(wallPosition.x - transform.right.x * 1, transform.position.y, wallPosition.z - transform.right.z * 1);
                float step = speed * Time.deltaTime;
                Vector3 endPosition = Vector3.MoveTowards(transform.position, new Vector3(wallPosition.x - transform.right.x * 1, transform.position.y, wallPosition.z - transform.right.z * 1), step);
                transform.position = endPosition;
                if(transform.position == new Vector3(wallPosition.x - transform.right.x * 1, transform.position.y, wallPosition.z - transform.right.z * 1) )
                {
                    isRight = false;
                }
            }
        }
        //Left
        if (Input.GetAxisRaw("Horizontal") < 0f && isRight == false && isUp == false && isDown == false)
        {
            isLeft = true;
            Debug.DrawRay(transform.position, Vector3.left * raycast, Color.blue);

            Physics.Raycast(transform.position, -Vector3.right, out RaycastHit hitInfo, raycast, layer);
            if (hitInfo.collider.gameObject.CompareTag("Wall"))
            {
                Debug.Log("hitwall");
                //transform.position = hitInfo.collider.transform.position + transform.right * 1;
                Vector3 wallPosition = hitInfo.collider.transform.position;
                //transform.position = new Vector3(wallPosition.x + transform.right.x * 1, transform.position.y, wallPosition.z + transform.right.z * 1);
                float step = speed * Time.deltaTime;
                Vector3 endPosition = Vector3.MoveTowards(transform.position, new Vector3(wallPosition.x + transform.right.x * 1, transform.position.y, wallPosition.z + transform.right.z * 1), step);
                transform.position = endPosition;
                if(transform.position == new Vector3(wallPosition.x + transform.right.x * 1, transform.position.y, wallPosition.z + transform.right.z * 1))
                {
                    isLeft = false;
                }
            }
        }
    }

}
