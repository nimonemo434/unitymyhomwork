using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class firstgame : MonoBehaviour
{

    float rotspeed = 0;
    bool isplay = false;
    GameObject chance;
    // Start is called before the first frame update
    void Start()
    {
        chance = GameObject.Find("chance");
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
        int carMoveChance = 0;

        if (therot > 330f && therot <= 360 || therot > 0f && therot <= 30)
        {
            chance.GetComponent<Text>().text = "·ê·¿ °á°ú : 1 ";
            carMoveChance = 1;
        }
        else if (therot > 30f && therot <= 90f)
        {
            chance.GetComponent<Text>().text = "·ê·¿ °á°ú : 2 ";
            carMoveChance = 2;
        }
        else if (therot > 90f && therot <= 150f)
        {
            chance.GetComponent<Text>().text = "·ê·¿ °á°ú : 1 ";
            carMoveChance = 1;
        }
        else if (therot > 150f && therot <= 210f)
        {
            chance.GetComponent<Text>().text = "·ê·¿ °á°ú : 3 ";
            carMoveChance = 3;
        }
        else if (therot > 210f && therot <= 270f)
        {
            chance.GetComponent<Text>().text = "·ê·¿ °á°ú : 2 ";
            carMoveChance = 2;
        }
        else if (therot > 270f && therot <= 330f)
        {
            chance.GetComponent<Text>().text = "·ê·¿ °á°ú : 4 ";
            carMoveChance = 4;
        }
    }
}
