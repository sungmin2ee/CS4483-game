using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    private void Start()
    {
        Init();
    }
    // Update is called once per frame
    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                break;

        }
    }
    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = 150;
                Arrange();
                break;
            case 1:
                break;
            case 2:
                break;
            default: 
                break;

        }
    }
    void Arrange()
    {
        for(int i = 0; i < count; i++)
        {
            Transform bullet = GameManager.Instance.pool.Get(prefabId).transform;
            bullet.parent = transform;
            Vector3 rotVec = Vector3.forward * 360 * i / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -1); // -1 is Inifinity Per.
        }
    }
}
