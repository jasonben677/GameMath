using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SAT
{
    public class Collision_SAT : MonoBehaviour
    {
        public Transform a;
        public Transform b;

        private SpriteRenderer a_s;
        private SpriteRenderer b_s;

        private Vector2 a_size;
        private Vector2 b_size;


        private void Awake()
        {
            a_s = a.GetComponent<SpriteRenderer>();
            a_size = new Vector2(a_s.sprite.rect.width, a_s.sprite.rect.height);

            b_s = b.GetComponent<SpriteRenderer>();
            b_size = new Vector2(b_s.sprite.rect.width, b_s.sprite.rect.height);
        }


        private void Start()
        {


        }


        // Update is called once per frame
        void Update()
        {
            if (CheckCollision(ToObb(a, a_size, a_s), ToObb(b, b_size, b_s)))
            {
                Debug.LogError("Collision");
            }
            else
            {
                Debug.Log("No");
            }
        }

        private Obb ToObb(Transform trans, Vector2 _size, SpriteRenderer _render)
        {
            Vector2 realSize = new Vector2(_size.x * trans.localScale.x / (_render.sprite.pixelsPerUnit * 2), _size.y * trans.localScale.y / (_render.sprite.pixelsPerUnit * 2));
            return new Obb(trans.position, realSize, trans.rotation);
        }


        private bool CheckCollision(Obb _a, Obb _b)
        {
            if (Separted(_a, _b, _a.right))
            {
                return false;
            }

            if (Separted(_a, _b, _a.up))
            {
                return false;
            }

            if (Separted(_a, _b, _b.right))
            {
                return false;
            }

            if (Separted(_a, _b, _b.up))
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// 將頂點投影在參考軸上
        /// </summary>
        /// <param name="a">a的頂點</param>
        /// <param name="b">b的頂點</param>
        /// <param name="axis">參考軸</param>
        /// <returns></returns>
        private bool Separted(Obb a, Obb b, Vector3 axis)
        {
            // 投影到axis
            float aMax = float.MinValue;
            float aMin = float.MaxValue;

            float bMax = float.MinValue;
            float bMin = float.MaxValue;

            for (int i = 0; i < a.vertice.Length; i++)
            {
                float adist = Vector3.Dot(a.vertice[i], axis);
                aMax = (adist > aMax) ? adist : aMax;
                aMin = (adist < aMin) ? adist : aMin;

                float bdist = Vector3.Dot(b.vertice[i], axis);
                bMax = (bdist > bMax) ? bdist : bMax;
                bMin = (bdist < bMin) ? bdist : bMin;
            }

            float total = Mathf.Max(aMax, bMax) - Mathf.Min(aMin, bMin);
            float sum = (aMax - aMin) + (bMax - bMin);
            return sum < total;
        }


    }

    public class Obb
    {
        public Vector3[] vertice
        {
            get;
        }

        public Vector3 right
        {
            get;
        }

        public Vector3 up
        {
            get;
        }

        public Vector3 forward
        {
            get;
        }

        public Obb(Vector3 center, Vector3 size, Quaternion rotation)
        {
            Vector2 max = size;
            Vector2 min = -size;


            vertice = new[]
            {
                // 1
                center + rotation * new Vector3(max.x, max.y, 0),
                // 2
                center + rotation * new Vector3(max.x, min.y, 0),
                // 3 
                center + rotation * new Vector3(min.x, min.y, 0),
                // 4
                center + rotation * new Vector3(min.x, max.y, 0)
            };

            right = rotation * Vector3.right;
            up = rotation * Vector3.up;
        }

    }
}

