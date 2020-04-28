using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class ScreenBlood : MonoBehaviour
{
    public float m_MaxIntensity = 1f;
    public float m_DealyTime = 1f;

    private bool m_IsStartFade = false;
    private float m_CurrentIntensity = 0.0f;
    private bool m_IsCanGrow = true;
    private bool m_IsCanReduce = false;

    private void Awake()
    {
        Fade(0);
        EventCenter.AddListener(EventDefine.ScreenBlood, StartEffect);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ScreenBlood, StartEffect);
        Fade(0);
    }
    private void StartEffect()
    {
        m_IsStartFade = true;
    }
    private void FixedUpdate()
    {
        if (m_IsStartFade)
        {
            if (m_IsCanGrow)
            {
                if (m_CurrentIntensity < m_MaxIntensity)
                {
                    m_CurrentIntensity += 0.02f;
                }
                else
                {
                    m_CurrentIntensity = m_MaxIntensity;
                    m_IsCanGrow = false;
                    StartCoroutine(Dealy());
                }
            }
            if (m_IsCanReduce)
            {
                if (m_CurrentIntensity > 0)
                {
                    m_CurrentIntensity -= 0.02f;
                }
                else
                {
                    m_CurrentIntensity = 0;
                    m_IsCanReduce = false;
                    m_IsStartFade = false;
                    m_IsCanGrow = true;
                }
            }
            Fade(m_CurrentIntensity);
        }
    }
    IEnumerator Dealy()
    {
        yield return new WaitForSeconds(m_DealyTime);
        m_IsCanReduce = true;
    }
    private void Fade(float intensity)
    {
        VignetteModel.Settings settings = GetComponent<PostProcessingBehaviour>().profile.vignette.settings;
        settings.intensity = intensity;
        GetComponent<PostProcessingBehaviour>().profile.vignette.settings = settings;
    }
}
