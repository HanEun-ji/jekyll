using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConvManager : MonoBehaviour { //Prologue manager..
    public Text NameText;
    public Text WordText;
    public static ConvManager cvm;

    private static int currentTextNum;
    
    // Use this for initialization
    void Start () {
        currentTextNum = 0;
        NameText.text = null;
        WordText.text = "정신을 차렸더니 눈 앞에 놓여진 것은 W컴퍼니 대표 XXX의 사진들이다.";
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            currentTextNum++;
            Prologue(currentTextNum);
        }

	}

    /*string LoadTextFile(string fileName)
    {
        string st="";
        StreamReader sr = new StreamReader(Application.dataPath + "/Resources/" + fileName);
        if (sr == null)
        {
            print("Error : " + Application.dataPath + "/Resources/" + fileName);
        }
        for(int i=0; i<currentTextNum; i++)
        {
            st = sr.ReadLine();
        }
        if (st == null)
        {
            currentTextNum = 0;
            SceneManager.LoadScene(2);
        }
        return st;
    }*/

    void Prologue(int n)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (n == 1)
            {
                NameText.text = "???";
                WordText.text = "이번 타깃은 CEO XXX으로 정했나보군..";
            }
            if (n == 2)
            {
                NameText.text = null;
                WordText.text = "CEO XXX은 국내 최고의 화학 기업 W컴퍼니의 대표이다.\n그가 죽게 되면 국가적으로 큰 손실이 일어난다.";
            }
            if (n == 3)
                WordText.text = "나의 또다른 인격은 청부 살인업자로, 나는 줄곧 그가 하는 일을 막아왔다.";
            if (n == 4)
            {
                NameText.text = "???";
                WordText.text = "빨리 XXX의 차고로 가서 살인을 막아야겠군.";
            }
            if(n==5)
                SceneManager.LoadScene(2);
        }
    }

}
