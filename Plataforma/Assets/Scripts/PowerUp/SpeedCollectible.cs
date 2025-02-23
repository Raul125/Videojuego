using System.Collections;
using UnityEngine;

public class SpeedCollectible : MonoBehaviour
{
    [SerializeField] private float speedMultiplicator;
    [SerializeField] private float duration;
    [SerializeField] private AudioClip speedSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        if (collision.TryGetComponent<PlayerMovement>(out var playerMovement))
        {
            SoundManager.Instance.PlaySound(speedSound);
            StartCoroutine(SpeedTime(playerMovement));
            Destroy(gameObject);
        }
    }

    private IEnumerator SpeedTime(PlayerMovement playerMovement)
    {
        var defaultSpeed = playerMovement.Speed;
        playerMovement.Speed *= speedMultiplicator;
        yield return new WaitForSeconds(duration);
        playerMovement.Speed = defaultSpeed;

    }
}
