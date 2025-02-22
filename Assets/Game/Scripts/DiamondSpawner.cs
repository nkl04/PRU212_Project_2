using UnityEngine;

public class DiamondSpawner : Spawner
{
    private void Start()
    {
        Spawn();
    }
    public override void Spawn()
    {
        // spawn diamond around the map
        if (prefab)
        {
            var radius = 10;
            var p = Vector2.zero;
            var numDiamonds = 10;

            for (var i = 0; i < numDiamonds; i++)
            {
                var diamond = ObjectPooler.Instance.GetObjectFromPool(prefab.name);
                diamond.transform.position = new Vector2(p.x, p.y) + Random.insideUnitCircle.normalized * radius;
                diamond.SetActive(true);
            }
        }
    }
}
