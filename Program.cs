using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace PragueParking_3._0
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
           
        }
   
        public void Run() 
        {
            
        }
    
        public void AddVehicle() 
        {

            SqlConnection cn = new SqlConnection(@"server = DESKTOP-E57017B\SQLEXPRESS; Database=Parking; Integrated Security=true");
            cn.Open();
            int id, parkspot;
            string typeOfVehicle, regnumb;
            Console.WriteLine("Enter id and parkspot");
            id = int.Parse(Console.ReadLine());
            parkspot = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter type and regnumb");
            typeOfVehicle = Console.ReadLine();
            //Add function that checks if regnumb exist in table
            regnumb = Console.ReadLine();
            
            SqlCommand cmd = new SqlCommand("insert into fordon values('" + id + "','" + typeOfVehicle + "','" + regnumb + "','" + parkspot + "')", cn);
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                Console.WriteLine("Insertion successful!");
            }
            cn.Close();

        }
   
        public void CheckForDuplicates() 
        {
            
        
        }
    
    }
}
