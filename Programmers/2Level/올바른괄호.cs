using System;
using System.Collections.Generic;

public class Solution {
    public bool solution(string s) {
        bool answer = true;
        Stack<char> stack = new Stack<char>();
        
        for (int i = 0; i < s.Length; i++)
        {
            if (s[0].Equals(')'))
            {
                answer = false;
                break;
            }
            
            if (s[i].Equals('('))
                stack.Push(s[i]);
            else
                if (stack.Count >= 1)
                {
                    stack.Pop();
                    answer = true;
                }
                else
                {
                    answer = false;
                    break;
                }
        }
        
        if (stack.Count > 0)
            answer = false;
        
        return answer;
    }
}
