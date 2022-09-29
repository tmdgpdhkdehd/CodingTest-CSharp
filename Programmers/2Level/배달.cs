#include <iostream>
#include <vector>
#include <queue>
using namespace std;

int solution(int N, vector<vector<int> > road, int K) {
    int answer = 0;

    vector<vector<int>> graph(N+1, vector<int>(N+1, 1e8));
    for(int i=1; i<=N; ++i) graph[i][i] = 0;
    
    for(auto r: road) {
    	// 제한사항
        graph[r[0]][r[1]] = min(graph[r[0]][r[1]], r[2]);
        graph[r[1]][r[0]] = min(graph[r[1]][r[0]], r[2]);
    }
    
    // Dijkstra Algorithm
    priority_queue<pair<int,int>> pq; // dist, index
    vector<int> dist(N+1, 1e8);
    
    pq.push({0, 1});
    dist[1] = 0;
    
    while(!pq.empty()) {
        int d = -pq.top().first;
        int node = pq.top().second;
        pq.pop();
        
        for(int i=1; i<=N; ++i) {
            if(i == node || graph[node][i] == 1e8) continue;
            
            if(dist[i] > d + graph[node][i]) {
                dist[i] = d + graph[node][i];
                
                pq.push({-dist[i], i});
            }
        }
    }

    for(int i=1; i<=N; ++i) {
        if(dist[i] <= K) answer++;
    }

    return answer;
}
