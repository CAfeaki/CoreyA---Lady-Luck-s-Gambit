using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSFX : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> sfxList;
    private int indexNum;

    public void PlaySFX()
    {
        indexNum = Random.Range(0, sfxList.Count - 1);
        audioSource.clip = sfxList[indexNum];
        audioSource.Play();
    }
}
