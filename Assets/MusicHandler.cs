using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    [SerializeField] private List<AudioSource> instruments;
    public void AddInstrument(int instrumentToAdd)
    {
        instruments[instrumentToAdd].mute = false;
    }

    public void MuteInstrument(int instrumentToRemove)
    {
        instruments[instrumentToRemove].mute = true;
    }
}
