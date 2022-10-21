public class Solution {
    public long solution(int n) {
        long answer = 0;
        
        if (n < 0)
            return 0;
        
        long[] jumpCase = new long[2000];
        
        jumpCase[0] = 1;
        jumpCase[1] = 1;
        
        for (int i = 2; i < n + 1; i++)
        {
            jumpCase[i] = (jumpCase[i - 1] + jumpCase[i - 2]) % 1234567;
        }
        
        return jumpCase[n];
    }
}
