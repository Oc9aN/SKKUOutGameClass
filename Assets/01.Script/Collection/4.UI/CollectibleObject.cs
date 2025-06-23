using UnityEngine;

public class CollectibleObject : MonoBehaviour
{
    [SerializeField] private string collectibleId;

    private void Start()
    {
        // 이미 수집된 오브젝트는 비활성화
        if (CollectibleManager.Instance.IsCollected(collectibleId))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (CollectibleManager.Instance.TryCollect(collectibleId))
        {
            // 수집 성공 시 피드백 & 비활성화
            OnCollected();
        }
    }

    private void OnCollected()
    {
        // 예: 사운드, 이펙트 추가 가능
        Debug.Log($"Collected: {collectibleId}");
        gameObject.SetActive(false);
    }
}