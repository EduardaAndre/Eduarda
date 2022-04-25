using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FollowPath : MonoBehaviour
{
    Transform goal; //goal para o direcionamento 
    public float speed = 5.0f;
    public float accuracy = 1.0f;
    public float rotSpeed = 2.0f;
    public GameObject wpManager;
    GameObject[] wps;
    GameObject currentNode;
    int currentWP = 0;
    Graph g;
    public GameObject wpHeli, wpRuin, wpFactory; 
    void Start()
    {
        wps = wpManager.GetComponent<WpManager>().waypoints;
        g = wpManager.GetComponent<WpManager>().graph;
        currentNode = wps[0];
    }
    public void GoToHeli()//GoToHeli = v� para o helicoptero
    {
        g.AStar(currentNode, wpHeli);
        currentWP = 0;
    }
    public void GoToRuin()//GoToRuin = v� para a ruina
    {
        g.AStar(currentNode, wpRuin);
        currentWP = 0;
    }
    public void GoToFactory()//GoToFactory = v� para a fabrica
    {
        g.AStar(currentNode, wpFactory);
        currentWP = 0;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (g.getPathLength() == 0 || currentWP == g.getPathLength())
            return;//ser� finalizado o m�todo se o waypoint estiver vazio ou for o ultimo

        
        currentNode = g.getPathPoint(currentWP);

        
        //quanto mais perto do n�, o tanque se mover� para o pr�ximo ponto
        if (Vector3.Distance(
        g.getPathPoint(currentWP).transform.position,
        transform.position) < accuracy)
        {
            currentWP++;
        }
        //Se n�o estiver no �ltimo waypoint, ele continua a movimenta��o
        if (currentWP < g.getPathLength())
        {
            goal = g.getPathPoint(currentWP).transform;
            // ignora a altura(y) e � a posi��o do waypoint para onde est� indo
           
            Vector3 lookAtGoal = new Vector3(goal.position.x,
            this.transform.position.y,
            goal.position.z); 
            //Vetor apontando deste objeto ao lookAtGoal.
            Vector3 direction = lookAtGoal - this.transform.position;
            //Ele est� rotacionando(Quartenion) o objeto para apontar pra lookAtGoal.
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
            Quaternion.LookRotation(direction),
            Time.deltaTime * rotSpeed);
            this.transform.Translate(Vector3.forward * speed);//� movido o objeto no seu eixo frontal.
        }
    }
}
