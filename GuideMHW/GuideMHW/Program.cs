using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Npgsql;

namespace GuideMHW
{
    



    interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        int TypeMonster(int type_id);

        

    }


    public class Weapon
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String NewName { get; set; }
        public bool awear { get; set; }

        public Weapon(String Name, String NewName)
        {
            this.Name = Name;
            this.NewName = NewName;
        }


    }

    [Table("monsters", Schema = "Monsters")]
    public class Monsters
    {
        public int Id { get; set; }

        [Column("Name")]
        public String Name { get; set; }

        [Column("description")]
        public String description { get; set; }
        public int habitat { get; set; }
        public int types { get; set; }
        public int elemental_weaknesses { get; set; }
        public int weak_spots { get; set; }

        public int damaged_parts { get; set; }

        public Monsters(String Name, String description, int habitat, int types, int weak_spots, int damaged_parts, int elemental_weaknesses)
        {
            this.Name = Name;
            this.description = description;
            this.habitat = habitat;
            this.types = types;
            this.weak_spots = weak_spots;
            this.damaged_parts = damaged_parts;
            this.elemental_weaknesses = elemental_weaknesses;
            
        }
        public Monsters(int Id, String Name, String description, int habitat, int types, int weak_spots, int damaged_parts, int elemental_weaknesses)
        {
            this.Id = Id;
            this.Name = Name;
            this.description = description;
            this.habitat = habitat;
            this.types = types;
            this.weak_spots = weak_spots;
            this.damaged_parts = damaged_parts;
            this.elemental_weaknesses = elemental_weaknesses;
        }

    }






    public class PostgreRepository : IRepository<Monsters>
    {
        string connectionString = "Host=localhost;Username=postgres;Password=1_j0twmda;Database=mosters";


        



        public IEnumerable<Monsters> GetAll()
        {
            using var con = new NpgsqlConnection(connectionString);
            con.Open();

            using var cmd = new NpgsqlCommand("SELECT * FROM monsters.all_monsters ORDER BY id ASC ", con);

            var reader = cmd.ExecuteReader();
            var result = new List<Monsters>();

            while (reader.Read())
            {
                result.Add(new Monsters(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7)));
            }

            return result;
        }
        public void Create(Monsters Monsters)
        {
            using var con = new NpgsqlConnection(connectionString);
            con.Open();
            
            string sql = string.Format("INSERT INTO  monsters.all_monsters" +
                   "(id, name, description, idofhabitat, idoftypes, " +
                   "idofelemental_weaknesses, idofdamaged_parts, idofweak_spots) " +
                   "VALUES (@id, @name, @description, @idofhabitat, @idoftypes, " +
                   "@idofelemental_weaknesses, @idofdamaged_parts, @idofweak_spots)");
            using var cmd = new NpgsqlCommand(sql, con);

            cmd.Parameters.Add(new NpgsqlParameter("@id", Monsters.Id));
            cmd.Parameters.Add(new NpgsqlParameter("@name", Monsters.Name));
            cmd.Parameters.Add(new NpgsqlParameter("@description", Monsters.description));
            cmd.Parameters.Add(new NpgsqlParameter("@idofhabitat", Monsters.habitat));
            cmd.Parameters.Add(new NpgsqlParameter("@idoftypes", Monsters.types));
            cmd.Parameters.Add(new NpgsqlParameter("@idofelemental_weaknesses", Monsters.elemental_weaknesses));
            cmd.Parameters.Add(new NpgsqlParameter("@idofdamaged_parts", Monsters.damaged_parts));
            cmd.Parameters.Add(new NpgsqlParameter("@idofweak_spots", Monsters.weak_spots));
            cmd.Prepare();

            cmd.ExecuteNonQuery();
        }
        public void Update(Monsters Monsters)
        {
            using var con = new NpgsqlConnection(connectionString);
            con.Open();

            using var cmd = new NpgsqlCommand($"UPDATE monsters.all_monsters SET name=@name, description=@description, " +
                $"idofhabitat=@idofhabitat, " +
                $"idoftypes=@idoftypes, " +
                $"idofelemental_weaknesses=@idofelemental_weaknesses, " +
                $"idofdamaged_parts=@idofdamaged_parts, " +
                $"idofweak_spots=@idofweak_spots WHERE id={Monsters.Id};", con);

            cmd.Parameters.Add(new NpgsqlParameter("name", Monsters.Name));
            cmd.Parameters.Add(new NpgsqlParameter("description", Monsters.description));
            cmd.Parameters.Add(new NpgsqlParameter("idofhabitat", Monsters.habitat));
            cmd.Parameters.Add(new NpgsqlParameter("idoftypes", Monsters.types));
            cmd.Parameters.Add(new NpgsqlParameter("idofelemental_weaknesses", Monsters.elemental_weaknesses));
            cmd.Parameters.Add(new NpgsqlParameter("idofdamaged_parts", Monsters.damaged_parts));
            cmd.Parameters.Add(new NpgsqlParameter("idofweak_spots", Monsters.weak_spots));
            cmd.ExecuteNonQuery();
        }
        public void Delete(int id)
        {
            using var con = new NpgsqlConnection(connectionString);
            con.Open();

            using var cmd = new NpgsqlCommand($"DELETE FROM monsters.all_monsters WHERE id={id};", con);

            cmd.ExecuteNonQuery();
        }

        public int TypeMonster(int type_id)
        {
            using var con = new NpgsqlConnection(connectionString);
            con.Open();

            using var cmd = new NpgsqlCommand($"select count (idoftypes)  from monsters.all_monsters where idoftypes={type_id};", con);

            var reader = cmd.ExecuteReader();
            reader.Read();

            return reader.GetInt32(0);

        }


    }

    class Program
    {
        static void Main(string[] args)
        {
           

            

            

            IRepository<Monsters> myRep = new PostgreRepository();
            char cmd;

            Console.WriteLine("\nБаза данных Монстров и оружия\n");

            do
            {
                Console.WriteLine("\n\n" +
                    "r - Показать таблицу монстров\n" +
                    "w - Добавление новой строки для таблицы монстров\n" +
                    "u - Редактирование строки таблицы монстров\n" +
                    "d - Удаление строки в таблице монстров\n" +
                    "a - Получение кол-ва монстров определенного типа\n" +
                    "j - Показать таблицу оружия (NoSql)\n" +
                    "k - Показать оружие оределенного типа\n" +
                    "m - Показать оружие меньше определенной массы\n"+
                    "l - Показать оружие с уроном от 1 значения до 2 значения\n");

                cmd = Console.ReadKey().KeyChar;
                switch (cmd)
                {
                    case 'r':
                        ShowMonsters(myRep);
                        break;
                    case 'w':
                        AddMonsters(myRep);
                        break;
                    case 'u':
                        UpdateMonsters(myRep);
                        break;
                    case 'd':
                        DeleteMonsters(myRep);
                        break;
                    case 'a':
                        TypeMonsters(myRep);
                        break;
                    case 'j':
                        ShowWeapon();
                        break;
                    case 'k':
                        ShowWeaponType();
                        break;
                    case 'm':
                        ShowWeaponMass();
                        break;
                    case 'l':
                        ShowWeaponDamage();
                        break;


                    case 'z':
                        Console.WriteLine("\n\n\nВыход из программы...");
                        break;
                }

            } while (cmd != 'y');
        }


        public static void ShowWeapon()
        {
            //подключение к NoSql
            var client = new MongoClient(
                    "mongodb://localhost:27017"
                );
            IMongoDatabase db = client.GetDatabase("weapons");
            var weapon = db.GetCollection<BsonDocument>("weapon");
            var documents = weapon.Find(new BsonDocument()).ToList();
            foreach (BsonDocument doc in documents)
            {
                Console.WriteLine(doc.ToString());
            }


        }

        //запросы к no sql
        public static void ShowWeaponType()
        {
            Console.WriteLine("\nВведите название типа оружия:");
            String type = Console.ReadLine();
            //подключение к NoSql
            var client = new MongoClient(
                    "mongodb://localhost:27017"
                );
            IMongoDatabase db = client.GetDatabase("weapons");
            
            var weapon = db.GetCollection<BsonDocument>("weapon");
            var filter = new BsonDocument("Type", type);
            var doc = weapon.Find(filter).ToList();
            foreach (var w in doc)
            {
                Console.WriteLine(w);
            }
            if(doc == null)
            {
                Console.WriteLine("\nОружия такого типа не обнаружено");
            }
        }

        public static void ShowWeaponMass()
        {
            Console.WriteLine("\nВведите массу оружия:");
            int mass = Convert.ToInt32(Console.ReadLine());
            //подключение к NoSql
            var client = new MongoClient(
                    "mongodb://localhost:27017"
                );
            IMongoDatabase db = client.GetDatabase("weapons");

            var weapon = db.GetCollection<BsonDocument>("weapon");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Lt("Mass", mass);
            var doc = weapon.Find(filter).ToList();
            foreach (var w in doc)
            {
                Console.WriteLine(w);
            }
            if (doc == null)
            {
                Console.WriteLine("\nОружия меньше такой массы не обнаружено");
            }
        }

        public static void ShowWeaponDamage()
        {
            Console.WriteLine("\nВведите первое значение:");
            int firstdamage = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nВведите второе значение:");
            int secondtdamage = Convert.ToInt32(Console.ReadLine());
            //подключение к NoSql
            var client = new MongoClient(
                    "mongodb://localhost:27017"
                );
            IMongoDatabase db = client.GetDatabase("weapons");

            var weapon = db.GetCollection<BsonDocument>("weapon");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Gt("Damage", firstdamage) & builder.Lt("Damage", secondtdamage);
            var doc = weapon.Find(filter).ToList();
            foreach (var w in doc)
            {
                Console.WriteLine(w);
            }
            if (doc == null)
            {
                Console.WriteLine("\nОружия в этом промежутке не обнаружено");
            }
        }

        static void ShowMonsters(IRepository<Monsters> myRep)
        {
            var MonstersList = new List<Monsters>(myRep.GetAll());
            Console.WriteLine($"-----------------------------------------------------------------------------------\n");
            Console.WriteLine($"\n\n Id |Имя                 |Описание");
            for (int i = 0; i < MonstersList.Count; i++)
            {
                Console.WriteLine($" {MonstersList[i].Id}  |{MonstersList[i].Name}|{MonstersList[i].description}");
                Console.WriteLine($"    |Место обитания Тип  |Элементальные слабости  Повреждаемые части    Слабые места");
                Console.WriteLine($"     {MonstersList[i].habitat}               {MonstersList[i].types}    {MonstersList[i].elemental_weaknesses}                       {MonstersList[i].damaged_parts}                     {MonstersList[i].weak_spots}\n");
            }
            Console.WriteLine($"-----------------------------------------------------------------------------------\n");
        }

        static void AddMonsters(IRepository<Monsters> myRep)
        {
            int habitat, types, weak_spots, damaged_parts, elemental_weaknesses, id;
            string name, description;

            Console.WriteLine("\nВведите id новго монстра");
            id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nВведите имя новго монстра");
            name = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Введите описание монстра");
            description = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Введите элементальные слабости");
            elemental_weaknesses = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Введите место обитания");
            habitat = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите тип");
            types = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите слабые места");
            weak_spots = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите повреждаемые части");
            damaged_parts = Convert.ToInt32(Console.ReadLine());

            Monsters Monsters = new Monsters(id, name, description, habitat, types, weak_spots, damaged_parts, elemental_weaknesses);

            myRep.Create(Monsters);
        }

        static void UpdateMonsters(IRepository<Monsters> myRep)
        {
            int id, habitat, types, weak_spots, damaged_parts, elemental_weaknesses;
            string name, description;

            Console.WriteLine("\nВведите ID ");
            id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nВведите имя новго монстра");
            name = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Введите описание монстра");
            description = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Введите элементальные слабости");
            elemental_weaknesses = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Введите место обитания");
            habitat = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите тип");
            types = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите слабые места");
            weak_spots = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите повреждаемые части");
            damaged_parts = Convert.ToInt32(Console.ReadLine());


            Monsters Monsters = new Monsters(id, name, description, habitat, types, weak_spots, damaged_parts, elemental_weaknesses);

            myRep.Update(Monsters);
        }
        static void DeleteMonsters(IRepository<Monsters> myRep)
        {
            int id;

            Console.WriteLine("\n Введите ID монстра");
            id = Convert.ToInt32(Console.ReadLine());

            myRep.Delete(id);
        }


        static void TypeMonsters(IRepository<Monsters> myRep)
        {
            int type_id;
            int monstrs;
            Console.WriteLine("\nПодсчитать монстров определенного типа:");
            Console.WriteLine("\nВведите id типа монстров:");
            type_id = Convert.ToInt32(Console.ReadLine());
            monstrs = myRep.TypeMonster(type_id);
            Console.WriteLine($"Монстров типом = {type_id}: {monstrs}");
            
        }
    }
}
