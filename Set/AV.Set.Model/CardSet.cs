using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AV.Set.Model
{
    /// <summary>
    /// Contains a combination of 3 cards which are follow special rules to be a set.
    /// </summary>
    public class CardSet
    {
        public virtual Card FirstCard { get; set; }
        public virtual Card SecondCard { get; set; }
        public virtual Card ThirdCard { get; set; }
    }
}
