using System;

namespace AV.Set.Model
{
    /// <summary>
    /// Represents an uniq card
    /// </summary>
    public class Card
    {
        public CardColor Color { get; set; }
        public Filling Filling { get; set; }
        public Shape Shape { get; set; }
        public byte ShapeCount { get; set; }

        public override bool Equals(object obj)
        {
            var right = (Card) obj;
            if (right != null)
            {
                return right.Color == Color && right.Filling == Filling && right.Shape == Shape &&
                       right.ShapeCount == ShapeCount;
            }
            return base.Equals(obj);
        }
    }
}
