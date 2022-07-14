using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CloseButton : MonoBehaviour
{
    private Button button;
    public Transform panel;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => panel.transform.DOScale(0f, 0.3f).OnComplete(() =>
        {
            UIManager.Instance.EnactiveWindow(panel.gameObject);
            panel.gameObject.SetActive(false);
            panel.transform.DOKill();
        }));
    }
}
