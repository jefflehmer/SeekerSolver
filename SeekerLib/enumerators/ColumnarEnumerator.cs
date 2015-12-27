using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeekerLib.enumerators
{
    // this iterator traverses each cell in each column.
    // it starts at the top of the first column on the left and moves down to the
    //    bottom cell before restarting at the top cell of the next column on the right.
    public class ColumnarEnumerator : IEnumerable<SpellCell>
    {
        // we are keeping the iterators separate from the GameBoard but we still need to "know" about the GameBoard.
        public GameBoard GameBoard { get; set; }

        public ColumnarEnumerator()
        {
        }

        public IEnumerator<SpellCell> GetEnumerator()
        {
            return new SpellCellEnumerator(GameBoard);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class SpellCellEnumerator : IEnumerator<SpellCell>
        {
            private GameBoard _gameBoard;
            private SpellCell _currentCell;

            public SpellCellEnumerator() { Reset(); }

            public SpellCellEnumerator(GameBoard gameBoard)
            {
                _gameBoard = gameBoard;
                Reset();
            }

            public SpellCell Current
            {
                get { return _currentCell; }
            }

            public void Dispose() { }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public bool MoveNext()
            {
                SpellCell nextCell = null;

                // if at the "reset" state then start at the top-most cell of the left-most pillar
                if (_currentCell == null)
                {
                    if ((_gameBoard != null) && 
                        (_gameBoard.Pillars != null && _gameBoard.Pillars.Count > 0) && 
                        (_gameBoard.Pillars[0].SpellCells != null && _gameBoard.Pillars[0].SpellCells.Count > 0))
                        nextCell = _gameBoard.Pillars[0].SpellCells[0];
                }
                else
                {
                    // if not at the last cell at the last pillar
                    if (_currentCell != _gameBoard.Pillars.Last().SpellCells.Last())
                    {
                        // if at bottom of a pillar move to the top of the next pillar
                        nextCell = (_currentCell == _currentCell.Pillar.SpellCells.Last()) 
                            ? _gameBoard.Pillars[_currentCell.Pillar.Offset + 1].SpellCells[0] 
                            : _currentCell.Cells[3];
                    }
                }

                if (nextCell != null)
                    _currentCell = nextCell;

                return (nextCell != null);
            }

            public void Reset()
            {
                _currentCell = null;
            }
        }
    }
}
