using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TradeValley.Misc
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;

        Camera cam;
        private float xMax, xMin, yMax, yMin;
        [SerializeField] private Tilemap tilemap;

        /// <summary>
        /// Returns the min and max tile vector3 when start the camera
        /// </summary>
        public static Action<Vector3,Vector3> ON_STAR_CAMERA;

        void Start()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            
            cam = Camera.main;

            Vector3 minTile = tilemap.CellToWorld(tilemap.cellBounds.min);
            Vector3 maxTile = tilemap.CellToWorld(tilemap.cellBounds.max);

            SetLimits(maxTile, minTile);
            
            if(ON_STAR_CAMERA != null) ON_STAR_CAMERA(minTile, maxTile);
        }

        void LateUpdate()
        {
            transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin, xMax), Mathf.Clamp(target.position.y, yMin, yMax), -10);
        }

        private void SetLimits(Vector3 maxTile, Vector3 minTile)
        {
            float heigth = 2f * cam.orthographicSize;
            float widht = heigth * cam.aspect;

            xMin = minTile.x + widht / 2;
            xMax = maxTile.x - widht / 2;

            yMin = minTile.y + heigth / 2;
            yMax = maxTile.y - heigth / 2;
            
        }
    }
}