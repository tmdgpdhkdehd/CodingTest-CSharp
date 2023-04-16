using System;
using System.Linq;
using System.Collections.Generic;

public class Solution {
    public int solution(int[,] targets) {
        int n = targets.GetLength(0);
        List<(int, int)> intervals = new List<(int, int)>();

        // (s, e) 형태의 튜플 리스트로 변환
        for (int i = 0; i < n; i++) {
            intervals.Add((targets[i, 0], targets[i, 1]));
        }

        // 시작점 s를 기준으로 정렬
        intervals.Sort();

        // 첫 번째 구간은 요격 미사일이 하나 필요함
        int answer = 1;
        int right = intervals[0].Item2;

        // 두 번째 구간부터 시작하여 요격 미사일 수 계산
        for (int i = 1; i < n; i++) {
            // 현재 구간의 시작점이 이전 구간의 끝점보다 크면 요격 미사일이 하나 더 필요함
            if (intervals[i].Item1 >= right) {
                answer++;
                right = intervals[i].Item2;
            } 
            // 현재 구간이 이전 구간에 포함되는 경우
            else if (intervals[i].Item1 < right && intervals[i].Item2 < right) {
                // 더 작은 끝점으로 갱신
                right = intervals[i].Item2;
            }
        }

        return answer;
    }
}
