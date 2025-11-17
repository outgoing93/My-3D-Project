using UnityEngine;

public enum ItemType { Plus, Minus, Bomb }

public class Item : MonoBehaviour
{
    public ItemType itemType;
    // 오브젝트 풀과 연결되는 부분

    // 수집 시 호출될 함수
    public void OnCollected()
    {
        switch (itemType)
        {
            case ItemType.Plus:
                GameManager.Instance.IncreaseHP(10);
                break;
            case ItemType.Minus:
                GameManager.Instance.DecreaseHP(10);
                break;
            case ItemType.Bomb:
                GameManager.Instance.GameOver();
                break;
        }

        GameManager.Instance.UpdateUI();

        // 아이템 비활성화 → 오브젝트 풀로 반환
        gameObject.SetActive(false);
    }
}
