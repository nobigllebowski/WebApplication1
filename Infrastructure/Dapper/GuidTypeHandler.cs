using Dapper;
using System.Data;

namespace Infrastructure.Dapper
{
    public class GuidTypeHandler : SqlMapper.TypeHandler<Guid>
    {
        public override Guid Parse(object value)
        {
            if (value == null || value is DBNull)
                return Guid.Empty;

            return Guid.Parse(value.ToString());
        }

        public override void SetValue(IDbDataParameter parameter, Guid value)
        {
            parameter.Value = value.ToString();
        }
    }
}
