using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;
    [SerializeField] public Transform startPosition;

    private void Awake()
    {
        instance = this;
    }
    
}
