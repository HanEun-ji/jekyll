using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager gm;
    public static int saved; //저장된 씬넘버

    // Use this for initialization
    void Start () {
        saved = 0;
	}
	
	// Update is called once per frame
	void Update () {
        
     
	}

    private void Awake() //싱글톤
    {
        GameManager.gm = this;
    }

    public void MoveScene(int n) //n번째 씬으로 이동
    {
        SceneManager.LoadScene(n);
    }

    public void GameStart()//게임 시작 버튼 누를 시
    {
        MoveScene(1);
    }

    public void SaveGame() //게임 저장
    {
        saved = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadScene()
    {
        if(saved == 0)
            Debug.Log("No Saved Data");

        else
            SceneManager.LoadScene(saved);
    }

    public int CurrentScene()//현재 씬 넘버 반환하는 메소드
    {
        int n;
        n = SceneManager.GetActiveScene().buildIndex;
        return n;
    }

    public Collider2D ClickTarget() //클릭된 오브젝트 반환하는 메소드
    {
        Vector2 Click_Point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D Click_Coll = Physics2D.OverlapPoint(Click_Point);
        return Click_Coll;
    }
}
