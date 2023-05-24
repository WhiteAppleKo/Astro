using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public int health;
    public int maxHealth = 100;

    private HealthBarScript healthBarScript;

    void Start()
    {
        health = maxHealth;
        healthBarScript = GameObject.Find("HealthBar").GetComponent<HealthBarScript>();
    }

    void Update()
    {
        if (health <= 0)
        {
            Debug.Log("Player ����");
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBarScript.DamageRocket();
    }
}
