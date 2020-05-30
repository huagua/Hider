using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMove : MonoBehaviour
{
    private GameObject bg;
    // Start is called before the first frame update
    void Start()
    {
        bg = GameObject.Find("Background");
    }

    // Update is called once per frame
    void Update()
    {
        bg.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(0, Time.time/20));
    }
}
