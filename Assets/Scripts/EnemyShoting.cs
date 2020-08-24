using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoting : MonoBehaviour
{
    public float maxDamage = 120f;
    public float minDamage = 45f;
    public AudioClip shotClip;
    public float flashIntensity = 3f;
    public float fadeSpeed = 10f;

    private Animator anim;
    private HashKeyAnimation hashId;
    private LineRenderer laserShotLine;
    private Light laserShotLight;
    private SphereCollider col;
    private Transform player;
    private PlayerHealth health;
    private bool shooting;
    private float scaledDamage;

    void Awake()
    {
        anim = GetComponent<Animator>();
        hashId = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashKeyAnimation>();
        laserShotLine = GetComponentInChildren<LineRenderer>();
        laserShotLight = laserShotLine.gameObject.GetComponent<Light>();
        col = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        health = player.GetComponent<PlayerHealth>();

        laserShotLine.enabled = false;
        laserShotLight.intensity = 0f;

        scaledDamage = maxDamage - minDamage;
    }

    void Update()
    {
        float shot = anim.GetFloat(hashId.shotFloat);

        if (shot > 0.5f && !shooting)
        {
            shoot();
        }
        if(shot < 0.5f )
        {
            shooting = false;
            laserShotLine.enabled = false;
        }

        laserShotLight.intensity = Mathf.Lerp(laserShotLight.intensity, 0f, fadeSpeed * Time.deltaTime);
    }

    void OnAnimatorIK(int layerIndex)
    {
        float aimWeight = anim.GetFloat(hashId.aimWeightFloat);
        anim.SetIKPosition(AvatarIKGoal.RightHand, player.position + Vector3.up * 1.5f);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, aimWeight);
    }
    void shoot()
    {
        shooting = true;
        float fractionDistance = (col.radius - (Vector3.Distance(transform.position , player.position))) / col.radius;
        float damage = scaledDamage * fractionDistance + minDamage;
        health.TakeDamage(damage);
        shotEffect();
    }

    void shotEffect()
    {
        laserShotLine.SetPosition(0, laserShotLine.transform.position);
        laserShotLine.SetPosition(1, player.position + Vector3.up * 1.5f);
        laserShotLine.enabled = true;
        laserShotLight.intensity = flashIntensity;
        AudioSource.PlayClipAtPoint(shotClip, laserShotLine.transform.position);
    }
}
