using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : BaseManager<GameManager>
{
    public TribeType tribeType;
    public RoleType roleType;
    private GameObject characterSelected;
    // Start is called before the first frame update
    void Start()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowScreen<ScreenHome>();
            ScreenHome scr= UIManager.Instance.GetExistScreen<ScreenHome>();
            if(scr != null)
            {
                UIManager.Instance.ShowScreen<ScreenHome>();
            }
        }
    }
}
