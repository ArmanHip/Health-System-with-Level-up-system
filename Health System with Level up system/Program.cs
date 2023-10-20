using System;
using System.Diagnostics;

namespace HealthSystem
{
    internal class Program
    {
        static int health;
        static int shield;
        static int lives;
        static int xp;
        static int level;

        static string healthwarning;


        static void Main(string[] args)
        {
            // Initializations
            UnitTestHealthSystem();
            UnitTestXPSystem();

            health = 100;
            shield = 100;
            lives = 3;
            xp = 0;
            level = 1;

            // Sample Gameplay

            HealthStatus();

            Console.WriteLine("New Game!");
            Console.WriteLine();

            ShowHud();
            Console.WriteLine();

            Console.WriteLine("Player is about to take 40 damage!");
            Console.WriteLine();
            TakeDamage(40);
            ShowHud();
            Console.WriteLine();

            Console.WriteLine("Player is about to gain 30 XP!");
            Console.WriteLine();
            XP(30);
            ShowHud();
            Console.WriteLine();

            // Trigger Error 

            Console.WriteLine("Player is about to take -15 damage!");
            Console.WriteLine();
            TakeDamage(-15);
            ShowHud();
            Console.WriteLine();

            Console.WriteLine("Player is about to take 120 damage!");
            Console.WriteLine();
            TakeDamage(120);
            ShowHud();
            Console.WriteLine();

            Console.WriteLine("Player is about to heal -10 health back!");
            Console.WriteLine();
            Heal(-10);
            ShowHud();
            Console.WriteLine();

            Console.WriteLine("Player is about to heal 20 health back!");
            Console.WriteLine();
            Heal(20);
            ShowHud();
            Console.WriteLine();

            Console.WriteLine("Player's shield is about to gain 65 charge back!");
            Console.WriteLine();
            RegenerateShield(65);
            ShowHud();
            Console.WriteLine();

            // Trigger error for XP gain

            Console.WriteLine("Player is about to gain -75 XP!");
            Console.WriteLine();
            XP(-75);
            ShowHud();
            Console.WriteLine();

            Console.WriteLine("Player is about to take 200 damage!");
            Console.WriteLine();
            TakeDamage(200);
            ShowHud();
            Console.WriteLine();

            Revive();

            Console.WriteLine();

            Console.WriteLine("Player is about to gain 150 XP!");
            Console.WriteLine();
            XP(150);
            ShowHud();
            Console.WriteLine();

            Console.WriteLine("Player is about to take 50 damage!");
            Console.WriteLine();
            TakeDamage(50);
            ShowHud();
            Console.WriteLine();

            // Trigger error for shield charge gain

            Console.WriteLine("Player is about to gain -35 shield charge back!");
            Console.WriteLine();
            RegenerateShield(-35);
            ShowHud();
            Console.WriteLine();

            Console.WriteLine("Player is about to take 125 damage!");
            Console.WriteLine();
            TakeDamage(125);
            ShowHud();
            Console.WriteLine();

            Console.WriteLine("Player is about to be fully healed!");
            Console.WriteLine();
            Heal(100);
            RegenerateShield(100);
            ShowHud();
            Console.WriteLine();

            Console.WriteLine("Press any key to exit");
            Console.WriteLine();
            Console.ReadKey(true);
        }

        // Shows the player stats

        static void ShowHud()
        {
            Console.WriteLine("Health Status" + ":" + healthwarning);
            Console.WriteLine("Health" + ":" + health);
            Console.WriteLine("Shield" + ":" + shield);
            Console.WriteLine("Lives" + ":" + lives);
            Console.WriteLine("XP" + ":" + xp);
            Console.WriteLine("Level" + ":" + level);
        }

        // Makes the player take damage

        static void TakeDamage(int damage)
        {
            if (damage < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Damage cannot be negative");
                Console.ResetColor();
                return; // return false
            }

            shield -= damage; // Make shield take damage first

            if (shield < 0) // If player shield is 0, make the damage effect player health
            {
                health += shield;
                shield = 0;
            }

            HealthStatus();

            if (health <= 0)
            {
                Console.WriteLine("Player has died!");
            }
        }

        // Add XP and increase levels

        static void XP(int amount)
        {
            if (amount < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: XP cannot be negative");
                Console.ResetColor();
                return; // retrun false 
            }

            xp += amount;
            Console.WriteLine("Player gained" + " " + amount + "XP!");

            // Track XP gain with a while loop

            while (xp >= level * 100)
            {
                xp -= level * 100;
                level = level + 1;
                Console.WriteLine("Player has reached level" + " " + level + "!");
            }
        }

        // Heals the players health

        static void Heal(int heal)
        {
            if (heal < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Can't add negative health!");
                Console.ResetColor();
                return; // retrun false
            }

            health += heal;
            health = Math.Min(health, 100); // Add range check for health
            HealthStatus();
        }

        // Heals the players shield

        static void RegenerateShield(int charge)
        {
            if (charge < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Charge amount cannot be negative!");
                Console.ResetColor();
                return; // retrun false
            }

            shield += charge;
            shield = Math.Min(shield, 100); // Add range check for shield
        }

        // Makes the player revive

        static void Revive()
        {
            if (lives > 0)
            {
                lives = lives - 1;
                health = 100;
                shield = 100;
                HealthStatus();
                Console.WriteLine();
                Console.WriteLine("Player has revived!");
            }

            else
            {
                Console.WriteLine("GAME OVER"); // If the player lost all lives
            }
        }

        // Checks the current health status of the player

        static void HealthStatus()
        {
            health = Math.Max(0, Math.Min(health, 100)); // Prevent the healh going into negatives

            if (health == 0)
                healthwarning = "Dead";
            else if (health <= 10)
                healthwarning = "Imminent Danger";
            else if (health <= 50)
                healthwarning = "Badly Hurt";
            else if (health <= 75)
                healthwarning = "Hurt";
            else if (health < 100)
                healthwarning = "Healthy";
            else
                healthwarning = "Perfect Health";
        }

        // Debug tester

        static void UnitTestHealthSystem()
        {
            Debug.WriteLine("Unit testing Health System started...");

            // TakeDamage()

            // TakeDamage() - only shield
            shield = 100;
            health = 100;
            lives = 3;
            TakeDamage(10);
            Debug.Assert(shield == 90);
            Debug.Assert(health == 100);
            Debug.Assert(lives == 3);

            // TakeDamage() - shield and health
            shield = 10;
            health = 100;
            lives = 3;
            TakeDamage(50);
            Debug.Assert(shield == 0);
            Debug.Assert(health == 60);
            Debug.Assert(lives == 3);

            // TakeDamage() - only health
            shield = 0;
            health = 50;
            lives = 3;
            TakeDamage(10);
            Debug.Assert(shield == 0);
            Debug.Assert(health == 40);
            Debug.Assert(lives == 3);

            // TakeDamage() - health and lives
            shield = 0;
            health = 10;
            lives = 3;
            TakeDamage(25);
            Debug.Assert(shield == 0);
            Debug.Assert(health == 0);
            Debug.Assert(lives == 3);

            // TakeDamage() - shield, health, and lives
            shield = 5;
            health = 100;
            lives = 3;
            TakeDamage(110);
            Debug.Assert(shield == 0);
            Debug.Assert(health == 0);
            Debug.Assert(lives == 3);

            // TakeDamage() - negative input
            shield = 50;
            health = 50;
            lives = 3;
            TakeDamage(-10);
            Debug.Assert(shield == 50);
            Debug.Assert(health == 50);
            Debug.Assert(lives == 3);

            // Heal()

            // Heal() - normal
            shield = 0;
            health = 90;
            lives = 3;
            Heal(5);
            Debug.Assert(shield == 0);
            Debug.Assert(health == 95);
            Debug.Assert(lives == 3);

            // Heal() - already max health
            shield = 90;
            health = 100;
            lives = 3;
            Heal(5);
            Debug.Assert(shield == 90);
            Debug.Assert(health == 100);
            Debug.Assert(lives == 3);

            // Heal() - negative input
            shield = 50;
            health = 50;
            lives = 3;
            Heal(-10);
            Debug.Assert(shield == 50);
            Debug.Assert(health == 50);
            Debug.Assert(lives == 3);

            // RegenerateShield()

            // RegenerateShield() - normal
            shield = 50;
            health = 100;
            lives = 3;
            RegenerateShield(10);
            Debug.Assert(shield == 60);
            Debug.Assert(health == 100);
            Debug.Assert(lives == 3);

            // RegenerateShield() - already max shield
            shield = 100;
            health = 100;
            lives = 3;
            RegenerateShield(10);
            Debug.Assert(shield == 100);
            Debug.Assert(health == 100);
            Debug.Assert(lives == 3);

            // RegenerateShield() - negative input
            shield = 50;
            health = 50;
            lives = 3;
            RegenerateShield(-10);
            Debug.Assert(shield == 50);
            Debug.Assert(health == 50);
            Debug.Assert(lives == 3);

            // Revive()

            // Revive()
            shield = 0;
            health = 0;
            lives = 2;
            Revive();
            Debug.Assert(shield == 100);
            Debug.Assert(health == 100);
            Debug.Assert(lives == 1);

            Debug.WriteLine("Unit testing Health System completed.");
            Console.Clear();
        }

        static void UnitTestXPSystem()
        {
            Debug.WriteLine("Unit testing XP / Level Up System started...");

            // IncreaseXP()

            // IncreaseXP() - no level up; remain at level 1
            xp = 0;
            level = 1;
            XP(10);
            Debug.Assert(xp == 10);
            Debug.Assert(level == 1);

            // IncreaseXP() - level up to level 2 (costs 100 xp)
            xp = 0;
            level = 1;
            XP(105);
            Debug.Assert(xp == 5);
            Debug.Assert(level == 2);

            // IncreaseXP() - level up to level 3 (costs 200 xp)
            xp = 0;
            level = 2;
            XP(210);
            Debug.Assert(xp == 10);
            Debug.Assert(level == 3);

            // IncreaseXP() - level up to level 4 (costs 300 xp)
            xp = 0;
            level = 3;
            XP(315);
            Debug.Assert(xp == 15);
            Debug.Assert(level == 4);

            // IncreaseXP() - level up to level 5 (costs 400 xp)
            xp = 0;
            level = 4;
            XP(499);
            Debug.Assert(xp == 99);
            Debug.Assert(level == 5);

            Debug.WriteLine("Unit testing XP / Level Up System completed.");
            Console.Clear();
        }
    }
}