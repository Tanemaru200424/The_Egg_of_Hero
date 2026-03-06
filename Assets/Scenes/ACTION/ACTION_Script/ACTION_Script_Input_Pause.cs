using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class ACTION_Script_Input_Pause : MonoBehaviour
{
    private AudioSource sesource = null;
    private AudioSource bgmsource = null;
    [SerializeField] private AudioClip clip;

    private bool ispause = false;

    void Start()
    {
        sesource = GameManager.instance.sesource;
        bgmsource = GameManager.instance.bgmsource;
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!ispause)
            {
                ispause = true;
                Time.timeScale = 0;
                bgmsource.Pause();
                sesource.PlayOneShot(clip);
            }
            else
            {
                ispause = false;
                Time.timeScale = 1;
                bgmsource.Play();
                sesource.PlayOneShot(clip);
            }
        }
    }
}
