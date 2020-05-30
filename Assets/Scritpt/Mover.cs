using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 此脚本控制普通行人和被感染行人从上至下运动
public class Mover : MonoBehaviour
{
	public float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.up * speed;
    }
}
