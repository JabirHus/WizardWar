using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float lifetime = 1.5f;
    public float riseSpeed = 2.0f;
    public TextMeshProUGUI text;

    void Start()
    {
        Destroy(gameObject, lifetime); // Destroy after lifetime
    }

    public void SetText(string message)
    {
        if (text != null)
        {
            text.text = message;
        }
    }
}
