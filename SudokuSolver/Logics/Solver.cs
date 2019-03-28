using SudokuSolver.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace SudokuSolver.Logics
{
    public class Solver
    {
        public int[][] Solve(int[][] sudoku)
        {
            int[] IntArray = ToIntArray(sudoku);
            int[] IntArrayCount = new int[81];
            for (int i1 = 0; i1 < IntArrayCount.Length; i1++)
            {
                if (IntArray[i1] > 0) {
                    IntArrayCount[i1] = -1;
                } else {
                    IntArrayCount[i1] = 0;
                }
                
            }
            int i = 0;
            int high = 0;
            while (i < IntArray.Length)
            {
                high = Math.Max(high, i);
                if (IntArrayCount[i] > -1) {
                    List<int> found = new List<int>();
                    found.AddRange(CheckY(i, IntArray));
                    found.AddRange(CheckX(i, IntArray));
                    found.AddRange(CheckGrid(i, IntArray));
                    found = RemoveNumbers(found, new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
                    //Debug.WriteLine("i="+i.ToString()+";");
                    //Debug.WriteLine("intcount = "+IntArrayCount[i].ToString());
                    //Debug.WriteLine("foundcount = " + found.Count.ToString());
                    if (found.Count == 0 || IntArrayCount[i] >= found.Count) {
                        IntArrayCount[i] = 0;
                        IntArray[i] = 0;
                        i--;
                        while (IntArrayCount[i] == -1)
                        {
                            i--;
                        }
                        IntArray[i] = 0;
                        IntArrayCount[i]++;
                    }
                    else
                    {
                        //Debug.WriteLine("found = " + found[IntArrayCount[i]].ToString());
                        IntArray[i] = found[IntArrayCount[i]];
                        i++;
                    }
                }
                else
                {
                    i++;
                }
            }

            return ToSudoku(IntArray);
        }

        public int[][] ToSudoku(int[] intArray) {
            int[][] sudoku = new int[9][];
            for (int i = 0; i < sudoku.Length; i++)
            {
                sudoku[i] = new int[9];
                for (int pos = 0; pos < 9; pos++)
                {
                    int x = i * 9 + pos;
                    sudoku[i][pos] = intArray[x];
                }
            }

            return sudoku;
        }

        public List<int> CheckX(int pos, int[] intArray)
        {
            List<int> found = new List<int>();
            for (int i = 0; i < 9; i++)
            {
                int x = pos - pos % 9 + i;
                found.Add(intArray[x]);
            }
            return found;
        }

        public List<int> RemoveNumbers(List<int> exclude, List<int> toExcludeFrom)
        {
            foreach (var item in exclude)
            {
                if (toExcludeFrom.FindIndex(x => x == item) > -1)
                {
                    toExcludeFrom.RemoveAt(toExcludeFrom.FindIndex(x => x == item));
                }
            }
            return toExcludeFrom;
        }

        public List<int> CheckY(int pos, int[] intArray)
        {
            List<int> found = new List<int>();
            for (int i = 0; i < 9; i++)
            {
                int x = pos % 9 + i * 9;
                //Debug.WriteLine(x.ToString() + " = POS");
                found.Add(intArray[x]);
            }
            return found;
        }
        public List<int> CheckGrid(int pos, int[] intArray)
        {
            List<int> found = new List<int>();

            List<int> hardList = new List<int> {
                0,0,0,3,3,3,6,6,6,
                0,0,0,3,3,3,6,6,6,
                0,0,0,3,3,3,6,6,6,
                27,27,27,30,30,30,33,33,33,
                27,27,27,30,30,30,33,33,33,
                27,27,27,30,30,30,33,33,33,
                54,54,54,57,57,57,60,60,60,
                54,54,54,57,57,57,60,60,60,
                54,54,54,57,57,57,60,60,60
            };

            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    //int col = pos % 9 + x;
                    //int row = pos / 9 + y;
                    //Debug.WriteLine(hardList[pos] + x + y * 9);
                    //found.Add(intArray[col + 9 * row]);
                    found.Add(intArray[hardList[pos] + x + y * 9]);
                }
            }
            return found;
        }


        public int[] ToIntArray(int[][] sudoku) {
            List<int> intList = new List<int>();
            foreach (var x in sudoku)
            {
                foreach (var y in x)
                {
                    intList.Add(y);
                }
            }
            Debug.WriteLine(intList.ToString());
            return intList.ToArray();
        }

        public int[][] Create(int[][] sudoku)
        {
            return sudoku;
        }
    }
}