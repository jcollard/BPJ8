using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionHalfway : Transition
{
    private bool isTransitioned = false;

    public override void Update()
    {
        if (Time.time > endTime)
        {
            FinishTransition();
            return;
        }

        float percentage = (Time.time - startTime) / duration;
        if(percentage >= 0.5 && isTransitioned == false)
        {
            isTransitioned = true;
            _onFinish.Invoke(null);
        }
        transform.localScale = Vector3.Lerp(startScale, endScale, percentage);
    }

    public override void StartTransition(Action<object> onFinish)
    {
        isTransitioned = false;
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

    public override void FinishTransition()
    {
        FrontLayerAnimator.runtimeAnimatorController = NextController;
        this.gameObject.SetActive(false);
    }

}