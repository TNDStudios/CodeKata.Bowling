﻿using System.Collections.Generic;

namespace Core
{
    public class BowlingGame
    {
        private Turn[] turns = new Turn[11]; // 10 turns + bonus depending on spare or strike in 10th

        public BowlingGame()
        {
        }
    }
    public class Turn
    {
        // Pins knocked down in each (try)
        public int Try1 { get; set; } = 0;
        public int Try2 { get; set; } = 0;

        public Turn()
        {

        }
    }
}