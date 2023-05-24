using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGold : MonoBehaviour
{
    [SerializeField]
    public int currentGold = 3;
    private HealthBarScript healthBarScript;

    private void Start()
    {
        healthBarScript = GameObject.Find("Canvas").GetComponent<HealthBarScript>();
    }

    public void TakeGold(int Gold)
    {
        // ���� ü���� damage��ŭ ����
        currentGold += Gold;
        Debug.Log("Player Gold : " + currentGold);
    }

    public void UseGold(int Gold)
    {
        // ���� ü���� damage��ŭ ����
        currentGold -= Gold;
        Debug.Log("Player Gold : " + currentGold);
    }
}
