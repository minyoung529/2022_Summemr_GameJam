using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Chrome : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private TrailSpawner trailSpawner;
    [SerializeField] private ChromeCollisionImpact collisionImpact;

    #region 이동 및 회전
    private RectTransform _rect;
    private Sequence seq;

    private bool isMoving = false;

    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private float moveSpeed = 10f;

    private Vector3 moveDir = Vector3.zero;

    private Vector3 startPos;
    private Vector3 beforePos;
    private Vector3 currentPos;
    #endregion

    #region 애니메이터 관련
    private Animator animator;
    readonly int changeTrigger = Animator.StringToHash("ChangeTrigger");
    readonly int rechangeTrigger = Animator.StringToHash("RechangeTrigger");
    #endregion

    private void Awake()
    {
        _rect = transform.GetComponent<RectTransform>();
        startPos = _rect.anchoredPosition;
        animator = GetComponent<Animator>();
        platformMask = LayerMask.NameToLayer("Platform");
    }

    private void Update()
    {
        beforePos = currentPos;
        currentPos = transform.position;
    }

    #region 스킬 사용 함수
    public void EnableChrome()
    {
        if (isMoving) return;
        isMoving = true;
        animator.SetTrigger(changeTrigger);
    }

    public void EndChange()
    {
        Vector2 point = Random.insideUnitCircle.normalized;
        Vector3 dir = new Vector3(point.x, 0, point.y);
        moveDir = dir;
        trailSpawner.EnableSpawn();
        StartCoroutine(DurationCoroutine());
    }

    private IEnumerator DurationCoroutine()
    {
        float time = 0f;
        while(time <= duration)
        {
            transform.position += moveDir * moveSpeed * Time.deltaTime;
            Vector3 dir = transform.rotation.eulerAngles + new Vector3(0, rotateSpeed * Time.deltaTime, 0);
            transform.rotation = Quaternion.Euler(dir);
            time += Time.deltaTime;
            yield return null;
        }
        SoundManager.Instance.SfxSoundOn(13);
        DisableChrome();
    }

    public void DisableChrome()
    {
        seq = DOTween.Sequence();
        seq.Append(_rect.DOAnchorPos(startPos, 1f));
        seq.Join(transform.DORotate(transform.rotation.eulerAngles + new Vector3(0, 360f, 0), 1f, RotateMode.FastBeyond360));
        seq.Append(transform.DORotate(new Vector3(90f, 0, 0), 0.1f));
        seq.AppendCallback(() => 
        {
            trailSpawner.DisableSpawn();
            animator.SetTrigger(rechangeTrigger);
        });
    }

    public void EndRechange()
    {
        isMoving = false;
    }
    #endregion

    #region 충돌 관련
    private LayerMask platformMask;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Platform"))
        {
            RaycastHit hit;
            if (Physics.Raycast(beforePos, moveDir, out hit, 10f, 1 << platformMask))
            {
                Vector3 dir = Vector3.Dot(-moveDir, hit.normal) * hit.normal * 2 + moveDir; //반사각 구하기
                dir.y = 0;
                moveDir = dir;
                ChromeCollisionImpact impact = PoolManager.Instance.Pop(collisionImpact) as ChromeCollisionImpact;
                impact.transform.position = hit.point;
                transform.position += new Vector3(0, 0, 1f);
                impact.SpawnImpact();
            }
        }
    }
    #endregion

    private void OnDisable()
    {
        seq.Kill();
    }
}
