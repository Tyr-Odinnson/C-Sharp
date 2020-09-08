using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, ISpawnable {
    public int hp = 3;
    public int maxHP = 3;
    public ParticleSystem explosionPrefab;

    private Tree target;
    private NavMeshAgent agent;
    private float t;
    private GUISkin skin;

    public Spawner spawner { get; set; }

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();

        skin = (GUISkin)Resources.Load("GUISkins/Skin");
        
        maxHP = hp;
    }

    private void Update() {
        GetTarget();

        if (!target) return;

        if (agent.remainingDistance <= agent.stoppingDistance) {
            ForceRotation();
            Attack();
        }
    }

    private void Attack() {
        t += Time.deltaTime;

        if (t >= 3) {
            t = 0;
            target.Degrade();
        }
    }

    private void ForceRotation() {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.x, direction.z);
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, angle * Mathf.Rad2Deg, 0));

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 180 * Time.deltaTime);
    }

    private void GetTarget() {
        if (target) return;

        Tree[] trees = FindObjectsOfType<Tree>();
        int r = Random.Range(0, trees.Length);
        target = trees[r];
        agent.SetDestination(target.transform.position);
    }

    public void SetSpawner(Spawner _spawner) {
        spawner = _spawner;
    }

    private void OnGUI() {
        if (hp < maxHP) {
            float division = (600 / (float)Screen.height);
            float w = 40 / division;
            float h = 10 / division;

            GUI.skin = skin;

            Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            screenPos.x -= w * 0.5f;
            screenPos.y = -screenPos.y + Screen.height - (80 / division);

            GUI.color = Color.red;
            GUI.Box(new Rect(screenPos, new Vector2(w, h)), "", skin.GetStyle("health"));

            GUI.color = Color.green;
            GUI.Box(new Rect(screenPos, new Vector2(w * (float)hp / maxHP, h)), "", skin.GetStyle("health"));
        }
    }

    public void Degrade() {
        hp--;

        if (hp <= 0) {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    
    public void OnDestroy() {
        spawner.Deduct();
    }
}
