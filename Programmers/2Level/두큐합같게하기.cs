using System;
using System.Linq;
using System.Collections.Generic;

public class Solution {
    public int solution(int[] queue1, int[] queue2) {        
        Queue<long> q1 = new Queue<long>();
        Queue<long> q2 = new Queue<long>();
        
        long q1_Add = 0;
        long q2_Add = 0;
        int cnt = 0;
        
        for (int i = 0; i < queue1.Length; i++)
        {
            q1.Enqueue(queue1[i]);
            q2.Enqueue(queue2[i]);
            
            q1_Add += queue1[i];
            q2_Add += queue2[i];
        }
        do
        {
            if (q1_Add > q2_Add)
            {
                long tmp = q1.Dequeue();
                q1_Add -= tmp;
                q2.Enqueue(tmp);
                q2_Add += tmp;
                cnt++;
            }
            else if (q1_Add < q2_Add)
            {
                long tmp = q2.Dequeue();
                q2_Add -= tmp;
                q1.Enqueue(tmp);
                q1_Add += tmp;
                cnt++;
            }
            
            if (q1_Add == q2_Add)
                return cnt;
        } while (cnt <= (queue1.Length + queue2.Length) * 2);
        
        return -1;
    }
}
