using System;
using System.Collections.Generic;
using System.Text;

// 1. 숫자인 곳이 있으면 bfs 실행, X인 곳이면 건너뛰기
// 2. bfs 실행하면 무한탐색을 막기 위해 해당 위치의 식량 값을 저장하고 해당 위치를 X 표시
// 3. bfs 실행 중, 상하좌우 중 연결되어 있는 곳(숫자)이 있으면 그 위치에 bfs 실행 반복
// 4. 더는 연결된 곳이 없으면 누적된 식량 값을 리스트에 저장
// 맵을 모두 탐색할 때까지 1~4번 반복
// 맵을 모두 탐색했으면 오름차순으로 정렬한 후 배열로 전환하여 반환
public class Solution {
    int colume = 0;     // 행
    int row = 0;        // 열

    public int bfs(int x, int y, string[] maps)
    {
        // 상하좌우
        int[] moveX = {-1, 1, 0, 0};
        int[] moveY = {0, 0, -1, 1};

        // 무한반복되지 않게 X로 만들어주고 식량에 더하기
        int food = (int)Char.GetNumericValue(maps[x][y]);
        // maps[x][y] = 'X'를 하려고 하면
        // property or indexer `string.this[int]' cannot be assigned to (it is read-only)
        // 라는 오류가 뜨기 때문에 StringBuilder 사용하여 바꿈
        StringBuilder sb = new StringBuilder(maps[x]);
        sb[y] = 'X';
        maps[x] = sb.ToString();

        // 상하좌우에 연결된 섬이 있는지 탐색
        for (int i = 0; i < 4; i++)
        {
            int nx = x + moveX[i];
            int ny = y + moveY[i];
            
            // 막혀있는 곳은 건너뛰기
            if (nx < 0 || nx >= colume || ny < 0 || ny >= row)
                continue;
            // X인 곳은 건너뛰기
            if (maps[nx][ny] == 'X')
                continue;
            // 섬이 있다면 bfs 실행하고 식량 누적
            else
                food += bfs(nx, ny, maps);
        }

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
                // X인 곳은 건너뛰기
                if (maps[i][j] == 'X')
                    continue;
                
                // 누적된 식량을 리스트에 저장
                int food = bfs(i, j, maps);
                answer.Add(food);
            }
        }

        // 리스트에 값이 없다면 섬이 없는 것이므로 -1을 반환하게끔 리스트에 -1 추가
        if (answer.Count == 0)
            answer.Add(-1);
        // 오름차순 정렬
        else
            answer.Sort();

        // 리스트를 배열로 전환
        return answer.ToArray();
    }
}
