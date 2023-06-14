using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresenterSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// �� Scene���� Presenter���� ��ӹ޾� ����Ѵ�.
    /// Singleton Ÿ������, ���� �̸� �����
    /// Presenter�� �����ϴ� Class���� Awake(), OnDisalbe()���� Presenter���� ȣ���ϴ� ���� ������Ѵ�.
    /// </summary>
    
    private static T instance;
    public static T GetInstance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }

        OnAwake();
    }

    protected virtual void OnAwake()
    {
    }

}
