using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject obstacle, obstacle2, obstacle3;
    public GameObject button, tutBox;

    public Sprite obsOn, obsOff, clear;

    private static bool isChecked, isTut;

    // Use this for initialization
    void Start()
    {
        button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTut)
            MapTutorial();
        else
            Mapping();
    }

    void MapTutorial() //맵 튜토리얼 설명
    {
        if (Input.GetMouseButtonDown(0))
        {
            tutBox.SetActive(false);
            isTut = true;
        }
    }

    void Mapping() //퀴즈 메소드
    {
        button.SetActive(true);
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D Click_Coll = this.gameObject.GetComponent<GameManager>().ClickTarget();
            if (Click_Coll.gameObject.CompareTag("obstacle"))
            {
                SpriteChange(Click_Coll);
            }
        }
    }

    void SpriteChange(Collider2D obj)
    {
        SpriteRenderer spr = obj.gameObject.GetComponent<SpriteRenderer>();
        if (spr.sprite == obsOn)
        {
            spr.sprite = obsOff;
            isChecked = false;
        }
        else if (spr.sprite == obsOff)
        {
            if (!isChecked)
            {
                spr.sprite = obsOn;
                isChecked = true;
            }
        }
    }

    void MapClear()
    {
        this.gameObject.GetComponent<ClickController>().StartConv("Success");
        ClickController.isMap = true;
        Invoke("BackTo", 2f);
    }

    public void DetButton()
    {
        SpriteRenderer spr = obstacle2.GetComponent<SpriteRenderer>();
        if (spr.sprite == obsOn)
        {
            MapClear();
            button.SetActive(false);
        }
        else
        {
            this.gameObject.GetComponent<ClickController>().StartConv("Fail");
        }
    }

    void BackTo()
    {
        this.gameObject.GetComponent<GameManager>().MoveScene(3);
    }
}