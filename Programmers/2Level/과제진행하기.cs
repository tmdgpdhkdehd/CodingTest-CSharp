using System;
using System.Collections.Generic;
using System.Linq;

public class Plan
{
    public string name;
    public int startTime;
    public int leftTime;
    
    public Plan(string name, int startTime, int leftTime) {
        this.name = name;
        this.startTime = startTime;
        this.leftTime = leftTime;
    }
}

public class Solution {
    public string[] solution(string[,] plans) {
        List<Plan> leftPlans = new List<Plan>();        // 남은 계획들
        List<Plan> stopPlans = new List<Plan>();        // 멈춘 계획들
        List<string> answer = new List<string>();       // 계획 끝낸 순서
        int currentTime = 0;        // 현재 시간
        
        // 문자열을 숫자로 바꿈
        for (int i = 0; i < plans.GetLength(0); i++)
        {
            // 문자열 자르기
            string[] startTime = plans[i,1].Split(':');
            
            // 시간은 60분과 똑같기 때문에 분단위로 만들어줌 ex) 11:30분은 11*60+30 = 690분
            Plan p = new Plan(plans[i, 0], int.Parse(startTime[0])*60 + int.Parse(startTime[1]), int.Parse(plans[i, 2]));
            leftPlans.Add(p);
        }
        
        // 올림차순 정렬
        leftPlans = leftPlans.OrderBy(x => x.startTime).ToList();
        
        // 처음 계획 시작
        Plan progress = leftPlans[0];
        leftPlans.RemoveAt(0);
        
        // 현재 시간은 처음 계획의 시작 시간
        currentTime = progress.startTime;
        
        // 남은 계획이 있다면
        while (leftPlans.Count != 0)
        {
            // 현재 시간 1분 증가시키고 진행 중인 계획의 남은 시간은 1분 감소
            currentTime++;
            progress.leftTime--;
            
            // 진행 중인 계획이 끝났다면
            if (progress.leftTime <= 0)
            {
                answer.Add(progress.name);
                
                // 다음 계획 시작 시간이 됐다면
                if (currentTime >= leftPlans[0].startTime)
                {
                    // 다음 계획 진행
                    progress = leftPlans[0];
                    leftPlans.RemoveAt(0);
                }
                // 다음 계획 시작 시간도 안 됐고 멈춰둔 계획도 없다면
                else if (stopPlans.Count == 0)
                {
                    // 시간을 다음 계획까지 당기고 다음 계획 진행
                    currentTime = leftPlans[0].startTime;
                    progress = leftPlans[0];
                    leftPlans.RemoveAt(0);
                }
                // 멈춰둔 계획이 있다면
                else if (stopPlans.Count != 0)
                {
                    // 최근에 멈춰둔 계획부터 실행
                    progress = stopPlans[stopPlans.Count - 1];
                    stopPlans.RemoveAt(stopPlans.Count - 1);
                }
            }
            // 진행 중인 게획이 있다면
            else
            {
                // 다음 계획 시작 시간이 됐다면
                if (currentTime >= leftPlans[0].startTime)
                {
                    // 진행 중인 계획 멈춰두고 다음 계획 진행
                    stopPlans.Add(progress);
                    progress = leftPlans[0];
                    leftPlans.RemoveAt(0);
                }
            }
        }
        
        // 마지막 남은 계획이 진행되면서 남은 계획 수가 0이 되므로, 직접 추가
        answer.Add(progress.name);
        
        // 멈춰둔 계획이 있다면
        if (stopPlans.Count != 0)
        {
            // 최근에 멈춰둔 계획부터 끝내기
            for (int i = stopPlans.Count - 1; i >= 0; i--)
            {
                answer.Add(stopPlans[i].name);
            }
        }
        
        return answer.ToArray();
    }
}
