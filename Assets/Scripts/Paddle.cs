using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float Speed = 2.0f;
    public float MaxMovement = 2.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.Instance.Difficulty == 1)
        {
            gameObject.transform.localScale = new Vector3(1.4f, .1f, 1);
        }

        if (DataManager.Instance.Difficulty == 2)
        {
            gameObject.transform.localScale = new Vector3(.8f, .1f, 1);
        }

        if (DataManager.Instance.Difficulty == 3)
        {
            gameObject.transform.localScale = new Vector3(.5f, .1f, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float input = Input.GetAxis("Horizontal");

        Vector3 pos = transform.position;
        pos.x += input * Speed * Time.deltaTime;

        if (pos.x > MaxMovement)
            pos.x = MaxMovement;
        else if (pos.x < -MaxMovement)
            pos.x = -MaxMovement;

        transform.position = pos;
    }
}
