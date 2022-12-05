using System;

public class Solution {
    public int solution(string s) {
        int answer = 0;
        bool final = false;
        
        char firstChar = s[0];
        int firstCount = 0;
        
        int otherCount = 0;
        
        for (int i = 0; i < s.Length; i++)
        {
            // s[i]가 첫 글자와 같다면 첫 글자 카운트 추가
            if (s[i] == firstChar)
            {
                firstCount++;
                
                // 마지막에 분해가 안 되도 +1
                if ((i + 1) == s.Length) answer++;
            }
            // s[i]가 다른 글자라면 다른 글자 카운트 추가
            else
            {
                otherCount++;
                
                if (firstCount == otherCount)
                {
                    answer++;
                    if ((i + 1) != s.Length) firstChar = s[i + 1];
                    firstCount = 0;
                    otherCount = 0;
                }
                else
                {
                    // 마지막에 분해가 안 되도 +1
                    if ((i + 1) == s.Length) answer++;
                }
            }
        }
        
        return answer;
    }
}
