using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public enum DirectType
{
    None = -1, Forward = 0, Backward = 1, Right = 2, Left = 3
}

public class BrickManager : MonoBehaviour
{
    public static BrickManager instance;

    public float raycastStep = 0.1f;
    public int count;
    public int point;

    private List<GameObject> bricks = new List<GameObject>();

    [SerializeField] private LayerMask layer;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform character;
    [SerializeField] private GameObject brick;

    //[SerializeField] private GameObject brick_road;
    //[SerializeField] private GameObject brick_bridge;
    //[SerializeField] private Brick brickScript;
    //[SerializeField] private UnBrick unBrickScript;       

    private Quaternion rotation = Quaternion.Euler(-90f, 0f, 0f);
    public void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        count = 0;
        point = 0;
    }
    
    public  void AddBrick()
    {
        count++;
        point++;
        UIManager.instance.SetText(point);
        Vector3 CharPosition = transform.position + new Vector3(0, 0.35f * (count + 1) -0.7f , 0);
        Vector3 BrickPosition = transform.position + new Vector3(0, 0.35f * count -0.7f, 0);

        character.position = CharPosition;

        //character.position += Vector3.up * 0.35f;

        GameObject newBrick = Instantiate(brick, BrickPosition, rotation);
        bricks.Add(newBrick);
        newBrick.transform.SetParent(transform);
    }

    public void RemoveBrick()
    {
        if (bricks.Count > 0 && count > 0)
        {
            count--;
            GameObject lastBrick = bricks[bricks.Count - 1];
            bricks.RemoveAt(bricks.Count - 1);
            Destroy(lastBrick); 

            character.position +=  - Vector3.up*0.35f;
            //Debug.Log(bricks.Count);
        }
        else
        { 
            LevelManager.instance.RestartLevel();
            OnInit();
            MovementManager.instance.OnInit();
        }
    }

    public void ClearBrick()
    {
        foreach (var brick in bricks)
        {
            Destroy(brick);
        }
        bricks.Clear();

        //Debug.Log(bricks.Count + "---");

        count = 0;

        character.position = transform.position;
        //character.position = MovementManager.Instance.EndPosition() - new Vector3(0,0,-2.5f);
    }   
}