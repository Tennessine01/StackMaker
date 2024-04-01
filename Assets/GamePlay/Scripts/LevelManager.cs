using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] public List<GameObject> mapPrefabs = new List<GameObject>();

    [SerializeField] Transform player;

    private int currentMapIndex = 0;
    private GameObject currentMap;


    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        AnimManager.instance.ChangeAnim("Idle");
        currentMapIndex = Random.Range(0, mapPrefabs.Count);
        currentMap = Instantiate(mapPrefabs[currentMapIndex], transform);

        player.position =   MapManager.instance.startPosition.position;
        MovementManager.instance.OnInit();
        BrickManager.instance.OnInit();
        BrickManager.instance.ClearBrick();

        UIManager.instance.backGround.SetActive(false);
        UIManager.instance.nextLevelButton.SetActive(false);
        UIManager.instance.restartButton.SetActive(false);

    }

    public void OnDespawn()
    {
        Destroy(currentMap);
    }

    public void NextLevel()
    {
        currentMapIndex = (currentMapIndex + 1) % mapPrefabs.Count;
        OnDespawn();
        OnInit();
    }

    public void RestartLevel()
    {
        OnDespawn();

        UIManager.instance.backGround.SetActive(false);
        UIManager.instance.nextLevelButton.SetActive(false);
        UIManager.instance.restartButton.SetActive(false);
        AnimManager.instance.ChangeAnim("Idle");
        currentMap = Instantiate(mapPrefabs[currentMapIndex], transform);

        player.position = MapManager.instance.startPosition.position;
        MovementManager.instance.OnInit();

        BrickManager.instance.ClearBrick();
        BrickManager.instance.OnInit();

    }

}
