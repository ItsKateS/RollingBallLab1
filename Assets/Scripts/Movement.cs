using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public float speed;
    public float jumpForce = 5f;

    private bool isGrounded;

    public int objects;

    private Rigidbody rb;
    private int collects;

    public TMP_Text Score;
    public TMP_Text Victory;
    public TMP_Text Timer;
    public TMP_Text CountDown;

    private float timeSpent;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collects = 0;

        Victory.enabled = false;
        SetText();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        timeSpent += Time.deltaTime; 
        int minutes = Mathf.FloorToInt(timeSpent / 60);
        int seconds = Mathf.FloorToInt(timeSpent % 60);
        Timer.text = "Time: " + string.Format("{0:0}:{1:00}", minutes, seconds);

        if (transform.position.y < -10) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void FixedUpdate()
    {
        float moveHor = Input.GetAxis("Horizontal");
        float moveVer = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3 (moveHor, 0.0f, moveVer);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            collects++;
            SetText();
        }
    }

    private void SetText()
    {
        Score.text = "Score: " + collects.ToString();

        if (collects >= objects)
        {
            Victory.enabled = true;
            StartCoroutine(LoadNextLevel());
        }
    }

    private IEnumerator LoadNextLevel()
    {
        for (int i = 5; i > 0; i--)
        {
            CountDown.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
        else
            SceneManager.LoadScene(0);
        
        Victory.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
