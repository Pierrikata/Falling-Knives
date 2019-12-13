using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovePlayer : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer sr;
    private float speed = 3f;
    private float minX = -2.7f, maxX = 2.7f;

    public Text timerText;
    private int timer;

    // Start is called before the first frame update

    void Start() {
        Time.timeScale = 1f;
        StartCoroutine(CountTime());
        timer = 0;
    }

    void Awake() {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update() {
        Move();
        PlayerBounds();
    }

    void PlayerBounds() {
        Vector3 temp = transform.position;
        if (temp.x > maxX)
            temp.x = maxX;
        else if (temp.x < minX)
            temp.x = minX;
        transform.position = temp;
    }

    void Move() {
        float h = Input.GetAxisRaw("Horizontal");
        Vector3 temp = transform.position;       // Get current position

        if (h > 0) {
            temp.x += speed * Time.deltaTime; // Edit current position (go right)
            sr.flipX = false;
            anim.SetBool("Walk", true); // call animator component
        }
        else if (h < 0) {
            temp.x -= speed * Time.deltaTime; // Edit currenc position (go left)
            sr.flipX = true;
            anim.SetBool("Walk", true);
        }
        else { anim.SetBool("Walk", false); }

        transform.position = temp; // assign current position back to value
    }

    IEnumerator RestartGame() {
        yield return new WaitForSecondsRealtime(2f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    IEnumerable CountTime() {
        yield return new WaitForSeconds(1f);
        timer++;

        timerText.text = "Timer: " + timer;

        StartCoroutine(CountTime());
    }

    private void StartCoroutine(IEnumerable enumerable)
    {
        throw new NotImplementedException();
    }

    void OnTriggerEnter2D(Collider2D target) {
        if (target.tag == "Knife") {
            Time.timeScale = 0f;
            StartCoroutine(RestartGame());
        }
    }

} // class
