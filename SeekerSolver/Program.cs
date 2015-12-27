using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeekerLib;
using SeekerLib.enumerators;

namespace SeekerSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new GameBoard();
            var columnar1 = new ColumnarEnumerator {GameBoard = board};
            foreach (var cell in columnar1)
            {
                cell.Spell = int.Parse(Console.ReadLine());
            }
            Console.Write("Push Enter to see the feedback:");
            Console.ReadLine();

            //columnar.GetEnumerator().Reset();
            var columnar2 = new ColumnarEnumerator {GameBoard = board};
            foreach (var cell in columnar2)
            {
                //Console.WriteLine("{0},{1}", cell.Pillar.Offset, cell.CellIdx);
                Console.WriteLine("{0}", cell.Spell);
            }
            Console.Write("Done!");
            var xyz = Console.ReadLine();// string.Empty;
        }
    }
}
