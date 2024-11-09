using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField]
    private float timeMultiplier;

    [SerializeField]
    private float startHour;

    [SerializeField]
    private TextMeshProUGUI timeText;


    [SerializeField]
    private float sunriseHour;

    [SerializeField]
    private float sunsetHour;

    [SerializeField]
    private Color dayAmbientLight;

    [SerializeField]
    private Color nightAmbientLight;

    private float maxDayAmbientLightIntensity;
    private float maxNightAmbientLightIntensity;

    [SerializeField]
    private AnimationCurve lightChangeCurve;

    [SerializeField]
    private Light sunLight;

    [SerializeField]
    private float maxSunLightIntensity;

    [SerializeField]
    private Light moonLight;

    [SerializeField]
    private float maxMoonLightIntensity;

  

    public EventHandler <OnDayChangeEventArgs> OnDayChange;

    private DateTime currentTime;
    private TimeSpan noon = new TimeSpan(12, 0, 0);

    private TimeSpan sunriseTime;
    private TimeSpan sunsetTime;

    void Start()
    {
        // set the current time 
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour); 

        sunriseTime =  TimeSpan.FromHours(sunriseHour);
        sunsetTime =  TimeSpan.FromHours(sunsetHour);

        maxDayAmbientLightIntensity = maxSunLightIntensity;
        maxNightAmbientLightIntensity = maxMoonLightIntensity;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimeOfDay();
        RotateSun();
        UpdateLightSettings();
    }

    private void UpdateTimeOfDay()
    {
        // update the current time add a seconds in every update times the speed of how much time passes
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);
        
        // if time UI exsist put the current time value in it
        if (timeText != null)
        { 
            timeText.text =  currentTime.ToString("HH:mm");
        }
    }

    private void RotateSun()
    {
        float sunLightRotation;
        // check if the the sun is still up
        if (currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime)
        {
            // calculate the total time span between sunrise to sun set
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(sunriseTime, sunsetTime);
            
            // calculate how much day time left after sunrise
            TimeSpan timeSinceSunrise = CalculateTimeDifference(sunriseTime, currentTime.TimeOfDay);
            
            // get the percentage of day time before sunset
            double percentageDayToNight = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;
            
            // check time if it is morning
            if (currentTime.TimeOfDay <= noon)
            {
                // calculate morning light intensity
                float percentageMoringToNoon = (((float)percentageDayToNight*100 ) / 50f) * 100f;
                float lightIntensity = (percentageMoringToNoon / 100) * maxDayAmbientLightIntensity ;
                
                // if morning light is higher than night light set light intensity
                RenderSettings.ambientIntensity  = lightIntensity > maxNightAmbientLightIntensity ? lightIntensity : maxNightAmbientLightIntensity ;

            }
            else
            { 
                // calculate noon intensity
                float percentageNoonToNight = ((((float)percentageDayToNight*100) - 50f) / 50f) * 100f;
                float lightIntensity =  maxDayAmbientLightIntensity * (1 - (percentageNoonToNight / 100f));

                // if noonlight is lower that higher that night intensity set noon light 
                RenderSettings.ambientIntensity  = lightIntensity > maxNightAmbientLightIntensity ? lightIntensity : maxNightAmbientLightIntensity ;
                
            }
            
            
            OnDayChange?.Invoke(this, new OnDayChangeEventArgs
            {
                isNightTime = false
            });

            sunLightRotation = Mathf.Lerp(0, 180, (float)percentageDayToNight);
        }
        else
        {
            // calculate the total time span between sunset to sun rise
            TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(sunsetTime, sunriseTime);

            // calculate houw the how much time 
            TimeSpan timeSinceSunset = CalculateTimeDifference(sunsetTime, currentTime.TimeOfDay);

            double percentage =  timeSinceSunset.TotalMinutes /  sunsetToSunriseDuration.TotalMinutes;
            
            RenderSettings.ambientIntensity = maxNightAmbientLightIntensity;

            OnDayChange?.Invoke(this, new OnDayChangeEventArgs
            {
                isNightTime = true
            });


            sunLightRotation = Mathf.Lerp(180, 360, (float)percentage);
        }

        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
    }

    private void UpdateLightSettings()
    {
        float dotProduct = Vector3.Dot(sunLight.transform.forward, Vector3.down);
        sunLight.intensity = Mathf.Lerp(0,maxSunLightIntensity,lightChangeCurve.Evaluate(dotProduct));
        moonLight.intensity = Mathf.Lerp(maxMoonLightIntensity, 0, lightChangeCurve.Evaluate(dotProduct));
        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, lightChangeCurve.Evaluate(dotProduct));
    }

    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;

        if (difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
        }

        return difference;
    }


    public class OnDayChangeEventArgs : EventArgs
    {
        public bool isNightTime;
    }
}
