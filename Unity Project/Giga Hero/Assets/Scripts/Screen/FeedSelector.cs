using Assets.Scripts.Screen;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class FeedSelector : Selector
    {

        public ItemMenu FoodMenu;

        public override ActionResult Select(GigaHero engine)
        {
            FoodMenu.Activate(engine);
            return ActionResult.NOTHING;
        }
    }
}