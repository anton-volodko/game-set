using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AV.Set.Model
{
    /// <summary>
    /// Represent a current game
    /// </summary>
    public class Game
    {
        public Game()
        {
            Players = new Collection<Player>();
            CardStack = CreateCardStack();
            CardBoard = new CardBoard();
            // move 9 cards to the board
            for(int cardIndex = 0; cardIndex < 9; cardIndex++)
            {
                CardBoard.Cards.Add(CardStack.Pop());
            }
        }

        public DateTime StartTime { get; set; }
        public GameState State { get; set; }
        public CardStack CardStack { get; private set; }
        public CardBoard CardBoard { get; private set; }
        public ICollection<Player> Players { get; private set; }

        private CardStack CreateCardStack()
        {
            // generate list of cards
            var cards = new LinkedList<Card>();
            foreach (var color in Enum.GetValues(typeof(CardColor)).OfType<CardColor>())
                foreach (var filling in Enum.GetValues(typeof(Filling)).OfType<Filling>())
                    foreach (var shape in Enum.GetValues(typeof(Shape)).OfType<Shape>())
                        for (byte shapeIndex = 0; shapeIndex < 3; shapeIndex++)
                                cards.AddLast(new Card()
                                           {
                                               Color = color,
                                               Filling = filling,
                                               Shape = shape,
                                               ShapeCount = (byte) (shapeIndex + 1)
                                           });

            // shake cards
            var shakedCards = new CardStack();
            Random rnd = new Random(23);
            for (int cardsIndex = cards.Count - 1; cardsIndex >= 0; cardsIndex--)
            {
                var cardIndexToMove = (int)Math.Floor(cardsIndex * rnd.NextDouble());
                var cardToMove = cards.Skip(cardIndexToMove).Take(1).Single();
                shakedCards.Push(cardToMove);
                cards.Remove(cardToMove);
            }
            return shakedCards;
        }
    }
}
