
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using UnityEngine;

public class MovePlatform1 : MonoBehaviour
{

    public float speed;
    public float timeLeft = 15;

    void Update()
    {


        if (timeLeft > -15)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft > 1)
                transform.Translate(Vector2.down * speed * Time.deltaTime);
            else
                if (timeLeft < 1)
                transform.Translate(Vector2.up * speed * Time.deltaTime);

        }
        else timeLeft = 15;



    }
}