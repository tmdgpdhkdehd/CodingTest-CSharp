using System;

public class Solution {
    public int solution(int[,] sizes) {
        int answer = 0;
        int width = 0;
        int height = 0;
        
        for (int i = 0; i < sizes.GetLength(0); i++)
        {
            if (sizes[i, 0] > sizes[i, 1])
            {
                int temp = sizes[i, 0];
                sizes[i, 0] = sizes[i, 1];
                sizes[i, 1] = temp;
            }
            
            if (sizes[i, 0] > width)
            {
                width = sizes[i, 0];
            }
            
            if (sizes[i, 1] > height)
            {
                height = sizes[i, 1];
            }
        }
        
        answer = width * height;
        
        return answer;
    }
}
