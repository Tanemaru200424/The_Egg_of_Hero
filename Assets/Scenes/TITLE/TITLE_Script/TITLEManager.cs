using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TITLEManager : MonoBehaviour
{
    public static TITLEManager instance = null;

    [SerializeField] private Image fadeimage = null;
    public GameObject firstsetbutton = null;
    public GameObject nowgroup = null;
    public bool canclose = false;

    [SerializeField] private AudioClip title = null;
    [SerializeField] private AudioClip close = null;
    private AudioSource bgmsource = null;
    private AudioSource sesource = null;

    [SerializeField] private GameObject menugroup = null;
    [SerializeField] private GameObject stageselectgroup = null;
    [SerializeField] private GameObject volumecontrolgroup = null;


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

    // Start is called before the first frame update
    void Start()
    {
        bgmsource = GameManager.instance.bgmsource;
        sesource = GameManager.instance.sesource;
        fadeimage.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);

        menugroup.SetActive(false);
        stageselectgroup.SetActive(false);
        volumecontrolgroup.SetActive(false);

        StartCoroutine("StartTITLEScene");
    }

    public void OnClose(InputAction.CallbackContext context)
    {
        if (context.started && canclose)
        {
            sesource.PlayOneShot(close);
            nowgroup.SetActive(false);
            menugroup.SetActive(true);
            canclose = false;
            EventSystem.current.SetSelectedGameObject(firstsetbutton);
        }
    }


    //タイトル開始
    public IEnumerator StartTITLEScene()
    {
        float alpha = 1.0f;//フェードイメージの最初の透明度
        fadeimage.color = new Color(0, 0, 0, alpha);
        while (alpha >= 0.0f)//フェードイメージが設定した濃さになるまで繰り返す
        {
            alpha -= 0.01f;
            fadeimage.color = new Color(0, 0, 0, alpha);
            yield return new WaitForSeconds(0.01f);
        }

        menugroup.SetActive(true);
        EventSystem.current.SetSelectedGameObject(firstsetbutton);

        bgmsource.clip = title;
        bgmsource.Play();
    }

    //アクションに遷移
    public IEnumerator ToACTIONScene()
    {
        menugroup.SetActive(false);
        stageselectgroup.SetActive(false);

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
    }

    public IEnumerator ExitGame()
    {
        menugroup.SetActive(false);

        float alpha = 0.0f;//フェードイメージの最初の透明度
        fadeimage.color = new Color(0, 0, 0, alpha);
        while (alpha <= 1.0f)//フェードイメージが設定した濃さになるまで繰り返す
        {
            alpha += 0.01f;
            fadeimage.color = new Color(0, 0, 0, alpha);
            yield return new WaitForSeconds(0.01f);
        }

        bgmsource.Stop();
        Application.Quit();//ゲームプレイ終了
    }
}
