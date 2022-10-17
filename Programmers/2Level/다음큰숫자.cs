using System;

class Solution 
{
    public int binary(int n)
    {
        int cnt = 0;
        
        while(n > 0)
        {
            if (n % 2 == 1)
                cnt++;
            
            n /= 2;
        }
        
        return cnt;
    }
    
    public int solution(int n) 
   {
        int cnt = 0;
        int bigNumber = n + 1;
        
        cnt = binary(n);
        
        while (true)
        {
            if (binary(bigNumber) == cnt)
                return bigNumber;
            
            bigNumber++;
        }
    }
}
