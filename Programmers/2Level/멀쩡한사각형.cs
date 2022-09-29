using namespace std;

long long solution(int w, int h) {
    long long answer = 0;

    for (int i = 0; i < w; ++i) {
        answer += (int)((double)h*i/w);
    }

    return 2 * answer;
}
