using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[ObjectSystem]
public class NpcerComponentAwakeSystem : AwakeSystem<NpcerComponent>
{
    public override void Awake(NpcerComponent self)
    {
        self.Awake();
    }
}

public class NpcerComponent : Component
{
    public static NpcerComponent Instance { get; private set; }

    public long spawnCount = 4;

    private readonly Dictionary<long, Npcer> IdNpcers = new Dictionary<long, Npcer>();

    public void Awake()
    {
        Instance = this;
    }

    public void AddAll(Npcer[] enemys)
    {
        foreach (Npcer tem in enemys)
        {
            if (IdNpcers.Keys.Contains(tem.Id)) return;
            this.IdNpcers.Add(tem.Id, tem);
            tem.Parent = this;
        }
    }
    public void Add(Npcer booker)
    {
        this.IdNpcers.Add(booker.Id, booker);
        booker.Parent = this;
    }

    public Npcer Get(long id)
    {
        this.IdNpcers.TryGetValue(id, out Npcer booker);
        return booker;
    }

    public void Remove(long id)
    {
        this.IdNpcers.Remove(id);
    }

    public int Count
    {
        get
        {
            return this.IdNpcers.Count;
        }
    }

    public Npcer[] GetAll()
    {
        return this.IdNpcers.Values.ToArray();
    }

    public override void Dispose()
    {
        if (this.IsDisposed)
        {
            return;
        }
        base.Dispose();

        foreach (Npcer booker in this.IdNpcers.Values)
        {
            booker.Dispose();
        }

        this.IdNpcers.Clear();

        Instance = null;
    }

}