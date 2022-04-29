using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlackJack;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack.Tests
{
    [TestClass()]
    public class HandTests
    {
        [TestMethod()]
        public void AddCardTest()
        {
            Deck d = new Deck();
            d.Cards[0] = new Card(CardFace.Ace, CardSuit.Clubs, 1);
            d.Cards[1] = new Card(CardFace.N8, CardSuit.Clubs, 8);
            d.Cards[2] = new Card(CardFace.N10, CardSuit.Clubs, 10);

            Hand h = new Hand();

            h.AddCard(d);
            h.AddCard(d);

            Assert.AreEqual(19, h.Score);

            h.AddCard(d);

            Assert.AreEqual(19, h.Score);
        }
    }
}