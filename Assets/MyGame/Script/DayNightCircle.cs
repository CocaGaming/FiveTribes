using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCircle : MonoBehaviour
{
    [SerializeField]
    private Light sun;
    [SerializeField, Range(0, 24)]
    private float timeOfDay;
    [SerializeField]
    private float sunRotationSpeed;
    [SerializeField]
    private Gradient skyColor;
    [SerializeField]
    private Gradient equatorColor;
    [SerializeField]
    private Gradient sunColor;

    private void Update()
    {
        timeOfDay += Time.deltaTime * sunRotationSpeed;
        if (timeOfDay > 24)
        {
            timeOfDay = 0;
        }
        UpdateSunRotation();
        UpdateLighting();
    }
    private void OnValidate()
    {
        UpdateSunRotation();
        UpdateLighting();
    }
    private void UpdateSunRotation()
    {
        float sunRotation = Mathf.Lerp(-90, 270, timeOfDay / 24);
        sun.transform.rotation = Quaternion.Euler(sunRotation, sun.transform.rotation.y, sun.transform.rotation.z);
    }
    private void UpdateLighting()
    {
        float timeFraction = timeOfDay / 24;
        //đổi màu môi trường
        RenderSettings.ambientEquatorColor=equatorColor.Evaluate(timeFraction);
        RenderSettings.ambientSkyColor=skyColor.Evaluate(timeFraction);
        sun.color = sunColor.Evaluate(timeFraction);

        //đổi màu skydome
        Color skyBoxTop = RenderSettings.skybox.GetColor("_ColorTop");
        skyBoxTop = skyColor.Evaluate(timeFraction);
        RenderSettings.skybox.SetColor("_ColorTop", skyBoxTop);
    }
}
