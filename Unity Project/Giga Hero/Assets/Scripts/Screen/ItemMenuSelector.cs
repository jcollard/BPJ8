using Assets.Scripts.Screen;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class ItemMenuSelector : Selector
    {

        public ItemMenu ItemMenu;

        public override ActionResult Select(GigaHero engine)
        {
            ItemMenu.Activate(engine);
            return ActionResult.NOTHING;
        }
    }
}