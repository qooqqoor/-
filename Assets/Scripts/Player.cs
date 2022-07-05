using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] float moveSpeed = 5f;
    GameObject currentFloor;
    [SerializeField] int hp;
    [SerializeField] GameObject HpBar;
    [SerializeField] Text scoreText;
    private Animator anim;
    public float score;
    float scoreTime;
    SpriteRenderer render;
    AudioSource deathSound;
    [SerializeField] GameObject replyButton;
    public bool special;
    Rigidbody2D specialBody;
    float specialTime;
    void Start()
    {
        hp = 10;
        score = 0;
        scoreTime = 0;
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        deathSound = GetComponent<AudioSource>();
        specialBody = GetComponent<Rigidbody2D>();
        special = false;
        specialTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(moveSpeed*Time.deltaTime,0,0);
            if (!special)
            {
                render.flipX = false;
                anim.SetBool("run",true);
            }
           
        }else if(Input.GetKey(KeyCode.A))
        {
            transform.Translate(-moveSpeed*Time.deltaTime,0,0);
            if (!special)
            {
                render.flipX = true;
                anim.SetBool("run",true);
            }
           
        }else if(Input.GetKey(KeyCode.W))
        {
            if (special)
            {
                transform.Translate(0,moveSpeed*Time.deltaTime,0);
            }
        }else if(Input.GetKey(KeyCode.S))
        {
            if (special)
            {
                transform.Translate(0,-moveSpeed*Time.deltaTime,0);
            }
        }else if(Input.GetKey(KeyCode.Z))
        {
            if (!special)
            {
                special = true;
                anim.SetBool("special",true);
                specialBody.gravityScale = 0; 
            }
           
        }
        else
        {
            anim.SetBool("run",false);
        }

        UpdateScore();
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Normal") && other.contacts[0].normal == new Vector2(0f, 1f))
        {
            currentFloor = other.gameObject;
            ModifyHp(1);
            other.gameObject.GetComponent<AudioSource>().Play();
        }else if (other.gameObject.CompareTag("Nails") && other.contacts[0].normal == new Vector2(0f, 1f))
        {
            currentFloor = other.gameObject;
            ModifyHp(-3);
            anim.SetTrigger("hurt");
            other.gameObject.GetComponent<AudioSource>().Play();
        }else if (other.gameObject.CompareTag("Ceiling"))
        {
            currentFloor.GetComponent<BoxCollider2D>().enabled = false;
            ModifyHp(-3);
            anim.SetTrigger("hurt");
            other.gameObject.GetComponent<AudioSource>().Play();
        }
        //throw new NotImplementedException();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DeathLine"))
        {
            Die();
        }
    }

    void ModifyHp(int num)
    {
        hp += num;
        if (hp > 10)
        {
            hp = 10;
        }else if (hp <= 0)
        {
            hp = 0;
            Die();
        }

        UpdateHpBar();
    }

    void UpdateHpBar()
    {
        for (int i = 0; i < HpBar.transform.childCount; i++)
        {
            if (hp > i)
            {
                HpBar.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                HpBar.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    void UpdateScore()
    {
        scoreTime += Time.deltaTime;
        if (scoreTime > 2f)
        {
            score++;
            scoreTime = 0f;
            scoreText.text = "地下" + score.ToString() + '層';

            if (special)
            {
                specialTime++;
                specialTimeOut();
            }
        }
    }

    void Die()
    {
        // deathSound.Play();
        // Time.timeScale = 0;
        // replyButton.SetActive(true);
    }

    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }

    void specialTimeOut()
    {
        if (specialTime >= 5)
        {
            special = false;
            specialTime = 0;
            anim.SetBool("special",false);
            specialBody.gravityScale = 1;
        }
    }
    
     
}
