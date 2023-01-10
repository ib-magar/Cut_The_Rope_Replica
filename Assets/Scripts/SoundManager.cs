using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource _source;
    private void Start()
    {
        _source= GetComponent<AudioSource>();
        GameObject.FindObjectOfType<_cutter>()._RopeCut+=RopeCut;
    }
    void RopeCut()
    {
        _source.Play();
    }
}
