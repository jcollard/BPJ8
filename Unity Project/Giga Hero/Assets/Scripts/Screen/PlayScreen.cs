using Assets.Scripts;
using Assets.Scripts.Screen;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScreen : MonoBehaviour, IScreen
{
    public Animator Animator;
    public GigaHero engine;

    public void Activate(GigaHero engine)
    {
        throw new NotImplementedException();
    }

    public Func<GigaHero, ActionResult> GetActionA()
    {
        throw new NotImplementedException();
    }

    public Func<GigaHero, ActionResult> GetActionB()
    {
        throw new NotImplementedException();
    }

    public Func<GigaHero, ActionResult> GetActionC()
    {
        throw new NotImplementedException();
    }

    public GameObject GetGameObject()
    {
        throw new NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        GigaHero.Screens.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        engine.GameState.Hero.HandleAnimator(Animator, engine.GameState);
    }
}
