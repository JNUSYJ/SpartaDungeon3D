using System;
using UnityEngine;

public class PlayerConditionController : MonoBehaviour
{
    public ConditionUI conditionUI;

    private Condition hp { get { return conditionUI.hp; } }
    private Condition stamina { get { return conditionUI.stamina; } }
    private Condition hunger { get { return conditionUI.hunger; } }

    public event Action OnTakeDamage;   // 데미지 입을 시 실행할 이벤트

    private void Update()
    {
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        HungerCheck();
        DieCheck();
    }

    // 배고픔이 0일 시 HP 감소
    private void HungerCheck()
    {
        float damageCausedByHunger = 1f;    // 배고픔 감소량

        if (hunger.curValue <= 0f)
        {
            hp.Subtract(damageCausedByHunger * Time.deltaTime);
        }
    }

    // HP가 0일 시 사망
    private void DieCheck()
    {
        if (hp.curValue <= 0f)
        {
            Die();
        }
    }

    // HP 회복
    public void Heal(float amount)
    {
        hp.Add(amount);
    }

    // 데미지
    public void Damage(float amount)
    {
        hp.Subtract(amount);
        OnTakeDamage?.Invoke();
    }

    // 사망
    public void Die()
    {
        Debug.Log("Died");
    }

    // 배고픔 회복
    public void Eat(float amount)
    {
        hunger.Add(amount);
    }

    // 스태미나 사용
    public void UseStamina(float amount)
    {
        stamina.Subtract(amount);
    }
}
