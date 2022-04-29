using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack
{
    public class Deck
    {
        public List<Card> Cards { get; private set; } = new List<Card>();

        public Deck()
        {
            foreach (CardSuit s in (CardSuit[])Enum.GetValues(typeof(CardSuit)))
            {
                foreach (CardFace f in (CardFace[])Enum.GetValues(typeof(CardFace)))
                {
                    Cards.Add(new Card(f, s, Math.Min((int)f, 10)));
                }
            }
        }

        public void Shuffle()
        {
            Random r = new Random();

            int i = 0;

            List<Card> c = new List<Card>();

            while (Cards.Count > 0)
            {
                i = r.Next(0, Cards.Count - 1);
                c.Add(Cards[i]);
                Cards.RemoveAt(i);
            }

            Cards = c;
        }

        public Card Draw()
        {
            if (Cards.Count < 1) { return null; }

            Card c = Cards[0];

            Cards.RemoveAt(0);

            return c;
        }
    }
}
