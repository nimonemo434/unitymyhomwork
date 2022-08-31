using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChanceScript : MonoBehaviour
{
    float Cspeed = 0;
    GameObject roulette; // �귿 ������Ʈ
    GameObject chance;
    // Start is called before the first frame update
    void Start()
    {
        roulette = GameObject.Find("roulette"); // �귿 ������Ʈ Ȯ��
        chance = GameObject.Find("chance");
    }

    // Update is called once per frame
    void Update()
    {
        roulette.transform.Rotate(0, 0, Cspeed); // �귿�� �ӵ�

        if (Cspeed == 0f) // �귿�� �ӵ��� 0�ΰ�� - �̹� �귿�� ��ũ��Ʈ�� ���� �ֱ⿡ 0�� �ȴ�
        {
            resultluck(roulette.transform.eulerAngles.z); // ���� �귿�� ��ġ�� ���� ���� ���
        }
    }
    void resultluck(float therot)
    {
        int carMoveChance = 0;

        if (therot > 330f && therot <= 360 || therot > 0f && therot <= 30)
        {
            chance.GetComponent<Text>().text = "�귿 ��� : 1 ";
            carMoveChance = 1;
        }
        else if (therot > 30f && therot <= 90f)
        {
            chance.GetComponent<Text>().text = "�귿 ��� : 2 ";
            carMoveChance = 2;
        }
        else if (therot > 90f && therot <= 150f)
        {
            chance.GetComponent<Text>().text = "�귿 ��� : 1 ";
            carMoveChance = 1;
        }
        else if (therot > 150f && therot <= 210f)
        {
            chance.GetComponent<Text>().text = "�귿 ��� : 3 ";
            carMoveChance = 3;
        }
        else if (therot > 210f && therot <= 270f)
        {
            chance.GetComponent<Text>().text = "�귿 ��� : 2 ";
            carMoveChance = 2;
        }
        else if (therot > 270f && therot <= 330f)
        {
            chance.GetComponent<Text>().text = "�귿 ��� : 4 ";
            carMoveChance = 4;
        }
    }
}
