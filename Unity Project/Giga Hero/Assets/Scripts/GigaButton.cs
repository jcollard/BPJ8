using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GigaButton : MonoBehaviour
{

    public Sprite Up;
    public Sprite Down;
    public ButtonAction Action { get; set; }
    public GigaHero Engine { get; set; }
    
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        this.spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        Action = new PokeButtonAction();
    }

    private void OnMouseDown()
    {
        this.spriteRenderer.sprite = Down;
    }

    private void OnMouseUp()
    {
        this.spriteRenderer.sprite = Up;
        Engine.HandleResult(Action.PerformAction(Engine.GameState));
    }
}
