using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public delegate void characterCollocate(int index,bool value);
public class HeroBattleIconUI : MonoBehaviour, IPointerClickHandler
{
    float clickTime = -1;
    bool isDoubleClick = false;

    public Sprite sprite;

    public Image _image;

    public bool isUse;

    public ObjectPoolList parant;

    public int heroIndex;

    public int faction;

    public characterCollocate collocate;

    private void Awake()
    {
        _image = GetComponent<Image>();
        Init();
    }

    private void OnDisable()
    {
        //parant.Enqueue(this.gameObject);
        Init();
    }

    public void Init()
    {
        isUse = false;
        clickTime = -1;
        _image.color = new Color(10, 10, 10, 1);
    }
    void OnMouseDoubleClick()
    {
        Debug.Log("더블클릭");
    }

    void OnMouseOneClick()
    {
        ImageChange();
        collocate(heroIndex, isUse);

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
            if ((Time.time - clickTime) < 0.19f)
            {
                isDoubleClick = true;
                OnMouseDoubleClick();
                clickTime = -1;
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
        yield return new WaitForSeconds(0.2f);
        if (isDoubleClick)
        {
            isDoubleClick = false;
            yield break;
        }
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
