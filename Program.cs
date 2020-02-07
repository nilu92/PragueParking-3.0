using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace PragueParking_3._1
{
    class Program
    {
        static void Main(string[] args)
        {

            SqlConnection cn = new SqlConnection(@"server=DESKTOP-E57017B\SQLEXPRESS; Database=PragueParking; Integrated Security= true" );
            cn.Open();
            Console.WriteLine(" Server Connection : {0}",cn.State);
            Console.ReadLine();
           //TestVariabler ska sättas in i VehicleType i db PragueParking
            int Id = 1;
            int Size = 1;
            
            string regNumb = "123456";
            string vehType = "Car";
            using (SqlCommand check_regNumb = new SqlCommand("SELECT COUNT(*) FROM VehicleType WHERE ([regNumb]= '" + regNumb + "')",cn)) 
            {
                if (check_regNumb.ExecuteScalar() != null) 
                {
                    int regNumbExist = (int)check_regNumb.ExecuteScalar();
                    if(regNumbExist > 0) 
                    {
                        Console.WriteLine("ERROR: Regnumber already exist in database!");
                        Console.ReadLine();
                        System.Environment.Exit(1);
                    }
                }
            
            }
                SqlCommand cmd = new SqlCommand("insert into VehicleType values('" + Id + "','" + vehType + "','" + Size + "','" + regNumb + "')", cn);
            int i = cmd.ExecuteNonQuery();
            cn.Close();
            Console.WriteLine("Insertion succesfull!");
        
        }
    
        public void AddVehicle() 
        {
            SqlConnection cn = new SqlConnection(@"server=DESKTOP-E57017B\SQLEXPRESS; Database=PragueParking; Integrated Security= true");
            cn.Open();
            int Id, Size, parkSpot;
            string regNumb, vehType;
            Console.WriteLine("Enter Id");
            string newId = Console.ReadLine();
            int.TryParse(newId, out Id);
            while(Id == 0 || Id > 100 || Id < 0) 
            {
                Console.WriteLine("Invalid Id");
                int.TryParse(newId, out Id);
                Console.Clear();
            }
            using (SqlCommand check_Id = new SqlCommand("SELECT COUNT(*) FROM vehicleType WHERE ([Id] = '"+Id+"')")) 
            {
                int IdExist = (int)check_Id.ExecuteScalar();
                if(IdExist > 0) 
                {
                    Console.WriteLine("Error: Id already exist");
                    cn.Close();
                    Console.ReadLine();
                    AddVehicle();
                }
            }
            Console.WriteLine("Enter Size");
            string newSize = Console.ReadLine();
            int.TryParse(newSize, out Size);
            
            //lägga till rimlig constraint
            Console.WriteLine(" Enter parkspot");
            string newParkSpot = Console.ReadLine();
            int.TryParse(newParkSpot, out parkSpot);
            while(parkSpot == 0 || parkSpot > 100 || parkSpot < 0) 
            {
                Console.WriteLine("Invalid spot, choose a spot between 1 - 100");
            }
            Console.WriteLine("Enter type of Vehicle");
            vehType = Console.ReadLine();
            vehType.ToLower();
            if(vehType == "MC" || vehType == "mc" || vehType == "m") 
            {
                Size = 1;
            }else
                if(vehType == "Car" || vehType == "car" || vehType == "c") 
            {
                Size = 2;
            }
            Console.WriteLine("Enter registration number");
            regNumb = Console.ReadLine();
            while(regNumb.Length != 6) 
            {
                Console.WriteLine("Invalid registration number! ");
                regNumb = Console.ReadLine();
            }
            using (SqlCommand check_regNumb = new SqlCommand("SELECT COUNT(*) FROM VehicleType WHERE ([regNumb]= '" + regNumb + "')", cn))
            {
                while (check_regNumb.ExecuteScalar() != null)
                {
                    int regNumbExist = (int)check_regNumb.ExecuteScalar();
                    if (regNumbExist > 0)
                    {
                        Console.WriteLine("ERROR: Regnumber already exist in database!");
                        Console.ReadLine();
                        AddVehicle();
                    }
                }

            }
            SqlCommand cmd = new SqlCommand("insert into VehicleType values('" + Id + "','" + vehType + "','" + Size + "','" + regNumb + "')", cn);
            int i = cmd.ExecuteNonQuery();
            cn.Close();
            Console.WriteLine("Insertion succesfull!");

        }
    
    
    }
}
