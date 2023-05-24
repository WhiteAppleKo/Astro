using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarScript : MonoBehaviour
{
    public Slider healthBarSlider;
    public TextMeshProUGUI healthBarValueText;
    private RocketHP rocketHP;

    void Start()
    {
        // RocketHP 스크립트를 가진 게임 오브젝트를 찾아서 rocketHP 변수에 할당합니다.
        rocketHP = GameObject.Find("Rocket").GetComponent<RocketHP>();
    }

    void Update()
    {
        // 매 프레임마다 체력바를 업데이트합니다.
        UpdateHealthBar();
    }

    public void DamageRocket()
    {
        // 로켓이 데미지를 입었을 때 호출되는 메서드입니다.
        rocketHP.TakeDamage(1); // 로켓의 체력을 1만큼 감소시킵니다.
        UpdateHealthBar(); // 체력바를 업데이트합니다.
    }

    private void UpdateHealthBar()
{
    // 로켓의 현재 체력과 최대 체력을 가져와서 체력바를 업데이트합니다.
    int currHealth = rocketHP.health;
    int maxHealth = rocketHP.maxHealth;

    healthBarValueText.text = currHealth.ToString() + "/" + maxHealth.ToString();
    healthBarSlider.maxValue = maxHealth;
    healthBarSlider.value = currHealth;
}

}
