using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProximityIndicator : MonoBehaviour
{
    public Transform targetObject; // ��� ����� � ����� �����
    public float maxDistance = 10f;

    private Image proximityBar;  //��� ����������
    private float initialBarWidth; //������ ���������� ���� �� ��������� ��� ���� �����������

    void Start()
    {
        proximityBar = CreateProximityBar();
        initialBarWidth = proximityBar.rectTransform.sizeDelta.x;
    }

    void Update()
    {
        if (targetObject != null)
        {
            float distance = Vector3.Distance(transform.position, targetObject.position);
            UpdateProximityBar(distance);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // ��� ������� ��� ������ ������
        {
            targetObject = other.transform;
            proximityBar.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // ������ ���� ��� ������ �������
        {
            targetObject = null;
            proximityBar.enabled = false;
        }
    }

    Image CreateProximityBar()
    {
        GameObject canvasObject = new GameObject("ProximityBarCanvas");
        canvasObject.transform.SetParent(transform);
        RectTransform canvasTransform = canvasObject.AddComponent<RectTransform>();
        canvasTransform.localPosition = Vector3.zero;

        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;

        Image barImage = new GameObject("ProximityBar").AddComponent<Image>();
        barImage.transform.SetParent(canvasObject.transform);
        RectTransform barTransform = barImage.rectTransform;
        barTransform.sizeDelta = new Vector2(initialBarWidth, 5f);

        canvasObject.layer = LayerMask.NameToLayer("UI"); // ���������� ���� UI

        return barImage;
    }

    void UpdateProximityBar(float distance)
    {
        float normalizedDistance = Mathf.Clamp01(distance / maxDistance);
        float newBarWidth = initialBarWidth * normalizedDistance;

        proximityBar.rectTransform.sizeDelta = new Vector2(newBarWidth, proximityBar.rectTransform.sizeDelta.y);
    }
}
