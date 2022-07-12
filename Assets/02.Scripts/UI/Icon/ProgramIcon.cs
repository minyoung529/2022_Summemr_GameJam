using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class ProgramIcon : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
<<<<<<< HEAD
    Image coolImage;

    [SerializeField]
    float coolTime;

=======
    float curCoolTime;
>>>>>>> september
    private bool canExecute;
    float coolTime;

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
    public void OnCoolTime(GameObject im)
    {
        StartCoroutine(OnCool(im.GetComponent<Image>()));
    }
    IEnumerator OnCool(Image im)
    {
        coolTime = curCoolTime;
        while (coolTime > 0.0f)
        {
            coolTime -= Time.deltaTime;
            im.fillAmount = 1 - (1.0f / coolTime);
            yield return new WaitForFixedUpdate();
        }
        im.gameObject.SetActive(false);
    }

    protected abstract void ExecuteProgram();
}
