using System.Collections;
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
    public AudioClip llamaPickup;
    /*
    public AudioClip[] minionJabberAudio = null;
    public AudioClip[] boxAudio = null;
    public AudioClip playerLandsAudio;
    public AudioClip explosionAudio;
    public AudioClip deathAudio;
    public AudioClip bombBounceAudio;
    public AudioClip jumpAudio;
    public AudioClip gruntAudio;
    public AudioClip minionDeathAudio;
    public AudioClip minionOuchAudio;
    public AudioClip minionSpawnAudio;
    public AudioClip[] minionFootstepAudio;
    public AudioClip punchAudio;

    */
    private UnityAction<Vector3> treeCrackEventListener;

    private UnityAction<Vector3> treeHitGroundEventListener;

    private UnityAction<Vector3> gateSwingEventListener;

    private UnityAction<Vector3> jumpSFXEventListener;

    private UnityAction<Vector3> llamaPickupSFXListener;
    /*

    private UnityAction<Vector3,float> boxCollisionEventListener;

    private UnityAction<Vector3, float> playerLandsEventListener;

    private UnityAction<Vector3, float> minionLandsEventListener;

    private UnityAction<Vector3> minionJabberEventListener;

    private UnityAction<Vector3> explosionEventListener;

    private UnityAction<Vector3> bombBounceEventListener;

    private UnityAction<Vector3> jumpEventListener;

    private UnityAction<GameObject> deathEventListener;

    private UnityAction<Vector3,MinionScript> minionDeathEventListener;

    private UnityAction<MinionScript> minionSpawnEventListener;

    private UnityAction<Vector3> minionFootstepEventListener;

    private UnityAction<Vector3> minionOuchEventListener;
    */

    void Awake()
    {
        treeCrackEventListener = new UnityAction<Vector3>(TreeCrackEventHandler);

        treeHitGroundEventListener = new UnityAction<Vector3>(TreeHitGroundEventHandler);

        gateSwingEventListener = new UnityAction<Vector3>(GateSwingEventHandler);

        jumpSFXEventListener = new UnityAction<Vector3>(JumpSFXEventHandler);

        llamaPickupSFXListener = new UnityAction<Vector3>(LlamaPickupSFXEventHandler);
        /*
        boxCollisionEventListener = new UnityAction<Vector3,float>(boxCollisionEventHandler);

        playerLandsEventListener = new UnityAction<Vector3, float>(playerLandsEventHandler);

        minionLandsEventListener = new UnityAction<Vector3, float>(minionLandsEventHandler);

        explosionEventListener = new UnityAction<Vector3>(explosionEventHandler);

        minionJabberEventListener = new UnityAction<Vector3>(minionJabberEventHandler);

        bombBounceEventListener = new UnityAction<Vector3>(bombBounceEventHandler);

        jumpEventListener = new UnityAction<Vector3>(jumpEventHandler);

        deathEventListener = new UnityAction<GameObject>(deathEventHandler);

        minionDeathEventListener = new UnityAction<Vector3,MinionScript>(minionDeathEventHandler);

        minionSpawnEventListener = new UnityAction<MinionScript>(minionSpawnEventHandler);

        minionFootstepEventListener = new UnityAction<Vector3>(minionFootstepEventHandler);

        minionOuchEventListener = new UnityAction<Vector3>(minionOuchEventHandler);
        */
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
        /*
        EventManager.StartListening<BoxCollisionEvent, Vector3,float>(boxCollisionEventListener);
        EventManager.StartListening<PlayerLandsEvent, Vector3, float>(playerLandsEventListener);
        EventManager.StartListening<MinionLandsEvent, Vector3, float>(minionLandsEventListener);
        EventManager.StartListening<MinionJabberEvent, Vector3>(minionJabberEventListener);
        EventManager.StartListening<ExplosionEvent, Vector3>(explosionEventListener);
        EventManager.StartListening<BombBounceEvent, Vector3>(bombBounceEventListener);
        EventManager.StartListening<JumpEvent, Vector3>(jumpEventListener);
        EventManager.StartListening<DeathEvent, GameObject>(deathEventListener);
        EventManager.StartListening<MinionDeathEvent, Vector3, MinionScript>(minionDeathEventListener);
        EventManager.StartListening<MinionSpawnEvent, MinionScript>(minionSpawnEventListener);
        EventManager.StartListening<MinionFootstepEvent, Vector3>(minionFootstepEventListener);
        EventManager.StartListening<MinionOuchEvent, Vector3>(minionOuchEventListener);
        */

    }

    void OnDisable()
    {
        EventManager.StopListening<TreeCrackEvent, Vector3>(treeCrackEventListener);
        EventManager.StopListening<TreeHitGroundEvent, Vector3>(treeHitGroundEventListener);
        EventManager.StopListening<GateSwingEvent, Vector3>(gateSwingEventListener);
        EventManager.StopListening<JumpSFXEvent, Vector3>(jumpSFXEventListener);
        EventManager.StopListening<LlamaPickupSFXEvent, Vector3>(llamaPickupSFXListener);
        /*
        EventManager.StopListening<BoxCollisionEvent, Vector3,float>(boxCollisionEventListener);
        EventManager.StopListening<PlayerLandsEvent, Vector3, float>(playerLandsEventListener);
        EventManager.StopListening<MinionLandsEvent, Vector3, float>(minionLandsEventListener);
        EventManager.StopListening<MinionJabberEvent, Vector3>(minionJabberEventListener);
        EventManager.StopListening<ExplosionEvent, Vector3>(explosionEventListener);
        EventManager.StopListening<BombBounceEvent, Vector3>(bombBounceEventListener);
        EventManager.StopListening<JumpEvent, Vector3>(jumpEventListener);
        EventManager.StopListening<DeathEvent, GameObject>(deathEventListener);
        EventManager.StopListening<MinionDeathEvent, Vector3, MinionScript>(minionDeathEventListener);
        EventManager.StopListening<MinionSpawnEvent, MinionScript>(minionSpawnEventListener);
        EventManager.StopListening<MinionFootstepEvent, Vector3>(minionFootstepEventListener);
        EventManager.StopListening<MinionOuchEvent, Vector3>(minionOuchEventListener);
        */
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
        sound.audioSrc.volume = 0.5f;
        sound.audioSrc.Play();
    }

    void LlamaPickupSFXEventHandler(Vector3 worldPos)
    {
        EventSound3D sound = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);
        sound.audioSrc.clip = llamaPickup;
        sound.audioSrc.minDistance = 0f;
        sound.audioSrc.maxDistance = 100f;
        sound.audioSrc.Play();
    }

    /*
    void boxCollisionEventHandler(Vector3 worldPos, float impactForce)
    {
        //AudioSource.PlayClipAtPoint(this.boxAudio, worldPos);

        const float halfSpeedRange = 0.2f;

        EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

        snd.audioSrc.clip = this.boxAudio[Random.Range(0,boxAudio.Length)];

        snd.audioSrc.pitch = Random.Range(1f-halfSpeedRange, 1f+halfSpeedRange);

        snd.audioSrc.minDistance = Mathf.Lerp(1f, 8f, impactForce /200f);
        snd.audioSrc.maxDistance = 100f;

        snd.audioSrc.Play();
    }

    void explosionEventHandler(Vector3 worldPos)
    {
        //AudioSource.PlayClipAtPoint(this.explosionAudio, worldPos, 1f);

        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

            snd.audioSrc.clip = this.explosionAudio;

            snd.audioSrc.minDistance = 50f;
            snd.audioSrc.maxDistance = 500f;

            snd.audioSrc.Play();
        }
    }

    void bombBounceEventHandler(Vector3 worldPos)
    {
        //AudioSource.PlayClipAtPoint(this.explosionAudio, worldPos, 1f);

        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

            snd.audioSrc.clip = this.bombBounceAudio;

            snd.audioSrc.minDistance = 10f;
            snd.audioSrc.maxDistance = 500f;

            snd.audioSrc.Play();
        }
    }
    */
}
