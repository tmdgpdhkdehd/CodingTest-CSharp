using System;
using System.Linq;
using System.Collections.Generic;

class Solution
{
    public int solution(int N, int[,] road, int K)
    {
        int answer = 0;
        int[] nCosts = new int[N];
        
        for (int i = 0; i < N; i++)
        {
            if (i == 0)
                nCosts[0] = 0;
            else
                nCosts[i] = int.MaxValue;
        }
        
        Queue<int> que = new Queue<int>();
        que.Enqueue(1);
        
        while (que.Count > 0)
        {
            int nCurrent = que.Dequeue();
            
            for (int i = 0; i < road.GetLength(0); i++)
            {
                int nStart = road[i, 0];
                int nEnd = road[i, 1];
                int nCost = road[i, 2];
                
                if (nStart == nCurrent)
                {
                    if (nCosts[nEnd - 1] > nCost + nCosts[nStart - 1])
                    {
                        nCosts[nEnd - 1] = nCost + nCosts[nStart - 1];
                        que.Enqueue(nEnd);
                    }
                }
                else if (nEnd == nCurrent)
                {
                    if (nCosts[nStart - 1] > nCost + nCosts[nEnd - 1])
                    {
                        nCosts[nStart - 1] = nCost + nCosts[nEnd -1];
                        que.Enqueue(nStart);
                    }
                }
            }
        }
        
        return nCosts.Where(x => x <= K).Count();
    }
}
