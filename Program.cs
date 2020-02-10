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
            SearchVehicle();
            // AddVehicle();

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
            string newId = Console.ReadLine();
            while (!int.TryParse(newId, out Id))
            {
                Console.WriteLine("Invalid Input!");
                newId = Console.ReadLine();
            }
            // Id = int.Parse(Console.ReadLine()); ;

            while (Id == 0 || Id > 100 || Id < 0)
            {
                Console.WriteLine("Invalid Id");
                Id = int.Parse(Console.ReadLine());
                Console.Clear();
            }
            using (SqlCommand check_Id = new SqlCommand("SELECT COUNT(*) FROM VehicleType WHERE ([Id] = '" + Id + "')", cn))
            {
                if (check_Id.ExecuteScalar() != null)
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
            while (!int.TryParse(newSize, out Size))
            {
                Console.WriteLine("Invalid Input!");
                newSize = Console.ReadLine();

            }


            //lägga till rimlig constraint
            Console.WriteLine(" Enter parkspot");
            //parkSpot = int.Parse(Console.ReadLine());
            string newparkspot = Console.ReadLine();
           
            
            while (!int.TryParse(newparkspot, out parkSpot))
            {
                Console.WriteLine("Invalid Input!");
                newparkspot = Console.ReadLine();
            }
            while (parkSpot == 0 || parkSpot > 100 || parkSpot < 0)
            {
                Console.WriteLine("Invalid spot, choose a spot between 1 - 100");
                parkSpot = int.Parse(Console.ReadLine());
            }

            using (SqlCommand checkSpot = new SqlCommand("select count(*) from ParkSpot where ([Id] = '" + parkSpot + "')", cn))
            {
                if(checkSpot.ExecuteScalar() != null) 
                {
                    int spotTaken = (int)checkSpot.ExecuteScalar();
                    while(spotTaken == parkSpot) 
                    {
                        Console.WriteLine("Error: spot is taken!");
                        parkSpot = int.Parse(Console.ReadLine());
                    }
                }
            }


            //Lägg till funktion som kollar om vald plats är upptagen.
            Console.WriteLine("Enter type of Vehicle");
            vehType = Console.ReadLine();
            vehType.ToUpper();
            if (vehType == "MC" || vehType == "mc" || vehType == "m")
            {
                Size = 1;
                Console.WriteLine("MC has the size of {0} Press enter to continue ", Size);
                Console.ReadLine();
            }
            else
                if (vehType == "Car" || vehType == "car" || vehType == "c")
            {
                Size = 2;
                Console.WriteLine("Car has the size of {0} Press enter to continue ", Size);
                Console.ReadLine();
            }
            Console.WriteLine("Enter registration number");
           
            regNumb = Console.ReadLine();
            bool CorrectUserInput;
            CorrectUserInput = CheckRegNumb(regNumb);
            while (!CorrectUserInput)
            {
                //CorrectUserInput = false;
                Console.WriteLine("Invalid registration number, the registration numb must be longer than 3 and shorter than 10 ");
                regNumb = Console.ReadLine();
                CorrectUserInput = CheckRegNumb(regNumb);
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
                TimeOfArrival(Id, ArrivalTime);
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
            SqlCommand command = new SqlCommand("insert into ParkSpot values('" + VehicleId + "')", con);
            command.ExecuteNonQuery();
            Console.WriteLine("inserted {0}", parkSpot);
            Console.ReadLine();
            SqlCommand insert = new SqlCommand("insert into Vehicleparkspot values('" + VehicleId + "','" + ParkspotId + "')", con);
            insert.ExecuteNonQuery();
            con.Close();
            Console.WriteLine("jajamensean");
        }
    
        bool CheckRegNumb(string regnumber) 
        {
            string regNumb = regnumber;
            bool CorrectUserInput = false;
            while (CorrectUserInput == false)
            {

                if (regNumb.Length < 3 || regNumb.Length > 10)
                {
                    return false;
                    /*CorrectUserInput = false;
                    Console.WriteLine("Invalid registration number, the registration numb ");
                    regNumb = Console.ReadLine();
                */
                }
                else
                {
                    //CorrectUserInput = true;
                    return true;
                }

               

            }
            return false; 



        }
         void SearchVehicle() 
        {
            string regNumb;
            /* int Id;
             int Size;
             DateTime ArrivalTime;
             */
            SqlConnection con = new SqlConnection(@"server=DESKTOP-E57017B\SQLEXPRESS; Database=PragueParking; Integrated Security= true");
            con.Open();
            //Söka med registration number.
            // få information ID,parkspot,Size,ArrivalTime, pris
            //Användaren väljer vad man vill göra med det angivna fordonet
            Console.WriteLine("Enter registration number!");
            regNumb = Console.ReadLine();
            bool CorrectUserInput;
            CorrectUserInput =  CheckRegNumb(regNumb);
            while (!CorrectUserInput) 
            {
                //CorrectUserInput = false;
                Console.WriteLine("Invalid registration number, the registration numb must be longer than 3 and shorter than 10 ");
                regNumb = Console.ReadLine();
                CorrectUserInput = CheckRegNumb(regNumb);
            }
            // kolla om regnumb finns i databas!
            //lägga till Using,try,catch för att undvika krasch vid felinmatning.
            //Arrival(regNumb);
            
            SqlCommand SelectWithReg = new SqlCommand("SELECT * FROM VehicleType WHERE([RegNumb])='"+regNumb+"' ", con);
            SqlDataReader reader = SelectWithReg.ExecuteReader();
            while (reader.Read()) 
            {
                string VehRegNumber = (string)reader["RegNumb"];
                string VehType = (string)reader["VehType"];
                int Size = (int)reader["Size"];
                int Id = (int)reader["Id"];
                DateTime ArrivalTime = Arrival(Id);
                int parkSpot = GetparkSpot(Id);
                Console.WriteLine("This is your car Regnumb: {0},VehType: {1}, Size : {2}, Id : {3}, DateofArrival : {4}, ParkSpot : {5}",VehRegNumber,VehType,Size,Id,ArrivalTime,parkSpot);
                Console.ReadLine();
            }
            Console.WriteLine("OK");
            Console.ReadLine();
        }
        
        int GetparkSpot(int Id) 
        {
            int ParkspotId = Id;
            SqlConnection con = new SqlConnection(@"server=DESKTOP-E57017B\SQLEXPRESS; Database=PragueParking; Integrated Security= true");
            con.Open();
            SqlCommand GetParkSpot = new SqlCommand("SELECT * FROM Vehicleparkspot WHERE([ParkspotId])='"+ParkspotId+"' ",con);
            SqlDataReader reader = GetParkSpot.ExecuteReader();
            while (reader.Read()) 
            {
                int parkspot = (int)reader["ParkspotId"];
                return parkspot;
            
            }
            return 0;
        }
        DateTime Arrival(int Id) 
        {
            int ArrivalId = Id;
            DateTime date = DateTime.Now;
            SqlConnection con = new SqlConnection(@"server=DESKTOP-E57017B\SQLEXPRESS; Database=PragueParking; Integrated Security= true");
            con.Open();
            SqlCommand DateOfArrival = new SqlCommand("SELECT * FROM ARRIVAL Where([ArrivalId])='"+ArrivalId+"'",con);
            SqlDataReader reader = DateOfArrival.ExecuteReader();
            while (reader.Read()) 
            {
                DateTime arrive = (DateTime)reader["ArrivalTime"];
                return arrive;
            }
            if (!reader.Read()) 
            {
                return date;
            }
            return date;
        }
        
        
        bool CheckIfSpotIsTaken(int parkSpot, string type) 
        {
            //Ska kolla om platsen är tagen och ifall det står en MC eller bil på platsen, det ska kunna stå 2st MC på en plats.
            int checkpark = parkSpot;
            string checkType = type;
            SqlConnection con = new SqlConnection(@"server=DESKTOP-E57017B\SQLEXPRESS; Database=PragueParking; Integrated Security= true");
            con.Open();
            using (SqlCommand checkSpot = new SqlCommand("select count(*) from ParkSpot where ([Id] = '"+checkpark+"')",con)) 
            {
                
            }

                return true;
        }
    
    
    }
}
