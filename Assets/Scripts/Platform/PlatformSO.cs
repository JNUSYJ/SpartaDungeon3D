using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO", menuName = "New Platform")]
public class PlatformSO : ScriptableObject
{
    [Header("Movement")]
    public List<Vector3> destination;   // 목표지점 좌표
    public List<float> moveSpeed;       // 이동속도
}
