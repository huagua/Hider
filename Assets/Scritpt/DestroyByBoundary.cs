using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour
{
	/*
		OnTriggerExit is called when the Collider other has stopped touching the trigger.
		当其他碰撞体停止碰撞时调用
	*/
    void OnTriggerExit(Collider other)
    {
    	Destroy(other.gameObject);
    }
}
