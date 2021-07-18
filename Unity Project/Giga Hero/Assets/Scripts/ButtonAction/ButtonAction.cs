using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{

    public class ButtonAction
    {
        public static readonly Func<GigaHero, ActionResult> Poke = (engine) => engine.GameState.Poke();
        public static readonly Func<GigaHero, ActionResult> OpenActionMenu = ActionResult.Nothing((engine) => engine.ActionMenu.Activate(engine));
    }

}