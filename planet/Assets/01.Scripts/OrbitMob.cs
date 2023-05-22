using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitMob : MonoBehaviour
{
    public GameObject target;
    public float moveSpeed;
    public Vector3 direction = Vector3.up;
    private GameObject enemyController;
    [SerializeField]
    public int Damage = 1;
    [SerializeField]
    public int Gold = 1;

    void start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(target.transform.position, direction, moveSpeed * Time.deltaTime);
        enemyController = GameObject.Find("EnemyCtrl");

    }

    public void MoveSpped(float speed)
    {
        moveSpeed = speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet")) {
            Debug.Log("Colliderd " + collision.name);
            enemyController.GetComponent<EnemyController>().AttackEnemy(Damage);
            enemyController.GetComponent<EnemyController>().KillEnemy(Gold);   
            Destroy(this.gameObject);
        }
    }
}