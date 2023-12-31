using DigitalRuby.ThunderAndLightning;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrike : MonoBehaviour
{
    [SerializeField] LightningBoltPrefabScript strikeLightning;
    [SerializeField] SoundPlayer strikeSound;
    [SerializeField] float boltOriginHeight;
    public float lastTime;

    [ContextMenu("Play")]
    public void Play()
    {
        strikeLightning.Camera = Camera.main;
        strikeLightning.Trigger(new Vector3(transform.position.x, boltOriginHeight, transform.position.z), transform.position);
        strikeSound.Play();
    }
}
