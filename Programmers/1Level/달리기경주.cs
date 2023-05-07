using System;
using System.Linq;
using System.Collections.Generic;

public class Solution {
    public string[] solution(string[] players, string[] callings) {
        // value로 key를 찾으면 시간복잡도 문제가 생겨 딕셔너리를 2개 선언
        Dictionary<int, string> keyRank = new Dictionary<int, string>();
        Dictionary<string, int> keyName = new Dictionary<string, int>();
        
        // 시간복잡도 문제로 배열은 사용 불가, 딕셔너리로 변환해야만 함
        for (int i = 0; i < players.Length; i++)
        {
            keyRank[i] = players[i];
            keyName[players[i]] = i;
        }
        
        // 순위 바꾸기
        for (int i = 0; i < callings.Length; i++)
        {
            // 불린 선수의 이름과 등수를 가져옴
            string name = callings[i];
            int index = keyName[name];

            // keyRank 변경 (등수로 이름 변경)
            keyRank[index] = keyRank[index - 1];
            keyRank[index - 1] = name;

            // keyName 변경 (이름으로 등수 변경)
            keyName[name] = index - 1;
            keyName[keyRank[index]] = index;
        }
        
        players = keyRank.Values.ToArray();
        
        return players;
    }
}
