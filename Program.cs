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
        CreaterData createSoldiers = new CreaterData();
        List<Soldier> soldiers1 = createSoldiers.CreateSoldiers(random);
        List<Soldier> soldiers2 = createSoldiers.CreateSoldiers(random);

        ShowData(soldiers1);
        ShowData(soldiers2);
        GetNewData(ref soldiers1, ref soldiers2);
        ShowData(soldiers1);
        ShowData(soldiers2);
    }

    private static void ShowData(List<Soldier> soldiers)
    {
        foreach (var soldier in soldiers)
       soldier.ShowData();

        Console.WriteLine();
    }

    private static void GetNewData(ref List<Soldier> soldiers1, ref List<Soldier> soldiers2)
    {
        IEnumerable<Soldier> filtredSoldiers = soldiers1.Where(sold => sold.Name.ToUpper().StartsWith("B"));
        soldiers2 = soldiers2.Union(filtredSoldiers).ToList();
        soldiers1 = soldiers1.Except(filtredSoldiers).ToList();
    }
}

public  class Soldier
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

    public void ShowData()=>
        Console.WriteLine($"ID: {Id} Name: {Name} Ranks: {Rank} Weapon: {Weapon} LifeTime: {LifeTime}");
}

public  class CreaterData
{
    public List<Soldier> CreateSoldiers(Random random)
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