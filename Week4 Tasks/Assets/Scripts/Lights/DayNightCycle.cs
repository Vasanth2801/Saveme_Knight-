using UnityEngine;
using UnityEngine.Rendering;

public class DayNightCycle : MonoBehaviour
{
    public Volume ppv;

    public float tick;
    public float seconds;
    public float minutes;
    public float hours;
    public float days = 1;

    public bool activateLights;
    public GameObject[] lights;

    void Start()
    {
        ppv = gameObject.GetComponent<Volume>();
    }

    void FixedUpdate()
    {
        CalcTime();
    }

    public void CalcTime()
    {
        seconds += Time.fixedDeltaTime * tick;

        if(seconds >= 60)
        {
            seconds = 0;
            minutes += 1;
        }

        if(minutes >= 60)
        {
            seconds = 0;
            hours += 1;
        }

        if(hours >= 24)
        {
            seconds = 0;
            days += 1;
        }

        ControlPPV();
    }

    public void ControlPPV()
    {
        if (hours >= 21 && hours <22) 
        {

            if (activateLights == false)
            {
                if (minutes > 45)
                {
                    for (int i = 0; i < lights.Length; i++)
                    {
                        lights[i].SetActive(true);
                    }

                    activateLights = true;
                }
            }
        }


        if(hours>=6 && hours <7)
        {
            if(activateLights == true)
            {
                if(minutes > 45)
                {
                    for(int i = 0;i<lights.Length;i++)
                    {
                        lights[i].SetActive(false);
                    }

                    activateLights = false;
                }
            }
        }
    }
}