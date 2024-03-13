using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class YellowButton : MonoBehaviour
{
    [SerializeField] AnimationClip buttonClickedAnim;
    [SerializeField] AnimationClip obstacleOpeningAnim;
    [SerializeField]private GameObject yellowObstacle;

    private Animator anim;
    private Animator yellowObstacleAnim;
    private bool gateAlreadyOpened = false;

    public static Action<float> yellowObstacleOpened;

    void Start()
    {
        GameObject  Parent_ = transform.parent.gameObject;
        anim = Parent_.GetComponent<Animator>();
        yellowObstacleAnim = yellowObstacle.GetComponent<Animator>();
        
    }

    private void OnMouseDown()
    {
        if (!gateAlreadyOpened)
        {

            gateAlreadyOpened = true;
            anim.SetBool("ButtonClicked", true);

            yellowObstacleAnim.SetBool("ButtonClicked", true);
            yellowObstacleOpened?.Invoke(obstacleOpeningAnim.length);

            Invoke("EndClickAnimation", buttonClickedAnim.length - 0.5f);
            Invoke("EndObstacleAnimation", obstacleOpeningAnim.length + 0.1f);


        }
    }

    private void EndClickAnimation()
    {
        anim.SetBool("ButtonClicked",false);
    }

    private void EndObstacleAnimation()
    {
        yellowObstacleAnim.SetBool("ButtonClicked", false);
        gateAlreadyOpened = false;
    }
}
