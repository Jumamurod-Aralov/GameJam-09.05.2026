using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Shape : MonoBehaviour, IPointerClickHandler
{
    public float speed = 200f;

    public TextMeshProUGUI letterText;
    public Image outline;

    RectTransform rect;

    [HideInInspector] public string currentLetter;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        outline.enabled = false;
    }

    void Update()
    {
        rect.anchoredPosition += Vector2.up * speed * Time.deltaTime;
    }

    public void Setup(string letter)
    {
        currentLetter = letter;
        letterText.text = letter;
        outline.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.CheckClick(this);
    }
}