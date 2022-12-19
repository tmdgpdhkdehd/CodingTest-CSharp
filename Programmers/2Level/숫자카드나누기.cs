using System;
using System.Collections.Generic;
using System.Linq;

public class Solution {
    // arrayA의 모든 숫자를 나눌 수 있고 arrayB의 모든 숫자를 나눌 수 없는 경우
    // 혹은 반대로 arrayB의 모든 숫자를 나눌 수 있고 arrayA의 모든 숫자를 나눌 수 없는 경우
    // 가장 큰 양의 정수를 구하라, 없다면 0 반환
    public List<int> divisor(int number)
    {
        List<int> divisorArray = new List<int>();
        
        // 1은 빼고 계산
        for (int i = 2; i <= number; i++)
        {
            // i로 나누어떨어지면 i는 number의 약수
            if (number % i == 0)
            {
                divisorArray.Add(i);
            }
        }
        return divisorArray;
    }
    
    public int solution(int[] arrayA, int[] arrayB) {
        int answer = 0;
        // 약수를 저장할 공간
        List<int> arrayA_Divisor = new List<int>();
        List<int> arrayB_Divisor = new List<int>();
        
        // arrayA와 arrayB 리스트에 각각의 약수 넣기
        arrayA_Divisor = divisor(arrayA.Min());
        arrayB_Divisor = divisor(arrayB.Min());

        // 같은 배열을 나눌 수 없는 약수가 존재한다면 제외시키기
        for (int i = 0; i < arrayA.Length; i++)
        {
            for (int j = 0; j < arrayA_Divisor.Count; j++)
            {
                if (arrayA[i] % arrayA_Divisor[j] != 0)
                {
                    arrayA_Divisor.RemoveAt(j);
                    j--;
                }
            }
            
            for (int j = 0; j < arrayB_Divisor.Count; j++)
            {
                if (arrayB[i] % arrayB_Divisor[j] != 0)
                {
                    arrayB_Divisor.RemoveAt(j);
                    j--;
                }
            }
        }
        
        // 상대 배열을 나눌 수 있는 약수가 존재한다면 제외시키기
        for (int i = 0; i < arrayA.Length; i++)
        {
            for (int j = 0; j < arrayB_Divisor.Count; j++)
            {
                if (arrayA[i] % arrayB_Divisor[j] == 0)
                {
                    arrayB_Divisor.RemoveAt(j);
                    j--;
                }
            }
            
            for (int j = 0; j < arrayA_Divisor.Count; j++)
            {
                if (arrayB[i] % arrayA_Divisor[j] == 0)
                {
                    arrayA_Divisor.RemoveAt(j);
                    j--;
                }
            }
        }
        
        // 요소가 있는지부터 검사
        if (arrayA_Divisor.Count > 0)
        {
            answer = arrayA_Divisor.Max();
        }
        if (arrayB_Divisor.Count > 0)
        {
            if (answer < arrayB_Divisor.Max())
            {
                answer = arrayB_Divisor.Max();
            }
        }
        
           
        return answer;
    }
}
