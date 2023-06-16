using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    [field: SerializeField] public int id { get; set; }
    [field: SerializeField] public double mass { get; set; }

    public Vector3 initialVelocity;
    public float _speedNotScaled;

    [SerializeField] GravitySimulation gravitySimulation;

    Vector3d currentVelocityDouble;

    Vector3d positionDouble;

    public float tilt, diameter = 1, rotationTime;

    private void Awake()
    {
        gravitySimulation = FindAnyObjectByType<GravitySimulation>();
    }

    void Start()
    {
        currentVelocityDouble = new Vector3d(initialVelocity);
        positionDouble = new Vector3d(transform.localPosition);
        transform.Rotate(0, 0, tilt);
    }

    private void FixedUpdate()
    {
        Rotation();
    }

    public void NewtonianGravitation(CelestialBody[] bodyToPull)
    {
        foreach (CelestialBody body in bodyToPull)
        {
            if (body != this)
            {
                double distanceBetweenBodiesSqr = (body.positionDouble - positionDouble).sqrMagnitude;
                Vector3 directionBetweenBodies = (body.transform.position - transform.position).normalized;

                double force = ((gravitySimulation.gravitationalConstant * (mass * body.mass)) / distanceBetweenBodiesSqr);
                Vector3d acceleration = new Vector3d(directionBetweenBodies) * (force / mass);

                currentVelocityDouble += acceleration * (Time.fixedDeltaTime * UnitScaling.fixedGameTime);
            }
        }
    }

        public void applyGravity()
    {
        positionDouble += currentVelocityDouble * (Time.fixedDeltaTime * UnitScaling.fixedGameTime);
        transform.localPosition = new Vector3((float)positionDouble.x, (float)positionDouble.y, (float)positionDouble.z);
    }

    void Rotation()
    {
        //360 degrees divided by total rotation time = degrees rotated per second
        double rotation = (360d / rotationTime) * (Time.fixedDeltaTime * UnitScaling.fixedGameTime);
        transform.Rotate(0, (float)rotation, 0);
    }
}
