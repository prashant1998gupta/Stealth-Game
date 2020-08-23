using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashKeyAnimation : MonoBehaviour
{

    public int dyingState;
    public int deadBool;
    public int locomotionState;
    public int shoutState;
    public int speedFloat;
    public int shoutingBool;
    public int sneakingBool;
    public int playerInSightBool;
    public int shotFloat;
    public int aimWeightFloat;
    public int AngularSpeedFloat;
    public int openBool;


    // Start is called before the first frame update
    void Start()
    {
        dyingState = Animator.StringToHash("Base.Dying");
        deadBool = Animator.StringToHash("Dead");
        locomotionState = Animator.StringToHash("Locomotion");
        shoutState = Animator.StringToHash("Shouting.Shout");
        speedFloat = Animator.StringToHash("Speed");
        shoutingBool = Animator.StringToHash("Shouting");
        sneakingBool = Animator.StringToHash("Sneaking");
        playerInSightBool = Animator.StringToHash("PlayerInSight");
        shotFloat = Animator.StringToHash("Shot");
        aimWeightFloat = Animator.StringToHash("AimWeight");
        AngularSpeedFloat = Animator.StringToHash("AngularSpeed");
        openBool = Animator.StringToHash("Open");

    }

   
}
