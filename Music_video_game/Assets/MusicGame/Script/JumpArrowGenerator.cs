using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpArrowGenerator : MonoBehaviour
{
    public List<float> songNodeTiming;      //Ÿ�̹��� �� �ִ� List
    public List<GameObject> nodeObjList;    //������ ��� �������� ������ �ִ� List

    float delta;
    public bool isStart = false;
    int nodeCount = 0;

    void Update()
    {
        //���� ���� �������� ����
        if (isStart == false)
        {
            return;
        }

        //�ð� ����
        delta += Time.deltaTime;
        while (true)
        {
            //���س��� ��庸�� ������ Ż��
            if (nodeCount >= songNodeTiming.Count)
            {
                break;
            }
            //���� ����°�� Ÿ�̹����� üũ
            if (songNodeTiming[nodeCount] <= delta)
            {
                //0~4�� ���� ������Ʈ ���� ����
                Instantiate(nodeObjList[Random.Range(0, 4)]);
                nodeCount++;
            }
            //�ƴϸ� while�� Ż��
            else
            {
                break;
            }

            //while���� �� ������ ���ÿ� ���� ��尡 ������ ���� �ֱ� ����.
            //��, 1�ж��� 2���� �������� 2���� ���ÿ� ���;���.
        }
    }
}
