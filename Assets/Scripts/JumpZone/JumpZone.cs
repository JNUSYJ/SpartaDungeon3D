using UnityEngine;

public class JumpZone : MonoBehaviour, IInteractable
{
    private Rigidbody _rigidbody;
    private Collider other;

    public string GetInteractInfo()
    {
        return "JumpZone\nYou can jump higher here";
    }

    public void OnInteract()
    {
        // 플레이어 충돌 시 강하게 점프
        if (other.gameObject.CompareTag("Player"))
        {
            _rigidbody = other.GetComponent<Rigidbody>();
            _rigidbody.AddForce(Vector3.up * 20f, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        this.other = other;
        OnInteract();
    }
}
