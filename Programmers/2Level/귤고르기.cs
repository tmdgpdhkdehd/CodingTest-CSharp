using System;
using System.Collections.Generic;
using System.Linq;

public class Solution {
    // k: 상자에 넣을 귤의 개수, tangerine: 귤의 크기 배열
    // 크기 종류를 최소화해서 상자를 채워야 한다
    public int solution(int k, int[] tangerine) {
        int answer = 0;
        Dictionary<int, int> count = new Dictionary<int, int>();
        
        for (int i = 0; i < tangerine.Length; i++)
        {
            if (count.ContainsKey(tangerine[i]))
            {
                count[tangerine[i]]++;
            }
            else
            {
                count.Add(tangerine[i], 1);
            }
        }
        
        // 내림차순 정렬
        count = count.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        
        foreach (var dic in count)
        {
            Console.Write(dic.Value);
            k -= dic.Value;
            answer++;
            
            if (k <= 0)
                break;
        }
        
        return answer;
    }
}
