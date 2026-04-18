using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class EnvironmentSaturation : MonoBehaviour
{
    public Volume vol;
    public Color pinkTint = new Color();
    public ColorAdjustments colourAdjustments;

    void Start()
    {
        if (vol.profile.TryGet(out colourAdjustments))
        {
        colourAdjustments.colorFilter.value = pinkTint;
        }
    }

    void Update()
    {
        float sat = GameManager.Instance.festivalSaturationLevel;
        ApplySaturation(sat);
    }

    void ApplySaturation(float sat)
    {// 0 = 80, 1=20
        colourAdjustments.saturation.value = Mathf.Lerp(-80f, 20f, sat);
    }
}
