using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScenePresenter : MonoBehaviour
{
    #region Static Presenter
    private static GameScenePresenter instance;
    public static GameScenePresenter GetInstance
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
            instance = this;
        }
    }
    #endregion

    public void ScoreUpdate(float scoreMultiple)
    {
        Debug.Log("���� ������Ʈ");
    }

    public void PlayerHPUpdate()
    {
        Debug.Log("Player HP ������Ʈ");
    }
}
