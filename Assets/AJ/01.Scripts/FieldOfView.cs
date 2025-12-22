using System;
using _Scripts.Core.Events;
using _Scripts.Core.Structs;
using _Scripts.Core.Utility;
using UnityEngine;

namespace AJ._01.Scripts
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class FieldOfView : MonoBehaviour
    {
        private Director director;
        [Range(0, 360)] [SerializeField] private float fov = 45f;
        [SerializeField] private int rayCount = 90;
        [SerializeField] private float viewDistance = 10f;
        [SerializeField] private LayerMask obstacleLayer;
        [SerializeField] private LayerMask evidence;
        private bool isTargetInFov = false;
        [SerializeField] private ChangeTargetEventChannel onTargetInFov; 
        private Mesh mesh;

        private void Awake()
        {
            if(director == null) director = GetComponentInParent<Director>();
        }

        private void Start()
        {
            mesh = new Mesh
            {
                name = "FOV"
            };
            GetComponent<MeshFilter>().mesh = mesh;
        }
        

        private void RotateFov()
        {
            Vector2 dir = ((Vector2)director.AgentCompo.steeringTarget - (Vector2)transform.position).normalized;
            transform.rotation = Quaternion.Euler(0, 0, GetAngleFromVector(dir));
        }

        private void Update()
        {
            bool direct = IsInFov();
            if (direct && !isTargetInFov)
            {
                isTargetInFov = true;
            }
            else if(!direct && isTargetInFov)
            {
                isTargetInFov = false;
            }
        }

        private void LateUpdate()
        {
            RotateFov();
            DrawFOV();
        }

        private void DrawFOV()
        {
            mesh.Clear();
            
            float startAngle = transform.eulerAngles.z + fov * 0.5f;
            float angleIncrease = fov / rayCount;
            Vector3[] vertices = new Vector3[rayCount + 2];
            Vector2[] uv = new Vector2[vertices.Length];
            int[] triangles = new int[rayCount * 3];

            vertices[0] = Vector3.zero;

            int vertexIndex = 1;
            int triangleIndex = 0;
            float angle = startAngle;
            for (int i = 0; i <= rayCount; i++)
            {
                Vector2 dir = GetVectorFromAngle(angle);

                RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, viewDistance, obstacleLayer);

                Vector2 vertexWorld = hit.collider == null ? (Vector2)transform.position + dir * viewDistance : hit.point;

                vertices[vertexIndex] = transform.InverseTransformPoint(vertexWorld);

                if (i > 0)
                {
                    triangles[triangleIndex + 0] = 0;
                    triangles[triangleIndex + 1] = vertexIndex - 1;
                    triangles[triangleIndex + 2] = vertexIndex;
                    triangleIndex += 3;
                }

                vertexIndex++;
                angle -= angleIncrease;
            }

            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;
            mesh.RecalculateBounds();
        }
        public bool IsInFov()
        {
            Vector2 dir = director.Target.position - transform.position;
            float dist = dir.magnitude;

            if (dist > viewDistance)
                return false;

            if (Vector2.Angle(transform.right, dir) > fov * 0.5f)
                return false;

            RaycastHit2D block = Physics2D.Raycast(transform.position, dir.normalized, dist, obstacleLayer);
            if (block.collider != null)
                return false;

            RaycastHit2D hit = Physics2D.Raycast(transform.position,dir.normalized, dist,evidence);
            if (hit.collider != null)
            {
                onTargetInFov.Raise(hit.transform);
                Logging.Log("FOV hit: " + hit.transform.name);
                return true;
            }
            
            return false;
        }
        private Vector2 GetVectorFromAngle(float angle)
        {
            float angleRad = angle * (Mathf.PI / 180f);
            return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
        }
        private float GetAngleFromVector(Vector2 dir)
        {
            dir = dir.normalized;
            float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            return n;
        }
    }
}
