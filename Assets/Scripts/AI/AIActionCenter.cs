using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class AIActionCenter : Agent
{
    public HeroController hero;
    public Transform creep;
    public AIKeyboard keyboard;
    public AIMouse mouse;
    [SerializeField] private float mouseSpeed;
    public int kills, deaths;
    private int killRewarded, deathPenalized;
    private float previousHP;

    public Transform cursor;

    public override void Initialize()
    {
        keyboard = new AIKeyboard();
        mouseSpeed = 5f;
        mouse = new AIMouse(new Vector2Int(0, 0));
        Reset();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        //Position of hero
        sensor.AddObservation(transform.position);
        //Position of enemy
        if (creep != null)
            sensor.AddObservation(creep.position);
        else
            sensor.AddObservation(new Vector3());
        //HP percentage of hero
        sensor.AddObservation(hero.stats[UnitStats.Stats.HPCur] / hero.stats[UnitStats.Stats.HP]);
        sensor.AddObservation(deaths);
        sensor.AddObservation(kills);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        //First 2 is mouse movement
        mouse.MoveMouse(new Vector2(Mathf.Clamp(vectorAction[0], -1f, 1f), Mathf.Clamp(vectorAction[1], -1f, 1f)) * mouseSpeed * Time.deltaTime);
        //3rd is mouse click, 4th is keyboard click
        if (Mathf.Clamp(vectorAction[2], -1f, 1f) < 0)
            mouse.ClickMouseButton(1);
        else
            mouse.LiftMouseButton(1);
        if (Mathf.Clamp(vectorAction[3], -1f, 1f) < 0)
            keyboard.ClickKey(KeyCode.Q);
        else
            keyboard.LiftKey(KeyCode.Q);

        //Reward system
        if (killRewarded != kills)
        {
            AddReward((kills - killRewarded) * 0.3f);
            killRewarded = kills;
        }
        if (deathPenalized != deaths)
        {
            AddReward(-(deaths - deathPenalized) * 0.3f);
            deathPenalized = deaths;
        }
        if (previousHP < hero.stats[UnitStats.Stats.HPCur] / hero.stats[UnitStats.Stats.HP])
        {
            AddReward(-0.1f);
        }
    }

    public override void OnEpisodeBegin()
    {
        //Reset env
        Reset();
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetAxis("Horizontal");
        actionsOut[1] = Input.GetAxis("Vertical");
        actionsOut[2] = 1f;
        if (Input.GetMouseButton(1))
        {
            actionsOut[2] = -1;
        }
        actionsOut[3] = 1f;
        if (Input.GetKey(KeyCode.Q))
        {
            actionsOut[3] = -1f;
        }
    }

    private void Reset()
    {
        mouse.CenterMouse();
        transform.position = new Vector3(mouse.position.x, 0f, mouse.position.y);
        kills = 0;
        deaths = 0;
        killRewarded = 0;
        deathPenalized = 0;
        previousHP = 0f;
    }

    private void FixedUpdate()
    {
        cursor.position = new Vector3(mouse.position.x, 1f, mouse.position.y);
    }
}
