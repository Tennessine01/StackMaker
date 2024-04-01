using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnBrick : MonoBehaviour
{
    [SerializeField] private GameObject unBrick;
    public bool isActive = false;

    void Start()
    {
       OnInit();
    }
    private void OnInit()
    {
        isActive = false;
        unBrick.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("%%%%%" + other.gameObject.name );
        if (other.CompareTag("Player") && !isActive)
        {
            if (unBrick != null)
            {
                isActive = true;
                unBrick.SetActive(true);
                BrickManager.instance.RemoveBrick();
            }
        }
    }
}
