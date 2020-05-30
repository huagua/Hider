using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public int maxScore;
	public Text maxScoreText;

    // 开始游戏方法，加载游戏场景
    public void OnStartGame()
    {
    	SceneManager.LoadScene(1);
    }

    void Start()
    {
        // 从本地获取到历史最高分，并显示到页面上
    	maxScore = PlayerPrefs.GetInt("maxScore");
    	maxScoreText.text = "历史最高分：" + maxScore;

    }
}
