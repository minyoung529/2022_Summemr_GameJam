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
        coolImage.fillAmount = Mathf.Lerp(0, 1, coolTime);
    }

    protected abstract void ExecuteProgram();
}
