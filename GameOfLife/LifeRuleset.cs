using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GameOfLife
{
    public class LifeRuleset
    {
        int[] neighborsToGrow;
        int[] neighborsToLive;
        int[] neighborsToDie;

        // Initializes the ruleset
        // givenGrowthArray holds the number of neighbors that leads to a new live cell
        // givenLiveArray holds the number of neighbors that keep the cell the same
        // givenDeathArray holds the number of neighbors that leads to cell death
        // Throws an ArgumentException if either array shares values with another array, or if there are missing digits (0-9) in the sum of all three arrays
        public LifeRuleset(int[] givenGrowthArray, int[] givenLiveArray, int[] givenDeathArray)
        {
            if (DoArraysIntersect(givenGrowthArray, givenLiveArray, givenDeathArray)) // if there is a shared value, the ruleset is invalid. throw exception
            {
                throw new ArgumentException("The rulesets are invalid: Shared values");
            }
            else if (AreThereAnyMissingDigits(givenGrowthArray, givenLiveArray, givenDeathArray))
            {
                throw new ArgumentException("The rulesets are invalid: Missing digits");
            }
            else
            {
                neighborsToGrow = givenGrowthArray;
                neighborsToLive = givenLiveArray;
                neighborsToDie = givenDeathArray;
            }
        }

        // Compares three int arrays
        // If they share any values, true is returned
        // Otherwise, false is returned
        private bool DoArraysIntersect(int[] first, int[] second, int[] third)
        {
            // at least one value is shared between the three arrays, return true
            if (first.Intersect(second).Count() == 0 || first.Intersect(third).Count() == 0 || second.Intersect(third).Count() == 0)
            {
                return true;
            }

            return false; // no intersection, return false
        }

        // Compares three int arrays
        // If there are any missing digits (0-9) return true
        // If there are no missing digits return false
        private bool AreThereAnyMissingDigits(int[] first, int[] second, int[] third)
        {
            List<int> digitsLeft = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // if an element of digitsLeft is in any of the parameter int arrays, remove that element from digitsLeft
            foreach (int element in digitsLeft)
            {
                if (first.Contains(element) || second.Contains(element) || third.Contains(element))
                {
                    digitsLeft.Remove(element);
                }
            }

            // if there are no elements in digitsLeft remaining, return false. otherwise, there are missing digits, so return true
            return (digitsLeft.Count != 0);
        }

    }
}
