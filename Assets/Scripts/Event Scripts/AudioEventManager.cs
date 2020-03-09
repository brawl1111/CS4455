﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioEventManager : MonoBehaviour
{

    public EventSound3D eventSound3DPrefab;

    public AudioClip treeCrack;
    public AudioClip treeHitGround;
    public AudioClip gateSwing;
    public AudioClip jumpSFX;
    public AudioClip llamaPickupSFX;
    public AudioClip SpinSFX;
    public AudioClip[] footstepSFX;
    public AudioClip boxBreak;
    public AudioClip spikeExtendSFX;


    private UnityAction<Vector3> treeCrackEventListener;

    private UnityAction<Vector3> treeHitGroundEventListener;

    private UnityAction<Vector3> gateSwingEventListener;

    private UnityAction<Vector3> jumpSFXEventListener;

    private UnityAction<Vector3> llamaPickupSFXListener;

    private UnityAction<Vector3> spinSFXListener;

    private UnityAction<Vector3> footstepSFXListener;

    private UnityAction<Vector3> boxBreakEventListener;

    private UnityAction<Vector3> spikeExtendSFXListener;

    void Awake()
    {
        treeCrackEventListener = new UnityAction<Vector3>(TreeCrackEventHandler);

        treeHitGroundEventListener = new UnityAction<Vector3>(TreeHitGroundEventHandler);

        gateSwingEventListener = new UnityAction<Vector3>(GateSwingEventHandler);

        jumpSFXEventListener = new UnityAction<Vector3>(JumpSFXEventHandler);

        llamaPickupSFXListener = new UnityAction<Vector3>(LlamaPickupSFXEventHandler);

        spinSFXListener = new UnityAction<Vector3>(SpinSFXEventHandler);

        footstepSFXListener = new UnityAction<Vector3>(FootStepSFXEventHandler);

        boxBreakEventListener = new UnityAction<Vector3>(BoxBreakEventHandler);

        spikeExtendSFXListener = new UnityAction<Vector3>(SpikeExtendSFXHandler);
    }


    // Use this for initialization
    void Start()
    {



    }


    void OnEnable()
    {
        EventManager.StartListening<TreeCrackEvent, Vector3>(treeCrackEventListener);
        EventManager.StartListening<TreeHitGroundEvent, Vector3>(treeHitGroundEventListener);
        EventManager.StartListening<GateSwingEvent, Vector3>(gateSwingEventListener);
        EventManager.StartListening<JumpSFXEvent, Vector3>(jumpSFXEventListener);
        EventManager.StartListening<LlamaPickupSFXEvent, Vector3>(llamaPickupSFXListener);
        EventManager.StartListening<SpinSFXEvent, Vector3>(spinSFXListener);
        EventManager.StartListening<FootstepSFXEvent, Vector3>(footstepSFXListener);
        EventManager.StartListening<BreakableBoxBreakEvent, Vector3>(boxBreakEventListener);
        EventManager.StartListening<SpikeExtendSFXEvent, Vector3>(spikeExtendSFXListener);
    }

    void OnDisable()
    {
        EventManager.StopListening<TreeCrackEvent, Vector3>(treeCrackEventListener);
        EventManager.StopListening<TreeHitGroundEvent, Vector3>(treeHitGroundEventListener);
        EventManager.StopListening<GateSwingEvent, Vector3>(gateSwingEventListener);
        EventManager.StopListening<JumpSFXEvent, Vector3>(jumpSFXEventListener);
        EventManager.StopListening<LlamaPickupSFXEvent, Vector3>(llamaPickupSFXListener);
        EventManager.StopListening<SpinSFXEvent, Vector3>(spinSFXListener);
        EventManager.StopListening<FootstepSFXEvent, Vector3>(footstepSFXListener);
        EventManager.StopListening<BreakableBoxBreakEvent, Vector3>(boxBreakEventListener);
        EventManager.StopListening<SpikeExtendSFXEvent, Vector3>(spikeExtendSFXListener);
    }

    void TreeCrackEventHandler(Vector3 worldPos)
    {
        EventSound3D sound = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);
        sound.audioSrc.clip = treeCrack;
        sound.audioSrc.minDistance = 0f;
        sound.audioSrc.maxDistance = 100f;
        sound.audioSrc.Play();
    }

    void TreeHitGroundEventHandler(Vector3 worldPos)
    {
        EventSound3D sound = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);
        sound.audioSrc.clip = treeHitGround;
        sound.audioSrc.minDistance = 0f;
        sound.audioSrc.maxDistance = 100f;
        sound.audioSrc.volume = 0.2f;
        sound.audioSrc.Play();
    }

    void GateSwingEventHandler(Vector3 worldPos)
    {
        EventSound3D sound = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);
        sound.audioSrc.clip = gateSwing;
        sound.audioSrc.minDistance = 0f;
        sound.audioSrc.maxDistance = 100f;
        sound.audioSrc.Play();
    }

    void JumpSFXEventHandler(Vector3 worldPos)
    {
        EventSound3D sound = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);
        sound.audioSrc.clip = jumpSFX;
        sound.audioSrc.minDistance = 0f;
        sound.audioSrc.maxDistance = 100f;
        sound.audioSrc.volume = 0.7f;
        sound.audioSrc.Play();
    }

    void LlamaPickupSFXEventHandler(Vector3 worldPos)
    {
        EventSound3D sound = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);
        sound.audioSrc.clip = llamaPickupSFX;
        sound.audioSrc.minDistance = 0f;
        sound.audioSrc.maxDistance = 100f;
        sound.audioSrc.volume = 0.2f;
        sound.audioSrc.Play();
    }

    void SpinSFXEventHandler(Vector3 worldPos)
    {
        EventSound3D sound = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);
        sound.audioSrc.clip = SpinSFX;
        sound.audioSrc.minDistance = 0f;
        sound.audioSrc.maxDistance = 100f;
        sound.audioSrc.Play();
    }

    void FootStepSFXEventHandler(Vector3 worldPos)
    {
        EventSound3D sound = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);
        sound.audioSrc.clip = this.footstepSFX[Random.Range(0, footstepSFX.Length)];
        sound.audioSrc.minDistance = 0f;
        sound.audioSrc.maxDistance = 100f;
        sound.audioSrc.volume = 0.2f;
        sound.audioSrc.Play();
    }

    void BoxBreakEventHandler(Vector3 worldPos)
    {
        EventSound3D sound = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);
        sound.audioSrc.clip = boxBreak;
        sound.audioSrc.minDistance = 0f;
        sound.audioSrc.maxDistance = 100f;
        sound.audioSrc.volume = 0.2f;
        sound.audioSrc.Play();
    }

    void SpikeExtendSFXHandler(Vector3 worldPos)
    {
        EventSound3D sound = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);
        sound.audioSrc.clip = spikeExtendSFX;
        sound.audioSrc.minDistance = 0f;
        sound.audioSrc.maxDistance = 100f;
        sound.audioSrc.volume = 0.07f;
        sound.audioSrc.spatialBlend = 0.5f;
        sound.audioSrc.rolloffMode = AudioRolloffMode.Linear;
        //sound.audioSrc.Play();
    }
}
