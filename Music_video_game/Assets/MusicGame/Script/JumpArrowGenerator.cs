using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpArrowGenerator : MonoBehaviour
{
    public List<float> songNodeTiming;      //타이밍이 들어가 있는 List
    public List<GameObject> nodeObjList;    //생성될 노드 프리팹을 가지고 있는 List

    float delta;
    public bool isStart = false;
    int nodeCount = 0;

    void Update()
    {
        //게임 시작 안했으면 종료
        if (isStart == false)
        {
            return;
        }

        //시간 측정
        delta += Time.deltaTime;
        while (true)
        {
            //정해놓은 노드보다 많으면 탈출
            if (nodeCount >= songNodeTiming.Count)
            {
                break;
            }
            //다음 노드번째의 타이밍인지 체크
            if (songNodeTiming[nodeCount] <= delta)
            {
                //0~4번 사이 오브젝트 랜덤 생성
                Instantiate(nodeObjList[Random.Range(0, 4)]);
                nodeCount++;
            }
            //아니면 while문 탈출
            else
            {
                break;
            }

            //while문을 쓴 이유는 동시에 여러 노드가 등장할 수도 있기 때문.
            //예, 1분때에 2개가 들어가있으면 2개가 동시에 나와야함.
        }
    }
}
