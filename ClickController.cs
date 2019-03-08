using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickController : MonoBehaviour {
    //Day1 매니저

    public Text WordText;

    public GameObject convBox, clearScene, tutBox;
    public GameObject bar,bomb;

    public Sprite Bar1, Bar2, Bar3,Bomb, clear;
    public Sprite d1Tut, memTut, newspap;

    public static int barCount;
    public static bool isTut, isMemTut, isBomb, isMem, isMap;
    private static bool isConv; //대화중인지

    private static bool hwa, news;

    // Use this for initialization
    void Start () {
        WordText.text = "";
        convBox.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        int cur = this.gameObject.GetComponent<GameManager>().CurrentScene(); //현재 씬 넘버
        if (!isTut && cur!=6 && cur!=5)
        {
            SpriteRenderer spr = tutBox.GetComponent<SpriteRenderer>();
            spr.sprite = d1Tut;
            if (Input.GetMouseButtonDown(0))
            {
                spr.sprite = null;
                isTut = true;
            }
        }
        else if (Input.GetMouseButtonDown(0) && !isMemTut && !isConv)
        {
            Collider2D Click_Coll = this.gameObject.GetComponent<GameManager>().ClickTarget();

            if (Click_Coll != null)
            {
                Debug.Log("Clicked" + Click_Coll);
                if (Click_Coll.gameObject.CompareTag("Car"))
                {
                    StartConv("차 안으로 들어간다.");
                    Invoke("NextScene", 1f);
                }
                
                else if (Click_Coll.gameObject.CompareTag("Garage"))
                {
                    StartConv("XXX CEO의 차고인 것 같다.");
                    Invoke("EndConv", 1f);
                }

                else if (Click_Coll.gameObject.CompareTag("hwayak"))
                {
                    if(!hwa)
                        barCount++;
                    hwa = true;
                    isConv = true;
                    
                }

                else if (Click_Coll.gameObject.CompareTag("Newspaper"))
                {
                   if (!news)
                        barCount++;
                    news = true;
                    isConv = true;
                }

                else if (Click_Coll.gameObject.CompareTag("CarDoor"))
                {
                    StartConv("다시 차고로 돌아간다.");
                    Invoke("PrevScene", 1f);
                }

                else if (Click_Coll.gameObject.CompareTag("Bomb")&&news&&hwa&&!isMem)
                {
                    StartConv("폭탄을 발견했다.");
                    //폭탄 스프라이트 변경
                    SpriteRenderer sp = bomb.GetComponent<SpriteRenderer>();
                    sp.sprite = Bomb;
                    //폭탄 해제 메소드
                    Invoke("ToBomb", 2f);
                }
            }
        }

        if (isMap && cur==3)
        {
            AllClear();
        }

        if (isMem)
        {
            if (cur == 3 && !isMap)
            {
                StartConv("핸들에 폭탄이 설치되어 있다.");
                Invoke("ToMap", 2f);
            }
        }
        if (isBomb && cur==3)
        {
            StartConv("폭탄 해제에 성공했다.");
            isBomb = false;
            barCount = 3;
            Invoke("EndConv", 1f);
        }

        if(cur!=5 && cur!=6)
            ChangeBar();

        if (isMemTut && cur==3)
        {
            if (Input.GetMouseButtonDown(0))
            {
                this.gameObject.GetComponent<GameManager>().MoveScene(4);
                isMemTut = false;
            }
        }

        if (isConv)
        {
            if (cur == 2 && Input.GetMouseButtonDown(0)) {
                if (WordText.text == "")
                    StartConv("화약이 만져진다.");
                else if (WordText.text == "화약이 만져진다.")
                    StartConv("차에 폭탄을 설치한 게 아닐까?");
                else if(WordText.text == "차에 폭탄을 설치한 게 아닐까?")
                {
                    isConv = false;
                    EndConv();
                }
            }
            else
            {
                SpriteRenderer sp = tutBox.GetComponent<SpriteRenderer>();
                sp.sprite = newspap;
                if (Input.GetMouseButtonDown(0))
                {
                    if (WordText.text == "")
                        StartConv("신문이 있다.");
                    else if (WordText.text == "신문이 있다.")
                        StartConv("'참 경영인 XXX.'");
                    else if (WordText.text == "'참 경영인 XXX.'")
                        StartConv("'항상 차에 탈 때도\n모두를 태우고 타는 모습을 보인다.'");
                    else if (WordText.text == "'항상 차에 탈 때도\n모두를 태우고 타는 모습을 보인다.'")
                        StartConv("폭탄은 XXX이 탄 후에 터지도록 설치했을 것이다.");
                    else if (WordText.text == "폭탄은 XXX이 탄 후에 터지도록 설치했을 것이다.")
                    {
                        EndConv();
                        sp.sprite = null;
                        isConv = false;
                    }
                }
            }
        }
    }

    public void StartConv(string word)
    {
        convBox.SetActive(true);
        WordText.text = word;
    }

    public void EndConv()
    {
        convBox.SetActive(false);
        WordText.text = "";
    }

    void ClickEndConv() //?
    {
        if (Input.GetMouseButtonDown(0))
            EndConv();
    }

    void NextScene()
    {
        int cur = this.gameObject.GetComponent<GameManager>().CurrentScene();
        this.gameObject.GetComponent<GameManager>().MoveScene(++cur);
    }

    void ToBomb()
    {
        this.gameObject.GetComponent<GameManager>().MoveScene(5);
    }

    void ToMap()
    {
        this.gameObject.GetComponent<GameManager>().MoveScene(6);
    }

    public void PrevScene()
    {
        int cur = this.gameObject.GetComponent<GameManager>().CurrentScene();
        this.gameObject.GetComponent<GameManager>().MoveScene(--cur);
    }

    void ChangeBar()
    {
        SpriteRenderer spr = bar.GetComponent<SpriteRenderer>();
        if (barCount == 1)
            spr.sprite = Bar1;
        else if (barCount == 2)
            spr.sprite = Bar2;
        else if (barCount == 3 && !isMem)
        {
            spr.sprite = Bar3;
            //기억의 전환 메소드
            Invoke("MemChange", 1f);
        }
    }
    
    void MemChange()
    {
        isMemTut = true;
        SpriteRenderer spr = tutBox.GetComponent<SpriteRenderer>();
        spr.sprite = memTut;
        barCount = 0;
    }

    void AllClear()
    {
        StartConv("모든 폭탄을 해제했다.");
        SpriteRenderer spr = clearScene.GetComponent<SpriteRenderer>();
        spr.sprite = clear;
        this.gameObject.GetComponent<GameManager>().SaveGame();
        Time.timeScale = 0f;
    }
}
