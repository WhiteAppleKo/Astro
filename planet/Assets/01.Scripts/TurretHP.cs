using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TurretHP : MonoBehaviour
{
    [SerializeField]
    public int currentHP = 20;     // 최대 체력

    void Update()
    {

    }

    private void Awake()
    {
        
    }

    public void TakeDamage(int damage)
    {
        // 현재 체력을 damage만큼 감소
        currentHP -= damage;
        Debug.Log("HP : " + currentHP);

        // 체력이 0이 되면 게임오버
        if (currentHP <= 0)
        {
        }
    }
}