using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace PragueParking_3._1
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();

            /*int ArrivalId = 5;
            DateTime ArrivalTime = DateTime.Now;
            SqlConnection con = new SqlConnection(@"server=DESKTOP-E57017B\SQLEXPRESS; Database=PragueParking; Integrated Security= true");
            con.Open();
            SqlCommand insert = new SqlCommand("insert into ARRIVAL values('"+ArrivalId+"','"+ArrivalTime+"')",con);
            insert.ExecuteNonQuery();
            Console.WriteLine("yaaay");
            con.Close();
            Console.ReadLine();
            */
          
           
            /* SqlConnection con = new SqlConnection(@"server=DESKTOP-E57017B\SQLEXPRESS; Database=PragueParking; Integrated Security= true");
             con.Open();
             SqlCommand insert = new SqlCommand("insert into Vehicleparkspot values('" + VehicleId + "','" + ParkspotId + "')", con);
             insert.ExecuteNonQuery();
             con.Close();
             Console.WriteLine("Insertion success!");
             Console.ReadLine();
             System.Environment.Exit(1);
            */
            /* SqlCommand com = new SqlCommand("DELETE FROM VehicleType",cn);
             com.ExecuteNonQuery();
             Console.WriteLine("Success");
             Console.ReadLine();
             */

            /*
             SqlConnection cn = new SqlConnection(@"server=DESKTOP-E57017B\SQLEXPRESS; Database=PragueParking; Integrated Security= true" );
             cn.Open();
             Console.WriteLine(" Server Connection : {0}",cn.State);
             Console.ReadLine();
            //TestVariabler ska sättas in i VehicleType i db PragueParking
             int Id = 2;
             int Size = 1;

             string regNumb = "test123";
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
             */
        }
    
        public void Run() 
        {
            AddVehicle();
        
        }
        public void AddVehicle() 
        {
            DateTime ArrivalTime = DateTime.Now;
            SqlConnection cn = new SqlConnection(@"server=DESKTOP-E57017B\SQLEXPRESS; Database=PragueParking; Integrated Security= true");
            cn.Open();
            Console.WriteLine(" Server Connection : {0}", cn.State);
            Console.ReadLine();
            
            int Id, Size, parkSpot;
            string regNumb, vehType;
            Console.WriteLine("Enter Id");
            Id = int.Parse(Console.ReadLine()); ;
            
            while(Id == 0 || Id > 100 || Id < 0) 
            {
                Console.WriteLine("Invalid Id");
                Id = int.Parse(Console.ReadLine());
                Console.Clear();
            }
            using (SqlCommand check_Id = new SqlCommand("SELECT COUNT(*) FROM VehicleType WHERE ([Id] = '" + Id+"')",cn)) 
            {
                if (check_Id.ExecuteScalar()!=null) 
                {
                    int IdExist = (int)check_Id.ExecuteScalar();

                    if (IdExist > 0)
                    {
                        Console.WriteLine("Error: Id already exist");
                        cn.Close();
                        Console.ReadLine();
                        AddVehicle();
                    }

                }
                
            }
            
            Console.WriteLine("Enter Size");
            string newSize = Console.ReadLine();
            while(!int.TryParse(newSize,out Size)) 
            {
                Console.WriteLine("Invalid Input!");
               newSize = Console.ReadLine();
              
            }
            
            
            //lägga till rimlig constraint
            Console.WriteLine(" Enter parkspot");
            //parkSpot = int.Parse(Console.ReadLine());
            string newparkspot = Console.ReadLine();
            while(!int.TryParse(newparkspot, out parkSpot)) 
            {
                Console.WriteLine("Invalid Input!");
                newparkspot = Console.ReadLine();
            }
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
            if(regNumb.Length != 6) 
            {
                Console.WriteLine("Invalid registration number! ");
                regNumb = Console.ReadLine();
            }
            using (SqlCommand check_regNumb = new SqlCommand("SELECT COUNT(*) FROM VehicleType WHERE ([regNumb]= '" + regNumb + "')", cn))
            {
                if (check_regNumb.ExecuteScalar() != null)
                {
                    int regNumbExist = (int)check_regNumb.ExecuteScalar();
                    if (regNumbExist > 0)
                    {
                        Console.WriteLine("ERROR: Regnumber already exist in database!");
                        Console.ReadLine();
                       // AddVehicle();
                    }
                }

            }

            SqlCommand cmd = new SqlCommand("insert into VehicleType values('" + Id + "','" + vehType + "','" + Size + "','" + regNumb + "')", cn);
            int i = cmd.ExecuteNonQuery();
            
            
            Console.WriteLine("Insertion succesfull!");
            Console.ReadLine();
            ParkVehicle(Id, parkSpot);
            TimeOfArrival(Id,ArrivalTime);
            cn.Close();
            


        }
        void TimeOfArrival(int Id, DateTime Arrival) 
        {
            int ArrivalId = Id;
            DateTime ArrivalTime = Arrival;
            SqlConnection con = new SqlConnection(@"server=DESKTOP-E57017B\SQLEXPRESS; Database=PragueParking; Integrated Security= true");
            con.Open();
            SqlCommand insert = new SqlCommand("insert into ARRIVAL values('" + ArrivalId + "','" + ArrivalTime + "')", con);
            insert.ExecuteNonQuery();
            con.Close();
        }
        void ParkVehicle(int Id, int parkSpot) 
        {
            int VehicleId = Id;
            int ParkspotId = parkSpot;
            SqlConnection con = new SqlConnection(@"server=DESKTOP-E57017B\SQLEXPRESS; Database=PragueParking; Integrated Security= true");
            con.Open();
            Console.WriteLine(" Server Connection : {0}", con.State);
            SqlCommand command = new SqlCommand("insert into ParkSpot values('"+VehicleId+"')",con);
            command.ExecuteNonQuery();
            Console.WriteLine("inserted {0}",parkSpot);
            Console.ReadLine();
            SqlCommand insert = new SqlCommand("insert into Vehicleparkspot values('"+VehicleId+"','"+ParkspotId+"')",con);
            insert.ExecuteNonQuery();
            con.Close();
            Console.WriteLine("jajamensean");
        }
    }
}
