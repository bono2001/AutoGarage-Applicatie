using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGarage_Applicatie
{
    public class DAL
    {
        private readonly string connectionString;

        public DAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DataTable GetOwners()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Owners", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable GetVehicles()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Vehicles", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public void AddOwner(string name)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Owners (Name) VALUES (@Name)", conn);
                cmd.Parameters.AddWithValue("@Name", name);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void AddVehicle(string licensePlate, string description, bool isCommercial, int? towingCapacity, int ownerId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Vehicles (LicensePlate, Description, IsCommercial, TowingCapacity, OwnerID) VALUES (@LicensePlate, @Description, @IsCommercial, @TowingCapacity, @OwnerID)", conn);
                cmd.Parameters.AddWithValue("@LicensePlate", licensePlate);
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@IsCommercial", isCommercial);
                cmd.Parameters.AddWithValue("@TowingCapacity", (object)towingCapacity ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@OwnerID", ownerId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateLicensePlate(int vehicleId, string newLicensePlate)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Vehicles SET LicensePlate = @LicensePlate WHERE VehicleID = @VehicleID", conn);
                cmd.Parameters.AddWithValue("@LicensePlate", newLicensePlate);
                cmd.Parameters.AddWithValue("@VehicleID", vehicleId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteVehicle(int vehicleId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Vehicles WHERE VehicleID = @VehicleID", conn);
                cmd.Parameters.AddWithValue("@VehicleID", vehicleId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable GetOwnerVehicles(int ownerId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Vehicles WHERE OwnerID = @OwnerID", conn);
                cmd.Parameters.AddWithValue("@OwnerID", ownerId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}

