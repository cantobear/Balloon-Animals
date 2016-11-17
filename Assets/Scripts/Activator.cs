using UnityEngine;
using System.Collections;

public class Activator : MonoBehaviour {

    SpriteRenderer sr;
    public KeyCode key;
    bool active = false;
    GameObject note, gm;
    Color old;
    public bool createMode;
    public GameObject n;
    
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        gm = GameObject.Find("GameManager");
        old = sr.color;
    }

    void Update () {

        if (Input.GetKeyDown(key))
            StartCoroutine(Pressed());

        if (Input.GetKeyDown(key) && active)
        {
            Destroy(note);
            AddScore();
            active = false;
        }
        else if (Input.GetKeyDown(key)&& !active)
        {
            if (PlayerPrefs.GetInt("Score") > 0)
                PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") - 1);
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        active = true;
        if (col.gameObject.tag == "Note")
            note = col.gameObject;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        active = false;
    }

    void AddScore()
    {
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + gm.GetComponent<ddrGameManager>().GetScore());
    }

    IEnumerator Pressed()
    {
        sr.color = new Color(255, 255, 255);
        yield return new WaitForSeconds(0.05f);
        sr.color = old;
    }

}
