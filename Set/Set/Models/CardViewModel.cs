using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AV.Web.Set.Models
{
    public class CardViewModel
    {
        public string Shape { get; set; }
        public byte ShapeCount { get; set; }
        public string Filling { get; set; }
        public string Color { get; set; }
        public IEnumerable<int> Coordinates { get; set; }
    }
}