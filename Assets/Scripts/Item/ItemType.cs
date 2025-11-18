using UnityEngine;

public enum ItemType { Plus, Minus, Bomb }

public class Item : MonoBehaviour
{
    public ItemType itemType;

    // 아이템 수집 시 호출
    public void OnCollected()
    {
        switch (itemType)
        {
            case ItemType.Plus:
                GameManager.Instance?.IncreaseHP(5);
                break;
            case ItemType.Minus:
                GameManager.Instance?.DecreaseHP(25);
                break;
            case ItemType.Bomb:
                GameManager.Instance?.GameOver();
                break;
        }

        // 아이템 비활성화 (오브젝트 풀 연결 가능)
        gameObject.SetActive(false);
    }
}
