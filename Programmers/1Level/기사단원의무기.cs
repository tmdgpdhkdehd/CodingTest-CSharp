using System;
using System.Linq;

public class Solution {
    public int divisor(int number)
    {
        int count = 0;
        
        for (int i = 1; i * i <= number; i++)
        {
            // i로 나누어떨어지면 i는 number의 약수
            if (number % i == 0)
            {
                count++;
                // 시간복잡도 문제로 넣어주어야 한다
                // i * i == number가 아닌 이상 짝이 존재하기 때문에 count++ 해준다
                if (i * i < number)
                    count++;
            }
        }
        return count;
    }
    
    public int solution(int number, int limit, int power) {
        int answer = 0;
        
        for (int i = 1; i <= number; i++)
        {
            int count = divisor(i);
            if (count > limit)
                count = power;
            answer += count;
        }
        
        return answer;
    }
}
