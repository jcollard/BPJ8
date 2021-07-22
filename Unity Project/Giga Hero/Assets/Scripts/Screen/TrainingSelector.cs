using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class TrainingSelector : Selector
    {

        public TrainingOption TrainingType = TrainingOption.Jog;

        public override ActionResult Select(GigaHero engine)
        {
            
            engine.GameState.Hero.Train(Training.Convert(TrainingType));
            engine.SetActiveScreen(engine.GameState);
            return ActionResult.NOTHING;
        }
    }
}