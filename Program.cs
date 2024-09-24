using Auto.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto
{
    public class Program
    {
        public static Connect conn = new Connect();
        static List<Car> cars = new List<Car>();

        static void feltoltes()
        {
            conn.Connection.Open();
            string sql = "SELECT * FROM `cars`";

            MySqlCommand cmd = new MySqlCommand(sql,conn.Connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            do
            {
                Car car = new Car();

                car.Id = reader.GetInt32(0);
                car.Brand = reader.GetString(1);
                car.Type = reader.GetString(2);
                car.License = reader.GetString(3);
                car.Date = reader.GetInt32(4);

                cars.Add(car);
            }
            while (reader.Read());

            
            conn.Connection.Close();
        }
        public static void AddCar()
        {
            conn.Connection.Open();
            Car car = new Car();
            Console.WriteLine("adja meg a Brand");
            car.Brand = Console.ReadLine();
            Console.WriteLine("adja meg a Type");
            car.Type = Console.ReadLine();
            Console.WriteLine("adja meg a License");
            car.License = Console.ReadLine();
            Console.WriteLine("adja meg a Date");
            car.Date = int.Parse(Console.ReadLine());

            string sql = $"INSERT INTO `cars`(`Brand`, `Type`, `License`, `Date`) VALUES ('{car.Brand}','{car.Type}','{car.License}',{car.Date})";

            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
            cmd.ExecuteNonQuery();

            conn.Connection.Close();
        }

        public static void UpdateCar() 
        {
            conn.Connection.Open();
            int date = 2550;
            string sql = $"UPDATE `cars` SET `Date`={date} WHERE Id = 223";

            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
            cmd.ExecuteNonQuery();

            conn.Connection.Close();
        }
        public static void DeleteCar()
        {
            conn.Connection.Open();
            int id = 305;
            string sql = $"DELETE FROM `cars` WHERE `Id`={id}";

            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
            cmd.ExecuteNonQuery();

            conn.Connection.Close();
        }
        public static void Kiiratas()
        {
            conn.Connection.Open();
            Console.WriteLine("adjon meg egy id");
            int id = int.Parse(Console.ReadLine());
            string sql = $"SELECT * FROM `cars` WHERE `Id`={id}";

            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            string brand,type,license ;
            int Id, date;
            reader.Read();
            brand = reader.GetString(1);
            type = reader.GetString(2);
            license = reader.GetString(3);
            Id = reader.GetInt32(0);
            date = reader.GetInt32(4);
       
            Console.WriteLine($"kiiratas: {Id} {brand} {type} {license} {date}");

            conn.Connection.Close();
        }

        static void Main(string[] args)
        {
            feltoltes();
            foreach (Car item in cars)
            {
                Console.WriteLine($"Auto gyártója: {item.Brand}, azonosítója: {item.Id}");
            }
            AddCar();
            UpdateCar();
            DeleteCar();
            Kiiratas();
            Console.ReadLine();
        }
    }
}
