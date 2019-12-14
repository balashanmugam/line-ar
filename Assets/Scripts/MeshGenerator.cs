using UnityEngine;


namespace LineAR {
    public class MeshGenerator : MonoBehaviour {
        [SerializeField] private bool startGrow = false;
        [SerializeField] private bool isPlayer;

        private float currentAngle = 0;
        private float angle = 10;
        private const float turnAngle = 2;

        [SerializeField] private float TIMESTEP = 0.01f;
        [SerializeField] private float timer = 0.5f;
        [SerializeField] private float horizontal;

        [SerializeField] private GameObject unitCircle;
        [SerializeField] private GameObject rbCirclePrefab;
        [SerializeField] private GameObject explosionPrefab;
        [SerializeField] private GameObject last;
        [SerializeField] private GameObject rbCircle;

        [SerializeField] private PlayerInput input;
        [SerializeField] private AudioSource explosionAudio;


        public bool StartGrow
        {
            get => startGrow;
            set {
                startGrow = value;
                if (value == false) {
                    SpawnExplosion();
                }
            }
        }

        public GameObject Last
        {
            get => last;
            set => last = value;
        }

        public GameObject RbCircle
        {
            get => rbCircle;
            set => rbCircle = value;
        }

        public bool IsPlayer
        {
            get => isPlayer;
            set => isPlayer = value;
        }

        public void SpawnExplosion() {
            // Spawn explosion at the point of collision
            var explosion = Instantiate(explosionPrefab, rbCircle.transform.position, rbCircle.transform.rotation);
            explosionAudio.Play();
        }

        private void Awake() {
            last = this.gameObject;

            input = GetComponent<PlayerInput>();

            // instantiate RB circle
            rbCircle = Instantiate(rbCirclePrefab, last.transform.position + (last.transform.forward * (0.005f * 2)),
                last.transform.rotation, gameObject.transform);

            currentAngle = last.transform.rotation.eulerAngles.y;
            Spawn(last.transform.rotation);
        }

        private void Update() {
            if (!startGrow) return;
            if (!isPlayer) return;

            // get input from the PlayerInput class
            horizontal = input.Horizontal;
            angle = 0;
            if (horizontal < 0) {
                // turn left
                angle = -turnAngle;
            }
            else if (horizontal > 0) {
                // turn right
                angle = turnAngle;
            }
        }

        private void FixedUpdate() {
            // keep spawning
            if (!startGrow) return;

            timer -= Time.fixedDeltaTime;
            if (timer <= 0) {
                currentAngle += angle;
                Spawn(Quaternion.Euler(0, currentAngle, 0));
                timer = TIMESTEP;
            }
        }

        private void Spawn(Quaternion rot) {
            //spawn a small cylinder

            // Spawn that single rigidbody infront of this.
            rbCircle.transform.position = last.transform.position + (last.transform.forward * (0.005f * 2f));
            rbCircle.transform.rotation = last.transform.rotation;

            var obj = Instantiate(unitCircle, last.transform.position + (last.transform.forward * 0.005f * 0.5f),
                rot, gameObject.transform);

            if (obj != null) {
                last = obj;
            }
        }
    }
}