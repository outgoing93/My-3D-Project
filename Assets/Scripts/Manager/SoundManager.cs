using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioClip PlusSound;
    [SerializeField] private AudioClip MinusSound;
    [SerializeField] private AudioClip BombSound;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(ItemType type)
    {
        switch (type)
        {
            case ItemType.Plus:
                audioSource.PlayOneShot(PlusSound);
                break;
            case ItemType.Minus:
                audioSource.PlayOneShot(MinusSound);
                break;
            case ItemType.Bomb:
                audioSource.PlayOneShot(BombSound);
                break;
        }
    }
}
