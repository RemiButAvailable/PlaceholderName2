using UnityEngine;

public static class Global
{
    public static bool inTutorial = false;
    public static int topWave = 0;
    public static int curWave = 0;

    public static void GameOver(int wave) {
        curWave = wave;
        if (wave > topWave) {
            topWave = wave;
        }
    }

}
