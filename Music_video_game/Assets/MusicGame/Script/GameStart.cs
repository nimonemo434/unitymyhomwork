using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    GameObject Button;
    // Start is called before the first frame update
    void Start()
    {
        Button = GameObject.Find("Button");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TestText()
    {
        Debug.Log("���� ����");
        Button.GetComponent<Button>().interactable = false;
    }
}
