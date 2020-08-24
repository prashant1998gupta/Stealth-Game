using UnityEngine;

public class AnimatorSetup
{
    public float speedDampTime = 0.1f;
    public float angularSpeedDampTime = 0.7f;
    public float angleResposseTime = 0.6f;

    private Animator anim;
    private HashKeyAnimation hashId;

    public AnimatorSetup(Animator anim , HashKeyAnimation hashId)
    {
        this.anim = anim;
        this.hashId = hashId;
    }

    public void SetUp(float speed , float angle)
    {
        //Debug.Log($"speed { speed} angle {angle}");
        float angularSpeed = angle / angleResposseTime;

        anim.SetFloat(hashId.speedFloat, speed, speedDampTime, Time.deltaTime);
        anim.SetFloat(hashId.AngularSpeedFloat, angularSpeed, angularSpeedDampTime, Time.deltaTime);
    }
}
