using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChanceScript : MonoBehaviour
{
    float Cspeed = 0;
    GameObject roulette; // ·ê·¿ ¿ÀºêÁ§Æ®
    GameObject chance;
    // Start is called before the first frame update
    void Start()
    {
        roulette = GameObject.Find("roulette"); // ·ê·¿ ¿ÀºêÁ§Æ® È®ÀÎ
        chance = GameObject.Find("chance");
    }

    // Update is called once per frame
    void Update()
    {
        roulette.transform.Rotate(0, 0, Cspeed); // ·ê·¿ÀÇ ¼Óµµ

        if (Cspeed == 0f) // ·ê·¿ÀÇ ¼Óµµ°¡ 0ÀÎ°æ¿ì - ÀÌ¹Ì ·ê·¿ÀÇ ½ºÅ©¸³Æ®´Â µû·Î ÀÖ±â¿¡ 0ÀÌ µÈ´Ù
        {
            resultluck(roulette.transform.eulerAngles.z); // ¸ØÃá ·ê·¿ÀÇ À§Ä¡¿¡ µû¶ó ¼ýÀÚ Ãâ·Â
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
