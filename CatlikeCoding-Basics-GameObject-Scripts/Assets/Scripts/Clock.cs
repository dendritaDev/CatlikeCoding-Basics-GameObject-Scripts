using System;
using UnityEngine;
public class Clock : MonoBehaviour 
{
    const float hoursToDegrees = 30f, minutesToDegrees = 6f, secondsToDegrees = 6f;

    [SerializeField]
    Transform hoursPivot, minutesPivot, secondsPivot;

    private void Update()
    {
        TimeSpan time = DateTime.Now.TimeOfDay; //Podriamos poner abajo para hour minute y second DateTime.Now, pero entonces eso se estaria calculando no a la vez

        //tmb podriamos hacer algo como auto time = DateTime.Now, que en c# seria en vez de poner auto, poner var, rollo:
        //var time = DateTime.Now.TimeOfDay;

        hoursPivot.localRotation  = 
            Quaternion.Euler(0f,0f, hoursToDegrees * (float)time.TotalHours);
        minutesPivot.localRotation =
            Quaternion.Euler(0f, 0f, minutesToDegrees * (float)time.TotalMinutes);
        secondsPivot.localRotation =
            Quaternion.Euler(0f, 0f, secondsToDegrees * (float)time.TotalSeconds);
    }
}