using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MovementManager : MonoBehaviour
{
    public static MovementManager instance;
    [SerializeField] public Transform player;
    [SerializeField] private LayerMask layer;


    public int pixelToDetect = 20;
    public bool fingerDown = false;

    private float raycast = 50f;
    private Vector2 startPos;
    private Vector3 currentEndPosition;
    private bool isMoving = false;


    DirectType DirectType;

    public void Awake()
    {
        instance = this;
    }
    public void OnInit()
    {
        isMoving = false;

    }
    // Update is called once per frame
    void Update()
    {
        if (fingerDown == false && Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            fingerDown = true;
        }
        if (fingerDown)
        {
            //startPos = Input.mousePosition;

            DirectType = GetDirect();
            if (DirectType != DirectType.None)
            {
                CheckMove(MoveDirect(DirectType));
            }
            //Debug.Log(GetDirect() + "--------");
        }
        if (fingerDown && Input.GetMouseButtonUp(0))
        {
            fingerDown = false;
        }
        Move(currentEndPosition);
    }
    public DirectType GetDirect()
    {
        if (Input.mousePosition.y >= startPos.y + pixelToDetect && !isMoving)
        {
            fingerDown = false;
            return DirectType.Forward;
        }
        if (Input.mousePosition.y <= startPos.y - pixelToDetect && !isMoving)
        {
            fingerDown = false;
            return DirectType.Backward;
        }
        if (Input.mousePosition.x >= startPos.x + pixelToDetect && !isMoving)
        {
            fingerDown = false;
            return DirectType.Right;
        }
        if (Input.mousePosition.x <= startPos.x - pixelToDetect && !isMoving)
        {
            fingerDown = false;
            return DirectType.Left;
        }
        return DirectType.None;
    }

    public Vector3 MoveDirect(DirectType directType)
    {
        // di chuyen theo huong direct type
        Vector3 direct = Vector3.forward;

        switch (directType)
        {
            case DirectType.Forward:
                isMoving = true;
                direct = Vector3.forward;
                break;
            case DirectType.Backward:
                isMoving = true;
                direct = -Vector3.forward;
                break;
            case DirectType.Right:
                isMoving = true;
                direct = Vector3.right;
                break;
            case DirectType.Left:
                isMoving = true;
                direct = -Vector3.right;
                break;
            case DirectType.None:
                isMoving = false;
                direct = Vector3.zero;
                break;
        }
        return direct;
    }

    public void CheckMove(Vector3 direct)
    {
        Physics.Raycast(player.position, direct, out RaycastHit hitInfo, raycast, layer);
        if (hitInfo.collider.gameObject.CompareTag("Wall"))
        {
            Vector3 wallPosition = hitInfo.collider.transform.position;
            Vector3 endPosition = new Vector3(wallPosition.x - direct.x * 1, player.position.y, wallPosition.z - direct.z * 1);
            if (currentEndPosition != endPosition)
            {
                currentEndPosition = endPosition;
            }
        }
    }

    public Vector3 EndPosition()
    {
        return currentEndPosition;
    }

    public void Move(Vector3 endPos)    
    {
        if (isMoving == true)
        {
            player.position = Vector3.MoveTowards(player.position, endPos, Time.deltaTime*10f);
            if (Vector3.Distance(player.position, endPos) < 0.01f)
            {
                isMoving = false;
                //AnimManager.instance.ChangeAnim("Jump");
            }
        }       
    }
}