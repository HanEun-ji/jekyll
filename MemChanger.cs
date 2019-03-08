using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MemChanger : MonoBehaviour {
    public Text announce;
    public GameObject tutBox;

    private bool isFlying, isTut, isFail;
	// Use this for initialization
	void Start () {
        Time.timeScale = 1f;
        announce.text = "";
	}
	
	// Update is called once per frame
	void Update () {
        if (!isTut)
        {
            if (Input.GetMouseButtonDown(0))
            {
                tutBox.SetActive(false);
                isTut = true;
            }
        }
        else
        {
            Vector2 movement = new Vector2(10, 0);
            transform.Translate(movement * Time.deltaTime * 0.3f);

            if (Input.GetMouseButtonDown(0) && !isFlying)
            {
                isFlying = true;
                Vector2 jump = new Vector2(0, 3.5f);
                transform.Translate(jump);
            }
        }

        if (isFail)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(4);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("disturb"))
        {
            Fail();
        }

        if (other.gameObject.CompareTag("ground"))
        {
            isFlying = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("goal"))
        {
            announce.text = "Goal";
            Invoke("Goal", 1f);
        }
    }

    void Goal()
    {
        ClickController.isMem = true;
        SceneManager.LoadScene(3); //기억 떠올리는 씬으로 이동합시다
    }

    void Fail()
    {
        announce.text = "Fail - 클릭하여 재도전";
        isFail = true;
        Time.timeScale = 0.0f;
    }
}
