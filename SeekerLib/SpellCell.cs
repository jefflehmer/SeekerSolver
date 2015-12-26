using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeekerLib
{
    public class SpellCell
    {
        static public int Counter = 0;
        public SpellCell()
        {
            ;
        }

        public SpellCell(Pillar pillar, int idx)
        {
            Spell = (10*pillar.Offset) + Counter++;
            Visited = false;
            Pillar = pillar;
            CellIdx = idx;

            Cells = new List<SpellCell>();
            for (int i = 0; i < 6; i++)
            {
                Cells.Add(new SpellCell());
            }
        }

        public int Spell { get; set; }
        public bool Visited { get; set; }
        public Pillar Pillar { get; set; }
        public int CellIdx { get; set; }

        public List<SpellCell> Cells { get; set; }
    }
}
