using System;
using System.Collections.Generic;
using System.Linq;

public class Solution {
    Dictionary<char, int> score_s = new Dictionary<char, int>()
    {
        {'R', 0}, {'T', 0}, {'C', 0}, {'F', 0}, {'J', 0}, {'M', 0}, {'A', 0}, {'N', 0}
    };
    
    public char compare(char a, char b)
    {
        if (score_s[a] >= score_s[b])
            return a;
        else
            return b;
    }
    
    public string solution(string[] survey, int[] choices) {
        string answer = "";
        
        for (int i = 0; i < survey.Length; i++)
        {
            // 매우 비동의 ~ 약간 비동의
            if (1 <= choices[i] && choices[i] <= 3)
            {
                switch(choices[i])
                {
                    case 1:
                        score_s[survey[i][0]] += 3;
                        break;
                    case 2:
                        score_s[survey[i][0]] += 2;
                        break;
                    case 3:
                        score_s[survey[i][0]] += 1;
                        break;
                }
            }
            // 약간 동의 ~ 매우 동의
            else if (5 <= choices[i] && choices[i] <= 7)
            {
                switch(choices[i])
                {
                    case 5:
                        score_s[survey[i][1]] += 1;
                        break;
                    case 6:
                        score_s[survey[i][1]] += 2;
                        break;
                    case 7:
                        score_s[survey[i][1]] += 3;
                        break;
                }
            }
        }
        
        char[] type = Enumerable.Repeat('a', 4).ToArray();
        
        type[0] = compare('R', 'T');
        type[1] = compare('C', 'F');
        type[2] = compare('J', 'M');
        type[3] = compare('A', 'N');
        
        answer = new string(type);
        
        return answer;
    }
}
