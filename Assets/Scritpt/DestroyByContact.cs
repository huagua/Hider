using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
    virus / varient people
    绑定病毒和被病毒感染的普通人
*/
public class DestroyByContact : MonoBehaviour
{
    public GameObject convertPeople;
    public GameManager gameController;

    /*
        Start is called on the frame when a script is enabled just before any of the Update methods are called the first time.
        最先调用的函数
    */
    void Start() 
    {
        /*
            获取GameController
                1、首先获取到GameController的对象实例
                2、再获取该对象中的GameController组件
                3、即可调用gameController中的方法
        */
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if(gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameManager>();
        }

        if(gameController == null)
        {
            Debug.Log("Counld not find the GameController");
        }
    }

    /*
        OnTriggerEnter is called when the GameObject collides with another GameObject.
        该对象与其他对象发生碰撞时调用
    */
    void OnTriggerEnter(Collider other)
    {
        // 如果与玩家碰撞，则游戏结束
    	if(other.gameObject.tag == "Player")
    	{
            // 销毁两个对象
            Destroy (other.gameObject);
            Destroy(this.gameObject);
            // 游戏结束
            gameController.GameOver();
    	}

        // 如果与普通行人碰撞，则感染行人
        if(other.gameObject.tag == "Origin People")
        {
            
            // 实例化被感染的行人
            Instantiate(convertPeople, other.transform.position, Quaternion.identity);

            // 如果当前对象不是被感染的行人，而是病毒，则需要销毁病毒
            if(this.gameObject.tag != "Varient People"){
                Destroy(this.gameObject);
            }

            // 销毁普通行人
            Destroy (other.gameObject);
        }
    }
}
