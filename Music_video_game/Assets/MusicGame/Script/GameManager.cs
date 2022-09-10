using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject hpGauge;
    // Start is called before the first frame update
    void Start()
    {
        hpGauge = GameObject.Find("Hp");
    }

    public void DecreaseHp()
    {
        hpGauge.GetComponent<Image>().fillAmount -= 0.1f;

        if (hpGauge.GetComponent<Image>().fillAmount == 0)
        {
            GameObject.Find("chance").GetComponent<Text>().text = "게임 오버";
        }
    }
}
