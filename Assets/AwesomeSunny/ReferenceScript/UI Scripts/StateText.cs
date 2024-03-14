using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateText : MonoBehaviour
{

    public TMPro.TextMeshProUGUI uiText;
    private float t = 0.0f;
    private float showTime = 0.0f;
    private bool playing;
    public static StateText Instance
    {
        get
        {
            if (_inst == null)
                _inst = FindObjectOfType<StateText>();
                
                return _inst;

        }
    }
    private static StateText _inst;

    public void UpdateText(string text, float showTime)
    {
        Stop();
        this.showTime = showTime;
        this.uiText.text = text;
        playing = true;
    }

    public void Stop()
    {
        t = 0;
        uiText.text = null;
        playing = false;
    }

    public void Update()
    {
        if (playing)
        {
            t += Time.deltaTime;
            if(t >= showTime)
            {
                Stop();
            }
            else
            {

            }
        }
    }
}
