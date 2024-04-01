using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimManager : MonoBehaviour
{
    public static AnimManager instance;
    private string currentAnimNumber;

    [SerializeField] private Animator anim;

    public void Awake()
    {
        instance = this;
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnimNumber != animName)
        {
            anim.SetTrigger(animName);
            currentAnimNumber = animName;
        }
    }
}
