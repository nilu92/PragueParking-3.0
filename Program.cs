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
            CheckVehicleLocation();
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
            using (SqlCommand check_regnumb = new SqlCommand("SELECT COUNT(*) FROM fordon WHERE ([regnumb] = '" + regnumb + "')", cn))
            {
                if (check_regnumb.ExecuteScalar() != null)
                {
                    int regnumbExist = (int)check_regnumb.ExecuteScalar();
                    if (regnumbExist > 0)
                    {
                        Console.WriteLine("regnumber exist!");
                        AddVehicle();
                    }

                }
                //SqlDataReader reader = check_regnumb.ExecuteReader();
                //if (reader.HasRows) 
                //{
                //    Console.WriteLine("already exist");
                //    AddVehicle();
                //}
                //else
                //{
                //    Console.WriteLine("does not exist");
                //}
                //reader.Close();
                //reader.Dispose();
            }

            SqlCommand cmd = new SqlCommand("insert into fordon values('" + id + "','" + typeOfVehicle + "','" + regnumb + "','" + parkspot + "')", cn);


            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                Console.WriteLine("Insertion successful!");
            }
            cn.Close();

        }

        public void RemoveFromDataBase()
        {
            SqlConnection cn = new SqlConnection(@"server = DESKTOP-E57017B\SQLEXPRESS; Database=Parking; Integrated Security=true");
            cn.Open();
            string regnumb;
            Console.WriteLine("Enter regnumb");
            regnumb = Console.ReadLine();
            using (SqlCommand check_regnumb = new SqlCommand("SELECT COUNT(*) FROM fordon WHERE ([regnumb] = '" + regnumb + "')", cn))
            {
                if (check_regnumb.ExecuteScalar() != null)
                {
                    int regnumbExist = (int)check_regnumb.ExecuteScalar();
                    if (regnumbExist > 0)
                    {
                        Console.WriteLine("regnumber exist!");
                        SqlCommand delete = new SqlCommand("Delete from fordon where ([regnumb] = '" + regnumb + "')", cn);
                        delete.ExecuteNonQuery();
                        Console.WriteLine("Vehicle removed");
                        cn.Close();
                        
                    }

                }
            }

        
        }
        public void CheckVehicleLocation() 
        {
            SqlConnection cn = new SqlConnection(@"server = DESKTOP-E57017B\SQLEXPRESS; Database=Parking; Integrated Security=true");
            cn.Open();
            string regnumb;
            Console.WriteLine("Enter regnumb");
            regnumb = Console.ReadLine();
            using (SqlCommand find_regnumb = new SqlCommand("SELECT COUNT(*) FROM fordon WHERE ([regnumb] = '" + regnumb + "')", cn)) 
            {
                if (find_regnumb.ExecuteScalar() != null) 
                {
                    int regnumbExist = (int)find_regnumb.ExecuteScalar();
                    if(regnumbExist > 0) 
                    {
                        Console.WriteLine("regnumb:{0} " , regnumbExist);
                        Console.ReadLine();
                        cn.Close();
                        //ask user what to do with vehicle
                    }
                    else
                        if(regnumbExist < 0) 
                    {
                        Console.WriteLine("There is no regnumber found");
                        CheckVehicleLocation();
                    }
                }


            }
        }
    }
}
