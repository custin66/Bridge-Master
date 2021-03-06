using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineEffectController : MonoBehaviour
{
    //#region Singleton

    //private static MachineEffectController instance = null;

    //// Game Instance Singleton
    //public static MachineEffectController Instance
    //{
    //    get
    //    {
    //        return instance;
    //    }
    //}

    //private void Awake()
    //{
    //    // if the singleton hasn't been initialized yet
    //    if (instance != null && instance != this)
    //    {
    //        Destroy(this.gameObject);
    //    }

    //    instance = this;
    //    DontDestroyOnLoad(this.gameObject);
    //}
    //#endregion


    [SerializeField] GameObject tamOturduParticle;
    private ParticleSystem particleSystem;

    void Awake()
    {
        particleSystem = tamOturduParticle.GetComponent<ParticleSystem>();
    }
    public void TrueHitParticlePlay()
    {
        particleSystem.Play();
    }
}
