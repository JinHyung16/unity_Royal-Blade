using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasRatioSetting : MonoBehaviour
{
    [SerializeField] private CanvasScaler canvasScaler;

    private float canvasAspectRatio = 9.0f / 16.0f; //Canvas���� �����ص� ���� -> 900:1600 �� ǥ��
    private float screenAspectRatio = 0.0f; //���� ������ ����Ǵ� â�� ������ �����´�.

    private void Start()
    {
        if (canvasScaler == null)
        {
            canvasScaler = GetComponent<CanvasScaler>();
        }
        
        screenAspectRatio = (float)Screen.width / Screen.height;

        if (canvasAspectRatio < screenAspectRatio)
        {
            //���� �÷������� ȭ���� width�� �� �������
            this.canvasScaler.matchWidthOrHeight = 1.0f; 
        }
        else
        {
            //���� �÷������� ȭ���� height �� �������
            this.canvasScaler.matchWidthOrHeight = 0.0f;
        }

    }
}
