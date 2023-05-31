using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        Console.Write("Podaj imię swojego bohatera: ");
        string name = Console.ReadLine();
        Hero hero = new Hero(name);

        Console.WriteLine($"{hero.Name} zaczynasz swoją podróż");
        Console.WriteLine($"Twoje statystyki: {hero}");

        while (hero.Hp > 0)
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"Twoje przedmioty: {string.Join(", ", hero.Items)}");
            Console.WriteLine($"Twoje statystyki: {hero}");
            int pdNeeded = 125 - hero.Pd;
            Console.WriteLine($"Potrzebujesz {pdNeeded} Pd do kolejnego poziomu!");
            Console.WriteLine("1 - walka");
            Console.WriteLine("2 - sklep");

            string input = Console.ReadLine();
            if (input == "stop")
                break;

            if (input == "1")
            {
                if (hero.Level % 5 == 0)
                {
                    Console.WriteLine(new string('!', 50));
                    Console.WriteLine("Nadszedł czas na walkę z Bossem!");
                    Boss boss = new Boss(hero.Level);
                    while (boss.Hp > 0)
                    {
                        Console.WriteLine($"{hero.Name}, walczysz teraz z {boss.Name}");
                        Console.WriteLine($"Przeciwnik ma {boss.Hp} Hp i zadaje ci {boss.Attack} obrażeń");

                        hero.Hp -= boss.Attack;
                        if (hero.Hp <= 0)
                            break;

                        Console.WriteLine($"Zostało ci {hero.Hp} Hp");
                        Console.WriteLine($"Twoje przedmioty: {string.Join(", ", hero.Items)}");
                        Console.WriteLine("1 - atak");
                        Console.WriteLine("2 - uleczyć");

                        string input1 = Console.ReadLine();
                        if (input1 == "stop")
                            break;

                        if (input1 == "1")
                        {
                            Console.WriteLine("1 - wykonaj Zwykły atak");
                            Console.WriteLine("2 - wykonaj Mocny atak");

                            string attackChoice = Console.ReadLine();
                            if (attackChoice == "1")
                            {
                                int attack = hero.RegularAttack();
                                boss.Hp -= attack;
                                Console.WriteLine($"Zadałeś {attack} obrażeń");
                                Console.WriteLine(new string('-', 50));
                            }
                            else if (attackChoice == "2")
                            {
                                if (!hero.HasSword())
                                {
                                    Console.WriteLine(new string('!', 50));
                                    Console.WriteLine("Zadełeś 0 obrażeń. Nie możesz tego zrobić! Potrzebujesz 'Miecz' aby użyć!");
                                }
                                else
                                {
                                    int attack = hero.PowerfulAttack();
                                    boss.Hp -= attack;
                                    Console.WriteLine($"Zadałeś {attack} obrażeń");
                                    Console.WriteLine(new string('-', 50));
                                }
                            }
                        }
                        else if (input1 == "2")
                        {
                            hero.UsePotion();
                        }
                    }

                    if (input1 == "stop")
                        break;

                    if (boss.Hp <= 0)
                    {
                        Console.WriteLine("Brawo! Pokonałeś Bossa!");
                        Console.WriteLine($"Dostałeś {boss.Coins} coinów i {boss.Exp} doświadczenia");
                        hero.Coins += boss.Coins;
                        hero.Exp += boss.Exp;
                        hero.LevelUp();
                    }
                    else
                    {
                        Console.WriteLine(new string('!', 50));
                        Console.WriteLine($"Niestety, {hero.Name} zginął! Koniec gry!");
                        break;
                    }
                }
                else
                {
                    Enemy enemy = new Enemy(hero.Level);
                    while (enemy.Hp > 0)
                    {
                        Console.WriteLine($"{hero.Name}, walczysz teraz z {enemy.Name}");
                        Console.WriteLine($"Przeciwnik ma {enemy.Hp} Hp i zadaje ci {enemy.Attack} obrażeń");

                        hero.Hp -= enemy.Attack;
                        if (hero.Hp <= 0)
                            break;

                        Console.WriteLine($"Zostało ci {hero.Hp} Hp");
                        Console.WriteLine($"Twoje przedmioty: {string.Join(", ", hero.Items)}");
                        Console.WriteLine("1 - atak");
                        Console.WriteLine("2 - uleczyć");

                        string input2 = Console.ReadLine();
                        if (input2 == "stop")
                            break;

                        if (input2 == "1")
                        {
                            Console.WriteLine("1 - wykonaj Zwykły atak");
                            Console.WriteLine("2 - wykonaj Mocny atak");

                            string attackChoice = Console.ReadLine();
                            if (attackChoice == "1")
                            {
                                int attack = hero.RegularAttack();
                                enemy.Hp -= attack;
                                Console.WriteLine($"Zadałeś {attack} obrażeń");
                                Console.WriteLine(new string('-', 50));
                            }
                            else if (attackChoice == "2")
                            {
                                if (!hero.HasSword())
                                {
                                    Console.WriteLine(new string('!', 50));
                                    Console.WriteLine("Zadełeś 0 obrażeń. Nie możesz tego zrobić! Potrzebujesz 'Miecz' aby użyć!");
                                }
                                else
                                {
                                    int attack = hero.PowerfulAttack();
                                    enemy.Hp -= attack;
                                    Console.WriteLine($"Zadałeś {attack} obrażeń");
                                    Console.WriteLine(new string('-', 50));
                                }
                            }
                        }
                        else if (input2 == "2")
                        {
                            hero.UsePotion();
                        }
                    }

                    if (input2 == "stop")
                        break;

                    if (enemy.Hp <= 0)
                    {
                        Console.WriteLine($"Brawo! Pokonałeś przeciwnika {enemy.Name}");
                        Console.WriteLine($"Dostałeś {enemy.Coins} coinów i {enemy.Exp} doświadczenia");
                        hero.Coins += enemy.Coins;
                        hero.Exp += enemy.Exp;
                        hero.LevelUp();
                    }
                    else
                    {
                        Console.WriteLine(new string('!', 50));
                        Console.WriteLine($"Niestety, {hero.Name} zginął! Koniec gry!");
                        break;
                    }
                }
            }
            else if (input == "2")
            {
                Shop shop = new Shop();
                Console.WriteLine("Witaj w sklepie! Co chcesz kupić?");
                Console.WriteLine("1 - Miecz (30 coinów)");
                Console.WriteLine("2 - Mikstura zdrowia (20 coinów)");

                string shopInput = Console.ReadLine();
                if (shopInput == "1")
                {
                    if (hero.Coins < 30)
                    {
                        Console.WriteLine(new string('!', 50));
                        Console.WriteLine("Nie masz wystarczającej ilości coinów!");
                    }
                    else
                    {
                        shop.SellSword(hero);
                    }
                }
                else if (shopInput == "2")
                {
                    if (hero.Coins < 20)
                    {
                        Console.WriteLine(new string('!', 50));
                        Console.WriteLine("Nie masz wystarczającej ilości coinów!");
                    }
                    else
                    {
                        shop.SellPotion(hero);
                    }
                }
            }
        }

        Console.WriteLine(new string('=', 50));
        Console.WriteLine("Dziękujemy za grę!");
    }
}

public class Hero
{
    public string Name { get; private set; }
    public int Level { get; private set; }
    public int Hp { get; set; }
    public int Pd { get; set; }
    public int Coins { get; set; }
    public int Exp { get; set; }
    public List<string> Items { get; set; }

    public Hero(string name)
    {
        Name = name;
        Level = 1;
        Hp = 100;
        Pd = 0;
        Coins = 0;
        Exp = 0;
        Items = new List<string>();
    }

    public override string ToString()
    {
        return $"Imię: {Name}, Poziom: {Level}, HP: {Hp}, PD: {Pd}, Coins: {Coins}, Exp: {Exp}";
    }

    public int RegularAttack()
    {
        return Level * 10;
    }

    public int PowerfulAttack()
    {
        return Level * 20;
    }

    public bool HasSword()
    {
        return Items.Contains("Miecz");
    }

    public void UsePotion()
    {
        Hp += 30;
        Pd += 15;
        Console.WriteLine($"Uleczono {Name} o 30 Hp i dodano 15 Pd");
    }

    public void LevelUp()
    {
        if (Exp >= 125)
        {
            Level++;
            Exp = 0;
            Console.WriteLine($"Gratulacje! {Name} awansował na poziom {Level}");
        }
    }
}

public class Enemy
{
    private static readonly string[] Names = { "Wilk", "Pająk", "Troll", "Smok" };
    private static readonly Random random = new Random();

    public string Name { get; private set; }
    public int Hp { get; private set; }
    public int Attack { get; private set; }
    public int Coins { get; private set; }
    public int Exp { get; private set; }

    public Enemy(int level)
    {
        Name = Names[random.Next(Names.Length)];
        Hp = level * 50;
        Attack = level * 10;
        Coins = level * 10;
        Exp = level * 25;
    }
}

public class Boss
{
    private static readonly string[] Names = { "Potwór", "Demon", "Czarnoksiężnik" };
    private static readonly Random random = new Random();

    public string Name { get; private set; }
    public int Hp { get; private set; }
    public int Attack { get; private set; }
    public int Coins { get; private set; }
    public int Exp { get; private set; }

    public Boss(int level)
    {
        Name = Names[random.Next(Names.Length)];
        Hp = level * 100;
        Attack = level * 20;
        Coins = level * 50;
        Exp = level * 75;
    }
}

public class Shop
{
    public void SellSword(Hero hero)
    {
        hero.Items.Add("Miecz");
        hero.Coins -= 30;
        Console.WriteLine($"{hero.Name}, zakupiłeś Miecz za 30 coinów");
    }

    public void SellPotion(Hero hero)
    {
        hero.Items.Add("Mikstura zdrowia");
        hero.Coins -= 20;
        Console.WriteLine($"{hero.Name}, zakupiłeś Miksturę zdrowia za 20 coinów");
    }
}
