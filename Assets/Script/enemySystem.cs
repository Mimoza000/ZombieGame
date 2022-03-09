using UnityEngine.UI;
using UnityEngine;
using UnityEngine.AI;
public class enemySystem : MonoBehaviour
{
    [Header("References")]
    public enemyStatus status;
    public Animator animator;
    [SerializeField] Slider HP_Bar;
    [SerializeField] Text HP;
    [SerializeField] GameObject enegyCore;
    [Header("Debug")]
    [SerializeField] float distance = -1;
    [SerializeField] bool activeLimiter;
    [Header("Value")]
    [SerializeField] float addtiveMaxRandom = 1;
    [SerializeField] float angularSpeed = 360;
    public int enemyHP = 0;
    [SerializeField] float generatorRate = 0.5f;
    float localRunSpeed;
    float localWalkSpeed;
    NavMeshAgent agent;
    Transform player;
    void Start()
    {
        GameObject.Find("Player").TryGetComponent<Transform>(out player);
        activeLimiter = true;
        animator.SetBool("dead",false);
        localRunSpeed = status.runSpeed + Random.Range(0,addtiveMaxRandom);
        localWalkSpeed = status.walkSpeed + Random.Range(0,addtiveMaxRandom);

        // NavMeshAgent
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = status.stopDistance;
        agent.angularSpeed = angularSpeed;
        agent.speed = localWalkSpeed;

        // Set UI of Enemy
        HP_Bar.maxValue = status.maxHP;
        enemyHP = status.maxHP;
    }

    // Update is called once per frame
    void Update()
    {   
        if (!GameManager.Instance.startTimer) return;
        // Canvas
        HP_Bar.value = enemyHP;
        HP_Bar.transform.forward = Camera.main.transform.forward;
        HP.transform.forward = Camera.main.transform.forward;
        HP.text = enemyHP.ToString();

        // Collision Dead Mode
        if (animator.GetBool("dead")) return;

        // Speed Distance
        agent.destination = player.position;
        distance = agent.remainingDistance;

        // Check State
        if (status.startWalkDistance >= distance && status.stopDistance <= distance)
        {
            if (activeLimiter) SetAnimator(true, false, false, localWalkSpeed);
            else SetAnimator(false, true, false, localRunSpeed);
        }
        else if (status.stopDistance >= distance)
        {
            if (activeLimiter) SetAnimator(false, false, true, localWalkSpeed);
            else SetAnimator(false, false, true, localRunSpeed);
        }
        else SetAnimator(false, false, false, 0);    
    }

    /// <summary>
    /// Set property of walk,run,attack,speed.
    /// </summary>
    /// <param name="walk"></param>
    /// <param name="run"></param>
    /// <param name="attack"></param>
    /// <param name="speed"></param>
    void SetAnimator(bool walk, bool run, bool attack, float speed)
    {
        animator.SetBool("walk", walk);
        animator.SetBool("run", run);
        animator.SetBool("attack", attack);
        agent.speed = speed;
    }

    public void Damaged(int damage)
    {
        enemyHP -= damage;
        if (enemyHP <= 0)
        {
            SetAnimator(false, false, false, 0);
            animator.SetBool("dead",true);
        }
        if (enemyHP <= status.maxHP / 2) activeLimiter = false;
    }

    public void Dead()
    {
        Invoke("EnegyCoreGenerator",generatorRate);
    }

    void EnegyCoreGenerator()
    {
        Instantiate(enegyCore,new Vector3(transform.position.x,transform.position.y + 0.2f,transform.position.z),Quaternion.identity);
        Destroy(this.gameObject);
    }
}
