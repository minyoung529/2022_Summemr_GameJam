using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class ProgramIcon : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    Image coolImage;

    [SerializeField]
    float curCoolTime;
    float coolTime;
    private bool canExecute;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!canExecute)
        {
            StartCoroutine(OnFirstClick());
        }
        else
        {
            ExecuteProgram();
        }
    }

    private IEnumerator OnFirstClick()
    {
        if (canExecute) yield break;

        canExecute = true;
        yield return new WaitForSeconds(0.5f);
        canExecute = false;
    }
    public void OnCoolTime()
    {
        StartCoroutine(OnCool());
    }
    IEnumerator OnCool()
    {
        coolImage.gameObject.SetActive(true);
        coolTime = curCoolTime;
        while (coolTime > 0.0f)
        {
            coolTime -= Time.deltaTime;
            coolImage.fillAmount = 1 - (1.0f / coolTime);
            yield return new WaitForFixedUpdate();
        }
        coolImage.gameObject.SetActive(false);
    }

    protected abstract void ExecuteProgram();
}
