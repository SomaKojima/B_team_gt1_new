using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleArray 
{
    public static void shuffle(int[] randomBuf ,int size)
    {
        for (int i = 0; i < size; i++)
        {
            int maxNum = (int)size;
            int randomOne = Random.Range(0, (maxNum - 1) * 100) % maxNum;
            int randomTwo = Random.Range(0, (maxNum - 1) * 100) % maxNum;

            // 入れ替え処理
            int buf = randomBuf[randomOne];
            randomBuf[randomOne] = randomBuf[randomTwo];
            randomBuf[randomTwo] = buf;
        }
    }
}
