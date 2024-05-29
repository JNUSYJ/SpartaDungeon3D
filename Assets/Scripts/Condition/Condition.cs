using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float startValue;    // 시작값
    public float curValue;      // 현재값
    public float maxValue;      // 최댓값
    public float passiveValue;  // 서서히 줄어드는 값

    public Image barImage;      // 바 이미지 컴포넌트

    private void Start()
    {
        curValue = startValue;
    }

    private void Update()
    {
        barImage.fillAmount = curValue / maxValue;
    }

    // value만큼 현재값 증가
    public void Add(float value)
    {
        curValue = Mathf.Min(curValue + value, maxValue);
    }

    // value만큼 현재값 감소
    public void Subtract(float value)
    {
        curValue = Mathf.Max(curValue - value, 0f);
    }
}
