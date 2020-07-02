using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;

namespace GameOfLife
{
    public class LifeRuleset
    {
        readonly int[] NeighborsToGrow;
        readonly int[] NeighborsToLive;
        readonly int[] NeighborsToDie;

		readonly static int[] REQUIRED_DIGITS = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

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
            else if (AreThereAnyMissingDigits(givenGrowthArray, givenLiveArray, givenDeathArray)) // if there are any missing digits (0-9), the ruleset is invalid. throw exception
            {
                throw new ArgumentException("The rulesets are invalid: Missing digits");
            }
            else // the ruleset is valid, initialize variables
            {
                NeighborsToGrow = givenGrowthArray;
                NeighborsToLive = givenLiveArray;
                NeighborsToDie = givenDeathArray;
            }
        }

        // Returns an int[] of the digits of neighbors that lead to a cell coming alive
        public int[] GetGrowthArray()
        {
            return (int[])NeighborsToGrow.Clone();
        }

        // Returns an int[] of the digits of neighbors that lead to a cell remaining alive (or dead)
        public int[] GetLivingArray()
        {
            return (int[])NeighborsToLive.Clone();
        }

        // Returns an int[] of the digits of neighbors that lead to a cell dying (or remaining dead)
        public int[] GetDeathArray()
        {
            return (int[])NeighborsToDie.Clone();
        }

        // Compares three int arrays
        // If they share any values, true is returned
        // Otherwise, false is returned
        public static bool DoArraysIntersect(int[] first, int[] second, int[] third)
        {
            // return true if at least one value shared between arrays
            return first.Intersect(second).Count() != 0 || first.Intersect(third).Count() != 0 || second.Intersect(third).Count() != 0;
        }

        // Compares three int arrays
        // If there are any missing digits (0-9) return true
        // If there are no missing digits return false
        public static bool AreThereAnyMissingDigits(int[] first, int[] second, int[] third)
        {
            List<int> digits = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            List<int> digitsLeft = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

            // if an element of digitsLeft is in any of the parameter int arrays, remove that element from digitsLeft
            foreach (int element in digits)
            {
                if (first.Contains(element) || second.Contains(element) || third.Contains(element))
                {
                    digitsLeft.Remove(element);
                }
            }

            // if there are no elements in digitsLeft remaining, return false. otherwise, there are missing digits, so return true
            return (digitsLeft.Count != 0);
        }

        // Returns true if the HashSet values is missing digits that are required to be in a LifeRuleset,
        // Returns false if this aspect is valid
        // Done by checking that each item in the REQUIRED_DIGITS array is in the values HashSet
        public static bool AreThereAnyMissingDigitsSingleSet(HashSet<int> values)
		{
            foreach (int item in REQUIRED_DIGITS)
			{
                if (!values.Contains(item))
				{
                    return true;
				}
			}
            return false;
		}
    }
}
