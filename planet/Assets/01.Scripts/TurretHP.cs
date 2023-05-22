using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Spine.Unity;

public class TurretHP : MonoBehaviour
{
    [SerializeField]
    public int currentHP = 3;     // 최대 체력

    public SkeletonAnimation skeletonAnimation;
    public GameObject thisObject;


    private void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        
    }

    void Update()
    {

    }

    private void Awake()
    {
        
    }
    private void destroy()
    {
        Destroy(this.gameObject);
    }

    public void TakeDamage(int damage)
    {
        // 현재 체력을 damage만큼 감소
        currentHP -= damage;
        Debug.Log("HP : " + currentHP);

        if (currentHP == 0)
        {
            skeletonAnimation.AnimationState.SetAnimation(0, "Die", true);
            Invoke("destroy", 0.5f); // 2초 후에 실행할 액션 지정
        }
    }
}