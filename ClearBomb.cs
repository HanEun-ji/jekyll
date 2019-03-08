using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearBomb : MonoBehaviour {
    public GameObject lightB, lightY, lightR;
    public GameObject lineB, lineY, lineR;
    public GameObject convBox;
    public GameObject tutBox;

    public Sprite tut1, tut2;
    public Sprite lightOff, bombSuc;

    public Text word;

    private bool isB, isR, isY;
    private bool bSuc, rSuc, ySuc;
    private bool isTut, isFail;

	// Use this for initialization
	void Start () {
        Time.timeScale = 1f;
        SpriteRenderer spr = tutBox.GetComponent<SpriteRenderer>();
        spr.sprite = tut1;
        convBox.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (!isTut)
            BombTutorial();
        else Routine();
        if (Input.GetMouseButtonDown(0))
        {
            if (isFail)
            {
                this.gameObject.GetComponent<GameManager>().MoveScene(5);
            }
            else
            {
                Collider2D Click_Coll = this.gameObject.GetComponent<GameManager>().ClickTarget();
                Debug.Log("Clicked" + Click_Coll.gameObject.tag);
                Check(Click_Coll);
            }
        }
        Success();

        
    }

    void Routine()
    {
        Invoke("OnB", 2f);
        Invoke("OffB", 3.5f);
        Invoke("BCheck", 3.5f);
        Invoke("OnR", 4f);
        Invoke("OffR", 5.5f);
        Invoke("RCheck", 5.5f);
        Invoke("OnY", 6f);
    }

    public void BombTutorial()
    {
        int cur = this.gameObject.GetComponent<GameManager>().CurrentScene();
        if (cur == 5) //좌석 폭탄
        {
            SpriteRenderer spr = tutBox.GetComponent<SpriteRenderer>();

            if (Input.GetMouseButtonDown(0))
            {
                if (spr.sprite == tut1)
                    spr.sprite = tut2;
                else if (spr.sprite == tut2)
                {
                    tutBox.SetActive(false);
                    isTut = true;
                }
            }
        }

    }

    void BCheck()
    {
        if (!bSuc) Fail();
    }

    void RCheck()
    {
        if (!rSuc) Fail();
    }

    void Check(Collider2D Click_Coll) //올바르게 클릭했는지
    {
        if (isB)
        {
            if (Click_Coll.gameObject.CompareTag("B"))
            {
                bSuc = true;
                SpriteRenderer spr = lineB.GetComponent<SpriteRenderer>();
                spr.sprite = bombSuc;
            }
            else Fail();
        }
        else if(isR)
        {
            if (Click_Coll.gameObject.CompareTag("R"))
            {
                rSuc = true;
                SpriteRenderer spr = lineR.GetComponent<SpriteRenderer>();
                spr.sprite = bombSuc;
            }
            else Fail();
        }
        else if (isY)
        {
            if (Click_Coll.gameObject.CompareTag("Y"))
            {
                ySuc = true;
                SpriteRenderer spr = lineY.GetComponent<SpriteRenderer>();
                spr.sprite = bombSuc;
            }
            else Fail();
        }
    }

    void Fail()
    {
        isFail = true;
        Time.timeScale = 0f;
        this.gameObject.GetComponent<ClickController>().StartConv("Fail - 클릭하여 재도전");
    }

    void Success()
    {
        if (bSuc&&ySuc&&rSuc)
        {
            this.gameObject.GetComponent<ClickController>().StartConv("Success");
            ClickController.isBomb = true;
            Invoke("BackTo", 2f);
        }
    }

    //이부분 bool 이용해서 합칠 수 있나 생각좀 해보셈

    void OnB()
    {
        isB = true;
        SpriteRenderer spr = lightB.GetComponent<SpriteRenderer>();
        spr.sprite = null;
    }

    void OffB()
    {
        isB = false;
        SpriteRenderer spr = lightB.GetComponent<SpriteRenderer>();
        spr.sprite = lightOff;
    }

    void OnR()
    {
        isR = true;
        SpriteRenderer spr = lightR.GetComponent<SpriteRenderer>();
        spr.sprite = null;
    }

    void OffR()
    {
        isR = false;
        SpriteRenderer spr = lightR.GetComponent<SpriteRenderer>();
        spr.sprite = lightOff;
    }

    void OnY()
    {
        isY = true;
        SpriteRenderer spr = lightY.GetComponent<SpriteRenderer>();
        spr.sprite = null;
    }

    void OffY()
    {
        isY = false;
        SpriteRenderer spr = lightY.GetComponent<SpriteRenderer>();
        spr.sprite = lightOff;
    }

    void BackTo()
    {
        this.gameObject.GetComponent<GameManager>().MoveScene(3);
    }
}
