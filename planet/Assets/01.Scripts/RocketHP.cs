using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Spine.Unity;
using UnityEngine.SceneManagement;

public class RocketHP : MonoBehaviour
{
    [SerializeField]
    public int currentHP = 3;     // 최대 체력

    private void Start()
    {
        
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
        SceneManager.LoadScene("Gameover");
    }

    public void TakeDamage(int damage)
    {
        // 현재 체력을 damage만큼 감소
        currentHP -= damage;
        Debug.Log("RocketHP : " + currentHP);

        if (currentHP == 0)
        {
            Invoke("destroy", 0.5f); // 2초 후에 실행할 액션 지정
        }
    }
}