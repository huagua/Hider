using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //可序列化
public class Boundary {
    public float xMin = -6f;
    public float xMax = 6f;
    public float yMin = -1f;
    public float yMax = 17f;
}

public class PlayerController : MonoBehaviour
{
	public float speed = 10f;
	public Boundary bound;
	Rigidbody r;

    public GameManager gameManager;

    public GameObject bolt; // 发射子弹
    public Transform spawnPos;  // 发射位置

    private float fireRate = 1f; // 发射时间间隔
    private float nextFire; // 下次发射时间

    // public AudioClip fire;

    void Start()
    {
        r = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 间断开火
        // if (!gameManager.gameOver && Time.time > nextFire)
        // {
        //     nextFire = Time.time + fireRate;
        //     // Instantiate(bolt, spawnPos.position, spawnPos.rotation);
        //     Instantiate(bolt, spawnPos.position, spawnPos.rotation);

        //     // GetComponent<AudioSource>().PlayOneShot(fire);  // 发射音效
        // }

        // 不间断开火
        // Instantiate(bolt, spawnPos.position, spawnPos.rotation);

        // 鼠标点击开火
        if (Input.GetButton ("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(bolt, spawnPos.position, spawnPos.rotation);
        }
    }

    /*
        Frame-rate independent MonoBehaviour.FixedUpdate message for physics calculations.
        固定更新，不受帧率的变化影响，它是以固定的时间间隔来被调用。

        一些物理属性的更新操作应该放在FxiedUpdate中操作，比如Force，Collider，Rigidbody等。
        外设的操作也是，比如说键盘或者鼠标的输入输出Input，因为这样GameObject的物理表现的更平滑，更接近现实。

        此处用作利用wasd键来控制player的上下左右移动
    */
    void FixedUpdate()
    {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(h, v, 0f);
        r.velocity = speed*move;
        r.position = new Vector3 (
            Mathf.Clamp(r.position.x, bound.xMin, bound.xMax),
            Mathf.Clamp(r.position.y, bound.yMin, bound.yMax),
            r.position.z
            );
    }

    /*
        is called when the user has clicked on a GUIElement or Collider and is still holding down the mouse.
        当鼠标拖拽GUI元素或者碰撞体时调用

        此处用来鼠标拖拽player进行移动，或者移动端手指拖拽
    */
    void OnMouseDrag()
    {
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        r.position = new Vector3 (
            Mathf.Clamp(targetPos.x, bound.xMin, bound.xMax),
            Mathf.Clamp(targetPos.y, bound.yMin, bound.yMax),
            r.position.z
            );
    }

}
