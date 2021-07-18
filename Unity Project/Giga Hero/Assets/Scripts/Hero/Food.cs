using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public enum FoodOption
    {
        Chicken, Fish, Apple
    }
    public class Food
    {
        private static readonly IDictionary<FoodOption, Food> LOOKUP = new Dictionary<FoodOption, Food>();
        public static readonly Food Chicken = new Food("Chicken").Str(8).Dex(2).Int(0).Nutrition(3);
        public static readonly Food Fish = new Food("Fish").Str(2).Dex(5).Int(3).Nutrition(5);
        public static readonly Food Apple = new Food("Apple").Str(0).Dex(2).Int(3).Nutrition(6);
        
        static Food()
        {
            LOOKUP.Add(FoodOption.Chicken, Chicken);
            LOOKUP.Add(FoodOption.Fish, Fish);
            LOOKUP.Add(FoodOption.Apple, Apple);
        }

        internal static Food Convert(FoodOption foodOption)
        {
            return LOOKUP[foodOption];
        }

        private string _name;
        public string Name { get { return _name; } }
        private int _str = 0;
        public int STR { get { return _str;  } }
        private int _dex = 0;
        public int DEX { get { return _dex; } }
        private int _int = 0;
        public int INT { get { return _int; } }
        private int _nutrition = 0;
        public int NUTRITION {  get { return _nutrition;  } }
        public int SATURATION {  get { return _str + _dex + _int; } }

        public Food(string name)
        {
            this._name = name;
        }

        public Food Str(int str)
        {
            this._str = str;
            return this;
        }

        public Food Dex(int dex)
        {
            this._dex = dex;
            return this;
        }

        public Food Int(int intelligence)
        {
            this._int = intelligence;
            return this;
        }

        public Food Nutrition(int nutrition)
        {
            this._nutrition = nutrition;
            return this;
        }

    }

}