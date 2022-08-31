using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstgame : MonoBehaviour
{

    float rotspeed = 0;
    bool isplay = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            rotspeed = 10;
            isplay = true;
        }
        if (isplay == false)
        {
            return;
        }

        rotspeed *= 0.95f;

        gameObject.transform.Rotate(0, 0, rotspeed);

        if(rotspeed < 0.0001f)
        {
            rotspeed = 0;
            resultluck(gameObject.transform.eulerAngles.z);
            isplay = false;
        }
    }
    void resultluck(float therot)
    {
        if(therot > 330f && therot <= 360 || therot > 0f && therot <= 30)
        {
            Debug.Log("1");
        }
        else if (therot > 30f && therot <= 90f)
        {
            Debug.Log("2");
        }
        else if (therot > 90f && therot <= 150f)
        {
            Debug.Log("1");
        }
        else if (therot > 150f && therot <= 210f)
        {
            Debug.Log("3");
        }
        else if (therot > 210f && therot <= 270f)
        {
            Debug.Log("2");
        }
        else if (therot > 270f && therot <= 330f)
        {
            Debug.Log("4");
        }
    }
}
