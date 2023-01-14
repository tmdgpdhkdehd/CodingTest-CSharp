using System;
using System.Collections.Generic;

public class Solution {
    // 행(튜플)을 col번째 열(컬럼)을 기준으로 오름차순 정렬 (col번째 열 값이 동일하면 첫 번째 열 값을 기준으로 내림차순 정렬)
    // 정렬된 행의 row_begin번째와 row_end번째를 각각 row_begin, row_end로 나눈 나머지들의 합을 비트 XOR 한다
    // XOR: 한 개만 참일 경우 true, '^'로 계산
    public int solution(int[,] data, int col, int row_begin, int row_end)
    {
        int result = 0;
        var list = new List<Tuple<List<int>>>();
        
        // 2차원 배열 data를 list<tuple>에 옮김
        for (int i = 0; i < data.GetLength(0); i++)
        {
            list.Add(new Tuple<List<int>>(new List<int>()));
            for (int j = 0; j < data.GetLength(1); j++)
            {
                list[i].Item1.Add(data[i, j]);
            }
        }
        
        // 리스트 오름차순 정렬
        list.Sort((a, b) =>
        {
            // 이미 오름차순이면 정렬X
            if (a.Item1[col - 1] < b.Item1[col - 1])
                return -1;
            // 오름차순이 아니면 정렬
            else if (a.Item1[col - 1] > b.Item1[col - 1])
                return 1;
            // col번째 값이 똑같을 때, 첫 번째 값을 기준으로 내림차순 정렬
            else
            {
                if (a.Item1[0] > b.Item1[0])
                    return -1;
                else return 1;
            }
        });
        
        // 정렬된 행의 row_begin번째와 row_end번째를 각각 row_begin, row_end로 나눈 나머지들의 합을 비트 XOR 
        for (int i = row_begin; i <= row_end; i++)
        {
            int sum = 0;
            // list[i-1].Item 처음부터 끝까지 반복하며 나머지를 sum에 누적
            foreach(int ele in list[i-1].Item1)
            {
                sum += ele % i;
            }
            // 누적된 나머지 합을 이전 값과 XOR
            result ^= sum;
        }
        return result;
    }  
}
