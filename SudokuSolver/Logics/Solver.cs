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
            for (int i = 0; i < IntArrayCount.Length; i++)
            {
                if (IntArray[i] > 0) {
                    IntArrayCount[i] = -1;
                } else {
                    IntArrayCount[i] = 0;
                }
                
            }
            for (int i = 0; i < IntArray.Length; i++)
            {
                if (IntArrayCount[i] > -1) {
                    List<int> found = new List<int>();
                    found.AddRange(CheckX(i, IntArray));
                    found.AddRange(CheckY(i, IntArray));
                    found.AddRange(CheckGrid(i, IntArray));
                    found = RemoveNumbers(found, new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
                    if (found.Count == 0) {
                        IntArrayCount[i] = 0;
                        i--;
                        IntArrayCount[i]++;
                    }
                    else
                    {
                        IntArray[i] = found[IntArrayCount[i]];
                    }
                }
                Debug.WriteLine(i.ToString());
            }

            return ToSudoku(IntArray);
        }

        public int[][] ToSudoku(int[] intArray) {
            int[][] sudoku = new int[9][];
            for (int i = 0; i < sudoku.Length; i++)
            {
                for (int pos = 0; pos < 9; pos++)
                {
                    int x = i - i % 9 + pos;
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
                int x = pos - pos % 9 + i * 9;
                found.Add(intArray[x]);
            }
            return found;
        }
        public List<int> CheckGrid(int pos, int[] intArray)
        {
            List<int> found = new List<int>();
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    int finalPos = pos - pos % 9 + x + y * 3;
                    found.Add(intArray[finalPos]);
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