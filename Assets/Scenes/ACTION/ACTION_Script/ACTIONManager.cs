using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;

public class ACTIONManager : MonoBehaviour
{
    public static ACTIONManager instance = null;

    [SerializeField] private Image fadeimage = null;

    private AudioSource sesource = null;
    private AudioSource bgmsource = null;

    [SerializeField] private AudioClip click;
    [SerializeField] private AudioClip gameover;
    [SerializeField] private AudioClip clear;
    [SerializeField] private AudioClip stagebgm;
    [SerializeField] private AudioClip bossbgm;

    [SerializeField] private GameObject returngamebutton = null;
    [SerializeField] private GameObject restartbutton = null;

    [SerializeField] private GameObject clearmessage = null;
    [SerializeField] private GameObject playerdatagroup = null;
    [SerializeField] private GameObject gameovergroup = null;
    [SerializeField] private GameObject pausegroup = null;

    [SerializeField] private GameObject player = null;
    [SerializeField] private Vector3 playerspornpos = new Vector3();
    [SerializeField] private Animator animator = null;
    [SerializeField] private PlayerInput playerinput = null;
    [SerializeField] private Rigidbody2D rb2d = null;
    private Vector2 movederection = new Vector2();
    private float xspeed, yspeed;
    [SerializeField] private float walkspeed = 0.0f;
    [SerializeField] private float spornspeed = 0.0f;

    private bool canpause = false;//ポーズ可能かどうか。
    public bool ispause = false;//ポーズ状態かどうか。
    private bool iscoroutine = false;//コルーチンを行っているかどうか。衝突防止。
    private bool isclear = false;//クリア状態かどうか。

    [SerializeField] private Slider hpbar = null;

    [SerializeField] private TMPro.TMP_Text zankitext = null;

    [SerializeField] private int maxhp = 0;
    public int hp = 0;
    public int zanki = 0;

    public int enemynum = 0;
    public int wavenum = 0;

    private void Awake()
    {
        //インスタンス化
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        sesource = GameManager.instance.sesource;
        bgmsource = GameManager.instance.bgmsource;

        clearmessage.SetActive(false);
        playerdatagroup.SetActive(false);
        gameovergroup.SetActive(false);
        pausegroup.SetActive(false);
        player.SetActive(false);

        hp = maxhp;
        hpbar.maxValue = maxhp;
        hpbar.value = hp;

        zankitext.text = "×" + zanki;

        enemynum = 0;
        wavenum = 0;

        fadeimage.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);

        if (GameManager.instance.enemieslists.Count == 0)
        {
            StartCoroutine("ToTITLEScene");
        }
        else
        {
            StartCoroutine("StartACTIONScene");
        }
    }

    void Update()
    {
        hpbar.value = hp;
        zankitext.text = "×" + zanki;

        if (ispause)
        {
            playerinput.actions.FindActionMap("Player").Disable();
        }
        else
        {
            playerinput.actions.FindActionMap("Player").Enable();
        }
    }

    void FixedUpdate()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk") ||
           animator.GetCurrentAnimatorStateInfo(0).IsName("SpecialAttack"))
        {
            if (movederection.x > 0)
            {
                player.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (movederection.x < 0)
            {
                player.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            xspeed = movederection.x * walkspeed;
            yspeed = movederection.y * walkspeed;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Sporn"))
        {
            if (movederection.x > 0)
            {
                player.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (movederection.x < 0)
            {
                player.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            xspeed = movederection.x * spornspeed;
            yspeed = movederection.y * spornspeed;
        }
        else
        {
            xspeed = 0.0f;
            yspeed = 0.0f;
        }
        rb2d.velocity = new Vector2(xspeed, yspeed);
    }

    //移動をおしっぱにしているとき
    public void OnWalk(InputAction.CallbackContext context)
    {
        //方向制御
        Vector2 derection = context.ReadValue<Vector2>();
        float Sin, Cos;
        if (derection.x == 0.0f && derection.y == 0.0f)
        {
            Sin = 0.0f;
            Cos = 0.0f;
        }
        else
        {
            Sin = derection.y / (float)System.Math.Sqrt(System.Math.Pow(derection.x, 2) + System.Math.Pow(derection.y, 2));
            Cos = derection.x / (float)System.Math.Sqrt(System.Math.Pow(derection.x, 2) + System.Math.Pow(derection.y, 2));
        }
        movederection = new Vector2(Cos, Sin);

        //アニメーション再生
        if (context.performed)
        {
            animator.SetBool("walk", true);
        }
        else if (context.canceled)
        {
            animator.SetBool("walk", false);
        }
    }

    public void OnNomalAttack(InputAction.CallbackContext context)
    {
        //アニメーション再生
        if (context.started &&
            (animator.GetCurrentAnimatorStateInfo(0).IsName("Stand") ||
             animator.GetCurrentAnimatorStateInfo(0).IsName("Walk")))
        {
            animator.SetTrigger("nomal");
        }
    }

    //強攻撃を押したとき
    public void OnStrongAttack(InputAction.CallbackContext context)
    {
        //アニメーション再生
        if (context.started &&
            (animator.GetCurrentAnimatorStateInfo(0).IsName("Stand") ||
             animator.GetCurrentAnimatorStateInfo(0).IsName("Walk")))
        {
            animator.SetTrigger("strong");
        }
    }

    //特殊攻撃を押したとき
    public void OnSpecialAttack(InputAction.CallbackContext context)
    {
        //アニメーション再生
        if (context.started &&
            (animator.GetCurrentAnimatorStateInfo(0).IsName("Stand") ||
             animator.GetCurrentAnimatorStateInfo(0).IsName("Walk")))
        {
            animator.SetTrigger("special");
        }
    }

    //カウンターを押したとき
    public void OnCounter(InputAction.CallbackContext context)
    {
        //アニメーション再生
        if (context.started &&
            (animator.GetCurrentAnimatorStateInfo(0).IsName("Stand") ||
             animator.GetCurrentAnimatorStateInfo(0).IsName("Walk")
             /*弱攻撃からカウンターへのキャンセル削除。アニメーションコントローラーの方も切っておく
             ||
             animator.GetCurrentAnimatorStateInfo(0).IsName("NomalAttack1") ||
             animator.GetCurrentAnimatorStateInfo(0).IsName("NomalAttack2") ||
             animator.GetCurrentAnimatorStateInfo(0).IsName("NomalAttack3")
             */))
        {
            animator.SetTrigger("counter");
        }
    }

    //ポーズにする
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started && canpause)
        {
            ispause = true;
            canpause = false;
            Time.timeScale = 0;
            bgmsource.Pause();
            pausegroup.SetActive(true);
            EventSystem.current.SetSelectedGameObject(returngamebutton);
            sesource.PlayOneShot(click);
        }
    }

    //ポーズ解除
    public void Returngame()
    {
        if (ispause)
        {
            ispause = false;
            canpause = true;
            Time.timeScale = 1;
            bgmsource.Play();
            pausegroup.SetActive(false);
        }
    }

    //プレイヤースポーン
    public void PlayerSporn()
    {
        player.transform.position = playerspornpos;
        player.SetActive(true);
        player.GetComponent<Animator>().Play("Sporn");
        hp = maxhp;
    }

    //ゲームオーバー
    public void GameOver()
    {
        if (!isclear && !iscoroutine)
        {
            ispause = true;
            canpause = false;
            Time.timeScale = 0;
            bgmsource.Pause();
            gameovergroup.SetActive(true);
            EventSystem.current.SetSelectedGameObject(restartbutton);
            
            sesource.PlayOneShot(gameover);
        }
    }

    public IEnumerator StartACTIONScene()
    {
        if (!iscoroutine)
        {
            iscoroutine = true;
            float alpha = 1.0f;//フェードイメージの最初の透明度
            fadeimage.color = new Color(0, 0, 0, alpha);
            while (alpha >= 0.0f)//フェードイメージが設定した濃さになるまで繰り返す
            {
                alpha -= 0.01f;
                fadeimage.color = new Color(0, 0, 0, alpha);
                yield return new WaitForSeconds(0.01f);
            }

            clearmessage.SetActive(false);
            playerdatagroup.SetActive(true);
            gameovergroup.SetActive(false);
            pausegroup.SetActive(false);

            bgmsource.clip = null;

            PlayerSporn();

            canpause = true;
            ispause = false;
            iscoroutine = false;
            isclear = false;

            yield return WaveStart();
        }
        else
        {
            yield return null;
        }
    }

    //ウェーブスタート
    public IEnumerator WaveStart()
    {
        yield return new WaitForSeconds(2.0f);
        wavenum ++;
        if (GameManager.instance.enemieslists[wavenum - 1].enemies.Count == 0)
        {
            yield return ToTITLEScene();
        }
        else
        {
            if (wavenum == GameManager.instance.enemieslists.Count)
            {
                bgmsource.clip = bossbgm;
                bgmsource.Play();
            }
            else if (wavenum == 1)
            {
                bgmsource.clip = stagebgm;
                bgmsource.Play();
            }
            yield return EnemySporn();
        }
    }

    //敵スポーン
    public IEnumerator EnemySporn()
    {
        enemynum = GameManager.instance.enemieslists[wavenum - 1].enemies.Count;
        for (int count = 0; count < enemynum; count++)
        {
            Instantiate(GameManager.instance.enemieslists[wavenum - 1].enemies[count].obj, GameManager.instance.enemieslists[wavenum - 1].enemies[count].pos, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
    }

    //ステージクリア
    public IEnumerator Clear()
    {
        if (!iscoroutine)
        {
            isclear = true;
            canpause = false;
            iscoroutine = true;

            bgmsource.Stop();
            yield return new WaitForSeconds(2.0f);
            sesource.PlayOneShot(clear);

            if (ispause)
            {
                ispause = false;
                Time.timeScale = 1;
            }

            clearmessage.SetActive(true);
            playerdatagroup.SetActive(false);
            gameovergroup.SetActive(false);
            pausegroup.SetActive(false);


            float alpha = 0.0f;//フェードイメージの最初の透明度
            fadeimage.color = new Color(0, 0, 0, alpha);
            yield return new WaitForSeconds(3.0f);
            while (alpha <= 1.0f)//フェードイメージが設定した濃さになるまで繰り返す
            {
                alpha += 0.01f;
                fadeimage.color = new Color(0, 0, 0, alpha);
                yield return new WaitForSeconds(0.01f);
            }
            bgmsource.Stop();
            SceneManager.LoadScene("TITLE");

            canpause = true;
            iscoroutine = false;
        }
        else
        {
            yield return null;
        }
    }

    //タイトルに遷移
    public IEnumerator ToTITLEScene()
    {
        if (!iscoroutine)
        {
            canpause = false;
            iscoroutine = true;
            if (ispause)
            {
                ispause = false;
                Time.timeScale = 1;
            }

            clearmessage.SetActive(false);
            playerdatagroup.SetActive(false);
            gameovergroup.SetActive(false);
            pausegroup.SetActive(false);

            float alpha = 0.0f;//フェードイメージの最初の透明度
            fadeimage.color = new Color(0, 0, 0, alpha);
            while (alpha <= 1.0f)//フェードイメージが設定した濃さになるまで繰り返す
            {
                alpha += 0.01f;
                fadeimage.color = new Color(0, 0, 0, alpha);
                yield return new WaitForSeconds(0.01f);
            }
            bgmsource.Stop();
            SceneManager.LoadScene("TITLE");

            canpause = true;
            iscoroutine = false;
        }
        else
        {
            yield return null;
        }
    }

    //リスタート（アクションに遷移）
    public IEnumerator ToACTIONScene()
    {
        if (!iscoroutine)
        {
            canpause = false;
            iscoroutine = true;
            if (ispause)
            {
                ispause = false;
                Time.timeScale = 1;
            }

            clearmessage.SetActive(false);
            playerdatagroup.SetActive(false);
            gameovergroup.SetActive(false);
            pausegroup.SetActive(false);

            float alpha = 0.0f;//フェードイメージの最初の透明度
            fadeimage.color = new Color(0, 0, 0, alpha);
            while (alpha <= 1.0f)//フェードイメージが設定した濃さになるまで繰り返す
            {
                alpha += 0.01f;
                fadeimage.color = new Color(0, 0, 0, alpha);
                yield return new WaitForSeconds(0.01f);
            }
            bgmsource.Stop();
            SceneManager.LoadScene("ACTION");

            canpause = true;
            iscoroutine = false;
        }
        else
        {
            yield return null;
        }
    }

}
