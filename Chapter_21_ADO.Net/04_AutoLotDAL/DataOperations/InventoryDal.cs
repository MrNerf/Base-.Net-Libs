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

        /// <summary>
        /// Создание подключения к БД с значением по умолчанию
        /// </summary>
        public InventoryDal() : this(@"Data Source = DRAGON; Initial Catalog = AutoLot; Integrated Security = True") { }

        /// <summary>
        /// Создание подключения к БД с заданной строкой подключения
        /// </summary>
        /// <param name="connectionString">Строка подключения к БД</param>
        public InventoryDal(string connectionString) => _connectionString = connectionString;

        #endregion

        #region Methods

        #region Base Methods

        /// <summary>
        /// Открывает подключение к базе данных
        /// </summary>
        public void OpenConnection()
        {
            _sqlConnection = new SqlConnection(_connectionString);
            _sqlConnection.Open();
        }

        /// <summary>
        /// Закрывает подключение к базе данных, если оно не было установлено
        /// </summary>
        public void CloseConnection()
        {
            if (_sqlConnection?.State != ConnectionState.Closed)
            {
                _sqlConnection?.Close();
            }
            
        }

        #endregion

        #region Queries

        /// <summary>
        /// Возвращает список записей в таблице Inventory
        /// </summary>
        /// <returns>Список записей в таблице Inventory</returns>
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

        /// <summary>
        /// Возвращает запись из таблицы Inventory
        /// </summary>
        /// <param name="carId">Значение carId в таблице Inventory</param>
        /// <returns>Возвращает найденный в таблице объект Car</returns>
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

        /// <summary>
        /// Метод добавления записи в таблицу Inventory
        /// </summary>
        /// <param name="color">Цвет автомобиля</param>
        /// <param name="mark">Марка автомобиля</param>
        /// <param name="petName">Дружественное имя автомобиля</param>
        /// <returns>Статус добавления строки в таблицу</returns>
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

        /// <summary>
        /// Метод добавления записи в таблицу Inventory
        /// </summary>
        /// <param name="car">Объект Car, который необходимо добавить в таблицу</param>
        /// <returns>Статус добавления строки в таблицу</returns>
        public int InsertAuto(Car car)
        {
            OpenConnection();
            int executeResult;
            using (var sqlCommend = new SqlCommand($"INSERT INTO Inventory (Mark, Color, PetName) VALUES (@Mark, @Color, @PetName)", _sqlConnection))
            {
                sqlCommend.CommandType = CommandType.Text;
                sqlCommend.Parameters.Add(new SqlParameter("@Mark", SqlDbType.NVarChar, 50) {Value = car.Mark});
                sqlCommend.Parameters.Add(new SqlParameter("@Color", SqlDbType.NVarChar, 50) { Value = car.Color });
                sqlCommend.Parameters.Add(new SqlParameter("@PetName", SqlDbType.NVarChar, 50) { Value = car.PetName });
                executeResult = sqlCommend.ExecuteNonQuery();
            }

            CloseConnection();
            return executeResult;
        }

        /// <summary>
        /// Метод удаления записи из таблицы Inventory
        /// </summary>
        /// <param name="carId">Значение carId для удаления</param>
        /// <returns>Статус удаления строки</returns>
        public int DeleteAuto(int carId)
        {
            OpenConnection();
            int executeResult;
            using (var sqlCommend = new SqlCommand($"DELETE FROM Inventory WHERE CarId = (@CarId)", _sqlConnection))
            {
                try
                {
                    sqlCommend.CommandType = CommandType.Text;
                    sqlCommend.Parameters.Add(new SqlParameter("@CarId", SqlDbType.Int) { Value = carId });
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

        /// <summary>
        /// Метод обновления записи в таблице Inventory
        /// </summary>
        /// <param name="carId">Значение carId для обновления</param>
        /// <param name="petName">Дружественное имя для обновления</param>
        /// <returns>Статус обновления строки</returns>
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

        #region StoredProcedure

        /// <summary>
        /// Запуск хранимой процедуры
        /// </summary>
        /// <param name="carId">Значение carId для которого необходимо вернуть дружественное имя</param>
        /// <returns>Дружественное имя автомобиля</returns>
        public string LookUpPetName(int carId)
        {
            OpenConnection();
            string petName;
            using (var sqlCommand = new SqlCommand("GetPetName", _sqlConnection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@CarId", SqlDbType.Int) {Value = carId, Direction = ParameterDirection.Input});
                sqlCommand.Parameters.Add(new SqlParameter("@PetName", SqlDbType.NVarChar, 50) { Direction = ParameterDirection.Output });
                sqlCommand.ExecuteNonQuery();
                petName = (string) sqlCommand.Parameters["@PetName"].Value;
            }

            CloseConnection();
            return petName;
        }

        #endregion

        #endregion

        public void Dispose()
        {
            CloseConnection();
        }
    }
}
