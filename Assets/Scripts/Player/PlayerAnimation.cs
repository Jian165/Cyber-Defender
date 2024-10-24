using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private const string IS_Forward = "IsForward";
    private const string IS_Backward = "IsBackward";
    private const string IS_Left = "IsLeft";
    private const string IS_Right = "IsRight";
    [SerializeField] private Player player;
    private void Awake()
    {
       animator = GetComponent<Animator>();
    }

    private void Update()
    {
        MovementAnimation();
    }

    private void MovementAnimation()
    {
        if (player.WalikngDirection().y > 0)
        {
            animator.SetBool(IS_Forward, true);
        }
        else if (player.WalikngDirection().y < 0)
        {

            animator.SetBool(IS_Backward, true);
        }
        else if (player.WalikngDirection().y == 0 && player.WalikngDirection().x < 0)
        {
            animator.SetBool(IS_Left, true);
        }
        else if (player.WalikngDirection().y == 0 && player.WalikngDirection().x > 0)
        {
            animator.SetBool(IS_Right, true);
        }
        else
        {
            animator.SetBool(IS_Right, false);
            animator.SetBool(IS_Left, false);
            animator.SetBool(IS_Forward, false);
            animator.SetBool(IS_Backward, false);
        }


        
    }
}

