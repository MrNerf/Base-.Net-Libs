using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using _04_AutoLotDAL.Models;

namespace _04_AutoLotDAL.DataOperations
{
    public class InventoryDal : IDisposable
    {
        #region Fields

        private readonly string _connectionString;

        private SqlConnection _sqlConnection;

        #endregion

        #region Ctors

        public InventoryDal() : this(@"Data Source = DRAGON; Initial Catalog = AutoLot; Integrated Security = True") { }


        public InventoryDal(string connectionString) => _connectionString = connectionString;

        #endregion

        #region Methods

        public void OpenConnection()
        {
            _sqlConnection = new SqlConnection(_connectionString);
            _sqlConnection.Open();
        }

        public void CloseConnection()
        {
            if (_sqlConnection?.State != ConnectionState.Closed)
            {
                _sqlConnection?.Close();
            }
            
        }

        public List<Car> GetInventoryCars()
        {
            OpenConnection();
            var inventoryCar = new List<Car>();
            using (var sqlDataCommand = new SqlCommand("SELECT * FROM Inventory", _sqlConnection))
            {
                sqlDataCommand.CommandType = CommandType.Text;
                using (var sqlDataReader = sqlDataCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (sqlDataReader.Read())
                    {
                        inventoryCar.Add( new Car()
                        {
                            CarId = (int) sqlDataReader["CarId"],
                            Color = (string)sqlDataReader["Color"],
                            Mark = (string)sqlDataReader["Mark"],
                            PetName = (string)sqlDataReader["PetName"]
                        });
                    }
                }
            }

            return inventoryCar;
        }

        public Car GetOneCars(int carId)
        {
            OpenConnection();
            var car = new Car();
            using (var sqlDataCommand = new SqlCommand($"SELECT * FROM Inventory WHERE CarId = {carId}", _sqlConnection))
            {
                sqlDataCommand.CommandType = CommandType.Text;
                using (var sqlDataReader = sqlDataCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (sqlDataReader.Read())
                    {
                        car.CarId = (int) sqlDataReader["CarId"];
                        car.Color = (string) sqlDataReader["Color"];
                        car.Mark = (string) sqlDataReader["Mark"];
                        car.PetName = (string) sqlDataReader["PetName"];
                    }
                }
            }

            return car;
        }

        public int InsertAuto(string color, string mark, string petName)
        {
            OpenConnection();
            int executeResult;
            using (var sqlCommend = new SqlCommand($"INSERT INTO Inventory (Mark, Color, PetName) VALUES ('{mark}', '{color}', '{petName}')", _sqlConnection))
            {
                sqlCommend.CommandType = CommandType.Text;
                executeResult = sqlCommend.ExecuteNonQuery();
            }

            CloseConnection();
            return executeResult;
        }

        public int InsertAuto(Car car)
        {
            OpenConnection();
            int executeResult;
            using (var sqlCommend = new SqlCommand($"INSERT INTO Inventory (Mark, Color, PetName) VALUES ('{car.Mark}', '{car.Color}', '{car.PetName}')", _sqlConnection))
            {
                sqlCommend.CommandType = CommandType.Text;
                executeResult = sqlCommend.ExecuteNonQuery();
            }

            CloseConnection();
            return executeResult;
        }

        public int DeleteAuto(int carId)
        {
            OpenConnection();
            int executeResult;
            using (var sqlCommend = new SqlCommand($"DELETE FROM Inventory WHERE CarId = '{carId}'", _sqlConnection))
            {
                try
                {
                    sqlCommend.CommandType = CommandType.Text;
                    executeResult = sqlCommend.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    throw new Exception($"Программа вернула ошибку {e.Message}\n" +
                                        "Сообщение для пользователя: Данный автомобиль продан");
                }
            }

            CloseConnection();
            return executeResult;
        }

        public int UpdateAuto(int carId, string petName)
        {
            OpenConnection();
            int executeResult;
            using (var sqlCommend = new SqlCommand($"UPDATE Inventory SET PetName = '{petName}' WHERE CarId = '{carId}'", _sqlConnection))
            {
                sqlCommend.CommandType = CommandType.Text;
                executeResult = sqlCommend.ExecuteNonQuery();
            }

            CloseConnection();
            return executeResult;
        }

        #endregion

        public void Dispose()
        {
            CloseConnection();
        }
    }
}
