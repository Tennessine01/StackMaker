using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Win : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("%%%%%" + other.gameObject.name );
        if (other.CompareTag("Player"))
        {
            BrickManager.instance.ClearBrick();
            AnimManager.instance.ChangeAnim("Win");

            Invoke("NextLevel", 5f);
        }
    }
    private void NextLevel()
    {
        UIManager.instance.backGround.SetActive(true);
        UIManager.instance.nextLevelButton.SetActive(true);
        UIManager.instance.restartButton.SetActive(true);
    }
}
