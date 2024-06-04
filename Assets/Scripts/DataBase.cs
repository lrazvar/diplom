using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

using Npgsql;
using UnityEngine;
using UnityEngine.Timeline;

public class DataBase
{

    static string connectionString = "Host=localhost;Port=5432;Database=UserData;Username=postgres1;Password=postgres";
    static NpgsqlConnection dbcon;
    
    public static void connectDB()
    {
        dbcon = new NpgsqlConnection(connectionString);
        dbcon.Open();
    }

    public static void disconnectDb()
    {
        dbcon.Close();
        dbcon = null;
    }


    public static bool CheckUser(string login, string password)
    {
       connectDB();
       // string sql = "SELECT COUNT(*) FROM public.users WHERE login = @login AND password = @password";
       string sql = "SELECT (password = crypt(@password, password)) FROM public.users WHERE login = @login";
       using (NpgsqlCommand command = new NpgsqlCommand(sql, dbcon))
       {
           command.Parameters.AddWithValue("@login", login);
           command.Parameters.AddWithValue("@password", password);
           int count = System.Convert.ToInt32(command.ExecuteScalar());
           if (count > 0)
           { // Пользователь существует
                return true;
           }
           else
           { 
               // Пользователь не существует
                return false;
           }
       }
       disconnectDb(); 
    }
    
    public static bool CheckTeacher(string login, string password)
    {

        connectDB();
        string sql = "SELECT is_teacher FROM public.users WHERE login = @login AND password = crypt(@password, password)";

        // Создаем команду для выполнения SQL-запроса
        using (NpgsqlCommand command = new NpgsqlCommand(sql, dbcon))
        {
            command.Parameters.AddWithValue("@login", login);
            command.Parameters.AddWithValue("@password", password);
            object result = command.ExecuteScalar();

            if (result != null)
            {
                return System.Convert.ToBoolean(result);
            }
            else
            {
                return false;
            }
        }
        disconnectDb();
    }

    public static string GetName(string login)
    {
        connectDB();
        // Создаем SQL-запрос
        string sql = "SELECT name FROM public.users WHERE login = @login";

        // Создаем команду для выполнения SQL-запроса
        using (NpgsqlCommand command = new NpgsqlCommand(sql, dbcon))
        {
            command.Parameters.AddWithValue("@login", login);
            object result = command.ExecuteScalar();
            
            if (result != null)
            {
                return System.Convert.ToString(result);
            }
            else
            {
                return "";
            }
        }
        disconnectDb();
    }
    
    public static int GetClass(string login)
    {
        connectDB();
        // Создаем SQL-запрос
        string sql = "SELECT class_id FROM public.users WHERE login = @login";

        // Создаем команду для выполнения SQL-запроса
        using (NpgsqlCommand command = new NpgsqlCommand(sql, dbcon))
        {
            command.Parameters.AddWithValue("@login", login);
            object result = command.ExecuteScalar();
            return System.Convert.ToInt32(result);
            
        }
        disconnectDb();
    }

    public static List<string> GetClasses()
    {
        List<string> classes = new List<string>();

        connectDB();

        // Создаем подключение к базе данных
        string sql = "SELECT name FROM public.classes";

        // Создаем команду для выполнения SQL-запроса
        using (NpgsqlCommand command = new NpgsqlCommand(sql, dbcon))
        {
            // Выполняем SQL-запрос и получаем результат
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                // Читаем данные и добавляем их в список 
                while (reader.Read())
                { 
                    classes.Add(reader.GetString(0));
                }
            }
        }
        
        return classes;
        disconnectDb();
    }


    public static bool CheckDoubleLogin(string login)
    {
        connectDB();
        string sql = "SELECT COUNT(*) FROM public.users WHERE login = @login";
        using (NpgsqlCommand command = new NpgsqlCommand(sql, dbcon))
        {
            command.Parameters.AddWithValue("@login", login);
            int count = System.Convert.ToInt32(command.ExecuteScalar());
            if (count > 0)
            { // Пользователь существует
                return true;
            }
            else
            { 
                // Пользователь не существует
                return false;
            }
        }
        disconnectDb(); 
    }

    public static int GetIdClass(string nameClass)
    {
        connectDB();

        // Создаем подключение к базе данных
        string sql = "SELECT id from public.classes WHERE name = @name";

        // Создаем команду для выполнения SQL-запроса
        // Выполняем SQL-запрос и получаем результат
        using (NpgsqlCommand command = new NpgsqlCommand(sql, dbcon))
        {
            command.Parameters.AddWithValue("@name", nameClass);
            object result = command.ExecuteScalar();
            
            return System.Convert.ToInt32(result);
        }
        
    }

    
    
    public static void CreateUser(string login, string password, string name, bool isTeacher, int idClass)
    {
        connectDB();

        // Создаем подключение к базе данных
        string sql = "INSERT INTO public.users (login, password, is_teacher, name, class_id, score_lvl1, score_lvl2, score_lvl3) VALUES (@login, crypt(@password, gen_salt('md5')), @isTeacher, @name, @class_id, 0, 0, 0)";

        // Создаем команду для выполнения SQL-запроса
        // Выполняем SQL-запрос и получаем результат
        using (NpgsqlCommand command = new NpgsqlCommand(sql, dbcon))
           {
                // Добавляем параметры в SQL-запрос
                command.Parameters.AddWithValue("@login", login);
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@isTeacher", isTeacher);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@class_id", idClass);

                // Выполняем SQL-запрос
                command.ExecuteNonQuery();
            }
        }

    public static string Rating(int idClass)
    {
        connectDB();
        string sql = "SELECT name, (score_lvl1 + score_lvl2 + score_lvl3) AS total_score FROM users WHERE class_id = @class ORDER BY total_score DESC";

        using (NpgsqlCommand command = new NpgsqlCommand(sql, dbcon))
        {
            command.Parameters.AddWithValue("@class", idClass);

            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                StringBuilder sb = new StringBuilder();
                int rank = 1;

                while (reader.Read())
                {
                    string name = reader.GetString(0);
                    int score = reader.GetInt32(1);

                    sb.Append(rank + ". " + name + ": " + score + "\n");

                    rank++;
                }

                return sb.ToString();
            }
        }
    }
    
    
    public static void UpdateScoreLvl1(string login, int newScore)
    {
        connectDB();
        string sql = "UPDATE users SET score_lvl1 = @newScore WHERE login = @login";

        using (NpgsqlCommand command = new NpgsqlCommand(sql, dbcon))
        {
            command.Parameters.AddWithValue("@newScore", newScore);
            command.Parameters.AddWithValue("@login", login);
            command.ExecuteNonQuery();
        }
    }
    
    public static void UpdateScoreLvl2(string login, int newScore)
    {
        connectDB();
        string sql = "UPDATE users SET score_lvl2 = @newScore WHERE login = @login";

        using (NpgsqlCommand command = new NpgsqlCommand(sql, dbcon))
        {
            command.Parameters.AddWithValue("@newScore", newScore);
            command.Parameters.AddWithValue("@login", login);
            command.ExecuteNonQuery();
        }
    }
    
    public static void UpdateScoreLvl3(string login, int newScore)
    {
        connectDB();
        string sql = "UPDATE users SET score_lvl3 = @newScore WHERE login = @login";

        using (NpgsqlCommand command = new NpgsqlCommand(sql, dbcon))
        {
            command.Parameters.AddWithValue("@newScore", newScore);
            command.Parameters.AddWithValue("@login", login);
            command.ExecuteNonQuery();
        }
    }
    
}


    

