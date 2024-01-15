using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUIElement : MonoBehaviour
{
    protected CanvasGroup canvasGroup;
    protected UIType uiType = UIType.UNKNOWN;
    protected bool isHide;
    private bool isInited;//ktra đã dc khởi tạo hay chưa

    public bool IsHide { get => isHide; }
    public CanvasGroup CanvasGroup { get => canvasGroup; }
    public bool IsInited { get => isInited; }
    public UIType UIType { get => uiType; }
    private void SetActiveGroupCanvas(bool isActive)
    {
        if (CanvasGroup != null)
        {
            CanvasGroup.blocksRaycasts = isActive;
            CanvasGroup.alpha = isActive ? 1 : 0;//,tương tự if else, nếu isActive=true thì alpha=1, nếu=false thì alpha=0 (1 là hiện, 0 là ẩn)
        }
    }

    public virtual void Init()//hàm khởi tạo
    {
        this.isInited = true;
        if (!this.gameObject.GetComponent<CanvasGroup>())//ktra coi có canvasgroup chưa, nếu chưa thì add vào
        {
            this.gameObject.AddComponent<CanvasGroup>();
        }
        this.canvasGroup = this.gameObject.GetComponent<CanvasGroup>();//add xong rồi gán vào biến canvasgroup
        this.gameObject.SetActive(true);
        Hide();//khi khởi tạo sẽ cho ẩn đi
    }
    public virtual void Show(object data)
    {
        this.gameObject.SetActive(true);
        this.isHide = false;
        SetActiveGroupCanvas(true);
    }
    public virtual void Hide()
    {
        this.isHide = true;
        SetActiveGroupCanvas(false);
    }
    public virtual void OnClickedBackButton()
    {

    }
}
