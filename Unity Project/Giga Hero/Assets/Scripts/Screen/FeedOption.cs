using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Screen
{
    public class FeedOption : ItemOption
    {

        public FoodOption FoodOption;

        public override ActionResult Select(GigaHero engine)
        {
            base.Select(engine);
            return engine.GameState.Hero.Feed(Food.Convert(FoodOption));
            
        }

    }
}