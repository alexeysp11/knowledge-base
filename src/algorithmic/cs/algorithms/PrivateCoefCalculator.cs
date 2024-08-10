using System;

namespace KnowledgeBase.Algorithmic.Algorithms
{
    /// <summary>
    /// 
    /// </summary>
    public class PrivateCoefCalculator : KnowledgeBase.Algorithmic.ILeetcodeProblem
    {
        public class CoefCalculator
        {
            private int m_x1, m_x2, m_x3, m_x4, m_x5;

            public CoefCalculator(int x1, int x2, int x3, int x4, int x5)
            {
                m_x1 = x1;
                m_x2 = x2;
                m_x3 = x3;
                m_x4 = x4;
                m_x5 = x5;
            }

            public int Calculate(int a1, int a2, int a3, int a4, int a5)
            {
                return (a1 * m_x1) + (a2 * m_x2) + (a3 * m_x3) + (a4 * m_x4) + (a5 * m_x5);
            }
        }

        public void Execute()
        {
            CoefCalculator calculator = new CoefCalculator(11, 22, 23, 4, 5);

            int a1 = 2;
            int a2 = 3;
            int a3 = 4;
            int a4 = 5;
            int a5 = 6;

            int result = calculator.Calculate(a1, a2, a3, a4, a5);
            Console.WriteLine("Result: " + result);

            // Определение значений приватных членов
            int m_x1 = (result - (a2 * 2) - (a3 * 3) - (a4 * 4) - (a5 * 5)) / a1;
            int m_x2 = (result - (a1 * m_x1) - (a3 * 3) - (a4 * 4) - (a5 * 5)) / a2;
            int m_x3 = (result - (a1 * m_x1) - (a2 * m_x2) - (a4 * 4) - (a5 * 5)) / a3;
            int m_x4 = (result - (a1 * m_x1) - (a2 * m_x2) - (a3 * m_x3) - (a5 * 5)) / a4;
            int m_x5 = (result - (a1 * m_x1) - (a2 * m_x2) - (a3 * m_x3) - (a4 * m_x4)) / a5;

            Console.WriteLine("m_x1: " + m_x1);
            Console.WriteLine("m_x2: " + m_x2);
            Console.WriteLine("m_x3: " + m_x3);
            Console.WriteLine("m_x4: " + m_x4);
            Console.WriteLine("m_x5: " + m_x5);
        }

    }
}