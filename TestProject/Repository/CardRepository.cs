using Dapper;
using simpleProject.Parameter;
using System.Data.SqlClient;

namespace TestProject.Repository
{
    public class CardRepository
    {
        private readonly string _connectString = @"Server=DESKTOP-HFMFUH6\LOCAL;Database=Localcard;Trusted_Connection=True;";


        public IEnumerable<Card> GetList()
        {
            using (var conn = new SqlConnection(_connectString))

                return conn.Query<Card>("SELECT * FROM card");

        }
        public Card Get(int id)
        {
            var sql =
                @"
                 SELECT * FROM Card Where Id =@id
                 ";
            var parameters = new DynamicParameters();
            parameters.Add("id", id);
            using (var conn = new SqlConnection(_connectString)) {
                var result = conn.QueryFirstOrDefault<Card>(sql, parameters);
                return result;
            }

            
        }
        /// <summary>
        /// 新增卡片
        /// </summary>
        /// <param name="parameter">參數</param>
        /// <returns></returns>
        public int Create(CardParameter parameter)
        {
            var sql =
            @"
        INSERT INTO Card 
        (   
            [id],
            [Name],
            [Description]
        ) 
        VALUES 
        (
            @id,
            @Name,
            @Description
        );
        
    ";

            using (var conn = new SqlConnection(_connectString))
            {
                var result = conn.QueryFirstOrDefault<int>(sql, parameter);
                return result;
            }
        }

        // 修改卡片

        public bool Update(int id, CardParameter parameter)
        {
            var sql =
            @" UPDATE Card SET  
             [Name] = @Name,
             [Description] = @Description
             WHERE 
             Id = @id";

            var parameters = new DynamicParameters(parameter);
            parameters.Add("Id", id, System.Data.DbType.Int32);

            using (var conn = new SqlConnection(_connectString))
            {
                var result = conn.Execute(sql, parameters);
                return result > 0;
            }
        }
        public void Delete(int id)
        {
            var sql =
         @"DELETE FROM Card WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, System.Data.DbType.Int32);

            using (var conn = new SqlConnection(_connectString))
            {
                var result = conn.Execute(sql, parameters);
            }
        }
    }
}
