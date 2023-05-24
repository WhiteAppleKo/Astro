using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketHP : MonoBehaviour
{
    
    public int maxHealth = 100;
    public int health ;
    private HealthBarScript healthBarScript;

    // 최대 체력


    private void Start()
    {
        // 초기화 작업 등을 수행할 수 있습니다.
        health = maxHealth;
        healthBarScript = GameObject.Find("Canvas").GetComponent<HealthBarScript>();
    }

    void Update()
    {
        // 체력과 관련된 업데이트 작업 등을 수행할 수 있습니다.
    }


    private void destroy()
    {  if (health <= 0)
        {Destroy(this.gameObject);
        SceneManager.LoadScene("Gameover");
        } // 게임오버 씬으로 전환할 수 있습니다.
    }

    public void TakeDamage(int damage)
    {
        // 현재 체력을 damage만큼 감소시킵니다.
        if (health > 0)
        {
            health -= damage;
            Debug.Log("RocketHP: " + health);
        }

        if (health == 0)
        {
            Invoke("destroy", 0.5f); // 0.5초 후에 destroy 메서드를 실행하여 로켓을 파괴합니다.
        }
    }
}
