using UnityEngine;

public class CollideSensor : MonoBehaviour {

    private int colliderCount = 0;

    private float disableTimer;

    private void OnEnable()
    {
        colliderCount = 0;
    }

    public bool State()
    {
        if (disableTimer > 0)
        {
            return false;
        }
            
        return colliderCount > 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        colliderCount++;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        colliderCount--;
    }

    void Update()
    {
        disableTimer -= Time.deltaTime;
    }

    public void Disable(float duration)
    {
        disableTimer = duration;
    }
}
