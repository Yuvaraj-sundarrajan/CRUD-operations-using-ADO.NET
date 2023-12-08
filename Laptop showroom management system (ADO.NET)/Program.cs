using System;
using System.Security.Cryptography.X509Certificates;
using MySql.Data.MySqlClient;

abstract class DataConnection
{
    public DataConnection()
    {

    }
    public abstract void AddData(MySqlConnection conn);
    public abstract void ReadData(MySqlConnection conn);
    public abstract void UpdateData(MySqlConnection conn);
    public abstract void DeleteData(MySqlConnection conn);
}
class LaptopDatabase : DataConnection
{

    public override void AddData(MySqlConnection conn)
    {
        conn.Open();
         Console.Write("Enter Laptop_ID: ");
        int? Id = int.Parse(Console.ReadLine());

        Console.Write("Enter Laptop_Brandname: ");
        string? Brandname =  Console.ReadLine();

        Console.Write("Enter Laptop_Modelname: ");
        string? Modelname = Console.ReadLine();

        Console.Write("Enter Laptop_Price: ");
        int? Price =  int.Parse(Console.ReadLine());

        string insertQuery = $"insert into laptop( Laptop_id,Laptop_Brand,Model_name,Price) values('{Id}' ,'{Brandname}' , '{Modelname}' ,'{Price} ')";
        MySqlCommand cmd = new MySqlCommand(insertQuery, conn);
        cmd.ExecuteNonQuery();
         Console.WriteLine("$=======*******Laptop related data inserted sucessfully*******=========$");
         Console.WriteLine("=======================================================");
        conn.Close();

    }
    public override void ReadData(MySqlConnection conn)
    {
        conn.Open();
        string sql = "SELECT * FROM laptop";
        MySqlCommand cmd = new MySqlCommand(sql, conn);
        MySqlDataReader rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
            Console.WriteLine(rdr[0] + " --- " + rdr[1] + "---" + rdr[2] + " ---" + rdr[3]);
        }
        rdr.Close();
        Console.WriteLine("$=======****** Laptop details shown Sucessfully******==========$");
        Console.WriteLine("=======================================================");
        conn.Close();
    }
    public override void UpdateData(MySqlConnection conn)
    {
        conn.Open();
        Console.WriteLine("Updating  values into the tables: ");
         Console.WriteLine("What you want to update");
         Console.WriteLine("1. Laptop Brand name");
            Console.WriteLine("2. Laptop Model name");
            Console.WriteLine("3. Laptop Price");
            Console.WriteLine("Which element you want to update please enter your choice.........");
            string? updatechoice = Console.ReadLine();
            string? updatedata=null;
           switch(updatechoice){
            case "1":
                updatedata = "Laptop_Brand";
                Console.Write("Enter the Laptop brand name : ");
                break;
            case "2":
                updatedata = "Model_name";
                Console.Write("Enter the Laptop model name: ");
                break;
            case "3":
                updatedata = "Price";
                Console.Write("Enter the laptop Price: ");
                break;

           }
        string? updatevalue = Console.ReadLine();
        Console.Write("\nEnter Laptop Id : ");
        string? Id = Console.ReadLine();
        string insertQuery = $"UPDATE laptop SET {updatedata}='{updatevalue}' where Laptop_ID ={Id}";
        MySqlCommand cmd = new MySqlCommand(insertQuery, conn);
        MySqlDataReader rdr = cmd.ExecuteReader();
        Console.WriteLine("$======*****Table Updated Sucessfully*****======$");
        Console.WriteLine("=======================================================");
        conn.Close();
       

    }
    public override void DeleteData(MySqlConnection conn)
    {
        conn.Open();
        Console.Write("\nEnter Laptop brand name : ");
        string name = Console.ReadLine()!;
        string insertQuery = $"delete from laptop where Laptop_Brand='{name}'";
        MySqlCommand cmd = new MySqlCommand(insertQuery, conn);
         cmd.ExecuteNonQuery();
        Console.WriteLine("$======****Data deleted  Sucessfully****======$");
        Console.WriteLine("=======================================================");
        conn.Close();
    }
}
class Program
{
    static void Main(string[] args)
    {
         string Str = "server=localhost;user=root;database=Laptop_Shop;port=3306;password=root";
        MySqlConnection conn = new MySqlConnection(Str);
        LaptopDatabase Laptopobj = new LaptopDatabase();

        while (true)
        {
            Console.WriteLine("Laptop Showroom Management System ");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("1. Enter Laptopdetails");
            Console.WriteLine("2. Show Laptopdetails");
            Console.WriteLine("3. Change Laptopdetails");
            Console.WriteLine("4. Clear Laptopdetails");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");
            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Laptopobj.AddData(conn);

                    break;
                case "2":
                    Laptopobj.ReadData(conn);

                    break;
                case "3":
                    Laptopobj.UpdateData(conn);

                    break;
                case "4":
                    Laptopobj.DeleteData(conn);
                    break;
                case "5":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("************Invalid choice. Please try again.***************");
                    break;
            }
        }
    }
}