using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{

    public float endTime, startTime;
    public float duration = 5.0F;
    public Vector3 endScale = new Vector3(1, 1, 1);
    public Vector3 startScale = new Vector3(0.25F, 0.25F, 0.25F);
    public Animator FrontLayerAnimator;
    public RuntimeAnimatorController NextController;
    public AudioSource AudioSource;
    protected Action<object> _onFinish;

    public void Start()
    {
        OptionsMenu.AddSFX(AudioSource);
    }

    public virtual void Update()
    {
        if (Time.time > endTime)
        {
            FinishTransition();
            return;
        }

        float percentage = (Time.time - startTime) / duration;
        transform.localScale = Vector3.Lerp(startScale, endScale, percentage);
    }

    public virtual void StartTransition(Action<object> onFinish)
    {
        startTime = Time.time;
        endTime = Time.time + duration;
        this.transform.position = new Vector3(0, 0, this.transform.position.z);
        this.gameObject.SetActive(true);
        this._onFinish = onFinish;
        if(AudioSource != null)
        {
            AudioSource.Play();
        }
    }

    public virtual void FinishTransition()
    {
        FrontLayerAnimator.runtimeAnimatorController = NextController;
        this.gameObject.SetActive(false);
        _onFinish.Invoke(null);
    }

}