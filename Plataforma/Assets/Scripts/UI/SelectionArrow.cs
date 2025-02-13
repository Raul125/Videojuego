using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    [SerializeField] private AudioClip changeSound;
    [SerializeField] private AudioClip interactSound;
    private RectTransform rectTransform;
    private int currentPos;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangePosition(-1);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangePosition(1);
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void Interact()
    {
        SoundManager.Instance.PlaySound(interactSound);
        options[currentPos].GetComponent<Button>().onClick.Invoke();
    }

    private void ChangePosition(int change)
    {
        currentPos += change;

        if (change != 0)
        {
            SoundManager.Instance.PlaySound(changeSound);
        }

        if (currentPos < 0)
        {
            currentPos = options.Length - 1;
        }
        else if (currentPos >= options.Length)
        {
            currentPos = 0;
        }

        rectTransform.position = new Vector3(rectTransform.position.x, options[currentPos].position.y, 0);
    }
}
