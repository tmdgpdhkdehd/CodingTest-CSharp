using System;
using System.Collections.Generic;
using System.Linq;

// fees[0]: 기본 시간
// fees[1]: 기본 요금
// fees[2]: 단위 시간(분)
// fees[3]: 단위 요금
public class Solution {
    public int[] solution(int[] fees, string[] records) {
        Dictionary<string, int> inCar = new Dictionary<string, int>();
        Dictionary<string, int> allTime = new Dictionary<string, int>();
        Dictionary<string, int> allCost = new Dictionary<string, int>();
        
        for (int i = 0; i < records.GetLength(0); i++)
        {
            int hour = Int32.Parse(records[i].Substring(0, 2));
            int minute = Int32.Parse(records[i].Substring(3, 2));
            int time = hour * 60 + minute;
            
            string currentCar = records[i].Substring(6, 4);
            string type = records[i].Substring(11, 2);
            
            // 들어오는 차라면
            if (type == "IN")
            {
                inCar.Add(currentCar, time);
                if (!allTime.ContainsKey(currentCar))
                {
                    allTime.Add(currentCar, 0);
                }
            }
            // 나가는 차라면
            else
            {   
                allTime[currentCar] += time - inCar[currentCar];
                inCar.Remove(currentCar);
            }
        }

        for (int i = 0; i < allTime.Count; i++)
        {
            string carName = allTime.Keys.ToArray()[i];
            allCost.Add(carName, 0);
            
            // 출차하지않았다면
            if (inCar.ContainsKey(carName))
            {
                if (1439 - inCar[carName] + allTime[carName] <= fees[0])
                {    
                    allCost[carName] += fees[1];
                }
                else
                {
                    double a = 1439 - inCar[carName] + allTime[carName] - fees[0];
                    double b = Math.Ceiling(a / fees[2]);
                    int ceiling = Convert.ToInt32(b);
                    allCost[carName] += fees[1] + ceiling * fees[3];
                }
            }
            else
            {
                int cost = 0;
                // 주차 요금 = 기본 요금 + ((주차 시간 - 기본 시간) / 단위 시간) * 단위 요금
                if (allTime[carName] <= fees[0])
                    cost = fees[1];
                else
                {
                    double a = allTime[carName] - fees[0];
                    double b = Math.Ceiling(a / fees[2]);
                    int ceiling = Convert.ToInt32(b);
                    cost = fees[1] + ceiling * fees[3];
                }
                allCost[carName] += cost;
            }
        }
        
        var orderCar = allCost.OrderBy(x => x.Key);
        int[] answer = new int[allCost.Count];
        int index = 0;
        foreach (var car in orderCar)
        {
            answer[index] = car.Value;
            index++;
        }
        
        return answer;
    }
}
