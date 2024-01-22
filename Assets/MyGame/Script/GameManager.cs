using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : BaseManager<GameManager>
{
    public TribeType tribeType;
    public RoleType roleType;
    private GameObject characterSelected;
    public float hour;

    private Light[] torchs;
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
    private void FixedUpdate()
    {
        if (ScreenGame.Instance != null)
        {
            hour = ScreenGame.Instance.hour;
        }

        torchs = FindObjectsOfType<Light>();
        if ((hour >= 18f && hour<=23f) || (hour>=0f && hour<=5f))//tối thì đuốc mới phát sáng
        {
            foreach (Light torch in torchs)
            {
                torch.enabled = true;
            }
        }
        else
        {
            foreach (Light torch in torchs)
            {
                torch.enabled = false;
            }
        }
    }
}
