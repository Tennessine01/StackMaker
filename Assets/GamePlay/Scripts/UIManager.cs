using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject nextLevelButton;
    public GameObject restartButton;
    public GameObject backGround;

    public void Awake()
    {
        instance = this;
    }
    [SerializeField] TMP_Text pointText;

    public void SetText(int point)
    {
        pointText.text = point.ToString();
    }

}
