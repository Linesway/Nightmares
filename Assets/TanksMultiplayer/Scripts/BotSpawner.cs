/*  This file is part of the "Tanks Multiplayer" project by Rebound Games.
 *  You are only allowed to use these resources if you've bought them from the Unity Asset Store.
 * 	You shall not license, sublicense, sell, resell, transfer, assign, distribute or
 * 	otherwise make available to any third party the Service or the Content. */

using System.Collections;
using UnityEngine;
using Photon;
using UnityEngine.UI;

namespace TanksMP
{          
    /// <summary>
    /// Responsible for spawning AI bots when in offline mode, otherwise gets disabled.
    /// </summary>
	public class BotSpawner : PunBehaviour
    {                
        /// <summary>
        /// Amount of bots to spawn across all teams.
        /// </summary>
        public int maxBots;
        
        /// <summary>
        /// Selection of bot prefabs to choose from.
        /// </summary>
        public GameObject[] prefabs;

        public int yeet = 5;

        public int yeeter;
        public int stages = 0;

        public int starter = 0;
        public GameObject LevelComplete;

        public float wait;

        public Text yeet1;
        
        
        void Awake()
        {

            stages = starter;
            yeet = yeeter;
            wait = 3;
            LevelComplete.SetActive(false);
            //disabled when not in offline mode
            if ((NetworkMode)PlayerPrefs.GetInt(PrefsKeys.networkMode) != NetworkMode.Offline)
                this.enabled = false;
        }

        
        IEnumerator Start()
        {
            //wait a second for all script to initialize
            yield return new WaitForSeconds(1);

            //loop over bot count
			for(int i = 0; i < yeet; i++)
            {
                //randomly choose bot from array of bot prefabs
                //spawn bot across the simulated private network
                int randIndex = Random.Range(0, 2);
                GameObject obj = PhotonNetwork.Instantiate(prefabs[randIndex].name, Vector3.zero, Quaternion.identity, 0);
                if(stages == 4)
                {
                    GameObject obj2 = PhotonNetwork.Instantiate(prefabs[randIndex].name, Vector3.zero, Quaternion.identity, 0);
                    GameObject obj3 = PhotonNetwork.Instantiate(prefabs[2].name, Vector3.zero, Quaternion.identity, 0);
                    //let the local host determine the team assignment
                    Player p2 = obj2.GetComponent<Player>();
                    p2.GetView().SetTeam(1);
                    //p.GetView().SetTeam(GameManager.GetInstance().GetTeamFill());

                     
                    //increase corresponding team size
                    PhotonNetwork.room.AddSize(p2.GetView().GetTeam(), +1);
                    GameManager.number++;
                    Player p3 = obj3.GetComponent<Player>();
                    p3.GetView().SetTeam(1);
                    //p.GetView().SetTeam(GameManager.GetInstance().GetTeamFill());
                    //increase corresponding team size
                    PhotonNetwork.room.AddSize(p3.GetView().GetTeam(), +1);
                    GameManager.number++;
                }
                if(NetworkManagerCustom.onlineSceneIndex == 2)
                {
                    if (stages == 6)
                    {
                        GameObject obj2 = PhotonNetwork.Instantiate(prefabs[randIndex].name, Vector3.zero, Quaternion.identity, 0);
                        GameObject obj3 = PhotonNetwork.Instantiate(prefabs[3].name, Vector3.zero, Quaternion.identity, 0);
                        //let the local host determine the team assignment
                        Player p2 = obj2.GetComponent<Player>();
                        p2.GetView().SetTeam(1);
                        //p.GetView().SetTeam(GameManager.GetInstance().GetTeamFill());


                        //increase corresponding team size
                        PhotonNetwork.room.AddSize(p2.GetView().GetTeam(), +1);
                        GameManager.number++;
                        Player p3 = obj3.GetComponent<Player>();
                        p3.GetView().SetTeam(1);
                        //p.GetView().SetTeam(GameManager.GetInstance().GetTeamFill());
                        //increase corresponding team size
                        PhotonNetwork.room.AddSize(p3.GetView().GetTeam(), +1);
                        GameManager.number++;
                    }
                    if (stages == 8)
                    {
                        Debug.Log("fajoaj");
                        GameObject obj2 = PhotonNetwork.Instantiate(prefabs[3].name, Vector3.zero, Quaternion.identity, 0);
                        GameObject obj3 = PhotonNetwork.Instantiate(prefabs[3].name, Vector3.zero, Quaternion.identity, 0);
                        //let the local host determine the team assignment
                        Player p2 = obj2.GetComponent<Player>();
                        p2.GetView().SetTeam(1);
                        //p.GetView().SetTeam(GameManager.GetInstance().GetTeamFill());


                        //increase corresponding team size
                        PhotonNetwork.room.AddSize(p2.GetView().GetTeam(), +1);
                        GameManager.number++;
                        Player p3 = obj3.GetComponent<Player>();
                        p3.GetView().SetTeam(1);
                        //p.GetView().SetTeam(GameManager.GetInstance().GetTeamFill());
                        //increase corresponding team size
                        PhotonNetwork.room.AddSize(p3.GetView().GetTeam(), +1);
                        GameManager.number++;
                    }
                }
                if(NetworkManagerCustom.onlineSceneIndex == 3)
                {
                    if (stages == 10)
                    {
                        Debug.Log("fajoaj");
                        GameObject obj2 = PhotonNetwork.Instantiate(prefabs[4].name, Vector3.zero, Quaternion.identity, 0);
                        
                        Player p2 = obj2.GetComponent<Player>();
                        p2.GetView().SetTeam(1);
                        //p.GetView().SetTeam(GameManager.GetInstance().GetTeamFill());


                        //increase corresponding team size
                        PhotonNetwork.room.AddSize(p2.GetView().GetTeam(), +1);
                        GameManager.number++;
                        
                    }
                }
                if (NetworkManagerCustom.onlineSceneIndex == 4)
                {
                    if (stages == 12)
                    {
                        Debug.Log("fajoaj");
                        GameObject obj2 = PhotonNetwork.Instantiate(prefabs[5].name, Vector3.zero, Quaternion.identity, 0);
                        Player p2 = obj2.GetComponent<Player>();
                        p2.GetView().SetTeam(1);
                        PhotonNetwork.room.AddSize(p2.GetView().GetTeam(), +1);
                        GameManager.number++;
                        GameObject obj3 = PhotonNetwork.Instantiate(prefabs[6].name, Vector3.zero, Quaternion.identity, 0);
                        Player p3 = obj3.GetComponent<Player>();
                        p3.GetView().SetTeam(1);
                        PhotonNetwork.room.AddSize(p3.GetView().GetTeam(), +1);
                        GameManager.number++;
                        GameObject obj4 = PhotonNetwork.Instantiate(prefabs[7].name, Vector3.zero, Quaternion.identity, 0);
                        Player p4 = obj4.GetComponent<Player>();
                        p4.GetView().SetTeam(1);
                        PhotonNetwork.room.AddSize(p4.GetView().GetTeam(), +1);
                        GameManager.number++;
                        GameObject obj5 = PhotonNetwork.Instantiate(prefabs[8].name, Vector3.zero, Quaternion.identity, 0);
                        Player p5 = obj5.GetComponent<Player>();
                        p5.GetView().SetTeam(1);
                        PhotonNetwork.room.AddSize(p5.GetView().GetTeam(), +1);
                        GameManager.number++;

                    }
                }
                if (NetworkManagerCustom.onlineSceneIndex == 5)
                {
                    if (stages == 14)
                    {
                        Debug.Log("fajoaj");
                        GameObject obj2 = PhotonNetwork.Instantiate(prefabs[5].name, Vector3.zero, Quaternion.identity, 0);
                        Player p2 = obj2.GetComponent<Player>();
                        p2.GetView().SetTeam(1);
                        PhotonNetwork.room.AddSize(p2.GetView().GetTeam(), +1);
                        GameManager.number++;
                       

                    }
                }
                if (NetworkManagerCustom.onlineSceneIndex == 6)
                {
                    if (stages == 16)
                    {
                        Debug.Log("fajoaj");
                        
                        GameObject obj3 = PhotonNetwork.Instantiate(prefabs[4].name, Vector3.zero, Quaternion.identity, 0);
                        Player p3 = obj3.GetComponent<Player>();
                        p3.GetView().SetTeam(1);
                        PhotonNetwork.room.AddSize(p3.GetView().GetTeam(), +1);
                        GameManager.number++;
                        GameObject obj4 = PhotonNetwork.Instantiate(prefabs[4].name, Vector3.zero, Quaternion.identity, 0);
                        Player p4 = obj4.GetComponent<Player>();
                        p4.GetView().SetTeam(1);
                        PhotonNetwork.room.AddSize(p4.GetView().GetTeam(), +1);
                        GameManager.number++;
                        GameObject obj5 = PhotonNetwork.Instantiate(prefabs[5].name, Vector3.zero, Quaternion.identity, 0);
                        Player p5 = obj5.GetComponent<Player>();
                        p5.GetView().SetTeam(1);
                        PhotonNetwork.room.AddSize(p5.GetView().GetTeam(), +1);
                        GameManager.number++;
                        GameObject obj6 = PhotonNetwork.Instantiate(prefabs[3].name, Vector3.zero, Quaternion.identity, 0);
                        Player p6 = obj6.GetComponent<Player>();
                        p6.GetView().SetTeam(1);
                        PhotonNetwork.room.AddSize(p6.GetView().GetTeam(), +1);
                        GameManager.number++;

                    }
                }
                if (NetworkManagerCustom.onlineSceneIndex == 7)
                {
                    if (stages == 18)
                    {
                        Debug.Log("fajoaj");

                        GameObject obj3 = PhotonNetwork.Instantiate(prefabs[4].name, Vector3.zero, Quaternion.identity, 0);
                        Player p3 = obj3.GetComponent<Player>();
                        p3.GetView().SetTeam(1);
                        PhotonNetwork.room.AddSize(p3.GetView().GetTeam(), +1);
                        GameManager.number++;
                        

                    }
                }
                if (NetworkManagerCustom.onlineSceneIndex == 8)
                {
                    if (stages == 20)
                    {
                        Debug.Log("fajoaj");

                        GameObject obj3 = PhotonNetwork.Instantiate(prefabs[8].name, Vector3.zero, Quaternion.identity, 0);
                        Player p3 = obj3.GetComponent<Player>();
                        p3.GetView().SetTeam(1);
                        PhotonNetwork.room.AddSize(p3.GetView().GetTeam(), +1);
                        GameManager.number++;


                    }
                }

                //let the local host determine the team assignment
                Player p = obj.GetComponent<Player>();
                p.GetView().SetTeam(1);
                //p.GetView().SetTeam(GameManager.GetInstance().GetTeamFill());
                //increase corresponding team size
                PhotonNetwork.room.AddSize(p.GetView().GetTeam(), +1);
                GameManager.number++;
                if(i == yeet - 1)
                {
                    Debug.Log("atleast11111");
                    stages++;
                    
                }
                
                
                
                
                

                yield return new WaitForSeconds(wait);
            }
        }
        void Update()
        {

            if (stages == 1)
            {
                if (GameManager.number == 0)
                {
                    Debug.Log("2nd Stage!!!!");
                    yeet = 8;
                    wait = 1.5f;
                    StartCoroutine("Start");
                    stages = 2;
                    GameManager.localPlayer2.GetView().SetHealth(GameManager.localPlayer2.maxHealth);
                }

            }
            if (stages == 3)
            {
                if (GameManager.number == 0)
                {
                    Debug.Log("3rd Stage!!!!");
                    yeet = 3;
                    wait = 1.5f;
                    StartCoroutine("Start");
                    stages = 4;
                    GameManager.localPlayer2.GetView().SetHealth(GameManager.localPlayer2.maxHealth);
                }

            }
            if (NetworkManagerCustom.onlineSceneIndex == 1)
            {
                if (stages == 5)
                {
                    if (GameManager.number == 0)
                    {
                        Debug.Log("YEET");
                        LevelComplete.SetActive(true);
                    }

                    

                }
            }
            
            if (NetworkManagerCustom.onlineSceneIndex == 2)
            {
                if (stages == 5)
                {
                    if (GameManager.number == 0)
                    {
                        Debug.Log("3rd Stage!!!!");
                        yeet = 1;
                        wait = 1.5f;
                        StartCoroutine("Start");
                        stages = 6;
                        GameManager.localPlayer2.GetView().SetHealth(GameManager.localPlayer2.maxHealth);
                    }

                }
                if (stages == 7)
                {
                    if (GameManager.number == 0)
                    {
                        Debug.Log("3rd Stage!!!!");
                        yeet = 1;
                        wait = 1.5f;
                        StartCoroutine("Start");
                        stages = 8;
                        GameManager.localPlayer2.GetView().SetHealth(GameManager.localPlayer2.maxHealth);
                    }

                }
                if (stages == 9)
                {
                    if (GameManager.number == 0)
                    {
                        Debug.Log("YEET");
                        LevelComplete.SetActive(true);
                    }



                }
            }
            if (NetworkManagerCustom.onlineSceneIndex == 3)
            {
                if (stages == 9)
                {
                    if (GameManager.number == 0)
                    {
                        Debug.Log("3rd Stage!!!!");
                        yeet = 1;
                        wait = 1.5f;
                        StartCoroutine("Start");
                        stages = 10;
                        GameManager.localPlayer2.GetView().SetHealth(GameManager.localPlayer2.maxHealth);
                    }

                }
                if (stages == 11)
                {
                    if (GameManager.number == 0)
                    {

                        LevelComplete.SetActive(true);
                    }



                }

            }
            if (NetworkManagerCustom.onlineSceneIndex == 4)
            {
                if (stages == 11)
                {
                    if (GameManager.number == 0)
                    {
                        Debug.Log("3rd Stage!!!!");
                        yeet = 1;
                        wait = 1.5f;
                        StartCoroutine("Start");
                        stages = 12;
                        GameManager.localPlayer2.GetView().SetHealth(GameManager.localPlayer2.maxHealth);
                    }

                }
                if (stages == 13)
                {
                    if (GameManager.number == 0)
                    {

                        LevelComplete.SetActive(true);
                    }



                }


            }
            if (NetworkManagerCustom.onlineSceneIndex == 5)
            {
                if (stages == 13)
                {
                    if (GameManager.number == 0)
                    {
                        Debug.Log("3rd Stage!!!!");
                        yeet = 1;
                        wait = 1.5f;
                        StartCoroutine("Start");
                        stages = 14;
                        GameManager.localPlayer2.GetView().SetHealth(GameManager.localPlayer2.maxHealth);
                    }

                }
                if (stages == 15)
                {
                    if (GameManager.number == 0)
                    {

                        LevelComplete.SetActive(true);
                    }



                }


            }
            if (NetworkManagerCustom.onlineSceneIndex == 6)
            {
                if (stages == 15)
                {
                    if (GameManager.number == 0)
                    {
                        Debug.Log("3rd Stage!!!!");
                        yeet = 3;
                        wait = 1.5f;
                        StartCoroutine("Start");
                        stages = 16;
                        GameManager.localPlayer2.GetView().SetHealth(GameManager.localPlayer2.maxHealth);
                    }

                }
                if (stages == 17)
                {
                    if (GameManager.number == 0)
                    {

                        LevelComplete.SetActive(true);
                    }



                }


            }
            if (NetworkManagerCustom.onlineSceneIndex == 7)
            {
                if (stages == 17)
                {
                    if (GameManager.number == 0)
                    {
                        Debug.Log("3rd Stage!!!!");
                        yeet = 1;
                        wait = 1.5f;
                        StartCoroutine("Start");
                        stages = 18;
                        GameManager.localPlayer2.GetView().SetHealth(GameManager.localPlayer2.maxHealth);
                    }

                }
                if (stages == 19)
                {
                    if (GameManager.number == 0)
                    {

                        LevelComplete.SetActive(true);
                    }



                }


            }
            if (NetworkManagerCustom.onlineSceneIndex == 8)
            {
                if (stages == 19)
                {
                    if (GameManager.number == 0)
                    {
                        Debug.Log("3rd Stage!!!!");
                        yeet = 1;
                        wait = 1.5f;
                        StartCoroutine("Start");
                        stages = 20;
                        GameManager.localPlayer2.GetView().SetHealth(GameManager.localPlayer2.maxHealth);
                    }

                }
                if (stages == 21)
                {
                    if (GameManager.number == 0)
                    {

                        LevelComplete.SetActive(true);
                    }



                }


            }

        }
    }
}
