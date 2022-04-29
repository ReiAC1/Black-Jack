using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack
{
    public class Hand
    {
        public List<Card> Cards { get; set; } = new List<Card>();

        public int Score { get; private set; }

        public void AddCard(Deck d)
        {
            Score = 0;

            Card c = d.Draw();

            if (c == null) { return; }

            Cards.Add(c);

            int end = Cards.Count;

            int aceCount = 0;
            int[] aceLocations = new int[4];

            // Count total num of aces, and add score
            for(int i = 0; i < end; i++)
            {
                c = Cards[i];

                if (c.Face == CardFace.Ace)
                {
                    aceLocations[aceCount] = i;
                    aceCount++;
                    Score++;
                }
                else
                {
                    Score += c.Value;
                }
            }

            // loop through each ace and add 10 for each of them if possible.
            for (int i = 0; i < aceCount; i++)
            {
                if (Score + 10 < 21)
                {
                    Score += 10;
                    Cards[aceLocations[i]].Value = 11;
                }
                else
                    break;
            }
        }
    }
}
