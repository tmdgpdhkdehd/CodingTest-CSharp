using System;
using System.Linq;
using System.Collections.Generic;

// 객실을 최소한 사용해야 한다
// 사용한 객실은 10분 뒤 사용 가능, 시작 시간은 무조건 00:00 ~ 23:59
// 모든 시작 시간이 똑같으면 answer(최소 룸) = 예약 수
// 가장 빠른 시간 입실시키기(A)
// A와 겹치는 시간은 ROOM을 추가해서 넣기 (B)
// A 혹은 B가 끝나는 시간 10분 뒤에 올 수 있는 가장 빠른 시간 붙이기
public class Solution {
    List<List<int>> int_Reservation = new List<List<int>>();        // 예약시간, [0]입실 시간 [1]퇴실 시간
    List<int> room = new List<int>();       // 호텔 방, 방의 입실 시간은 중요하지 않으므로 퇴실 시간만 사용
    
    // 새로운 방에 입실
    public void PlusRoom(int i)
    {
        room.Add(int_Reservation[i][1] + 10);
    }
    
    // 기존 방에 입실
    public void EnterRoom(int room_Num, int i)
    {
        room[room_Num] = int_Reservation[i][1] + 10;
    }
    
    public int solution(string[,] book_time) {
        // 문자열을 숫자로 바꿈
        for (int i = 0; i < book_time.GetLength(0); i++)
        {
            string[] in_Time = book_time[i,0].Split(':');
            string[] out_Time = book_time[i,1].Split(':');
            
            // 시간은 60분과 똑같기 때문에 분단위로 만들어줌 ex) 11:30분은 11*60+30 = 690분
            int_Reservation.Add(new List<int> {int.Parse(in_Time[0])*60 + int.Parse(in_Time[1]), int.Parse(out_Time[0])*60 + int.Parse(out_Time[1])});
        }
        
        // 내림차순 정렬
        int_Reservation = int_Reservation.OrderByDescending(x => x[0]).ToList();
        PlusRoom(int_Reservation.Count-1);
        int_Reservation.RemoveAt(int_Reservation.Count-1);
        
        while (int_Reservation.Count != 0)
        {
            for (int i = int_Reservation.Count - 1; i >= 0; i--)
            {
                bool isEnter = false;
                for (int j = 0; j < room.Count; j++)
                {
                    // 현재 방의 퇴실 시간이 입실 시간보다 늦다면 다른 방도 찾아본다
                    if (room[j] > int_Reservation[i][0])
                    {
                        continue;
                    }
                    // 들어갈 수 있는 방을 찾았다
                    else
                    {
                        isEnter = true;
                        EnterRoom(j, i);
                        break;
                    }
                }
                
                // 들어가지 못했다면 룸 하나 만들어서 들어가게 함
                if (!isEnter)
                {
                    PlusRoom(i);
                }
                
                // 룸 퇴실시간 변경됐으니 다시 오름차순 정렬
                room = room.OrderBy(x => x).ToList();
                int_Reservation.RemoveAt(i);
            }
        }
        
        
        return room.Count;
    }
}
