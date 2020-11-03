using Photon.Pun;
using UnityEngine;

namespace Arcaedion.Multiplayer { 

    [RequireComponent(typeof(Rigidbody))]
    public class ControleJogadorNet : MonoBehaviourPunCallbacks
    {
        [SerializeField] private float _velocidadeDeMovimento = 5;
        [SerializeField] private float _forcaDoPulo = 300;
        [SerializeField] private Rigidbody _rb;

        private ControleJogador _jogador = null;
        private Camera _camera;

        public Rigidbody Rb { get => _rb; set => _rb = value; }

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _camera = Camera.main;
        }

        private void Update()
        {
            Move();
            Pula();

            if (Input.GetButton("Fire1") && _jogador != null)
            {
                Ray cameraRay = _camera.ScreenPointToRay(Input.mousePosition);
                Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
                float rayLength;

                if (groundPlane.Raycast(cameraRay, out rayLength))
                {
                    Vector3 pointToLook = cameraRay.GetPoint(rayLength);
                    Debug.DrawLine(cameraRay.origin, pointToLook, Color.cyan);

                    //transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
                    _jogador.Rb.AddForce(new Vector3(pointToLook.x * 100, transform.position.y, pointToLook.z * 100));
                }
            }
        }

        private void Pula()
        {
            if (Input.GetButtonDown("Jump"))
            {
                var ray = new Ray(transform.position, Vector3.down);

                if (Physics.Raycast(ray, 1f))
                {
                    Rb.AddForce(Vector3.up * _forcaDoPulo, ForceMode.Impulse);

                }
            }
        }

        private void Move()
        {
            var x = Input.GetAxis("Horizontal") * _velocidadeDeMovimento;
            var z = Input.GetAxis("Vertical") * _velocidadeDeMovimento;

            Rb.velocity = new Vector3(x, Rb.velocity.y, z);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag.Equals("Player"))
                _jogador = other.gameObject.GetComponent<ControleJogador>();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag.Equals("Player"))
                _jogador = null;
        }
    }
}