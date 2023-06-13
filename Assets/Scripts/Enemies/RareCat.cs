using UnityEngine;
using UnityEngine.Pool;

public class RareCat : BaseEnemy
{
    [SerializeField] private RareCatData rareCatData;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private static readonly int onHit = Animator.StringToHash("onHit");

    private Rigidbody2D rigid2D;
    private Animator animator;

    //UnityEngine.Pool ���� ������
    private IObjectPool<RareCat> managedPool;
    private bool isPoolRelease = false;

    private void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        rareCatData.OnAfterDeserialize();
        isPoolRelease = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Shield"))
        {
            Debug.Log(collision.tag);
            OnBounce();
        }
    }

    #region UnityEngine.Pool Functions
    public void SetManagedPool(IObjectPool<RareCat> poolObj)
    {
        managedPool = poolObj;
    }

    public void DestroyManagedPool()
    {
        if (!isPoolRelease)
        {
            managedPool.Release(this);
            isPoolRelease = true;
        }
    }
    #endregion
    public override void OnDamge(int damage)
    {
        Debug.Log("�˿� �°� �ֽ��ϴ�");
        rareCatData.enemyHpRuntime -= damage;
        OnBounce();
        GameScenePresenter.GetInstance.ScoreUpdate(1.8f);
        if (rareCatData.enemyHpRuntime <= 0)
        {
            OnDied();
        }
    }

    private void OnDied()
    {
        this.DestroyManagedPool();
    }

    /// <summary>
    /// Player�� ����� ���� ������ Enemy�ų�
    /// Ȥ�� �������� ��Ȳ���� ���ε鳢�� �浹�� ��� ȣ��
    /// </summary>
    private void OnBounce()
    {
        animator.SetTrigger(onHit);
        rigid2D.AddForce(Vector2.up * bouncePower, ForceMode2D.Impulse);
    }
}
