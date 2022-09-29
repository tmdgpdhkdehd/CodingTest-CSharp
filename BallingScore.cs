using System;

namespace ConsoleApp7
{
    enum ScoreType
    {
        Open,
        Strike,
        Double,
        Triple,
        Spare
    }

    class Game
    {
        int totalRound = 10;                // 전체 라운드 개수
        double currentRound = 0;            // 분기별 현재 라운드
        int round = 0;                      // 현재 라운드
        bool isFinal = false;               // 게임 종료 여부
        int[] score = new int[10];          // 각 라운드 점수
        int[,] half = new int[10, 3];        // 각 라운드에 쓰러트린 핀 개수
        ScoreType beforeType = 0;           // 이전 라운드의 득점 타입

        public void KnockedDownPins(int downPin)
        {
            if (isFinal)
            {
                Console.WriteLine("경기가 끝났습니다!");
                return;
            }

            currentRound += 0.5;
            round = (int)Math.Ceiling(currentRound);

            if (downPin < 0 || downPin > 10)
            {
                ErrorMessage();
                return;
            }

            // 10번째 라운드가 11번째로 넘어가지 않게 하기 위해
            if (currentRound >= totalRound)
            {
                round = totalRound;
            }

            score[round - 1] = -1;

            // 라운드의 마지막 분기이며 마지막 라운드가 아닐 때
            if (currentRound % 1 == 0 && round < totalRound)
            {
                half[round - 1, 1] = downPin;

                if (half[round - 1, 0] + half[round - 1, 1] > 10)
                {
                    half[round - 1, 1] = -1;
                    ErrorMessage();
                    return;
                }

                if (round >= 2)
                {
                    GetScore(beforeType, downPin);
                }

                if (downPin == 10 || half[round - 1, 0] + downPin == 10)
                {
                    beforeType = ScoreType.Spare;
                }
                else
                {
                    beforeType = ScoreType.Open;

                    GetScore(beforeType, downPin);
                }
            }
            // 라운드의 첫 분기이며 마지막 라운드가 아닐 때
            else if (currentRound % 1 != 0 && round < totalRound)
            {
                half[round - 1, 0] = downPin;
                half[round - 1, 1] = -1;

                if (round >= 2)
                {
                    GetScore(beforeType, downPin);
                }

                if (downPin == 10)
                {
                    if (round >= 3 && beforeType == ScoreType.Double)
                    {
                        beforeType = ScoreType.Triple;

                        GetScore(beforeType, downPin);
                    }
                    else if (round >= 2 && beforeType == ScoreType.Strike)
                    {
                        beforeType = ScoreType.Double;
                    }
                    else
                    {
                        beforeType = ScoreType.Strike;
                    }
                    currentRound += 0.5;        // 스트라이크일 때는 바로 다음 라운드로 넘어가게
                }
            }
            // 마지막 라운드일 때
            else
            {
                beforeType = ScoreType.Open;        // 10번째 라운드의 점수 계산은 다른 라운드와 다르기 때문에 편의를 위해 ScoreType.Open 적용

                switch (currentRound)
                {
                    case 9.5:
                        half[round - 1, 0] = downPin;
                        half[round - 1, 1] = -1;
                        half[round - 1, 2] = -1;
                        break;
                    case 10.0:
                        half[round - 1, 1] = downPin;
                        if (half[round - 1, 0] + half[round - 1, 1] > 10 && half[round - 1, 0] != 10)
                        {
                            half[round - 1, 1] = -1;
                            ErrorMessage();
                            return;
                        }
                        if (half[round - 1, 0] + half[round - 1, 1] != 10 && half[round - 1, 1] != 10)
                        {
                            isFinal = true;
                        }
                        break;
                    case 10.5:
                        half[round - 1, 2] = downPin;
                        isFinal = true;
                        break;
                    default:
                        break;
                }
                GetScore(beforeType, downPin);
            }

            SetPinText();
            SetScoreText();
        }

        // 득점
        public int GetScore(ScoreType type, int downPin)
        {
            switch (type)
            {
                case ScoreType.Open:
                    // 10번째 라운드일 때
                    if (round >= totalRound)
                    {
                        // 첫 분기
                        if (currentRound == 9.5)
                        {
                            if (downPin == 10 && half[round - 2, 0] == 10 && half[round - 3, 0] == 10)
                                GetScore(ScoreType.Triple, downPin);
                            else if (downPin != 10 && half[round - 2, 0] == 10)
                                GetScore(ScoreType.Triple, downPin);
                            return score[round - 1] = score[round - 2] + half[round - 1, 0];
                        }
                        // 중간 분기
                        else if (currentRound == 10.0)
                        {
                            if (downPin == 10 && half[round - 1, 0] == 10 && half[round - 2, 0] == 10)
                                score[round - 2] = score[round - 3] + 30;
                            else if (downPin != 10 && half[round - 1, 0] == 10 && half[round - 2, 0] == 10)
                                score[round - 2] = score[round - 3] + 20 + half[round - 1, 1];
                            else if (downPin != 10 && half[round - 1, 0] != 10 && half[round - 2, 0] == 10)
                                score[round - 2] = score[round - 3] + 10 + half[round - 1, 0] + half[round - 1, 1];
                            return score[round - 1] = score[round - 2] + half[round - 1, 0] + half[round - 1, 1];
                        }
                        // 마지막 분기
                        else
                        {
                            return score[round - 1] = score[round - 2] + half[round - 1, 0] + half[round - 1, 1] + half[round - 1, 2];
                        }
                    }

                    if (currentRound % 1 == 0)
                    {
                        if (round == 1)
                            return score[round - 1] = half[round - 1, 0] + half[round - 1, 1];
                        else
                            return score[round - 1] = score[round - 2] + half[round - 1, 0] + half[round - 1, 1];
                    }
                    break;
                case ScoreType.Strike:
                    if (currentRound % 1 == 0 && downPin != 10)
                    {
                        if (round == 2)
                            return score[round - 2] = 10 + half[round - 1, 0] + half[round - 1, 1];
                        else
                            return score[round - 2] = score[round - 3] + 10 + half[round - 1, 0] + half[round - 1, 1];
                    }
                    break;
                case ScoreType.Double:
                    if (currentRound % 1 == 0 && downPin != 10)
                    {
                        if (round == 3)
                            return score[round - 2] = 10 + half[round - 1, 0] + half[round - 1, 1];
                        else
                            return score[round - 2] = score[round - 3] + 10 + half[round - 1, 0] + half[round - 1, 1];
                    }
                    else if (currentRound % 1 != 0 && downPin != 10)
                    {
                        if (round == 3)
                            return score[round - 3] = 20 + half[round - 1, 0];
                        else
                            return score[round - 3] = score[round - 4] + 20 + half[round - 1, 0];
                    }
                    break;
                case ScoreType.Triple:
                    if (downPin == 10)
                    {
                        if (round == 3)
                        {
                            return score[round - 3] = 30;
                        }
                        else
                        {
                            return score[round - 3] = score[round - 4] + 30;
                        }
                    }
                    else if (currentRound % 1 == 0 && downPin != 10)
                    {
                        return score[round - 2] = score[round - 3] + 10 + half[round - 1, 0] + half[round - 1, 1];
                    }
                    else if (currentRound % 1 != 0 && downPin != 10)
                    {
                        return score[round - 3] = score[round - 4] + 20 + half[round - 1, 0];
                    }
                    break;
                case ScoreType.Spare:
                    if (currentRound % 1 != 0)
                    {
                        if (round == 2)
                            return score[round - 2] = 10 + half[round - 1, 0];
                        else
                            return score[round - 2] = score[round - 3] + 10 + half[round - 1, 0];
                    }
                    break;
                default:
                    break;
            }
            return 0;
        }

        // 핀 텍스트 세팅
        public void SetPinText()
        {
            // 쓰러트린 핀
            for (int i = 1; i <= totalRound; i++)
            {
                if (i >= totalRound)
                {
                    // 10번째 라운드일 때
                    if (round == totalRound)
                    {
                        string[] XText = new string[3];

                        for (int j = 0; j < 3; j++)
                        {
                            if (half[i - 1, j] == 0)
                                XText[j] = "-";
                            else if (half[i - 1, j] == 10)
                                XText[j] = "X";
                            else if (half[i - 1, j] != -1)
                                XText[j] = half[i - 1, j].ToString();
                            else
                                XText[j] = " ";

                            if (half[i - 1, 0] + half[i - 1, 1] == 10)
                                XText[1] = "/";
                        }

                        if (currentRound % 1 == 0)
                        {
                            Console.Write($"{i}:[{XText[0]},{XText[1]}, ]  ");
                        }
                        else
                        {
                            Console.Write($"{i}:[{XText[0]},{XText[1]},{XText[2]}]  ");
                        }
                    }
                    else
                    {
                        Console.Write($"{i}:[     ]");
                    }
                }
                // 라운드가 i보다 같거나 클 때 (점수가 있을 때)
                else if (round >= i)
                {
                    if (half[i - 1, 0] == 10)
                    {
                        Console.Write($"{i}:[ X ]  ");
                    }
                    else if (half[i - 1, 0] + half[i - 1, 1] == 10)
                        Console.Write($"{i}:[{half[i - 1, 0]},/]  ");
                    else if (half[i - 1, 0] == 0 && half[i - 1, 1] == 0)
                        Console.Write($"{i}:[-,-]  ");
                    else if (half[i - 1, 0] == 0 && half[i - 1, 1] != -1)
                        Console.Write($"{i}:[-,{half[i - 1, 1]}]  ");
                    else if (half[i - 1, 0] == 0 && half[i - 1, 1] == -1)
                        Console.Write($"{i}:[-, ]  ");
                    else if (half[i - 1, 0] != 0 && half[i - 1, 1] == 0)
                        Console.Write($"{i}:[{half[i - 1, 0]},-]  ");
                    else if (half[i - 1, 1] == -1)
                        Console.Write($"{i}:[{half[i - 1, 0]}, ]  ");
                    else
                        Console.Write($"{i}:[{half[i - 1, 0]},{half[i - 1, 1]}]  ");
                }
                else
                    Console.Write($"{i}:[   ]  ");
            }

            Console.WriteLine("");
    }

    // 득점 텍스트 세팅
    public void SetScoreText()
    {
            for (int i = 1; i <= totalRound; i++)
            {
                if (score[i - 1] == -1)
                {
                    Console.Write($"  [   ]  ");
                    continue;
                }

                if (i >= totalRound)
                {
                    if (currentRound > 9.5 && half[round - 1, 0] + half[round - 1, 1] != 10 && half[round - 1, 1] != 10 || currentRound == 10.5)
                    {
                        string scoreText = score[i - 1].ToString();
                        Console.Write($"   [{scoreText.PadLeft(5)}]  ");
                    }
                    else
                        Console.Write($"   [     ]");
                }
                else if (round >= i)
                {
                    string scoreText = score[i - 1].ToString();
                    Console.Write($"  [{scoreText.PadLeft(3)}]  ");
                }
                else
                    Console.Write($"  [   ]  ");
            }

            Console.WriteLine("");
        }

        // 에러 메시지 출력 및 해당 라운드 분기 무효
        public void ErrorMessage()
        {
            currentRound -= 0.5;
            Console.WriteLine("잘못된 값이 입력되었습니다!");
        }
    }

    class Program
    {
        public static void Main()
        {
            var game = new Game();
            game.KnockedDownPins(4);
            game.KnockedDownPins(6);
            game.KnockedDownPins(5);
            game.KnockedDownPins(5);
            game.KnockedDownPins(10);
            game.KnockedDownPins(6);
        }
    }
}
