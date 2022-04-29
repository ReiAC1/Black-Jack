using System;
using BlackJack;

namespace BlackJackPlayer
{
    class Program
    {

        public static void Play()
        {
            Console.Clear();

            string icons = "♥♦♠♣";

            Deck d = new Deck();
            d.Shuffle();

            Hand player = new Hand();
            Hand dealer = new Hand();

            Console.Write("Enter your name (max 6 chars): ");
            string name = Console.ReadLine().ToUpper();
            name = name.Length > 6 ? name.Substring(0, 6) : name;
            Console.WriteLine();

            bool showDealer = false;

            // round start
            player.AddCard(d);
            dealer.AddCard(d);
            player.AddCard(d);
            dealer.AddCard(d);

            while (true)
            { 

                Console.Clear();

                // Draw Dealer
                Console.SetCursorPosition(4, 0);

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("DEALER ");
                Console.ForegroundColor = ConsoleColor.White;

                if (player.Score == 21 || dealer.Score == 21)
                {
                    showDealer = true;
                }
                
                if (!showDealer && dealer.Cards.Count > 0)
                {
                    Console.Write("(XX) ");

                    Console.Write("{0}{1} ", icons[(int)dealer.Cards[0].Suit], (dealer.Cards[0].Value < 10) ? "" + dealer.Cards[0].Value + " " : "" + dealer.Cards[0].Value);
                    DrawCard(24, Console.CursorTop + 1, dealer.Cards[0]);
                    

                    for (int i = 1; i < dealer.Cards.Count; i++)
                    {
                        Console.Write("XX ");

                        int x = Console.CursorLeft;
                        int y = Console.CursorTop;

                        DrawCard(24 + (i * 13), Console.CursorTop + 1, null);

                        Console.SetCursorPosition(x, y);
                    }
                }
                else
                {
                    Console.Write("({0}) ", (dealer.Score < 10) ? " " + dealer.Score : "" + dealer.Score);

                    for (int i = 0; i < dealer.Cards.Count; i++)
                    {
                        Console.Write("{0}{1} ", icons[(int)dealer.Cards[i].Suit], (dealer.Cards[i].Value < 10) ? "" + dealer.Cards[i].Value + " " : "" + dealer.Cards[i].Value);

                        int x = Console.CursorLeft;
                        int y = Console.CursorTop;

                        DrawCard(24 + (i * 13), Console.CursorTop + 1, dealer.Cards[i]);

                        Console.SetCursorPosition(x, y);
                    }
                }

                DrawCard(24, 1, dealer.Cards[0]);

                // Draw Player
                Console.SetCursorPosition(4, 12);

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(name);
                Console.SetCursorPosition(11, 12);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("({0}) ", (player.Score < 10) ? " " + player.Score : "" + player.Score);

                for (int i = 0; i < player.Cards.Count; i++)
                {
                    Console.Write("{0}{1} ", icons[(int)player.Cards[i].Suit], (player.Cards[i].Value < 10) ? "" + player.Cards[i].Value + " " : "" + player.Cards[i].Value);

                    int x = Console.CursorLeft;
                    int y = Console.CursorTop;

                    DrawCard(24 + (i * 13), Console.CursorTop + 1, player.Cards[i]);
                }

                Console.SetCursorPosition(0, 24);

                if (showDealer)
                {

                    string wlt = "";

                    if ((player.Score > dealer.Score && player.Score <= 21) || (dealer.Score > 21 && player.Score <= 21))
                    {
                        wlt = "wins";
                    }
                    else if (player.Score > 21 || (dealer.Score > player.Score && dealer.Score <= 21))
                    {
                        wlt = "loses";
                    }
                    else
                    {
                        wlt = "ties";
                    }

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"Your hand ({player.Score})");

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($" {wlt} to the ");

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"dealer's hand ({dealer.Score})");

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Would you like to play another round? Y/N");

                    string ln = Console.ReadLine();

                    while (ln.ToLower() != "n" && ln.ToLower() != "y")
                    {
                        Console.Write("Please enter Y/N only: ");
                        ln = Console.ReadLine();
                    }

                    if (ln.ToLower() == "n")
                    {
                        break;
                    }

                    player.Cards.Clear();
                    dealer.Cards.Clear();

                    d = new Deck();
                    d.Shuffle();

                    player.AddCard(d);
                    dealer.AddCard(d);
                    player.AddCard(d);
                    dealer.AddCard(d);

                    showDealer = false;

                    continue;
                }

                Console.WriteLine("1. Hit");
                Console.WriteLine("2. Stand");

                int val = 0;

                while (true)
                {
                    if (!int.TryParse(Console.ReadLine(), out val))
                    {
                        Console.Write("Enter a valid option: ");
                    }
                    else if (val != 1 && val != 2)
                    {
                        Console.Write("Enter a valid option: ");
                    }
                    else
                    {
                        break;
                    }
                }

                if (val == 1)
                {
                    player.AddCard(d);

                    if (player.Score > 21)
                    {
                        showDealer = true;
                    }
                }
                else
                {
                    showDealer = true;
                    while (val == 2 && dealer.Score < 17)
                    {
                        dealer.AddCard(d);
                    }
                }

                if (dealer.Score < 17)
                {
                    dealer.AddCard(d);
                }

                Console.WriteLine("Press Enter to continue.");

                Console.ReadLine();
            }
        }

        public static void DrawPattern(int x, int y, int width, int height, int value, BlackJack.CardSuit s)
        {

            string chars = "♥♦♠♣";
            char c = chars[(int)s];


            if (value == 1)
            {
                Console.SetCursorPosition(x + (width / 2), y + (height / 2));
                Console.Write(c);
            }
            else if (value == 2)
            {
                Console.SetCursorPosition(x + 3, y + 1);
                Console.Write(c);
                Console.SetCursorPosition(x + width - 4, y + height - 2);
                Console.Write(c);
            }
            else if (value == 3)
            {
                Console.SetCursorPosition(x + 3, y + 1);
                Console.Write(c);
                Console.SetCursorPosition(x + width - 4, y + height - 2);
                Console.Write(c);
                Console.SetCursorPosition(x + (width / 2), y + (height / 2));
                Console.Write(c);
            }
            else if (value == 4)
            {
                Console.SetCursorPosition(x + 3, y + 1);
                Console.Write(c);
                Console.SetCursorPosition(x + width - 4, y + height - 2);
                Console.Write(c);
                Console.SetCursorPosition(x + width - 4, y + 1);
                Console.Write(c);
                Console.SetCursorPosition(x + 3, y + height - 2);
                Console.Write(c);
            }
            else if (value == 5)
            {
                Console.SetCursorPosition(x + 3, y + 1);
                Console.Write(c);
                Console.SetCursorPosition(x + width - 4, y + height - 2);
                Console.Write(c);
                Console.SetCursorPosition(x + width - 4, y + 1);
                Console.Write(c);
                Console.SetCursorPosition(x + 3, y + height - 2);
                Console.Write(c);
                Console.SetCursorPosition(x + (width / 2), y + (height / 2));
                Console.Write(c);
            }
            else if (value == 6)
            {
                Console.SetCursorPosition(x + 3, y + 1);
                Console.Write(c);
                Console.SetCursorPosition(x + width - 4, y + height - 2);
                Console.Write(c);
                Console.SetCursorPosition(x + width - 4, y + 1);
                Console.Write(c);
                Console.SetCursorPosition(x + 3, y + height - 2);
                Console.Write(c);
                Console.SetCursorPosition(x + 3, y + (height / 2));
                Console.Write(c);
                Console.SetCursorPosition(x + width - 4, y + (height / 2));
                Console.Write(c);
            }
            else if (value == 7)
            {
                Console.SetCursorPosition(x + 3, y + 1);
                Console.Write(c);
                Console.SetCursorPosition(x + width - 4, y + height - 2);
                Console.Write(c);
                Console.SetCursorPosition(x + width - 4, y + 1);
                Console.Write(c);
                Console.SetCursorPosition(x + 3, y + height - 2);
                Console.Write(c);
                Console.SetCursorPosition(x + 3, y + (height / 2));
                Console.Write(c);
                Console.SetCursorPosition(x + width - 4, y + (height / 2));
                Console.Write(c);
                Console.SetCursorPosition(x + (width / 2), y + (height / 2));
                Console.Write(c);
            }
            else if (value == 8)
            {
                Console.SetCursorPosition(x + 3, y + 1);
                Console.Write(c);
                Console.SetCursorPosition(x + 4, y + 2);
                Console.Write(c);
                Console.SetCursorPosition(x + width - 4, y + height - 2);
                Console.Write(c);
                Console.SetCursorPosition(x + width - 5, y + height - 3);
                Console.Write(c);
                Console.SetCursorPosition(x + width - 4, y + 1);
                Console.Write(c);
                Console.SetCursorPosition(x + width - 5, y + 2);
                Console.Write(c);
                Console.SetCursorPosition(x + 3, y + height - 2);
                Console.Write(c);
                Console.SetCursorPosition(x + 4, y + height - 3);
                Console.Write(c);
            }
            else if (value == 9)
            {
                Console.SetCursorPosition(x + 3, y + 1);
                Console.Write(c);
                Console.SetCursorPosition(x + 4, y + 2);
                Console.Write(c);
                Console.SetCursorPosition(x + width - 4, y + height - 2);
                Console.Write(c);
                Console.SetCursorPosition(x + width - 5, y + height - 3);
                Console.Write(c);
                Console.SetCursorPosition(x + width - 4, y + 1);
                Console.Write(c);
                Console.SetCursorPosition(x + width - 5, y + 2);
                Console.Write(c);
                Console.SetCursorPosition(x + 3, y + height - 2);
                Console.Write(c);
                Console.SetCursorPosition(x + 4, y + height - 3);
                Console.Write(c);
                Console.SetCursorPosition(x + (width / 2), y + (height / 2));
                Console.Write(c);
            }
            else if (value == 10)
            {
                Console.SetCursorPosition(x + 3, y + 1);
                Console.Write(c);
                Console.SetCursorPosition(x + 4, y + 2);
                Console.Write(c);
                Console.SetCursorPosition(x + width - 4, y + height - 2);
                Console.Write(c);
                Console.SetCursorPosition(x + width - 5, y + height - 3);
                Console.Write(c);
                Console.SetCursorPosition(x + width - 4, y + 1);
                Console.Write(c);
                Console.SetCursorPosition(x + width - 5, y + 2);
                Console.Write(c);
                Console.SetCursorPosition(x + 3, y + height - 2);
                Console.Write(c);
                Console.SetCursorPosition(x + 4, y + height - 3);
                Console.Write(c);
                Console.SetCursorPosition(x + 4, y + height / 2);
                Console.Write(c);
                Console.SetCursorPosition(x + width - 5, y + height / 2);
                Console.Write(c);
            }
        }

        public static void DrawCard(int x, int y, BlackJack.Card card)
        {
            int cx = Console.CursorLeft;
            int cy = Console.CursorTop;

            Console.BackgroundColor = ConsoleColor.White;

            int width = 11;
            int height = 7;

            // Set background
            for (int i = x; i < x + width; i++)
            {
                for (int j = y; j < y + height; j++)
                {
                    Console.SetCursorPosition(i, j);

                    Console.Write(" ");
                }
            }

            if (card == null) { Console.BackgroundColor = ConsoleColor.Black; return; }

            ConsoleColor tc = (int)card.Suit < 2 ? ConsoleColor.Red : ConsoleColor.Black;
            Console.ForegroundColor = tc;

            // Set numeral indicators
            Console.SetCursorPosition(x + 1, y);
            if ((card.Value > 1 && card.Value < 10) || card.Face == BlackJack.CardFace.N10)
            {
                Console.Write(card.Value);

                Console.SetCursorPosition(x + width - 3, y + height - 1);
                Console.Write(card.Value);

                DrawPattern(x, y, width, height, card.Value, card.Suit);
            }
            else
            {
                switch (card.Face)
                {
                    case BlackJack.CardFace.Ace:
                        Console.Write("A");
                        Console.SetCursorPosition(x + width - 3, y + height - 1);
                        Console.Write("A");
                        DrawPattern(x, y, width, height, 1, card.Suit);
                        break;
                    case BlackJack.CardFace.Jack:
                        Console.Write("J");
                        Console.SetCursorPosition(x + width - 3, y + height - 1);
                        Console.Write("J");
                        DrawPattern(x, y, width, height, 1, card.Suit);
                        break;
                    case BlackJack.CardFace.Queen:
                        Console.Write("Q");
                        Console.SetCursorPosition(x + width - 3, y + height - 1);
                        Console.Write("Q");
                        DrawPattern(x, y, width, height, 1, card.Suit);
                        break;
                    case BlackJack.CardFace.King:
                        Console.Write("K");
                        Console.SetCursorPosition(x + width - 3, y + height - 1);
                        Console.Write("K");
                        DrawPattern(x, y, width, height, 1, card.Suit);
                        break;
                }
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(cx, cy);
        }

        public static bool ShowMenu()
        {

            int val = 0;

            while (true)
            {
                Console.WriteLine("1. Play BlackJack");
                Console.WriteLine("2. Shuffle and Show Deck");
                Console.WriteLine("3. Exit");

                Console.Write("Choice: ");

                if (!int.TryParse(Console.ReadLine(), out val))
                {
                    Console.Write("Please Enter a number for your choice: ");
                }
                else if (val == 1)
                {
                    Play();
                }
                else if (val == 2)
                {
                    BlackJack.Deck d = new BlackJack.Deck();
                    d.Shuffle();
                    Console.WriteLine("Press enter to show next set of cards:");
                    int x = Console.CursorLeft, y = Console.CursorTop;

                    int count = 0;

                    foreach (BlackJack.Card c in d.Cards)
                    {
                        int perLine = Console.BufferWidth / 13;
                        int tempX = x + ((count % perLine) * 13);
                        int tempY = y + ((count / perLine) * 8);

                        count++;

                        DrawCard(tempX, tempY, c);
                    }

                    Console.ReadLine();
                }
                else if (val == 3)
                {
                    return false;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Please Enter a valid number from the menu: ");
                    continue;
                }

                Console.Clear();
            }
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            ShowMenu();
        }
    }
}
