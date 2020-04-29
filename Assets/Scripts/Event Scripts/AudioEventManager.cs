using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioEventManager : MonoBehaviour
{

    public float spikeMaxDistance = 50.0f;
    public float croakMaxDistance = 40.0f;
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
    public AudioClip swordSwing;
    public AudioClip flinchHit;
    public AudioClip eatAppleSFX;
    public AudioClip playerHurtSFX;
    public AudioClip DroneShootSFX;
    public AudioClip bombFootstepSFX;
    public AudioClip checkpointSFX;
    public AudioClip buttonClick;
    public AudioClip startButtonClick;
    public AudioClip targetHitSFX;
    public AudioClip mtnFallSFX;
    public AudioClip croakSFX;
    public AudioClip fanLiftSFX;
    public AudioClip goombaDeathSFX;


    private UnityAction<Vector3> treeCrackEventListener;

    private UnityAction<Vector3> treeHitGroundEventListener;

    private UnityAction<Vector3> gateSwingEventListener;

    private UnityAction<Vector3> jumpSFXEventListener;

    private UnityAction<Vector3> llamaPickupSFXListener;

    private UnityAction<Vector3> spinSFXListener;

    private UnityAction<Vector3> footstepSFXListener;

    private UnityAction<Vector3> boxBreakEventListener;

    private UnityAction<Vector3> spikeExtendSFXListener;

    private UnityAction<Vector3> swordSwingEventListener;

    private UnityAction<Vector3> flinchHitEventListener;

    private UnityAction<Vector3> eatAppleSFXEventListener;

    private UnityAction<Vector3> playerHurtSFXEventListener;

    private UnityAction<Vector3> droneShootSFXEventListener;

    private UnityAction<Vector3> bombFootstepSFXEventListener;

    private UnityAction<Vector3> checkpointSFXEventListener;

    private UnityAction<Vector3> buttonClickEventListener;

    private UnityAction<Vector3> startButtonClickEventListener;

    private UnityAction<Vector3> targetHitSFXEventListener;

    private UnityAction<Vector3> mtnFallSFXEventListener;

    private UnityAction<Vector3> croakSFXEventListener;

    private UnityAction<Vector3> fanLiftSFXEventListener;

    private UnityAction<Vector3> goombaDeathSFXEventListener;

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

        swordSwingEventListener = new UnityAction<Vector3>(swordSwingEventHandler);

        flinchHitEventListener = new UnityAction<Vector3>(FlinchHitEventHandler);

        eatAppleSFXEventListener = new UnityAction<Vector3>(eatAppleSFXEventHandler);

        playerHurtSFXEventListener = new UnityAction<Vector3>(playerHUrtSFXEventHandler);

        droneShootSFXEventListener = new UnityAction<Vector3>(droneShootSFXEventHandler);

        bombFootstepSFXEventListener = new UnityAction<Vector3>(bombFootstepSFXEventHandler);

        checkpointSFXEventListener = new UnityAction<Vector3>(checkpointSFXEventHandler);

        buttonClickEventListener = new UnityAction<Vector3>(ButtonClickEventHandler);

        startButtonClickEventListener = new UnityAction<Vector3>(StartButtonClickEventHandler);

        targetHitSFXEventListener = new UnityAction<Vector3>(targetHitSFXEventHandler);

        mtnFallSFXEventListener = new UnityAction<Vector3>(MtnFallSFXEventHandler);

        croakSFXEventListener = new UnityAction<Vector3>(CroakSFXEventHandler);

        fanLiftSFXEventListener = new UnityAction<Vector3>(FanLiftSFXEventHandler);

        goombaDeathSFXEventListener = new UnityAction<Vector3>(GoombaDeathSFXEventHandler);
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
        EventManager.StartListening<SwordSwing, Vector3>(swordSwingEventListener);
        EventManager.StartListening<FlinchHit, Vector3>(flinchHitEventListener);
        EventManager.StartListening<EatAppleSFXEvent, Vector3>(eatAppleSFXEventListener);
        EventManager.StartListening<PlayerHurtSFXEvent, Vector3>(playerHurtSFXEventListener);
        EventManager.StartListening<DroneShootSFX, Vector3>(droneShootSFXEventListener);
        EventManager.StartListening<BombFootstepSFXEvent, Vector3>(bombFootstepSFXEventListener);
        EventManager.StartListening<CheckpointSFXEvent, Vector3>(checkpointSFXEventListener);
        EventManager.StartListening<ButtonClickEvent, Vector3>(buttonClickEventListener);
        EventManager.StartListening<StartButtonClickEvent, Vector3>(startButtonClickEventListener);
        EventManager.StartListening<TargetHitSFXEvent, Vector3>(targetHitSFXEventListener);
        EventManager.StartListening<MtnFallSFXEvent, Vector3>(mtnFallSFXEventListener);
        EventManager.StartListening<CroakSFXEvent, Vector3>(croakSFXEventListener);
        EventManager.StartListening<FanSFXEvent, Vector3>(fanLiftSFXEventListener);
        EventManager.StartListening<GoombaDeathSFXEvent, Vector3>(goombaDeathSFXEventListener);
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
        EventManager.StopListening<SwordSwing, Vector3>(swordSwingEventListener);
        EventManager.StopListening<FlinchHit, Vector3>(flinchHitEventListener);
        EventManager.StopListening<EatAppleSFXEvent, Vector3>(eatAppleSFXEventListener);
        EventManager.StopListening<PlayerHurtSFXEvent, Vector3>(playerHurtSFXEventListener);
        EventManager.StopListening<DroneShootSFX, Vector3>(droneShootSFXEventListener);
        EventManager.StopListening<BombFootstepSFXEvent, Vector3>(bombFootstepSFXEventListener);
        EventManager.StopListening<CheckpointSFXEvent, Vector3>(checkpointSFXEventListener);
        EventManager.StopListening<ButtonClickEvent, Vector3>(buttonClickEventListener);
        EventManager.StopListening<StartButtonClickEvent, Vector3>(startButtonClickEventListener);
        EventManager.StopListening<TargetHitSFXEvent, Vector3>(targetHitSFXEventListener);
        EventManager.StopListening<MtnFallSFXEvent, Vector3>(mtnFallSFXEventListener);
        EventManager.StopListening<CroakSFXEvent, Vector3>(croakSFXEventListener);
        EventManager.StopListening<FanSFXEvent, Vector3>(fanLiftSFXEventListener);
        EventManager.StopListening<GoombaDeathSFXEvent, Vector3>(goombaDeathSFXEventListener);
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
        sound.audioSrc.minDistance = 1f;
        sound.audioSrc.maxDistance = spikeMaxDistance;
        sound.audioSrc.volume = 0.2f;
        sound.audioSrc.spatialBlend = 1.0f;
        sound.audioSrc.rolloffMode = AudioRolloffMode.Linear;
        sound.audioSrc.Play();
    }

    void swordSwingEventHandler(Vector3 worldPos)
    {
        EventSound3D sound = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);
        sound.audioSrc.clip = swordSwing;
        sound.audioSrc.minDistance = 0f;
        sound.audioSrc.maxDistance = 100f;
        sound.audioSrc.Play();
    }


    void FlinchHitEventHandler(Vector3 worldPos)
    {
        EventSound3D sound = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);
        sound.audioSrc.clip = flinchHit;
        sound.audioSrc.minDistance = 0f;
        sound.audioSrc.maxDistance = 100f;
        sound.audioSrc.volume = 0.5f;
        sound.audioSrc.Play();
    }

    void eatAppleSFXEventHandler(Vector3 worldPos)
    {
        EventSound3D sound = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);
        sound.audioSrc.clip = eatAppleSFX;
        sound.audioSrc.minDistance = 0f;
        sound.audioSrc.maxDistance = 100f;
        sound.audioSrc.volume = 0.2f;
        sound.audioSrc.Play();
    }

    void playerHUrtSFXEventHandler(Vector3 worldPos)
    {
        EventSound3D sound = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);
        sound.audioSrc.clip = playerHurtSFX;
        sound.audioSrc.minDistance = 0f;
        sound.audioSrc.maxDistance = 100f;
        sound.audioSrc.volume = 1.0f;

        sound.audioSrc.Play();
    }

    void droneShootSFXEventHandler(Vector3 worldPos)
    {
        EventSound3D sound = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);
        sound.audioSrc.clip = DroneShootSFX;
        sound.audioSrc.minDistance = 1f;
        sound.audioSrc.maxDistance = spikeMaxDistance;
        sound.audioSrc.volume = 0.2f;
        sound.audioSrc.spatialBlend = 1.0f;
        sound.audioSrc.rolloffMode = AudioRolloffMode.Linear;
        // sound.audioSrc.loop = true;
        sound.audioSrc.Play();
    }

    void bombFootstepSFXEventHandler(Vector3 worldPos)
    {
        EventSound3D sound = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);
        sound.audioSrc.clip = bombFootstepSFX;
        sound.audioSrc.minDistance = 1f;
        sound.audioSrc.maxDistance = spikeMaxDistance;
        sound.audioSrc.volume = 0.2f;
        sound.audioSrc.spatialBlend = 1.0f;
        sound.audioSrc.rolloffMode = AudioRolloffMode.Linear;
        sound.audioSrc.Play();
    }

    void checkpointSFXEventHandler(Vector3 worldPos)
    {
        EventSound3D sound = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);
        sound.audioSrc.clip = checkpointSFX;
        sound.audioSrc.minDistance = 0f;
        sound.audioSrc.maxDistance = 100f;
        sound.audioSrc.volume = 0.15f;
        sound.audioSrc.Play();
    }

    void ButtonClickEventHandler(Vector3 worldPos)
    {
        EventSound3D sound = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);
        sound.audioSrc.clip = buttonClick;
        sound.audioSrc.minDistance = 0f;
        sound.audioSrc.maxDistance = 100f;
        sound.audioSrc.volume = 0.15f;
        sound.audioSrc.Play();
    }

    void StartButtonClickEventHandler(Vector3 worldPos)
    {
        EventSound3D sound = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);
        sound.audioSrc.clip = startButtonClick;
        sound.audioSrc.minDistance = 0f;
        sound.audioSrc.maxDistance = 100f;
        sound.audioSrc.volume = 0.15f;
        sound.audioSrc.Play();
    }

    void targetHitSFXEventHandler(Vector3 worldPos)
    {
        EventSound3D sound = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);
        sound.audioSrc.clip = targetHitSFX;
        sound.audioSrc.minDistance = 0f;
        sound.audioSrc.maxDistance = 100f;
        sound.audioSrc.volume = 0.50f;
        sound.audioSrc.Play();
    }

    void MtnFallSFXEventHandler(Vector3 worldPos)
    {
        EventSound3D sound = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);
        sound.audioSrc.clip = mtnFallSFX;
        sound.audioSrc.minDistance = 0f;
        sound.audioSrc.maxDistance = 100f;
        sound.audioSrc.volume = 0.50f;
        sound.audioSrc.Play();
    }

    void CroakSFXEventHandler(Vector3 worldPos)
    {
        EventSound3D sound = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);
        sound.audioSrc.clip = croakSFX;
        sound.audioSrc.minDistance = 1f;
        sound.audioSrc.maxDistance = croakMaxDistance;
        sound.audioSrc.volume = 0.2f;
        sound.audioSrc.spatialBlend = 1.0f;
        sound.audioSrc.rolloffMode = AudioRolloffMode.Linear;
        sound.audioSrc.Play();
    }

    void FanLiftSFXEventHandler(Vector3 worldPos)
    {
        EventSound3D sound = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);
        sound.audioSrc.clip = fanLiftSFX;
        sound.audioSrc.minDistance = 0f;
        sound.audioSrc.maxDistance = 100f;
        sound.audioSrc.volume = 0.50f;
        sound.audioSrc.Play();
    }

    void GoombaDeathSFXEventHandler(Vector3 worldPos)
    {
        EventSound3D sound = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);
        sound.audioSrc.clip = goombaDeathSFX;
        sound.audioSrc.minDistance = 0f;
        sound.audioSrc.maxDistance = 100f;
        sound.audioSrc.volume = 0.50f;
        sound.audioSrc.Play();
    }
}
