using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    敌机控制脚本
*/
public class EnemyController : MonoBehaviour
{
	public Boundary bound;
	public GameManager gameController;
	private float speed = 0.1f;
	private float timeWait = 0.05f;
	Rigidbody r;

    public GameObject bolt; // 发射子弹
    public Transform spawnPos;  // 发射位置

    private float fireRate = 1f; // 发射时间间隔
    private float nextFire; // 下次发射时间

    // Start is called before the first frame update
    void Start()
    {
    	r = GetComponent<Rigidbody>();
    	StartCoroutine(moveEnemy());
    }

    // Update is called once per frame 每一帧都会调用
    void Update()
    {
    	// 间断开火
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(bolt, spawnPos.position, spawnPos.rotation);
        }
    	
    }

    // 固定时间移动敌机
    IEnumerator moveEnemy()
    {
        yield return new WaitForSeconds(timeWait);

    	while(!gameController.gameOver){
        	moveLeftOrRight();
        	yield return new WaitForSeconds(timeWait);
    	}
    }

    // 左右移动敌机
    void moveLeftOrRight()
    {
        // 首先判定敌机是否在左右边界，如果在边界则需要调转方向
    	if (r.position.x == bound.xMin)
    	{
    		speed = 0.1f;
    	}else if(r.position.x == bound.xMax)
    	{
    		speed = -0.1f;
    	}
    	
        // 每次移动的距离是speed = 0.1f
    	r.MovePosition(r.position+Vector3.right*speed);
    	r.position = new Vector3 (
    		Mathf.Clamp(r.position.x, bound.xMin, bound.xMax), // 控制边界
    		r.position.y,
    		r.position.z
    		);
    }
}
