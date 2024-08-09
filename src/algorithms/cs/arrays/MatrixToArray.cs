using System.Collections.Generic; 

namespace Studying.Leetcode.Algorithms
{
    /// <summary>
    /// 
    /// </summary>
    public class MatrixToArray : Studying.Leetcode.ILeetcodeProblem
    {
        /// <summary>
        /// 
        /// </summary>
        public void Execute()
        {
            // Matrix 1
            int[,] matrix1 = new int[3,3] {{1,2,3},{4,5,6},{7,8,9}}; 
            var array1 = CounterClockwise(matrix1); 
            PrintMatrix(matrix1); 
            PrintArray(array1); 

            // Matrix 2
            int[,] matrix2 = new int[3,4] {{1,2,3,4},{5,6,7,8},{9,10,11,12}}; 
            var array2 = CounterClockwise(matrix2); 
            PrintMatrix(matrix2); 
            PrintArray(array2); 
        }

        /// <summary>
        /// 
        /// </summary>
        private static int[] CounterClockwise(int[,] arr)
        {
            // k - starting row index
            // m - ending row index
            // l - starting column index
            // n - ending column index
            // i - iterator
            int i, k = 0, l = 0;
            int m = arr.GetLength(0); 
            int n = arr.GetLength(1); 

            List<int> result = new List<int>(); 
    
            // initialize the count
            int cnt = 0;
    
            // total number of elements in matrix
            int total = m * n;

            while (k < m && l < n)
            {
                if (cnt == total)
                    break;
    
                // Get the first column from the remaining columns
                for (i = k; i < m; ++i)
                {
                    result.Add(arr[i,l]); 
                    cnt++;
                }
                l++;
    
                if (cnt == total)
                    break;
    
                // Get the last row from the remaining rows
                for (i = l; i < n; ++i)
                {
                    result.Add(arr[m - 1, i]); 
                    cnt++;
                }
                m--;
    
                if (cnt == total)
                    break;
    
                // Get the last column from the remaining columns
                if (k < m) 
                {
                    for (i = m - 1; i >= k; --i)
                    {
                        result.Add(arr[i, n - 1]); 
                        cnt++;
                    }
                    n--;
                }
    
                if (cnt == total)
                    break;
    
                // Get the first row from the remaining rows
                if (l < n)
                {
                    for (i = n - 1; i >= l; --i)
                    {
                        result.Add(arr[k, i]); 
                        cnt++;
                    }
                    k++;
                }
            }
            return result.ToArray();  
        }

        /// <summary>
        /// 
        /// </summary>
        private void PrintMatrix(int[,] matrix)
        {
            System.Console.WriteLine("matrix:"); 
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    System.Console.Write(matrix[i,j] + " "); 
                }
                System.Console.WriteLine(); 
            }
            System.Console.WriteLine(); 
        }

        /// <summary>
        /// 
        /// </summary>
        private void PrintArray(int[] array)
        {
            System.Console.WriteLine("array:"); 
            foreach (var element in array)
            {
                System.Console.Write(element + " "); 
            }
            System.Console.WriteLine(); 
        }
    }
}