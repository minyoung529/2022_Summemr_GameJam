using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ProgramIcon : MonoBehaviour, IPointerClickHandler
{
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

    protected abstract void ExecuteProgram();
}
