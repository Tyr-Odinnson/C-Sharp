public interface ISpawnable {
    Spawner spawner { get; set; }

    void SetSpawner(Spawner _spawner);
    void OnDestroy();
}