using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GigaButton : MonoBehaviour
{

    public Sprite Up;
    public Sprite Down;
    public AudioSource Click;
    public Func<GigaHero, ActionResult> Action { get; set; }
    public GigaHero Engine { get; set; }
    
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        OptionsMenu.AddSFX(Click);
        this.spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        this.spriteRenderer.sprite = Down;
    }

    private void OnMouseUp()
    {

        this.Click.Play();
        this.spriteRenderer.sprite = Up;
        Engine.HandleResult(Action.Invoke(Engine));
    }
}
