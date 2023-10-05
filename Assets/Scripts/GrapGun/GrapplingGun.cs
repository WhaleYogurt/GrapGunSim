using UnityEngine;
public class GrapplingGun : MonoBehaviour {

    [System.Serializable]
    public class WeaponVarient
    {
        public string Name;
        public GameObject model;
        public GameObject gunTip;
        public float spring;
        public float damper;
        public float massScale;
        public float inAirMobilityMultiplier;
        public float maxDistance;
    }
    [System.Serializable]
    public class Grip
    {
        public Transform gripLocation;
        public KeyCode FireButton = KeyCode.Mouse0;
    }
    public PlayerMovement playerMovementScript;
    private float baseInAirMobility;
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, cam, player;
    private float maxDistance;
    private SpringJoint joint;
    public float spring, damper, massScale;
    public PlayerMovement playerScript;
    public KeyCode FireGrapplingGun;
    [Header("0 = CenterFire, 1 = LeftHand, 2 = RightHand, 3 = Custom")]
    public int mode = 0;
    public Grip[] PossibleGrips;
    [Header("0 = Normal, 1 = Red")]
    public int varient = 0;
    public WeaponVarient[] varientStats;


    void Awake() {
        lr = GetComponent<LineRenderer>();
        baseInAirMobility = playerMovementScript.inAirMovementSpeedMultiplier;
    }

    void Update() {
        spring = varientStats[varient].spring;
        damper = varientStats[varient].damper;
        massScale = varientStats[varient].massScale;
        foreach (WeaponVarient varientStat in varientStats)
        {
            varientStat.model.SetActive(false);
        }
        varientStats[varient].model.SetActive(true);
        varientStats[varient].model.transform.position = PossibleGrips[mode].gripLocation.position;
        // varientStats[varient].model.transform.rotation = PossibleGrips[mode].gripLocation.rotation;
        maxDistance = varientStats[varient].maxDistance;
        FireGrapplingGun = PossibleGrips[mode].FireButton;
        gunTip = varientStats[varient].gunTip.transform;
        if (Input.GetKeyDown(FireGrapplingGun)) {
            StartGrapple();
        }
        else if (Input.GetKeyUp(FireGrapplingGun) || playerScript.grounded) {
            StopGrapple();
        }
    }

    //Called after Update
    void LateUpdate() {
        DrawRope();
    }

    /// <summary>
    /// Call whenever we want to start a grapple
    /// </summary>
    void StartGrapple() {
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxDistance, whatIsGrappleable)) {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            //The distance grapple will try to keep from grapple point. 
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            //Adjust these values to fit your game.
            joint.spring = spring;
            joint.damper = damper;
            joint.massScale = massScale;

            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;
            playerMovementScript.inAirMovementSpeedMultiplier = varientStats[varient].inAirMobilityMultiplier;
        }
    }


    /// <summary>
    /// Call whenever we want to stop a grapple
    /// </summary>
    void StopGrapple() {
        lr.positionCount = 0;
        Destroy(joint);
        playerMovementScript.inAirMovementSpeedMultiplier = baseInAirMobility;
    }

    private Vector3 currentGrapplePosition;
    
    void DrawRope() {
        //If not grappling, don't draw rope
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);
        
        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, currentGrapplePosition);
    }

    public bool IsGrappling() {
        return joint != null;
    }

    public Vector3 GetGrapplePoint() {
        return grapplePoint;
    }
}
