using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using Panda;

namespace BehaviourTrees
{
    public class BehaviourTreeAI : MonoBehaviour
    {
        public Transform player;
        public Transform bulletSpawn;
        public Slider healthBar;   
        public GameObject bulletPrefab;

        NavMeshAgent agent;
        public Vector3 destination; // The movement destination.
        public Vector3 target;      // The position to aim to.
        float health = 100.0f;
        float rotSpeed = 5.0f;

        float visibleRange = 80.0f;
        float shotRange = 40.0f;

        Vector3 lastPosition = Vector3.one * float.MinValue;

        void Start()
        {
            agent = this.GetComponent<NavMeshAgent>();
            agent.stoppingDistance = shotRange - 5; //for a little buffer
            InvokeRepeating("UpdateHealth",5,0.5f);
        }

        void Update()
        {
            Vector3 healthBarPos = Camera.main.WorldToScreenPoint(this.transform.position);
            healthBar.value = (int)health;
            healthBar.transform.position = healthBarPos + new Vector3(0,60,0);
        }

        void UpdateHealth()
        {
            if(health < 100)
                health ++;
        }

        void OnCollisionEnter(Collision col)
        {
            if(col.gameObject.tag == "bullet")
            {
                health -= 10;
            }
        }

        [Task]
        public bool IsHealthLessThan(float value)
        {
            return health < value;
        }

        [Task]
        public bool InDanger(float minDist)
        {
            return Vector3.Distance(player.transform.position, transform.position) < minDist;
        }

        [Task]
        public void TakeCover()
        {
            Vector3 awayFromPlayer = transform.position - player.transform.position;
            Vector3 dest = transform.position + awayFromPlayer * 5;
            agent.SetDestination(dest);
            Task.current.Succeed();
        }

        [Task]
        public void PickRandomDestination()
        {
            Vector3 dest = new Vector3(Random.Range(-100,100), 0, Random.Range(-100,100));
            agent.SetDestination(dest);
            Task.current.Succeed();
        }

        [Task]
        public void PickDestination(float x, float z)
        {
            Vector3 dest = new Vector3(x, 0, z);
            agent.SetDestination(dest);
            Task.current.Succeed();
        }

        [Task]
        public void MoveToDestination()
        {
            if(Task.isInspected)
                Task.current.debugInfo = string.Format("t={0:0.00}", Time.time);

            if(agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
                Task.current.Succeed();
        }

        [Task]
        public void TargetPlayer()
        {
            target = player.transform.position;
            Task.current.Succeed();
        }

        [Task]
        public void LookAtTarget()
        {
            Vector3 direction = target - transform.position;

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(direction),
                Time.deltaTime * rotSpeed
            );

            float angle = Vector3.Angle(transform.forward, direction);
            Debug.Log(angle);
            if(Task.isInspected)
                Task.current.debugInfo = string.Format("angle={0}", angle);

            if(angle < 5.0f)
                Task.current.Succeed();
        }

        [Task]
        public bool Fire()
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward*500);
            return true;
        }

        [Task]
        bool SeePlayer()
        {
            Vector3 distance = player.transform.position - transform.position;

            RaycastHit hit;
            bool seeWall  = false;

            Debug.DrawRay(transform.position, distance, Color.red);
            if(Physics.Raycast(transform.position, distance, out hit))
            {
                if(hit.collider.CompareTag("wall"))
                {
                    seeWall = true;
                }
            }

            if(Task.isInspected)
                Task.current.debugInfo = string.Format("Wall = {0}", seeWall);

            if(distance.magnitude < visibleRange && !seeWall)
                return true;
            else
                return false;
        }

        [Task]
        bool Turn (float angle)
        {
            Vector3 p = transform.position + Quaternion.AngleAxis(angle, Vector3.up) * transform.forward;
            target = p;
            return true;
        }

        [Task]
        public bool Explode()
        {
            Destroy(healthBar.gameObject);
            Destroy(gameObject);
            return true;
        }

        [Task]
        public void SetTargetDestination()
        {
            agent.SetDestination(target);
            Task.current.Succeed();
        }

        [Task]
        bool ShotLinedUp()
        {
            Vector3 distance = target - transform.position;
            
            if(distance.magnitude < shotRange && Vector3.Angle(transform.forward, distance) < 1.0f)
                return true;
            else
                return false;
        }

        [Task]
        void SaveLastPosition()
        {
            lastPosition = transform.position;
            Task.current.Succeed();
        }

        [Task]
        void TargetLastPosition()
        {
            agent.SetDestination(lastPosition);
            GameObject lastPos = GameObject.CreatePrimitive(PrimitiveType.Cube);
            lastPos.transform.position = lastPosition;
            Task.current.Succeed();
        }

        [Task]
        void UnsetLastPosition()
        {
            lastPosition = Vector3.one * float.MinValue;
            Task.current.Succeed();
        }

        [Task]
        bool HasLastPosition()
        {
            return lastPosition != Vector3.one * float.MinValue;
        }
    }
}

