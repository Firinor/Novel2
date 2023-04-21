using FirMath;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.DNA
{
    public static class DNACodeGenerator
    {
        public static T[] NewCode<T>(int count) where T : Enum
        {
            var result = new T[count];
            Array values = Enum.GetValues(typeof(T));

            List<int> cards = GameMath.AFewCardsFromTheDeck(count, values.Length);

            for(int i = 0; i < cards.Count; i++)
            {
                result[i] = (T)values.GetValue(i);
            }

            return result;
        }
    }
}