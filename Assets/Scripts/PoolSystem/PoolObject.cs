using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    /// <summary>
    /// PoolDictionary���� Pooling�� ������Ʈ���� ��� PoolObject�� ����Ѵ�.
    /// ���� Pooling�� ������Ʈ�� PoolObject�� ������Ʈ�� ���ٸ� ������ ������Ʈ�� �����Ѵ�.
    /// </summary>

    private string objName;
    public string Name
    {
        get
        {
            return this.objName;
        }
        set
        {
            this.objName = value;
        }
    }
    public void RemovePrefab()
    {
        this.gameObject.SetActive(false);
    }
}
