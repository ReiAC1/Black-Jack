using System;

namespace BlackJack
{
    public enum CardFace
    {
        Ace = 1,
        N2,
        N3,
        N4,
        N5,
        N6,
        N7,
        N8,
        N9,
        N10,
        Jack = 11,
        Queen = 12,
        King = 13
    }

    public enum CardSuit
    {
        Hearts,
        Diamonds,
        Spades,
        Clubs
    }

    public class Card
    {
        int _value = 0;

        public CardFace Face { get; private set; }
        public CardSuit Suit { get; private set; }

        public int Value
        {
            get
            {
                return _value;
            }

            set
            {
                if (value > 10 && Face != CardFace.Ace) { value = 10; }
                _value = value;
            }
        }

        public Card(CardFace f, CardSuit s, int v)
        {
            Face = f;
            Suit = s;
            Value = v;
        }
    }
}
