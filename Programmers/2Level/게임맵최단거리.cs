using System;
using System.Collections.Generic;

class Solution {
    Queue<(int, int)> q = new Queue<(int, int)>(); 

    int[] moveX = {-1, 1, 0, 0};
    int[] moveY = {0, 0, -1, 1};

    public void bfs(int x, int y, int[,] maps)
    {
        q.Enqueue((x, y));

        while(q.Count != 0)
        {
            (int a, int b) = q.Dequeue();

            for (int i = 0; i < 4; i++)
            {
                int nx = a + moveX[i];
                int ny = b + moveY[i];

                if (nx < 0 || nx >= colume || ny < 0 || ny >= row)
                    continue;
                if (maps[nx, ny] == 0)
                    continue;
                if (maps[nx, ny] == 1)
                {
                    maps[nx, ny] = maps[a, b] + 1;
                    q.Enqueue((nx, ny));
                }
            }
        }
    }
    public int solution(int[,] maps) {
        colume = maps.GetLength(0);
        row = maps.GetLength(1);
        
        bfs(0, 0, maps);
        
        if (maps[colume-1, row-1] == 1)
            return -1;
        return (maps[colume-1, row-1]);
    }
}
