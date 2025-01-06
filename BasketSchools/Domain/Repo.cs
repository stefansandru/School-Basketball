namespace BasketSchools.Domain;

public interface Repo
{
    void Add(Entity e);
    void Delete(int id);
    void Update(Entity e);
    Entity FindById(int id);
    List<Entity> FindAll();
}