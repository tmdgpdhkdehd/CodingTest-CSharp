#include <stdio.h>
#include <stdbool.h>
#include <stdlib.h>

// numbers_len은 배열 numbers의 길이입니다.
int solution(int numbers[], size_t numbers_len) {
    int tennumbers[] = {1,2,3,4,5,6,7,8,9};
    int answer = 45;
    
    for (int i = 0; i < sizeof(tennumbers) / sizeof(tennumbers[0]); i ++)
    {
        for (int j = 0; j < numbers_len; j++)
            if (tennumbers[i] == numbers[j])
            {
                answer = answer - numbers[j];
            }
    }
    
    return answer;
}
