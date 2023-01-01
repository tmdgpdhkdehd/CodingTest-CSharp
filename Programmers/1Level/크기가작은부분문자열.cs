using System;

// p의 길이만큼 t를 쪼개기 ex) p가 3이면 t+n, t+n1, t+n2 숫자 3개 가져오기
// 쪼갠 t가 p보다 작거나 같은만큼 answer++
public class Solution {
    public int solution(string t, string p) {
        int answer = 0;
        
        // t 처음부터 끝까지 p랑 비교
        for (int i = 0; i < t.Length - p.Length + 1; i++)
        {
            string tmp = "";
            for (int j = 0; j < p.Length; j++)
            {
                tmp += t[i + j];
            }
            
            // 쪼갠 t가 p보다 작거나 같으면 answer++
            if (Convert.ToInt64(tmp) <= Convert.ToInt64(p))
                answer++;
        }
        
        return answer;
    }
}
