using UnityEngine;

public enum ItemType { Plus, Minus, Bomb }

public class Item : MonoBehaviour
{
    public ItemType itemType;

    public void OnCollected()
    {
        SoundManager.Instance.PlaySound(itemType);

        switch (itemType)
        {
            case ItemType.Plus:
                GameManager.Instance.IncreaseHP(5);
                break;

            case ItemType.Minus:
                GameManager.Instance.DecreaseHP(25);
                break;

            case ItemType.Bomb:
                GameManager.Instance.GameOver();
                break;
        }

        gameObject.SetActive(false);
    }
}
