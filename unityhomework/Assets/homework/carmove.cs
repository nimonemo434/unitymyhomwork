using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class carmove : MonoBehaviour
{
    float speed = 0;
    Vector2 startpos;
    int anotherchance;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("roulette").GetComponent<firstgame>().carMoveChance > 0)
        {

            if (Input.GetMouseButtonDown(0))
            {
                startpos = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Vector2 endpos = Input.mousePosition;
                float swipeliength = endpos.x - startpos.x;

                speed = swipeliength / 500.0f;

                GetComponent<AudioSource>().Play();

                GameObject.Find("roulette").GetComponent<firstgame>().carMoveChance -= 1;
                anotherchance = GameObject.Find("roulette").GetComponent<firstgame>().carMoveChance;

                GameObject.Find("chance").GetComponent<Text>().text = $"주어진 기회:{anotherchance}";
            }

            transform.Translate(speed, 0, 0);

            speed *= 0.98f;

            if (GameObject.Find("roulette").GetComponent<firstgame>().carMoveChance == 0) 
            {
                GameObject.Find("chance").GetComponent<Text>().text = "게임 끝.";
            }
        }
    }
}
