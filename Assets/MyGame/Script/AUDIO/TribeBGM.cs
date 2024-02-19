using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TribeBGM : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponentInParent<PlayerInputManager>() != null && other.gameObject.CompareTag("Tungus"))
        {
            if (AudioManager.HasInstance)
            {
                AudioManager.Instance.PlayBGM(AUDIO.BGM_TUNGUS);
            }
        }
        if (other.gameObject.GetComponentInParent<PlayerInputManager>() != null && other.gameObject.CompareTag("Viking"))
        {
            if (AudioManager.HasInstance)
            {
                AudioManager.Instance.PlayBGM(AUDIO.BGM_VIKING);
            }
        }
        if (other.gameObject.GetComponentInParent<PlayerInputManager>() != null && other.gameObject.CompareTag("Asian"))
        {
            if (AudioManager.HasInstance)
            {
                AudioManager.Instance.PlayBGM(AUDIO.BGM_MENU);
            }
        }
        if (other.gameObject.GetComponentInParent<PlayerInputManager>() != null && other.gameObject.CompareTag("Orc"))
        {
            if (AudioManager.HasInstance)
            {
                AudioManager.Instance.PlayBGM(AUDIO.BGM_ORC);
            }
        }
    }
}
