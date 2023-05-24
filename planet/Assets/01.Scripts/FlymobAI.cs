using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class FlymobAI : MonoBehaviour
{
    [Range(1, 15)]
    [SerializeField]
    private float viewRadius = 11;

    [SerializeField]
    private float detectionCheckDelay = 0.1f;

    [SerializeField]
    private Transform target;

    public int damage = 1;

    [SerializeField]
    private LayerMask playerLayerMask;

    [SerializeField]
    private LayerMask visibilityLayer;

    public SkeletonAnimation skeletonAnimation;
    public GameObject thisObject;
    public string enemyTag;
    private Transform enemyTransform;
    private Transform thisTransform;

    public Vector2 Direction;

    public float FireRate;
    float nextTimeToFire = 0;
    public float Force;

    public bool TargetVisible { get; private set; }

    private OrbitMob OM;

    private TurretHP turretHP;
    private RocketHP rocketHP;
    private new string name;

    public Transform Target
    {
        get => target;
        set
        {
            target = value;
            TargetVisible = false;
        }
    }

    private void Start()
    {
        StartCoroutine(DetectionCoroutine());
        OM = GetComponent<OrbitMob>();
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        skeletonAnimation.AnimationState.SetAnimation(0, "Idle_Loop", true);
        skeletonAnimation.timeScale = 1;
    }

    private void Update()
    {
        if (Target != null)
            TargetVisible = CheckTargetVisible();
    }

    private bool CheckTargetVisible()
    {
        var result = Physics2D.Raycast(transform.position, Target.position - transform.position, viewRadius, visibilityLayer);
        if (result.collider != null)
        {
            return (playerLayerMask & (1 << result.collider.gameObject.layer)) != 0;
        }
        Debug.Log("No object hit");
        return false;
    }

    private void DetectTarget()
    {
        if (Target == null)
        {
            CheckIfPlayerInRange();
        }
        else if (Target != null)
        {
            DetectIfOutOfRange();
            CheckIfPlayerInRange();
            Debug.Log("나감");
        }
    }

    private void DetectIfOutOfRange()
    {
        if (Target == null || Target.gameObject.activeSelf == false || Vector2.Distance(transform.position, Target.position) > viewRadius + 1)
        {
            skeletonAnimation.AnimationState.SetAnimation(0, "Idle_Loop", true);
            skeletonAnimation.timeScale = 1;
            Target = null;
            Debug.Log("탈출함");
        }
    }

    private void CheckIfPlayerInRange()
    {
        GameObject enemyObject = GameObject.FindGameObjectWithTag(enemyTag);
        Collider2D collision = Physics2D.OverlapCircle(transform.position, (viewRadius / 2), playerLayerMask);
        if (collision != null)
        {
            Target = collision.transform;
            name = collision.gameObject.name;
            if (Target != null)
            {
                GameObject targetObject = GameObject.Find(name);
                Debug.Log(name);

                rocketHP = targetObject.GetComponent<RocketHP>();

                turretHP = targetObject.GetComponent<TurretHP>();

                OM.MoveSpped(0.0f);
                skeletonAnimation.timeScale = 0.5f;
                skeletonAnimation.AnimationState.SetAnimation(0, "Attack", true);

            }
            if (Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1 / FireRate;
                if (name != null && name.Equals("Rocket"))
                {
                    rocketHP.TakeDamage(damage);
                }
                else
                {
                    turretHP.TakeDamage(damage);
                }
            }
        }
        else
        {
            skeletonAnimation.AnimationState.SetAnimation(0, "Idle_Loop", true);
            OM.MoveSpped(5.0f);
        }
    }

    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(detectionCheckDelay);
        DetectTarget();
        StartCoroutine(DetectionCoroutine());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, viewRadius / 2);
    }
}