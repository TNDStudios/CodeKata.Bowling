﻿using System;
using System.Collections.Generic;

namespace Core
{
    public class BowlingGame
    {
        public Player[] Players = new Player[2];

        public BowlingGame()
        {
        }

        public BowlingGame(string player1Symbols, string player2Symbols)
        {
            Players[0] = new Player(player1Symbols);
            Players[1] = new Player(player2Symbols);
        }
    }

    public class Player
    {
        public List<Turn> Turns = new List<Turn>();

        public int TotalScore
        {
            get
            {
                List<KeyValuePair<int, int>> pins = new List<KeyValuePair<int, int>>();

                for (int turnId = 0; turnId < Turns.Count; turnId ++)
                {
                    Turn turn = Turns[turnId];
                    int position = turnId + 1;

                    if (turn.Try1 == 10)
                    {
                        pins.Add(new KeyValuePair<int, int>(position, turn.Try1));
                    }
                    else
                    {
                        pins.Add(new KeyValuePair<int, int>(position, turn.Try1));
                        pins.Add(new KeyValuePair<int, int>(position, turn.Try2));
                    }

                    if (turn.Bonus != 0)
                    {
                        pins.Add(new KeyValuePair<int, int>(position, turn.Bonus));
                    }
                }

                int result = 0;

                for (int position = 0; position < pins.Count; position++)
                {
                    int turnId = pins[position].Key;
                    int pinScore = pins[position].Value;

                    result += pinScore;

                    // Last game?
                    if (turnId == 10)
                    {
                    }
                    else
                    {
                        // Strike? Add next two balls
                        if (pinScore == 10)
                        {
                            if (position < pins.Count - 1)
                                result += pins[position + 1].Value;

                            if (position < pins.Count - 2)
                                result += pins[position + 2].Value;
                        }
                    }
                }

                return result;
            }
        }

        public Player(string symbols)
        {
            string[] split = symbols.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (string symbol in split)
            {
                Turns.Add(new Turn(symbol));
            }
        }
    }

    public class Turn
    {
        // Pins knocked down in each (try)
        public int Try1 { get; set; } = 0;
        public int Try2 { get; set; } = 0;
        public int Bonus { get; set; } = 0;

        public bool Stike
        {
            get => (Try1 == 10);
        }

        public Turn()
        {

        }

        public Turn(string symbol)
        {
            symbol = symbol.ToLower();
            if (symbol == "x")
            {
                Try1 = 10;
                Try2 = 0;
            }
            else if (symbol.EndsWith("-"))
            {
                int.TryParse(symbol.Replace("-", ""), out int try1Check);
                Try1 = try1Check;
                Try2 = 0;
            }
            else if (symbol.StartsWith("-"))
            {
                Try1 = 0;
                int.TryParse(symbol.Replace("-", ""), out int try2Check);
                Try2 = try2Check;
            }
            else if (symbol.Contains("/"))
            {
                string[] parts = symbol.Split('/', StringSplitOptions.RemoveEmptyEntries);
                Try1 = (parts.Length != 0) ? int.Parse(parts[0].ToString()) : 0;
                Try2 = 10 - Try1;
                if (parts.Length == 2)
                    Bonus = int.Parse(parts[1]);
            }
        }
    }
}
