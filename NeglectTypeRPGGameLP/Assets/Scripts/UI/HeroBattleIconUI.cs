using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class HeroBattleIconUI : MonoBehaviour, IPointerClickHandler
{
    float clickTime = -1;
    bool isDoubleClick = false;

    public Sprite sprite;

    public Image _image;

    public bool isUse;
    private void Awake()
    {
        _image = GetComponent<Image>();
        Init();
    }

    public void Init()
    {
        isUse = false;
        clickTime = -1;
    }
    void OnMouseDoubleClick()
    {
        Debug.Log("더블클릭");
        //isDoubleClick = false;
    }

    void OnMouseOneClick()
    {
        Debug.Log("클릭");
        ImageChange();
    }

    void ImageChange()
    {
        isUse = !isUse;
        if (isUse)
            _image.color = new Color(10, 10, 10, 0.1f);
        else
            _image.color = new Color(10, 10, 10, 1);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isDoubleClick)
        { 
            if ((Time.time - clickTime) < 0.3f)
            {
                isDoubleClick = true;
                OnMouseDoubleClick();
                //clickTime = -1;
            }
            else
            {
                if(!isDoubleClick)
                    StartCoroutine(ClickWaitCoroutine());
                clickTime = Time.time;
            }
        }
    }

    IEnumerator ClickWaitCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        if(isDoubleClick)
            yield break;
        OnMouseOneClick();
    }


    //아래는 IBeginDragHandler, IDragHandler , IEndDragHandler를 상속받아서 사용 드래그용
    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //}

    //public void OnDrag(PointerEventData eventData)
    //{
    //}

    //public void OnEndDrag(PointerEventData eventData)
    //{
    //}

}
