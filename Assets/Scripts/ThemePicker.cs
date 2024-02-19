using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemePicker : MonoBehaviour
{
    [SerializeField] GameObject dayBackground;
    [SerializeField] GameObject nightBackground;

    [SerializeField] Sprite[] birdSprites;
    [SerializeField] Animator birdAnimator;

    [SerializeField] RuntimeAnimatorController[] birdAnimators;

    public static bool isNight = false;

    private void Awake()
    {
        SetTheme();
    }

    void SetTheme()
    {
        int randomTheme = Random.Range(0, 2);

        isNight = randomTheme != 0;

        print($"ThemePicker : Random theme number was {randomTheme} - Night is on = {isNight}");
        dayBackground.SetActive(!isNight);
        nightBackground.SetActive(isNight);

        randomTheme = Random.Range(0, birdSprites.Length);

        print($"ThemePicker : Random bird theme number was {randomTheme}");
        birdAnimator.runtimeAnimatorController = birdAnimators[randomTheme];


    }
}
