using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Button button;
    private float scaleFactor = 0.8f; // Tỷ lệ khi nút bị giữ
    private Vector3 originalScale; // Kích thước gốc của nút

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    void Start()
    {
        originalScale = button.transform.localScale; // Lưu kích thước gốc của nút
    }

    // Phương thức được gọi khi nhấn nút
    public void OnPointerDown(PointerEventData eventData)
    {
        button.transform.localScale = originalScale * scaleFactor;
    }

    // Phương thức được gọi khi thả nút
    public void OnPointerUp(PointerEventData eventData)
    {
        button.transform.localScale = originalScale;
    }
}