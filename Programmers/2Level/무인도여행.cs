using System;
using System.Collections.Generic;
using System.Text;

// 상하좌우 연결되어 있으면 숫자 합하고 합한 곳은 X 표시
// 더는 연결된 곳이 없으면 합한 숫자는 배열에 저장
// X표시 된 곳은 건너뛰기
// maps 끝까지 탐색 마쳤으면 배열을 오름차순 정렬하고 반환
public class Solution {
    Queue<(int, int)> q = new Queue<(int, int)>();
    int colume = 0;
    int row = 0;

    public int bfs(int x, int y, string[] maps)
    {
        // X라면 건너뛰기
        if (maps[x][y] == 'X')
            return 0;

        int[] moveX = {-1, 1, 0, 0};
        int[] moveY = {0, 0, -1, 1};

        // 무한반복되지 않게 X로 만들어주고 식량에 더하기
        int food = (int)Char.GetNumericValue(maps[x][y]);
        StringBuilder sb = new StringBuilder(maps[x]);
        sb[y] = 'X';
        maps[x] = sb.ToString();

        q.Enqueue((x, y));

        //while(q.Count != 0)
        //{
            (int a, int b) = q.Dequeue();

            for (int i = 0; i < 4; i++)
            {
                int nx = a + moveX[i];
                int ny = b + moveY[i];

                if (nx < 0 || nx >= colume || ny < 0 || ny >= row)
                    continue;
                if (maps[nx][ny] == 'X')
                    continue;
                else
                {
                    food += bfs(nx, ny, maps);
                    //q.Enqueue((nx, ny));
                }
            }
        //}

        return food;
    }

    public int[] solution(string[] maps) {
        List<int> answer = new List<int>();
        colume = maps.Length;
        row = maps[0].Length;

        for (int i = 0; i < colume; i++)
        {
            for (int j = 0; j < row; j++)
            {
                int food = bfs(i, j, maps);
                if (food == 0)
                {
                    continue;
                }
                else
                {
                    answer.Add(food);
                }
            }
        }

        if (answer.Count == 0)
        {
            answer.Add(-1);
        }
        else
        {
            answer.Sort();
        }

        return answer.ToArray();
    }
}
