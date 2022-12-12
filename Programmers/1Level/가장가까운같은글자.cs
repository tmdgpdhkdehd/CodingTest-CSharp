using System;
using System.Collections.Generic;
using System.Linq;

// 반환은 문자열 길이와 같다
// 일단 Dictionary<char, int>를 이용해서 만들어본다
// 같은 글자는 어차피 가까운 걸 선택하기 때문에 마지막 위치만 한 번 저장해주면 된다
// dictionary에서 해당 글자의 value 값을 answer로 전달한다
// 만약, 처음 나온 글자면 answer에는 -1을 저장해주고 dictionary에서 해당 글자의 value 값은 0으로 지정해준다
// 반복문에서 i가 1씩 높아질 때마다 dictionary의 모든 글자의 value 값을 1씩 올려준다
public class Solution {
    public int[] solution(string s) {
        int[] answer = Enumerable.Repeat<int>(0, s.Length).ToArray<int>();
        Dictionary<char, int> location = new Dictionary<char, int>();
        
        for (int i = 0; i < s.Length; i++)
        {
            location.Keys.ToList().ForEach (key =>
                {
                    location[key] += 1;
                });
            
            if (!location.ContainsKey(s[i]))
            {
                answer[i] = -1;
                location.Add(s[i], 0);
            }
            else
            {
                answer[i] = location[s[i]];
                location[s[i]] = 0;
            }
        }
        
        return answer;
    }
}
