#include <string>
#include <vector>

using namespace std;

int solution(vector<int> absolutes, vector<bool> signs) {
    // sings 참이면 absolutes 양수, sings 거짓이면 absolutes 음수
    int answer = 0;
    
    for (int i = 0; i < absolutes.size(); i ++)
    {
        if (signs[i] == false)
        {
            absolutes[i] *= -1;
        }
        answer += absolutes[i];
    }
    
    return answer;
}
