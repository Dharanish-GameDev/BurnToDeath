using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _fireBallSFX;
    [SerializeField] private AudioSource _blindSFX;
    [SerializeField] private AudioSource _forwardSliceSFX;
    [SerializeField] private AudioSource _healthClaimSFX;

    public void PlayingFireBallSFX()
    {
        if(!_fireBallSFX.isPlaying)
        {
            _fireBallSFX.Play();
        }
    }
    public void PlayingBlindSFX()
    {
        if(!_blindSFX.isPlaying)
        {
            _blindSFX.Play();
        }
    }
    public void PlayingSwordSFX()
    {
        _forwardSliceSFX.Play();
    }
    public void PlayingHpClaimSFX()
    {
        if(!_healthClaimSFX.isPlaying)
        {
            _healthClaimSFX.Play();
        }
    }
}
