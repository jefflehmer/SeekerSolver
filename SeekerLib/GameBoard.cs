using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeekerLib
{
    public class GameBoard
    {
        #region fields, properties, constants, and enums
        protected enum PillarType
        {
            One = 6,
            Two = 7
        }
        #endregion fields, properties, constants, andenums

        public GameBoard()
        {
            CreatePillars().ConnectPillars();
        }

        private GameBoard CreatePillars()
        {
            int i = 0;
            _pillars = new List<Pillar>
            {
                 GeneratePillar(PillarType.One, i++)
                ,GeneratePillar(PillarType.Two, i++)
                ,GeneratePillar(PillarType.One, i++)
                ,GeneratePillar(PillarType.Two, i++)
                ,GeneratePillar(PillarType.One, i++)
                ,GeneratePillar(PillarType.Two, i++)
                ,GeneratePillar(PillarType.One, i++)
                ,GeneratePillar(PillarType.Two, i++)
                ,GeneratePillar(PillarType.One, i)
            };
            return this;
        }

        private GameBoard ConnectPillars()
        {
            foreach (var pillar in Pillars)
            {
                foreach (var spellCell in pillar.SpellCells)
                {
                    ConnectSpellCell(spellCell, Pillars);
                }
            }

            return this;
        }

        // not the most elegant way of connecting the network. does anyone want to suggest a cleaner way?
        private void ConnectSpellCell(SpellCell spellCell, List<Pillar> pillars)
        {
            var pillar = spellCell.Pillar;
            var offset = pillar.Offset;
            var idx = spellCell.CellIdx;

            // the indices start above and go clockwise
            /*************************** top ***************************/
            if (idx > 0) // if not at the top of the pillar already
                spellCell.Cells[0] = pillar.SpellCells[idx-1];

            /*************************** right ***************************/
            if (offset%2 == 0) // even numbered pillar
            {
                if (offset < 8) // looking for pillar not on the right edge (8 is last pillar starting at zero)
                {
                    var nextPillar = Pillars[offset + 1];
                    spellCell.Cells[1] = nextPillar.SpellCells[idx];
                    spellCell.Cells[2] = nextPillar.SpellCells[idx + 1];
                }
            }
            else // odd numbered pillar
            {
                var nextPillar = Pillars[offset + 1];
                if (idx > 0)
                    spellCell.Cells[1] = nextPillar.SpellCells[idx - 1];
                if (idx < nextPillar.SpellCells.Count)
                    spellCell.Cells[2] = nextPillar.SpellCells[idx];
            }

            /*************************** bottom ***************************/
            if (idx < pillar.SpellCells.Count-1) // if not at the bottom of the pillar
                spellCell.Cells[3] = pillar.SpellCells[idx+1];

            /*************************** left ***************************/
            if (offset%2 == 0) // even numbered pillar
            {
                if (offset > 0) // looking for pillar not on the left edge
                {
                    var lastPillar = Pillars[offset - 1];
                    spellCell.Cells[4] = lastPillar.SpellCells[idx + 1];
                    spellCell.Cells[5] = lastPillar.SpellCells[idx];
                }
            }
            else // odd numbered pillar
            {
                var lastPillar = Pillars[offset - 1];
                if (idx < lastPillar.SpellCells.Count)
                    spellCell.Cells[4] = lastPillar.SpellCells[idx];
                if (idx > 0)
                    spellCell.Cells[5] = lastPillar.SpellCells[idx - 1];
            }

        }

        #region pillars
        private List<Pillar> _pillars = null;
        public List<Pillar> Pillars { get { return _pillars; } set { _pillars = value; } }

        private Pillar GeneratePillar(PillarType pillarType, int offset)
        {
            SpellCell.Counter = 0;
            return new Pillar(offset, (int)pillarType);
        }
        #endregion pillars
    }
}
