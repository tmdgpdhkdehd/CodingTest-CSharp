using System;
using System.Linq;

public class Solution {
    // k: 사과의 최대 점수, m: 한 상자에 들어가는 사과의 수, score: 사과들의 점수
    public int solution(int k, int m, int[] score) {
        int answer = 0;
        int[,] box = new int[score.Length / m, m];
        
        score = score.OrderByDescending(n => n).ToArray();
        
        int scoreIndex = 0;
        // 행
        for (int i = 0; i < box.GetLength(0); i++)
        {
            // 열
            for (int j = 0; j < m; j++)
            {
                box[i, j] = score[scoreIndex];
                scoreIndex++;
            }
            
            // 내림차순 정렬했기 때문에 무조건 마지막 인덱스가 최솟값이다
            answer += box[i, m - 1] * m;
        }
        
        return answer;
    }
}
