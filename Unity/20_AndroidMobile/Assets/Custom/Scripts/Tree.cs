using UnityEngine;

public class Tree : MonoBehaviour, ISpawnable {
    public int hp = 10;
    private int maxHP;
    private GUISkin skin;

    public Spawner spawner { get; set; }

    private void Awake() {
        skin = (GUISkin)Resources.Load("GUISkins/Skin");
        maxHP = hp;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.tag == "Player") {
            Destroy(gameObject);
        }
    }

    private void OnGUI() {
        if (hp < maxHP) {
            float division = (600 / (float)Screen.height);
            float w = 40 / division;
            float h = 10 / division;

            GUI.skin = skin;

            Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            screenPos.x -= w * 0.5f;
            screenPos.y = -screenPos.y + Screen.height - (100 / division);

            GUI.color = Color.red;
            GUI.Box(new Rect(screenPos, new Vector2(w, h)), "", skin.GetStyle("health"));

            GUI.color = Color.green;
            GUI.Box(new Rect(screenPos, new Vector2(w * (float)hp / maxHP, h)), "", skin.GetStyle("health"));
        }
    }

    public void SetSpawner(Spawner _spawner) {
        spawner = _spawner;
    }

    public void Degrade() {
        hp--;

        if (hp <= 0) {
            Destroy(gameObject);
        }
    }

    public void OnDestroy() {
        spawner.Deduct();
    }
}
