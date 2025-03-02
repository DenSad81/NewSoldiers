using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Program
{
    private static void Main(string[] args)
    {
        Random random = new Random();
        CreaterSoldiers createSoldiers = new CreaterSoldiers();
        FilterByFirstLetter filter = new FilterByFirstLetter("B");
        List<Soldier> soldiers1 = createSoldiers.Create(random);
        List<Soldier> soldiers2 = createSoldiers.Create(random);

        new Shower(soldiers1).ShowData();
        new Shower(soldiers2).ShowData();

        if (filter.TryGetDataByFilter(soldiers1, out IEnumerable<Soldier> filtredCollection) == false)
        {
            Console.WriteLine("Matches not found");
            return;
        }

        new Shower((soldiers1.Except(filtredCollection)).ToList()).ShowData();
        new Shower((soldiers2.Union(filtredCollection)).ToList()).ShowData();
    }
}

public interface IShowData
{
    void ShowData();
}

public class Shower : IShowData
{
    private IEnumerable<Object> _data;

    public Shower(IEnumerable<object> data)
    {
        _data = data;
    }

    public void ShowData()
    {
        if (_data is List<Soldier>)
        {
            List<Soldier> soldiers = (List<Soldier>)_data;

            foreach (var soldier in soldiers)
                soldier.ShowData();

            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("Wrong data type");
            return;
        }
    }
}

public class Soldier : IShowData
{
    private static int s_ids = 0;

    public Soldier(string name, string rank, string weapon, int lifeTime)
    {
        Name = name;
        Weapon = weapon;
        LifeTime = lifeTime;
        Rank = rank;
        Id = s_ids++;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Weapon { get; private set; }
    public string Rank { get; private set; }
    public int LifeTime { get; private set; }

    public void ShowData() =>
        Console.WriteLine($"ID: {Id} Name: {Name} Ranks: {Rank} Weapon: {Weapon} LifeTime: {LifeTime}");
}

public class FilterByFirstLetter
{
    private string _firstLetter;

    public FilterByFirstLetter(string firstLetter = "A")
    {
        _firstLetter = firstLetter.ToUpper();
    }

    public bool TryGetDataByFilter(List<Soldier> rawCollection, out IEnumerable<Soldier> filtredCollection)
    {
        filtredCollection = rawCollection.Where(soldier => soldier.Name.ToUpper().StartsWith(_firstLetter));

        if (filtredCollection != null)
            return true;
        else
            return false;
    }
}

public class CreaterSoldiers
{
    public List<Soldier> Create(Random random)
    {
        int minLifeTime = 0;
        int maxLifeTime = 24;
        int quantityOfSoldiers = 20;
        List<Soldier> soldiers = new List<Soldier>();

        for (int i = 0; i < quantityOfSoldiers; i++)
        {
            Names tempName = (Names)random.Next(0, (int)Names.MaxValue);
            string name = tempName.ToString();
            Ranks tempRank = (Ranks)random.Next(0, (int)Ranks.MaxValue);
            string rank = tempRank.ToString();
            Weapons tempWeapon = (Weapons)random.Next(0, (int)Weapons.MaxValue);
            string weapon = tempWeapon.ToString();
            int tempLifeTame = random.Next(minLifeTime, maxLifeTime);

            soldiers.Add(new Soldier(name, rank, weapon, tempLifeTame));
        }

        return soldiers;
    }
}

public enum Names
{
    Pavel,
    Boris,
    Klaiv,
    Ken,
    Pol,
    Chak,
    Ben,
    Den,
    Alex,
    Petr,
    Ger,
    Andre,
    Iosif,
    Donald,
    Mark,
    Abby,
    MaxValue
}

public enum Weapons
{
    Gun,
    MachineGun,
    Rifle,
    Shotgun,
    Grenade,
    GrenadeLauncher,
    Bomb,
    NuclearBomb,
    MaxValue
}

public enum Ranks
{
    Cavalier,
    Crusader,
    Housecarl,
    Ninja,
    Samurai,
    Viking,
    Archer,
    HorseArcher,
    Crossbowman,
    Arquebusier,
    MaxValue
}