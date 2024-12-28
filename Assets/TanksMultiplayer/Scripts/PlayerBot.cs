/*  This file is part of the "Tanks Multiplayer" project by Rebound Games.
 *  You are only allowed to use these resources if you've bought them from the Unity Asset Store.
 * 	You shall not license, sublicense, sell, resell, transfer, assign, distribute or
 * 	otherwise make available to any third party the Service or the Content. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TanksMP
{          
    /// <summary>
    /// Implementation of AI bots by overriding methods of the Player class.
    /// </summary>
	public class PlayerBot : Player
    {
        //custom properties per PhotonPlayer do not work in offline mode
        //(actually they do, but for objects spawned by the master client,
        //PhotonPlayer is always the local master client. This means that
        //setting custom player properties would apply to all objects)
        [HideInInspector] public string myName;
        [HideInInspector] public int teamIndex;
        [HideInInspector] public int health;
        [HideInInspector] public int shield;
        [HideInInspector] public int ammo;
        [HideInInspector] public int currentBullet;

        /// <summary>
        /// Radius in units for detecting other players.
        /// </summary>
        public float range = 6f;

        public string MyREALname;

        public GameObject hitFX;
        //list of enemy players that are in range of this bot
        private List<GameObject> inRange = new List<GameObject>();

        //reference to the agent component
        private NavMeshAgent agent;

        public int damage;
        //current destination on the navigation mesh
        private Vector3 targetPoint;

        //timestamp when next shot should happen
        private float nextShot;

        //toggle for update logic
        private bool isDead = false;

        public Transform[] otherstuff;

        public bool isWizard = false;

        public static PlayerBot Master1;

        public bool isElemental = false;

        public bool isWizardBOSS = false;

        public static bool oof = false;

        public bool isMinion = false;
        public bool isMinion2 = false;
        Vector3 randrand;

        public GameObject[] Prefabs;

        public Transform[] Prefabs1;

        public static Vector3 free;
        public static Vector3 free2;
        public static PlayerBot MASTER;
        public static Vector3 free1;
        public static Vector3 free3;
        public static Vector3 free4;
        public static Vector3 free5;

        public GameObject[] BULLETS;

        public bool isLaser = false;
        public bool isLaser1 = false;
        public bool isLaser2 = false;
        public bool isLaser3 = false;

        public bool isEyeLord = false;

        public static bool PortalDown = false;

        public bool cantfire = false;

        public bool already = false;
        public bool isPortal = false;
        public float waitAgain;
        
        //called before SyncVar updates
        void Start()
        {           
            
            //get components and set camera target
            camFollow = Camera.main.GetComponent<FollowTarget>();
            agent = GetComponent<NavMeshAgent>();
            agent.speed = moveSpeed;

            //get corresponding team and colorize renderers in team color
            int oof = Random.Range(0, 4);
            targetPoint = otherstuff[oof].position;
            agent.Warp(targetPoint);
            if(isPortal == true)
            {
                Vector3 targetPointa = new Vector3 (0, 0, 0);
                agent.Warp(targetPointa);
                InvokeRepeating("SpawnAnything", 0, 20);
            }

            Team team = GameManager.GetInstance().teams[GetView().GetTeam()];
            for(int i = 0; i < renderers.Length; i++)
                renderers[i].material = team.material;
            
			//set name in label
            label.text = MyREALname;
            //call hooks manually to update
            OnHealthChange(GetView().GetHealth());
            OnShieldChange(GetView().GetShield());
            if(isEyeLord)
            {
                if(!isDead)
                {
                    InvokeRepeating("SpawnMegas", 0, 40);
                    InvokeRepeating("SpawnNormal", 0, 40);
                }
            }
            if (isWizard)
            {
                if(!isDead)
                {
                    InvokeRepeating("Teleport", 2, 3.5f);
                }
                
            }
            if (isWizardBOSS)
            {
                if (!isDead)
                {
                    InvokeRepeating("Teleport", 2, waitAgain);
                }

            }
            if (isWizardBOSS == true)
            {
                free = Prefabs1[0].transform.position;
                free2 = Prefabs1[1].transform.position;
                GameObject obj = PhotonNetwork.Instantiate(Prefabs[0].name, Vector3.zero, Quaternion.identity, 0);
                //let the local host determine the team assignment
                Player p = obj.GetComponent<Player>();
                p.GetView().SetTeam(1);
                GameObject obj2 = PhotonNetwork.Instantiate(Prefabs[1].name, Vector3.zero, Quaternion.identity, 0);
                //let the local host determine the team assignment
                Player p2 = obj2.GetComponent<Player>();
                p2.GetView().SetTeam(1);


            }
            if(isElemental)
            {
                GameObject obj = PhotonNetwork.Instantiate(Prefabs[0].name, Vector3.zero, Quaternion.identity, 0);
                //let the local host determine the team assignment
                Player p = obj.GetComponent<Player>();
                p.GetView().SetTeam(1);
                GameObject obj2 = PhotonNetwork.Instantiate(Prefabs[1].name, Vector3.zero, Quaternion.identity, 0);
                //let the local host determine the team assignment
                Player p2 = obj2.GetComponent<Player>();
                p2.GetView().SetTeam(1);
                GameObject obj3 = PhotonNetwork.Instantiate(Prefabs[2].name, Vector3.zero, Quaternion.identity, 0);
                //let the local host determine the team assignment
                Player p3 = obj3.GetComponent<Player>();
                p3.GetView().SetTeam(1);
                GameObject obj4 = PhotonNetwork.Instantiate(Prefabs[3].name, Vector3.zero, Quaternion.identity, 0);
                //let the local host determine the team assignment
                Player p4 = obj4.GetComponent<Player>();
                p4.GetView().SetTeam(1);
            }
            
            
            //start enemy detection routine
            StartCoroutine(DetectPlayers());
        }
        public void SpawnMegas()
        {
            GameObject obj3 = PhotonNetwork.Instantiate(Prefabs[1].name, Vector3.zero, Quaternion.identity, 0);
            Player p3 = obj3.GetComponent<Player>();
            p3.GetView().SetTeam(1);
            PhotonNetwork.room.AddSize(p3.GetView().GetTeam(), +1);
            GameManager.number++;
            
            
        }
        public void SpawnNormal()
        {
            GameObject obj3 = PhotonNetwork.Instantiate(Prefabs[0].name, Vector3.zero, Quaternion.identity, 0);
            Player p3 = obj3.GetComponent<Player>();
            p3.GetView().SetTeam(1);
            PhotonNetwork.room.AddSize(p3.GetView().GetTeam(), +1);
            GameManager.number++;
            GameObject obj4 = PhotonNetwork.Instantiate(Prefabs[0].name, Vector3.zero, Quaternion.identity, 0);
            Player p4 = obj4.GetComponent<Player>();
            p4.GetView().SetTeam(1);
            PhotonNetwork.room.AddSize(p4.GetView().GetTeam(), +1);
            GameManager.number++;
           
        }
        public void SpawnAnything()
        {
            int rand = Random.Range(0, 10);
            int rand2 = Random.Range(0, 10);
            GameObject obj3 = PhotonNetwork.Instantiate(Prefabs[rand].name, Vector3.zero, Quaternion.identity, 0);
            Player p3 = obj3.GetComponent<Player>();
            p3.GetView().SetTeam(1);
            PhotonNetwork.room.AddSize(p3.GetView().GetTeam(), +1);
            GameManager.number++;
            GameObject obj4 = PhotonNetwork.Instantiate(Prefabs[rand2].name, Vector3.zero, Quaternion.identity, 0);
            Player p4 = obj4.GetComponent<Player>();
            p4.GetView().SetTeam(1);
            PhotonNetwork.room.AddSize(p4.GetView().GetTeam(), +1);
            GameManager.number++;

        }
        public void stopit()
        {
            oof = false;
            moveSpeed = 4;
            cantfire = false;
            GameManager.localPlayer2.GetView().SetHealth(GameManager.localPlayer2.maxHealth);
            GameManager.number = 1;
            MyREALname = "NightMare";
            label.text = MyREALname;
            CancelInvoke();
        }
        private void Update()
        {
            agent.speed = moveSpeed;
            if (isWizardBOSS == true)
            {
                free = Prefabs1[0].transform.position;
                free2 = Prefabs1[1].transform.position;
                MASTER = this;
                if(this.GetView().GetHealth() <= 50 && this.GetView().GetHealth() >= 30)
                {
                    waitAgain = 2.5f;
                }
                if (this.GetView().GetHealth() <= 29 && this.GetView().GetHealth() >= 10)
                {
                    waitAgain = 2.0f;
                }
                if (this.GetView().GetHealth() <= 9)
                {
                    waitAgain = 0.75f;
                }


            }
            if(NetworkManagerCustom.onlineSceneIndex == 8)
            {
                if(!isPortal)
                {
                    if(PortalDown == true)
                    {
                        PhotonNetwork.Destroy(gameObject);

                    }
                }
            }
            if(isPortal)
            {
                if(this.GetView().GetHealth() <= 10)
                {
                    if(already == false)
                    {
                        PortalDown = true;
                        this.GetView().SetHealth(maxHealth);
                        oof = true;
                        Invoke("stopit", 5f);
                        already = true;

                    }
                    
                }
                if(isPortal)
                {
                    if(already)
                    {
                        if(this.GetView().GetHealth() <= 100 && this.GetView().GetHealth() >= 70)
                        {
                            fireRate = 2;
                        }
                        if (this.GetView().GetHealth() <= 69 && this.GetView().GetHealth() >= 30)
                        {
                            fireRate = 1;
                        }
                        if (this.GetView().GetHealth() <= 29)
                        {
                            fireRate = 0.5f;
                        }

                    }
                }
            }
            if (isElemental == true)
            {
                free1 = Prefabs1[0].transform.position;
                free3 = Prefabs1[1].transform.position;
                free4 = Prefabs1[2].transform.position;
                free5 = Prefabs1[3].transform.position;
                Master1 = this;
                if (this.GetView().GetHealth() <= 70 && this.GetView().GetHealth() >= 50)
                {
                    moveSpeed = 3;
                }
                if (this.GetView().GetHealth() <= 49 && this.GetView().GetHealth() >= 30)
                {
                    moveSpeed = 4.5f;
                }
                if (this.GetView().GetHealth() <= 29)
                {
                    moveSpeed = 7f;
                }

            }

            if (!isMinion && !isMinion2 && !isLaser && !isLaser1 && !isLaser2 && !isLaser3)
            {
                agent.SetDestination(GameManager.localPlayer2.transform.position);
               
            }
            randrand = new Vector3(Random.Range(15.67f, -14.49f), 0, Random.Range(-16.6f, 15.84f));
            if (isLaser)
            {
                agent.Warp(free1);
                if (Master1.isDead == true)
                {
                    PhotonNetwork.Destroy(gameObject);
                }
            }
            if (isLaser1)
            {
                agent.Warp(free3);
                if (Master1.isDead == true)
                {
                    PhotonNetwork.Destroy(gameObject);
                }
            }
            if (isLaser2)
            {
                agent.Warp(free4);
                if (Master1.isDead == true)
                {
                    PhotonNetwork.Destroy(gameObject);
                }
            }
            if (isLaser3)
            {
                agent.Warp(free5);
                if (Master1.isDead == true)
                {
                    PhotonNetwork.Destroy(gameObject);
                }
            }
            if (isMinion)
            {
                agent.Warp(free);
                if(MASTER.isDead == true)
                {
                    PhotonNetwork.Destroy(gameObject);
                }
            }
            if (isMinion2)
            {
                agent.Warp(free2);
                if (MASTER.isDead == true)
                {
                    PhotonNetwork.Destroy(gameObject);
                }
            }

            if (isDead)
            {
                CancelInvoke();
            }
        }

        public void Teleport()
        {
            PoolManager.Spawn(hitFX, transform.position, Quaternion.identity);
            agent.Warp(randrand);
            PoolManager.Spawn(hitFX, transform.position, Quaternion.identity);
            if(isWizardBOSS)
            {
                CancelInvoke("Teleport");
                InvokeRepeating("Teleport", waitAgain, waitAgain);
            }
            
        }


        //sets inRange list for player detection
        IEnumerator DetectPlayers()
        {
            //wait for initialization
            yield return new WaitForEndOfFrame();
            
            //detection logic
            while(true)
            {
                //empty list on each iteration
                inRange.Clear();

                //casts a sphere to detect other player objects within the sphere radius
                Collider[] cols = Physics.OverlapSphere(transform.position, range, LayerMask.GetMask("Player"));
                //loop over players found within bot radius
                for (int i = 0; i < cols.Length; i++)
                {
                    //get other Player component
                    //only add the player to the list if its not in this team
                    Player p = cols[i].gameObject.GetComponent<Player>();
                    if(p.GetView().GetTeam() != GetView().GetTeam() && !inRange.Contains(cols[i].gameObject))
                    {
                        inRange.Add(cols[i].gameObject);   
                    }
                }
                
                //wait a second before doing the next range check
                yield return new WaitForSeconds(1);
            }
                }

        
        //calculate random point for movement on navigation mesh
        private void RandomPoint(Vector3 center, float range, out Vector3 result)
        {
            //clear previous target point
            result = Vector3.zero;
            
            //try to find a valid point on the navmesh with an upper limit (10 times)
            for (int i = 0; i < 10; i++)
            {
                //find a point in the movement radius
                Vector3 randomPoint = center + (Vector3)Random.insideUnitCircle * range;
                randomPoint.y = 0;
                NavMeshHit hit;

                //if the point found is a valid target point, set it and continue
                if (NavMesh.SamplePosition(randomPoint, out hit, 2f, NavMesh.AllAreas)) 
                {
                    result = hit.position;
                    break;
                }
            }
            
            //set the target point as the new destination
            agent.SetDestination(GameManager.localPlayer2.transform.position);
        }

        
        void FixedUpdate()
        {
            //don't execute anything if the game is over already,
            //but termine the agent and path finding routines
            if(GameManager.GetInstance().IsGameOver())
            {
                agent.isStopped = true;
                StopAllCoroutines();
                enabled = false;
                return;
            }
            
            //don't continue if this bot is marked as dead
            if(isDead) return;

            //stat visualization does not update automatically
            OnHealthChange(health);
            OnShieldChange(shield);

            //no enemy players are in range
            if(inRange.Count == 0)
            {
                //if this bot reached the the random point on the navigation mesh,
                //then calculate another random point on the navmesh on continue moving around
                //with no other players in range, the AI wanders from team spawn to team spawn
                if(Vector3.Distance(transform.position, targetPoint) < agent.stoppingDistance)
                {
                    int teamCount = GameManager.GetInstance().teams.Length;
                    RandomPoint(GameManager.GetInstance().teams[Random.Range(0, teamCount)].spawn.position, range, out targetPoint);
                }
            }
            else
            {
                //if we reached the targeted point, calculate a new point around the enemy
                //this simulates more fluent "dancing" movement to avoid being shot easily
                if(Vector3.Distance(transform.position, targetPoint) < agent.stoppingDistance)
                {
                    RandomPoint(inRange[0].transform.position, range * 2, out targetPoint);
                }
                
                //shooting loop 
                for(int i = 0; i < inRange.Count; i++)
                {
                    RaycastHit hit;
                    //raycast to detect visible enemies and shoot at their current position
                    if (Physics.Linecast(transform.position, inRange[i].transform.position, out hit))
                    {
                        //get current enemy position and rotate this turret
                        Vector3 lookPos = inRange[i].transform.position;
                        turret.LookAt(lookPos);
                        turret.eulerAngles = new Vector3(0, turret.eulerAngles.y, 0);
                        turretRotation = (short)turret.eulerAngles.y;

                        //find shot direction and shoot there
                        Vector3 shotDir = lookPos - transform.position;
                        if (isEyeLord)
                        {
                            if (this.GetView().GetHealth() <= 50)
                            {
                                this.GetView().SetBullet(1);
                            }
                        }
                        if(cantfire == false)
                        {
                            if(isPortal)
                            {
                                int rand1 = Random.Range(0, 12);
                                this.GetView().SetBullet(rand1);
                            }
                            Shoot(new Vector2(shotDir.x, shotDir.z));
                        }
                        
                        break;
                    }
                }
            }
        }

        
        /// <summary>
        /// Override of the base method to handle bot respawn separately.
        /// </summary>
        [PunRPC]
        protected override void RpcRespawn()
        {
            StartCoroutine(Respawn());
        }
        
        
        //the actual respawn routine
        IEnumerator Respawn()
        {   
            //stop AI updates
            isDead = true;
            inRange.Clear();
            agent.isStopped = true;
            PhotonNetwork.room.AddSize(GetView().GetTeam(), -1);
            GameManager.number--;
            //detect whether the current user was responsible for the kill
            //yes, that's my kill: increase local kill counter
            if (killedBy == GameManager.GetInstance().localPlayer.gameObject)
            {
                GameManager.GetInstance().ui.killCounter[0].text = (int.Parse(GameManager.GetInstance().ui.killCounter[0].text) + 1).ToString();
                GameManager.GetInstance().ui.killCounter[0].GetComponent<Animator>().Play("Animation");
            }

            if (explosionFX)
            {
			     //spawn death particles locally using pooling and colorize them in the player's team color
                 GameObject particle = PoolManager.Spawn(explosionFX, transform.position, transform.rotation);
                 ParticleColor pColor = particle.GetComponent<ParticleColor>();
                 if(pColor) pColor.SetColor(Color.red);
            }
				
			//play sound clip on player death
            if(explosionClip) AudioManager.Play3D(explosionClip, transform.position);

            //toggle visibility for all rendering parts (off)
            ToggleComponents(false);
            //wait global respawn delay until reactivation
            yield return new WaitForSeconds(GameManager.GetInstance().respawnTime);
            //toggle visibility again (on)
            //ToggleComponents(true);

            //respawn and continue with pathfinding
            //targetPoint = GameManager.GetInstance().GetSpawnPosition(GetView().GetTeam());
            //transform.position = targetPoint;
            //agent.Warp(targetPoint);
            //agent.isStopped = false;
            //isDead = false;
        }


        //disable rendering or blocking components
        void ToggleComponents(bool state)
        {
            GetComponent<Rigidbody>().isKinematic = state;
            GetComponent<Collider>().enabled = state;

            for (int i = 0; i < transform.childCount; i++)
                transform.GetChild(i).gameObject.SetActive(state);
        }
    }
}
