namespace BasketSchools.Domain;

public interface Repo
{
    void Add(Entity e);

    Entity FindById(int id);
    List<Entity> FindAll();
}