using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeekerLib
{
    public class Pillar
    {
        public int Offset { get; set; }
        public List<SpellCell> SpellCells { get; set; }
        public Pillar(int offset, int numCells)
        {
            Offset = offset;
            SpellCells = new List<SpellCell>();
            for (int idx = 0; idx < numCells; idx++)
            {
                var cell = new SpellCell(this, idx);
                SpellCells.Add(cell);
            }
        }
    }
}
