using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private PlayerBehaviourController playerController;

    [Header("Attack Btn�� ���õ� �����͵�")]
    [SerializeField] private RectTransform attackBtnRectTrans;
    [SerializeField] private Image attackFilledImg;
    [SerializeField] private Image attackFullImg;

    [Header("Defense Btn�� ���õ� �����͵�")]
    [SerializeField] private RectTransform defenseBtnRectTrans;
    [SerializeField] private Image defenseFilledImg;
    [SerializeField] private Image defenseFullImg;

    [Header("Jump Btn�� ���õ� �����͵�")]
    [SerializeField] private RectTransform jumpBtnRectTrans;
    [SerializeField] private Image jumpFilledImg;
    [SerializeField] private Image jumpFullImg;

    [Header("Btn�� ������ ����")]
    [SerializeField] private float joyBtnMoveRange = 220.0f; //y�� �ִ� 310 - ���� y�� 90 = 220


    //Joy Stick�� Drag�� ���õ� �����͵�
    private RectTransform joyBtnMoveRectTrans;

    //���ݰ� ������ ������ ������ -> 1.0f / ���⺰ ������ -> ����� �ӽ� ������ ����
    private float attackGauage = 0.0f;
    private float defenseGauage = 0.0f;
    private float jumpGauage = 0.0f;

    //���ݰ� ������ �������� �� ä���� ��� true�� ��
    private bool isAttackSpecial = false;
    private bool isDefenseSpecial = false;
    private bool isJumpSpeical  = false;

    private void Awake()
    {
        joyBtnMoveRectTrans = GetComponent<RectTransform>();
    }
    private void Start()
    {
        if (playerController == null)
        {
            playerController = GameObject.Find("Character").GetComponent<PlayerBehaviourController>();
        }

        attackFilledImg.fillAmount = 0.0f;
        defenseFilledImg.fillAmount = 0.0f;
        jumpFilledImg.fillAmount = 0.0f;

        isAttackSpecial = false;
        isDefenseSpecial = false;
        isJumpSpeical = false;

        attackFullImg.enabled = false;
        defenseFullImg.enabled = false;
        jumpFullImg.enabled = false;

        attackGauage = 0.1f;
        defenseGauage = 0.05f;
        jumpGauage = 0.3f;

    }

    #region Button - EventTrigger System Functions
    public void AttackButtonDown(BaseEventData data)
    {
        attackFilledImg.fillAmount += attackGauage;
        if (1.0f <= attackFilledImg.fillAmount)
        {
            isAttackSpecial = true;
            attackFullImg.enabled = true;
        }
        playerController.Attack();
    }

    public void AttackButtonDrag(BaseEventData data)
    {
        PointerEventData eventData = (PointerEventData)data;
        if (isAttackSpecial)
        {
            Vector2 inputPos = eventData.position - joyBtnMoveRectTrans.anchoredPosition;
            inputPos.x = 0;
            Vector2 inputDir = inputPos.y < joyBtnMoveRange ? inputPos : inputPos.normalized * joyBtnMoveRange;
            attackBtnRectTrans.anchoredPosition = inputDir;
            if (inputDir.y == attackBtnRectTrans.anchoredPosition.y)
            {
                AttackGauageReset();
                playerController.AttackSpecial();
            }
        }
    }

    public void AttackButtonDragEnd(BaseEventData data)
    {
        attackBtnRectTrans.anchoredPosition = Vector2.zero;
    }
    private void AttackGauageReset()
    {
        attackFilledImg.fillAmount = 0.0f;
        attackFullImg.enabled = false;
        isAttackSpecial = false;
    }

    public void DefenseButtonDown(BaseEventData data)
    {
        defenseFilledImg.fillAmount += defenseGauage;
        if (1.0f <= defenseFilledImg.fillAmount)
        {
            isDefenseSpecial = true;
            defenseFullImg.enabled = true;
        }

        playerController.Defense();
    }

    private void DefenseGauageReset()
    {
        defenseFilledImg.fillAmount = 0.0f;
        defenseFullImg.enabled = false;
        isDefenseSpecial = false;
    }

    public void DefenseButtonDrag(BaseEventData data)
    {
        PointerEventData eventData = (PointerEventData)data;
        if (isDefenseSpecial)
        {
            Vector2 inputPos = eventData.position - defenseBtnRectTrans.anchoredPosition;
            inputPos.x = 0;
            Vector2 inputDir = inputPos.y < joyBtnMoveRange ? inputPos : inputPos.normalized * joyBtnMoveRange;
            defenseBtnRectTrans.anchoredPosition = inputDir;
            if (inputDir.y == defenseBtnRectTrans.anchoredPosition.y)
            {
                DefenseGauageReset();
                playerController.DefenseSpecial();
            }
        }
    }

    public void DefenseButtonDragEnd(BaseEventData data)
    {
        defenseBtnRectTrans.anchoredPosition = Vector2.zero;
    }


    public void JumpButtonDown(BaseEventData data)
    {
        if (playerController.IsGround)
        {
            jumpFilledImg.fillAmount += jumpGauage;
            if (1.0f <= jumpFilledImg.fillAmount)
            {
                isJumpSpeical = true;
                jumpFullImg.enabled = true;
            }
            playerController.Jump();
        }
    }

    public void JumpButtonDrag(BaseEventData data)
    {
        PointerEventData eventData = (PointerEventData)data;
        if (isJumpSpeical)
        {
            Vector2 inputPos = eventData.position - joyBtnMoveRectTrans.anchoredPosition;
            inputPos.x = 0;
            Vector2 inputDir = inputPos.y < joyBtnMoveRange ? inputPos : inputPos.normalized * joyBtnMoveRange;
            jumpBtnRectTrans.anchoredPosition = inputDir;
            if (inputDir.y == jumpBtnRectTrans.anchoredPosition.y)
            {
                JumpGauageReset();
                playerController.JumpSpecial();
            }
        }
    }

    public void JumpButtonDragEnd(BaseEventData data)
    {
        jumpBtnRectTrans.anchoredPosition = Vector2.zero;
    }

    private void JumpGauageReset()
    {
        jumpFilledImg.fillAmount = 0.0f;
        jumpFullImg.enabled = false;
        isJumpSpeical = false;
    }
    #endregion
}
