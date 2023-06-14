using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

public class RareCat : BaseEnemy
{
    [SerializeField] private RareCatData rareCatData;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private static readonly int onHit = Animator.StringToHash("onHit");

    private Rigidbody2D rigid2D;
    private Animator animator;

    private bool isActive = false;

    //UnityEngine.Pool ���� ������
    private IObjectPool<RareCat> managedPool;

    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        rareCatData.OnAfterDeserialize();
        rigid2D.bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnEnable()
    {
        rareCatData.OnAfterDeserialize();
        isActive = true;
        rareCatData.OnAfterDeserialize();
        //FallToGround().Forget();
    }

    private void OnDisable()
    {
        isActive = false;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shield") || collision.CompareTag("Sword"))
        {
            OnBounce();
        }

        if (collision.CompareTag("DieZone"))
        {
            this.DestroyManagedPool();
        }
    }

    #region UnityEngine.Pool Functions
    public void SetManagedPool(IObjectPool<RareCat> poolObj)
    {
        managedPool = poolObj;
    }

    public void DestroyManagedPool()
    {
        managedPool.Release(this);
    }
    #endregion
    public override void OnDamge(int damage)
    {
        HitDamagePoolManager.GetInstance.GetHitDamageText(this.transform, damage.ToString());
        OnBounce();
        rareCatData.enemyHpRuntime -= damage;
        if (rareCatData.enemyHpRuntime <= 0)
        {
            OnDied();
        }
    }

    private async UniTaskVoid FallToGround()
    {
       while (isActive)
        {
            rigid2D.velocity = Vector2.down * Time.deltaTime * fallDownPower;
            await UniTask.Yield(cancellationToken: this.GetCancellationTokenOnDestroy());
        }
    }
    private void OnDied()
    {
        GameScenePresenter.GetInstance.UpdateScore();
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
