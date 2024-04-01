using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private GameObject brick;
    public bool isActive = true;

    void Start()
    {
        OnInit();
    }                                                                                                                                                                                                                                                                                                                                                                                                               

    private void OnInit()
    {
        isActive = true;
        brick.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isActive)
        {
            if(brick != null)
            {
                isActive = false;
                brick.SetActive(false);
                BrickManager.instance.AddBrick();
            }
        }
    }
}
