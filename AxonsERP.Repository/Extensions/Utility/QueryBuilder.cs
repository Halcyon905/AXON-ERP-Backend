using AxonsERP.Contracts.Utils;
using AxonsERP.Entities.RequestFeatures;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Reflection;
using System.Text;

namespace AxonsERP.Repository.Extensions.Utility
{
    public static class QueryBuilder
    {
        public static string CreateWhereQuery<T, U>(List<Search>? searchs, string? searchTermAlis, string? searchTermName, string? searchTermValue, ref OracleDynamicParameters dynParams, string prefixParamSpecial = null)
        {
            var prefixParam = 0;
            var sqlSearchFilter = string.Empty;
            var sqlSearchTerm = string.Empty;

            #region SEARCH_FILTER
            if (searchs != null)
            {
                foreach (var search in searchs)
                {
                    if (string.IsNullOrEmpty(search.Name) || string.IsNullOrEmpty(search.Value) || string.IsNullOrEmpty(search.Condition))
                    {
                        if (search.Condition != "ISNULL" && search.Condition != "ISNOTNULL")
                        {
                            continue;
                        }
                    }

                    // Check Properties
                    var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    var propertyFromQueryName = search.Name;
                    var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));
                    if (objectProperty == null)
                        continue;

                    var value = search.Value;
                    var comparisonOperator = "";
                    switch (search.Condition)
                    {
                        case "CONTAINS":
                            value = $"%{value}%";
                            comparisonOperator = "LIKE";
                            break;
                        case "STARTWITH":
                            value = $"{value}%";
                            comparisonOperator = "LIKE";
                            break;
                        case "ENDWITH":
                            value = $"%{value}";
                            comparisonOperator = "LIKE";
                            break;
                        case "GREATER":
                            comparisonOperator = ">";
                            break;
                        case "LESSER":
                            comparisonOperator = "<";
                            break;
                        case "GREATEROREQUAL":
                            comparisonOperator = ">=";
                            break;
                        case "LESSEROREQUAL":
                            comparisonOperator = "<=";
                            break;
                        case "EQUAL":
                            comparisonOperator = "=";
                            break;
                        case "NOTEQUAL":
                            comparisonOperator = "!=";
                            break;
                        case "ISNULL":
                            comparisonOperator = "IS NULL";
                            break;
                        case "ISNOTNULL":
                            comparisonOperator = "IS NOT NULL";
                            break;
                        default:
                            continue;
                    }

                    prefixParam += 1;
                    var paramName = objectProperty.Name + "_" + prefixParam.ToString();
                    if (!string.IsNullOrEmpty(prefixParamSpecial))
                    {
                        paramName += "_" + prefixParamSpecial;
                    }
                    switch (objectProperty.PropertyType.Name.ToUpper())
                    {
                        case "DECIMAL":
                            dynParams.Add(paramName, OracleDbType.Decimal, ParameterDirection.Input, decimal.Parse(value));
                            break;
                        case "INT":
                            dynParams.Add(paramName, OracleDbType.Int32, ParameterDirection.Input, int.Parse(value));
                            break;
                        case "DATETIME":
                            dynParams.Add(paramName, OracleDbType.Date, ParameterDirection.Input, DateTime.Parse(value).Date);
                            break;
                        case "STRING":
                            dynParams.Add(paramName, OracleDbType.Varchar2, ParameterDirection.Input, value);
                            break;
                        default:
                            dynParams.Add(paramName, OracleDbType.Varchar2, ParameterDirection.Input, value);
                            break;
                    }

                    var alias = (search.Alias != null) ? $"{search.Alias}." : "";
                    var whereClause = $"{alias}{StringUtil.ConvertCamelToOracleWord(objectProperty.Name)} {comparisonOperator} {(comparisonOperator == "IS NULL" || comparisonOperator == "IS NOT NULL" ? "" : $":{paramName}")}";
                    if (string.IsNullOrEmpty(sqlSearchFilter))
                        sqlSearchFilter = $"{whereClause}";
                    else
                        sqlSearchFilter = $"{sqlSearchFilter} AND {whereClause}";
                }
            }

            #endregion

            #region SEARCH_TERM
            if (!string.IsNullOrEmpty(searchTermName) && !string.IsNullOrEmpty(searchTermValue))
            {

                PropertyInfo[] propertyInfos = typeof(U)
                     .GetProperties(BindingFlags.Public | BindingFlags.Instance);

                var whereParams = searchTermName.Trim().Split(',');
                foreach (var param in whereParams)
                {
                    if (string.IsNullOrWhiteSpace(param))
                        continue;

                    var propertyFromQueryName = param.Trim();
                    var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));
                    if (objectProperty == null)
                        continue;


                    var alias = (searchTermAlis != null) ? $"{searchTermAlis}." : "";
                    var whereClause = $"{alias}{StringUtil.ConvertCamelToOracleWord(objectProperty.Name)} LIKE :{objectProperty.Name}";

                    if (string.IsNullOrEmpty(sqlSearchTerm))
                        sqlSearchTerm = $"{whereClause}";
                    else
                        sqlSearchTerm = $"{sqlSearchTerm} OR {whereClause}";


                    dynParams.Add(objectProperty.Name, OracleDbType.Varchar2, ParameterDirection.Input, $"%{searchTermValue}%");
                }
            }
            #endregion

            #region RESULT
            string result = string.Empty;

            if (!string.IsNullOrEmpty(sqlSearchFilter))
            {
                if (!string.IsNullOrEmpty(sqlSearchTerm))
                {
                    result = $"{sqlSearchFilter} AND ({sqlSearchTerm})";
                }
                else
                {
                    result = sqlSearchFilter;
                }
            }
            else if (!string.IsNullOrEmpty(sqlSearchTerm))
            {
                result = $"({sqlSearchTerm})";
            }

            return result;
            #endregion
        }

        public static string CreateOrderQuery<T>(string? orderByQueryString, char alias)
        {
            if (string.IsNullOrEmpty(orderByQueryString))
                return string.Empty;

            var orderParams = orderByQueryString.Trim().Split(',');
            PropertyInfo[] propertyInfos = typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();
            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;
                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));
                if (objectProperty == null)
                    continue;

                var direction = param.ToString().EndsWith(" desc") ? "desc" : "asc";
                orderQueryBuilder
                 .Append($"{alias}.{StringUtil.ConvertCamelToOracleWord(objectProperty.Name.ToString())} {direction}, ");
            }
            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            return string.IsNullOrEmpty(orderQuery) ? "" : orderQuery;
        }




        public static PagedList<T> GetPagedList<T, TSearchFilter, TSearchTerm>(IDbConnection? connection,
                                                                                string tableName,
                                                                                string selectColumns,
                                                                                string countColumn,
                                                                                char alias,
                                                                                RequestParameters parameters,
                                                                                string joinStatement = "",
                                                                                string conditionSpecial = "")
        {
            // SEARCH
            var condition = string.Empty;
            var dynParams = new OracleDynamicParameters();
            if ((parameters.Search != null) || (!string.IsNullOrEmpty(parameters.SearchTermName) && !string.IsNullOrEmpty(parameters.SearchTermValue)))
            {
                var whereCause = QueryBuilder.
                    CreateWhereQuery<TSearchFilter, TSearchTerm>
                    (parameters.Search, parameters.SearchTermAlias, parameters.SearchTermName, parameters.SearchTermValue, ref dynParams);

                condition = $" {(!string.IsNullOrEmpty(whereCause) ? "AND " + whereCause : "")}";
            }

            // ORDER BY
            var orderBy = "";
            if (!string.IsNullOrEmpty(parameters.OrderBy))
            {
                orderBy = QueryBuilder.CreateOrderQuery<T>(orderByQueryString: parameters.OrderBy, alias);
                orderBy = $" {(!string.IsNullOrEmpty(orderBy) ? "ORDER BY " + orderBy : "")}";
            }

            // PAGING
            var skip = (parameters.PageNumber - 1) * parameters.PageSize;
            var paging = "OFFSET :skip ROWS FETCH NEXT :take ROWS ONLY";

            // SQL QUERY
            var query = @$"BEGIN OPEN :rslt1 FOR SELECT COUNT({countColumn}) FROM {tableName} {alias} {joinStatement} WHERE 1=1 {condition} {conditionSpecial};
                           OPEN :rslt2 FOR SELECT {selectColumns}
                               FROM {tableName} {alias} {joinStatement} 
                               WHERE 1=1 {condition} {conditionSpecial} {orderBy} {paging};
                           END;";

            dynParams.Add(":rslt1", OracleDbType.RefCursor, ParameterDirection.Output);
            dynParams.Add(":rslt2", OracleDbType.RefCursor, ParameterDirection.Output);
            dynParams.Add(":skip", OracleDbType.Int32, ParameterDirection.Input, skip);
            dynParams.Add(":take", OracleDbType.Int32, ParameterDirection.Input, parameters.PageSize);

            using var multi = connection.QueryMultiple(query, dynParams);
            var count = multi.ReadSingle<int>();
            var entities = multi.Read<T>().ToList();
            return new PagedList<T>(entities, count, parameters.PageNumber, parameters.PageSize);
        }

    }
}
