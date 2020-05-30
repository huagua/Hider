using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 整个场景控制脚本
public class GameManager : MonoBehaviour
{
	public Vector3 spawnValues;           
	public GameObject[] originPeoples;
    
    private int numPerWave = 10;
    private float spawnWait = 0.8f;    //普通行人间隔时间
    private float startWait = 1.5f;    // 初始等待时间
    private float wavesWait = 3f;    //每波普通行人等待时间

    public GameObject pausePanel;
    public GameObject gameOverPanel;

    public Text scoreText;      //游戏场景中显示分数
    public Text finalScore;         // gameover时显示分数


    public Button pauseRestartButton;    //暂停界面重新游戏按钮
    public Button pauseMenuButton;       //暂停界面菜单按钮

    public Button restartButton;    //重新游戏按钮
    public Button menuButton;       //菜单按钮
    public Button pauseButton;      //暂停按钮
    public Button continueButton;      //继续游戏按钮

    public static int score;        //游戏时的分数

    public bool gameOver;           // 游戏结束标志

    // 重新开始游戏方法
    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    // 返回菜单
    public void ReturnMenu()
    {
        SceneManager.LoadScene(0);
    }

    // 暂停游戏方法
    public void OnPauseGame()
    {
    	Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    // 继续游戏方法
    public void OnContinueGame()
    {
        pausePanel.SetActive(false);
    	Time.timeScale = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(Time.timeScale == 0) Time.timeScale = 1;
        StartCoroutine(SpawnWaves());
        resetData();
    }

    // resetData() 初始化数据
    private void resetData()
    {
        score = 0;
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        gameOver = false;
    }

    // SpawnWaves 批量产生普通行人, 一批15个，循环产生
    IEnumerator SpawnWaves() 
    {
        yield return new WaitForSeconds(startWait);
        while(!gameOver) 
        {
            for(int i = 0; i < numPerWave; i++)
            {
                Spawn();
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(wavesWait);
        }
        
    }

    // Spawn 产生一个普通行人
    void Spawn()
    {
        GameObject originPeople = originPeoples[Random.Range(0, originPeoples.Length)];
    	Vector3 p = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
    	Quaternion q = Quaternion.identity;
    	Instantiate(originPeople, p, q);
    }

    // AddScore 计分函数
    public void AddScore (int v) 
    {
        score += v;
        scoreText.text = score.ToString();
    }

    // GameOver 游戏结束后需要进行的操作
    public void GameOver()
    {
        gameOver = true;
        gameOverPanel.SetActive(true);
        finalScore.text = score.ToString();
        SaveData();
    }

    // 将历史最高分存储起来
    private void SaveData()
    {
        int tmp = PlayerPrefs.GetInt("maxScore");
        if(tmp < score){
            PlayerPrefs.SetInt("maxScore", score);
        }
    }
}
