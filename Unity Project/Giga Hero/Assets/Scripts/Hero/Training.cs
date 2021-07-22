using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public enum TrainingOption
    {
        Weights, Jog, Study
    }
    public class Training
    {
        private static readonly IDictionary<TrainingOption, Training> LOOKUP = new Dictionary<TrainingOption, Training>();
        public static readonly Training Weights = new Training("Weights", TeenState.WEIGHTS);
        public static readonly Training Jog = new Training("Jog", TeenState.JOG);
        public static readonly Training Study = new Training("Study", TeenState.STUDY);
        
        static Training()
        {
            LOOKUP.Add(TrainingOption.Weights, Weights);
            LOOKUP.Add(TrainingOption.Jog, Jog);
            LOOKUP.Add(TrainingOption.Study, Study);
        }

        internal static Training Convert(TrainingOption trainOption)
        {
            return LOOKUP[trainOption];
        }

        private string _name;
        private TeenState _teenState;
        public TeenState TeenState { get { return _teenState; } }

        public Training(string name, TeenState state)
        {
            this._name = name;
            this._teenState = state;
        }


    }

}