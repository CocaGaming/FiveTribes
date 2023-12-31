﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : BaseManager<UIManager>
{
    public GameObject cScreen, cPopup, cNotify, cOverlap, gContainer, gWrapper;//container, gameobject
    public Canvas MyCanvas;
    public EventSystem MyEventSystem;

    private Dictionary<string, BaseScreen> screens = new Dictionary<string, BaseScreen>();//lưu dưới dạng dictionary để khi tìm kiếm lấy ra nhanh nhất
    private Dictionary<string, BasePopup> popups = new Dictionary<string, BasePopup>();
    private Dictionary<string, BaseNotify> notifies = new Dictionary<string, BaseNotify>();
    private Dictionary<string, BaseOverlap> overlaps = new Dictionary<string, BaseOverlap>();

    public Dictionary<string, BaseScreen> Screens => screens;//chỉ get dc ko set dc
    public Dictionary<string, BasePopup> Popups => popups;
    public Dictionary<string, BaseNotify> Notifies => notifies;
    public Dictionary<string, BaseOverlap> Overlaps => overlaps;

    private BaseScreen curScreen;
    private BasePopup curPopup;
    private BaseNotify curNotify;
    private BaseOverlap curOverlap;

    public BaseScreen CurSceen => curScreen;
    public BasePopup CurPopup => curPopup;
    public BaseNotify CurNotify => curNotify;
    public BaseOverlap CurOverlap => curOverlap;

    private const string SCREEN_RESOURCES_PATH = "Prefabs/UI/Screen/";//đường dẫn
    private const string POPUP_RESOURCES_PATH = "Prefabs/UI/Popup/";
    private const string NOTIFY_RESOURCES_PATH = "Prefabs/UI/Notify/";
    private const string OVERLAP_RESOURCES_PATH = "Prefabs/UI/Overlap/";

    private const string NAME_SCREEN_CONTAINER = "CONTAINER_SCREEN";
    private const string NAME_POPUP_CONTAINER = "CONTAINER_POPUP";
    private const string NAME_NOTIFY_CONTAINER = "CONTAINER_NOTIFY";
    private const string NAME_OVERLAP_CONTAINER = "CONTAINER_OVERLAP";
    private const string NAME_UI_CONTAINER = "UI_CONTAINER";
    private const string NAME_UI_WRAPPER = "UI_WRAPPER";

    private List<string> rmScreens = new List<string>();//remove những cái ko cần
    private List<string> rmPopups = new List<string>();
    private List<string> rmNotifiess = new List<string>();
    private List<string> rmOverlaps = new List<string>();

    protected override void Awake()
    {
        base.Awake();
#if UNITY_EDITOR//code đổi tên thành chữ in hoa trên editor cho dễ nhìn
        this.cScreen.name = NAME_SCREEN_CONTAINER;
        this.cPopup.name = NAME_POPUP_CONTAINER;
        this.cNotify.name = NAME_NOTIFY_CONTAINER;
        this.cOverlap.name = NAME_OVERLAP_CONTAINER;
        this.gContainer.name = NAME_UI_CONTAINER;
        this.gWrapper.name = NAME_UI_WRAPPER;
#endif
    }
    public void HideAll()
    {
        HideAllScreens();
        HideAllPopups();
        HideAllNotifies();
        HideAllOverlaps();
    }

    #region Screen
    private BaseScreen GetNewScreen<T>() where T : BaseScreen
    {
        string nameScreen = typeof(T).Name;
        GameObject pfScreen = GetUIPrefab(UIType.SCREEN, nameScreen);
        if (pfScreen == null || !pfScreen.GetComponent<BaseScreen>())
        {
            throw new MissingReferenceException("Can not found " + nameScreen + " screen. !!!");
        }
        GameObject ob = Instantiate(pfScreen) as GameObject;
        ob.transform.SetParent(this.cScreen.transform);
        ob.transform.localScale = Vector3.one;
        ob.transform.localPosition = Vector3.zero;
#if UNITY_EDITOR
        ob.name = "SCREEN_" + nameScreen;
#endif
        BaseScreen screenScr = ob.GetComponent<BaseScreen>();
        screenScr.Init();
        return screenScr;
    }
    public void HideAllScreens()
    {
        BaseScreen screenScr = null;
        foreach (KeyValuePair<string, BaseScreen> item in screens)
        {
            screenScr = item.Value;
            if (screenScr == null || screenScr.IsHide)//nếu screen đó ko tồn tại hoặc đang ẩn thì next tới screen tiếp theo
                continue;
            screenScr.Hide();

            if (screens.Count <= 0)//trong vòng lặp ko có screen nào
                break;
        }
    }
    public T GetExistScreen<T>() where T : BaseScreen
    {
        string screenName = typeof(T).Name;
        if (screens.ContainsKey(screenName))
        {
            return screens[screenName] as T;
        }
        return null;
    }
    private void RemoveScreen(string v)
    {
        for (int i = 0; i < rmScreens.Count; i++)
        {
            if (rmScreens[i].Equals(v))
            {
                if (screens.ContainsKey(v))
                {
                    Destroy(screens[v].gameObject);
                    screens.Remove(v);

                    Resources.UnloadUnusedAssets();
                    System.GC.Collect();
                }
                break;
            }
        }
    }
    public void ShowScreen<T>(object data = null, bool forceShowData = false) where T : BaseScreen
    {
        string screenName = typeof(T).Name;//show tên của screen 
        BaseScreen result = null;

        if (curScreen != null)
        {
            var curName = curScreen.GetType().Name;
            if (curName.Equals(screenName))
            {
                result = curScreen;
            }
            else
            {
                RemoveScreen(curName);
            }
        }

        if (result == null)
        {
            if (!screens.ContainsKey(screenName))
            {
                BaseScreen screenScr = GetNewScreen<T>();
                if (screenScr != null)
                {
                    screens.Add(screenName, screenScr);
                }
            }

            if (screens.ContainsKey(screenName))
            {
                result = screens[screenName];
            }
        }
        bool isShow = false;
        if (result != null)
        {
            if (forceShowData)
            {
                isShow = true;
            }
            else
            {
                if (result.IsHide)
                {
                    isShow = true;
                }
            }
        }

        if (isShow)
        {
            curScreen = result;
            result.transform.SetAsLastSibling();//set ở vị trí cuối trong scene
            result.Show(data);
        }
    }
    #endregion

    #region Popup
    private BasePopup GetNewPopup<T>() where T : BasePopup
    {
        string namePopup = typeof(T).Name;
        GameObject pfPopup = GetUIPrefab(UIType.POPUP, namePopup);
        if (pfPopup == null || !pfPopup.GetComponent<BasePopup>())
        {
            throw new MissingReferenceException("Cant found " + namePopup + " popup. !!!");
        }

        GameObject ob = Instantiate(pfPopup) as GameObject;
        ob.transform.SetParent(this.cPopup.transform);
        ob.transform.localScale = Vector3.one;
        ob.transform.localPosition = Vector3.zero;
#if UNITY_EDITOR
        ob.name = "POPUP_" + namePopup;
#endif
        BasePopup popupScr = ob.GetComponent<BasePopup>();
        popupScr.Init();
        return popupScr;
    }

    public void HidePopup<T>(bool force = false) where T : BasePopup
    {
        string popupName = typeof(T).Name;
        if (popups.ContainsKey(popupName))
        {
            if (force || !popups[popupName].IsHide)
            {
                popups[popupName].Hide();
            }
        }
    }
    public void HideAllPopups()
    {
        BasePopup scr = null;
        foreach (KeyValuePair<string, BasePopup> item in popups)
        {
            scr = item.Value;
            if (scr == null || scr.IsHide)
                continue;
            scr.Hide();

            if (popups.Count <= 0)
                break;
        }
    }
    public T GetExistPopup<T>() where T : BasePopup
    {
        string namePopup = typeof(T).Name;
        if (popups.ContainsKey(namePopup))
        {
            return popups[namePopup] as T;
        }
        return null;
    }

    private void RemovePopup(string v)
    {
        for (int i = 0; i < rmPopups.Count; i++)
        {
            if (rmPopups[i].Equals(v))
            {
                if (popups.ContainsKey(v))
                {
                    Destroy(popups[v].gameObject);
                    popups.Remove(v);
                }
                break;
            }
        }
    }

    public void ShowPopup<T>(object data = null, bool forceShow = false) where T : BasePopup
    {
        string namePopup = typeof(T).Name;
        BasePopup result = null;
        if (curPopup != null)
        {
            var curName = curPopup.GetType().Name;
            if (curName.Equals(namePopup))
            {
                result = curPopup;
            }
            else
            {
                RemovePopup(curName);
            }
        }

        if (result == null)
        {
            if (!popups.ContainsKey(namePopup))
            {
                BasePopup popupScr = GetNewPopup<T>();
                if (popupScr != null)
                {
                    popups.Add(namePopup, popupScr);
                }
            }
            if (popups.ContainsKey(namePopup))
            {
                result = popups[namePopup];
            }
        }
        bool isShow = false;
        if (result != null)
        {
            if (forceShow)
            {
                isShow = true;
            }
            else
            {
                if (result.IsHide)
                {
                    isShow = true;
                }
            }
        }

        if (isShow)
        {
            curPopup = result;
            HideAllPopups();
            result.transform.SetAsLastSibling();
            result.Show(data);
        }
    }
    #endregion
    #region Notify
    private BaseNotify GetNewNotify<T>() where T : BaseNotify
    {
        string nameNotify = typeof(T).Name;
        GameObject pfNotify = GetUIPrefab(UIType.NOTIFY, nameNotify);
        if (pfNotify == null || !pfNotify.GetComponent<BaseNotify>())
        {
            throw new MissingReferenceException("Cant found " + nameNotify + " notify. !!!");
        }
        GameObject ob = Instantiate(pfNotify) as GameObject;
        ob.transform.SetParent(this.cNotify.transform);
        ob.transform.localScale = Vector3.one;
        ob.transform.localPosition = Vector3.zero;
#if UNITY_EDITOR
        ob.name = "NOTIFY_" + nameNotify;
#endif
        BaseNotify notifyScr = ob.GetComponent<BaseNotify>();
        notifyScr.Init();
        return notifyScr;
    }

    public void HideNotify<T>(bool force = false) where T : BaseNotify
    {
        string notifyName = typeof(T).Name;
        if (notifies.ContainsKey(notifyName))
        {
            if (force || !notifies[notifyName].IsHide)
            {
                notifies[notifyName].Hide();
            }
        }
    }

    public void HideAllNotifies()
    {
        BaseNotify notifyScr = null;
        foreach (KeyValuePair<string, BaseNotify> item in notifies)
        {
            notifyScr = item.Value;
            if (notifyScr == null || notifyScr.IsHide)
                continue;
            notifyScr.Hide();
            if (notifies.Count <= 0)
                break;
        }
    }

    public T GetExistNotify<T>() where T : BaseNotify
    {
        string notifyName = typeof(T).Name;
        if (notifies.ContainsKey(notifyName))
        {
            return notifies[notifyName] as T;
        }
        return null;
    }

    private void RemoveNotify(string v)
    {
        for (int i = 0; i < rmNotifiess.Count; i++)
        {
            if (rmNotifiess[i].Equals(v))
            {
                if (notifies.ContainsKey(v))
                {
                    Destroy(notifies[v].gameObject);
                    notifies.Remove(v);
                }
                break;
            }
        }
    }

    public void ShowNotify<T>(object data = null, bool forceShow = false) where T : BaseNotify
    {
        string nameNotify = typeof(T).Name;
        BaseNotify result = null;

        if (curNotify != null)
        {
            var curName = curNotify.GetType().Name;
            if (curName.Equals(nameNotify))
            {
                result = curNotify;
            }
            else
            {
                RemoveNotify(curName);
            }
        }

        if (result == null)
        {
            if (!notifies.ContainsKey(nameNotify))
            {
                BaseNotify notifyScr = GetNewNotify<T>();
                if (notifyScr != null)
                {
                    notifies.Add(nameNotify, notifyScr);
                }
            }
            if (notifies.ContainsKey(nameNotify))
            {
                result = notifies[nameNotify];
            }
        }

        bool isShow = false;
        if (result != null)
        {
            if (forceShow)
            {
                isShow = true;
            }
            else
            {
                if (result.IsHide)
                {
                    isShow = true;
                }
            }
        }

        if (isShow)
        {
            curNotify = result;
            result.transform.SetAsFirstSibling();
            result.Show(data);
        }
    }
    #endregion
    #region Overlap
    private BaseOverlap GetNewOverlap<T>() where T : BaseOverlap
    {
        string overlapName = typeof(T).Name;
        GameObject pfOverlap = GetUIPrefab(UIType.OVERLAP, overlapName);
        if (pfOverlap == null || !pfOverlap.GetComponent<BaseOverlap>())
        {
            throw new MissingReferenceException("Cant found " + overlapName + " overlap. !!!");
        }

        GameObject ob = Instantiate(pfOverlap) as GameObject;
        ob.transform.SetParent(this.cOverlap.transform);
        ob.transform.localScale = Vector3.one;
        ob.transform.localPosition = Vector3.zero;
#if UNITY_EDITOR
        ob.name = "OVERLAP_" + overlapName;
#endif
        BaseOverlap scr = ob.GetComponent<BaseOverlap>();
        scr.Init();
        return scr;
    }

    public void HideAllOverlaps()
    {
        BaseOverlap scr = null;
        foreach (KeyValuePair<string, BaseOverlap> item in overlaps)
        {
            scr = item.Value;
            if (scr == null || scr.IsHide)
                continue;
            scr.Hide();
        }
    }

    public T GetExistOverlap<T>() where T : BaseOverlap
    {
        string overlapName = typeof(T).Name;
        if (overlaps.ContainsKey(overlapName))
        {
            return overlaps[overlapName] as T;
        }
        return null;
    }

    public void HideOverlap<T>(bool force = false) where T : BaseOverlap
    {
        string overlapName = typeof(T).Name;
        if (overlaps.ContainsKey(overlapName))
        {
            if (force || !overlaps[overlapName].IsHide)
            {
                overlaps[overlapName].Hide();
            }
        }
    }

    private void RemoveOverlap(string v)
    {
        for (int i = 0; i < rmOverlaps.Count; i++)
        {
            if (rmOverlaps[i].Equals(v))
            {
                if (overlaps.ContainsKey(v))
                {
                    overlaps.Remove(v);
                    Destroy(overlaps[v].gameObject);
                }
                break;
            }
        }
    }

    public void ShowOverlap<T>(object data = null, bool force = false) where T : BaseOverlap
    {
        string overlapName = typeof(T).Name;
        BaseOverlap result = null;

        if (curOverlap != null)
        {
            var curName = curOverlap.GetType().Name;
            if (curName.Equals(overlapName))
            {
                result = curOverlap;
            }
            else
            {
                RemoveOverlap(curName);
            }
        }

        if (result == null)
        {
            if (!overlaps.ContainsKey(overlapName))
            {
                BaseOverlap scr = GetNewOverlap<T>();
                if (overlaps != null)
                {
                    overlaps.Add(overlapName, scr);
                }
            }
            if (overlaps.ContainsKey(overlapName))
            {
                result = overlaps[overlapName];
            }
        }
        if (result != null && (result.IsHide || force))
        {
            curOverlap = result;
            result.transform.SetAsLastSibling();
            result.Show(data);
        }
    }
    #endregion

    private GameObject GetUIPrefab(UIType type, string uiName)
    {
        GameObject result = null;
        var defaultPath = "";
        if (result == null)
        {
            switch (type)
            {
                case UIType.SCREEN:
                    {
                        defaultPath = SCREEN_RESOURCES_PATH + uiName;
                    }
                    break;
                case UIType.POPUP:
                    {
                        defaultPath = POPUP_RESOURCES_PATH + uiName;
                    }
                    break;
                case UIType.NOTIFY:
                    {
                        defaultPath = NOTIFY_RESOURCES_PATH + uiName;
                    }
                    break;
                case UIType.OVERLAP:
                    {
                        defaultPath = OVERLAP_RESOURCES_PATH + uiName;
                    }
                    break;
            }
            result = Resources.Load(defaultPath) as GameObject;//kiểu GetUIPrefab là GO nên phải return dạng GO
        }
        return result;
    }
}

