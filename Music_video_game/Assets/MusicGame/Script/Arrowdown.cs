using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrowdown : MonoBehaviour
{
    GameObject player;
    float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        Vector2 p1 = gameObject.transform.position;
        Vector2 p2 = player.transform.position;

        Vector2 dir = p1 - p2;

        float d = dir.magnitude;

        float r1 = 0.5f;
        float r2 = 1.0f;

        if (d <= r1 + r2)
        {
            GameObject director = GameObject.Find("GameManager");
            director.GetComponent<GameManager>().DecreaseHp();

            Destroy(gameObject);
        }
    }
}
