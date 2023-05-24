using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;



//구현방식
//RayCast로 선탐지 -> 원 범위로 후탐지 -> 탐지 -> 사격
//코루틴으로 반복

public class FlymobAI : MonoBehaviour
{
    // 시야 반경
    [Range(1, 15)]
    [SerializeField] //해당 맴버변수를 Inspector로 사용하기 위해 선언
    private float viewRadius = 11;

    // 탐지 체크 주기
    [SerializeField]
    private float detectionCheckDelay = 0.1f;

    // 타겟
    [SerializeField]
    private Transform target;

    public int damage = 1;

    // 플레이어 레이어 마스크
    [SerializeField]
    private LayerMask playerLayerMask;

    // 가시성 레이어
    [SerializeField]
    private LayerMask visibilityLayer;

    //스켈레톤 오브젝트
    public SkeletonAnimation skeletonAnimation;
    public GameObject thisObject;
    public string enemyTag;
    private Transform enemyTransform;
    private Transform thisTransform;

    public Vector2 Direction;
    // public GameObject Gun;

    //총알
    public float FireRate;
    float nextTimeToFire = 0;
    public float Force;
    //public Transform Shootpoint;
    //aim bone
    //private string aimbone = Aim;

    // 타겟이 가시화면 내 있는지 여부
    [field: SerializeField]
    public bool TargetVisible { get; private set; }

    private OrbitMob OM;

    private TurretHP turretHP;
    private RocketHP rocketHP;
    private string name;
    public float moveSpeed;

    //타겟 식별 람다식
    public Transform Target
    {
        get => target;
        set
        {
            target = value;
            TargetVisible = false;
        }
    }

    //코루틴 시작
    private void Start()
    {
        StartCoroutine(DetectionCoroutine());
        OM = GetComponent<OrbitMob>();
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        skeletonAnimation.AnimationState.SetAnimation(0, "Idle_Loop", true);
        skeletonAnimation.timeScale = 1;
    }

    //매 프레임마다 타겟식별 체크
    private void Update()
    {
        if (Target != null)
        TargetVisible = CheckTargetVisible();

    }

    //레이케스트로 타겟 식별 함수
    private bool CheckTargetVisible()
    {

        var result = Physics2D.Raycast(transform.position, Target.position - transform.position, viewRadius, visibilityLayer);
        if (result.collider != null)
        {
            return (playerLayerMask & (1 << result.collider.gameObject.layer)) != 0;
        }
        Debug.Log("No object hit"); // 디버그 로그 출력
        return false;
    }

    //타겟탐지
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

    //타겟이 시야에서 살아졌는지 체크
    private void DetectIfOutOfRange()
    {
        if (Target == null || Target.gameObject.activeSelf == false || Vector2.Distance(transform.position, Target.position) > viewRadius + 1)
        {
            skeletonAnimation.AnimationState.SetAnimation(0, "Idle_Loop", true);
            skeletonAnimation.timeScale = 1;
            Target = null;
            Debug.Log("탈출함"); // 디버그 메시지 출력
        }
    }
 //   skeletonAnimation.AnimationState.SetAnimation(0, "Attack", true);
    //타겟이 시야에 있는지 충돌 체크
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
                
                OM.MoveSpeed(0.0f);
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
            OM.MoveSpeed(moveSpeed);
        }
    }


    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(detectionCheckDelay);
        DetectTarget();
        StartCoroutine(DetectionCoroutine());
    }

    //사격범위 그리기
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, viewRadius / 2);
    }
}