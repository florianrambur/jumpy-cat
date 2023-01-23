using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public EnumManager.GameState state;
    public static GameManager instance;

    [Header("Animations")]
    public Animator cameraAnimator;
    public Animator playerAnimator;

    [Header("Stats")]
    [SerializeField] private int health = 3;
    [SerializeField] private float score;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SoundManager.ToggleSoundActivation();
        }

        if (state == EnumManager.GameState.PENDING && Input.GetKeyDown(KeyCode.Space))
        {
            state = EnumManager.GameState.START;

            GetComponent<UI>().CloseWaitingPanel();
        }

        if (state == EnumManager.GameState.START)
        {
            SoundManager.StartMainSong();

            score += 12f * Time.deltaTime;

            GetComponent<UI>().UpdateScoreText((int) score);

            if (health == 0)
            {
                Lost();
            }
        }

        if (state == EnumManager.GameState.LOST)
        {
            GetComponent<UI>().OpenEndingPanel((int) score);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
     
    public void TakeDamage()
    {
        health -= 1;

        SoundManager.StartTakeDamageSFX();

        cameraAnimator.SetTrigger("Shake");
        playerAnimator.SetTrigger("TakeDamage");
    }

    public void Lost()
    {
        state = EnumManager.GameState.LOST;

        playerAnimator.SetBool("IsDead", true);

        SoundManager.StartDefeatSong();

        PlayerPrefs.SetInt("highscore", (int) score);
    }

    public int GetHealth()
    {
        return health;
    }
}
