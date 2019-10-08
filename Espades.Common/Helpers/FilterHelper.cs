using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Espades.Common.Helpers
{
    public class FilterHelper
    {
        #region Public variables
        public int Page { get; set; }
        public int PageSize { get; set; }
        public ICollection<FilterItem> FilterItems { get; set; }
        public OrderType? OrderType { get; set; }
        public string OrderBy { get; set; }
        public int Skip
        {
            get
            {
                int x = (Page > 0 ? Page : 1) - 1;
                return (x * PageSize);
            }
        }
        public int Take => PageSize;
        public string[] includes = Array.Empty<string>();
        #endregion Private variables

        #region Public methods
        public string[] GetIncludes()
        {
            return includes;
        }

        public void SetIncludes(string[] value)
        {
            includes = value;
        }

        public Expression<Func<T, bool>> GetExpression<T>() where T : class
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "arg");

            if (FilterItems?.Count > 0)
            {
                List<Expression> expList = new List<Expression>();

                FilterItems.ToList().ForEach(listItens =>
                {
                    if (listItens.Itens.Count > 0)
                    {
                        List<Expression> expListItem = new List<Expression>();

                        listItens.Itens.ForEach(item =>
                        {
                            Expression name = null;

                            if (item.Field.Contains("."))
                            {
                                string[] splits = item.Field.Split('.');

                                name = param;

                                foreach (string part in splits)
                                {
                                    name = Expression.Property(name, part);
                                }
                            }
                            else
                            {
                                name = Expression.Property(param, item.Field);
                            }

                            Type type = name.Type;
                            Expression value = null;

                            value = GetValueConstant(item.ValueOne, type);

                            MethodInfo toLower = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
                            Expression expLower = null;

                            if (!type.FullName.ToLower().Contains("guid")
                                && !type.FullName.ToLower().Contains("bool")
                                && !type.FullName.ToLower().Contains("int")
                                && !type.FullName.ToLower().Contains("decimal")
                                && !type.FullName.ToLower().Contains("datetime")
                                && !type.GetTypeInfo().IsEnum)
                            {
                                expLower = Expression.Call(name, toLower);
                            }

                            Expression clausule = null;
                            switch (item.Clausule)
                            {
                                case "Contains":
                                    clausule = Expression.Call(expLower, type.GetMethod(item.Clausule, new[] { type }), value);
                                    break;
                                case "StartsWith":
                                    clausule = Expression.Call(expLower, type.GetMethod(item.Clausule, new[] { type }), value);
                                    break;
                                case "EndsWith":
                                    clausule = Expression.Call(expLower, type.GetMethod(item.Clausule, new[] { type }), value);
                                    break;
                                case "GreaterThanOrEqual":
                                    clausule = Expression.GreaterThanOrEqual(name, value);
                                    break;
                                case "LessThanOrEqual":
                                    clausule = Expression.LessThanOrEqual(name, value);
                                    break;
                                case "Equal":
                                    clausule = Expression.Equal(expLower ?? name, value);
                                    break;
                                case "NotEqual":
                                    clausule = Expression.NotEqual(expLower ?? name, value);
                                    break;
                                case "Between":
                                    Expression valueTwo = GetValueConstant(item.ValueTwo, type);
                                    BinaryExpression expL = Expression.GreaterThanOrEqual(name, value);
                                    BinaryExpression expR = Expression.LessThanOrEqual(name, valueTwo);
                                    clausule = Expression.And(expL, expR);
                                    break;
                                default:
                                    throw new Exception("Invalid Clausule");

                            }


                            if (expLower != null)
                            {
                                BinaryExpression expL = Expression.NotEqual(name, Expression.Constant(null));
                                clausule = Expression.AndAlso(expL, clausule);
                            }

                            expListItem.Add(clausule);
                        });

                        Expression expItem = null;

                        expListItem.ForEach(e =>
                        {
                            if (expItem == null)
                            {
                                expItem = e;
                            }
                            else
                            {
                                expItem = Expression.Or(e, expItem);
                            }
                        });

                        if (expItem != null)
                        {
                            expList.Add(expItem);
                        }
                    }
                });

                Expression exp = null;

                if (expList.Count > 0)
                {
                    foreach (Expression e in expList)
                    {
                        if (exp == null)
                        {
                            exp = e;
                        }
                        else
                        {
                            exp = Expression.And(e, exp);
                        }
                    }
                }

                if (exp != null)
                {
                    return Expression.Lambda<Func<T, bool>>(exp, param);
                }
                else
                {
                    exp = Expression.Equal(param, param);
                    return Expression.Lambda<Func<T, bool>>(exp, param);
                }
            }
            else
            {
                BinaryExpression exp = Expression.Equal(param, param);
                return Expression.Lambda<Func<T, bool>>(exp, param);
            }
        }

        public Func<T, object> GetOrderBy<T>()
        {
            Expression exp = null;
            ParameterExpression param = Expression.Parameter(typeof(T), "x");

            if (OrderBy.Contains("."))
            {
                List<string> splits = OrderBy.Split('.').ToList();
                exp = param;

                splits.ForEach(part =>
                {
                    exp = Expression.Property(exp, part);
                });
            }
            else
            {
                exp = Expression.Property(param, OrderBy);
            }

            return Expression.Lambda<Func<T, object>>(Expression.Convert(exp, typeof(object)), param).Compile();
        }

        public List<string> ExtractPropValues(string propName)
        {
            List<Item> items = FilterItems.SelectMany(s => s.Itens).ToList();
            return items.Where(x => x.Field.ToLower() == propName.ToLower()).Select(x => x.ValueOne).ToList();
        }
        #endregion Public methods

        #region Private methods
        private static Expression GetValueConstant(string itemValue, Type type)
        {
            try
            {
                Expression value;

                if (type.FullName.Contains("Guid"))
                {
                    if (type.FullName.Contains("Nullable"))
                    {
                        Guid? nullableGuid = new Guid(itemValue);
                        value = Expression.Constant(nullableGuid, typeof(Guid?));
                    }
                    else
                    {
                        value = Expression.Constant(new Guid(itemValue));
                    }
                }
                else if (type.FullName.Contains("Boolean"))
                {
                    if (type.FullName.Contains("Nullable"))
                    {
                        bool? nullableBool = (itemValue == "true" || itemValue == "1");
                        value = Expression.Constant(nullableBool, typeof(bool?));
                    }
                    else
                    {
                        value = Expression.Constant((itemValue == "true" || itemValue == "1"));
                    }
                }
                else if (type.FullName.Contains("Int32"))
                {
                    if (type.FullName.Contains("Nullable"))
                    {
                        int? nullableInt = Convert.ToInt32(itemValue);
                        value = Expression.Constant(nullableInt, typeof(int?));
                    }
                    else
                    {
                        value = Expression.Constant(Convert.ToInt32(itemValue));
                    }
                }
                else if (type.FullName.Contains("Int64"))
                {
                    if (type.FullName.Contains("Nullable"))
                    {
                        long? nullableLong = Convert.ToInt64(itemValue);
                        value = Expression.Constant(nullableLong, typeof(long?));
                    }
                    else
                    {
                        value = Expression.Constant(Convert.ToInt64(itemValue));
                    }
                }
                else if (type.FullName.Contains("Decimal"))
                {
                    if (type.FullName.Contains("Nullable"))
                    {
                        decimal? nullableDecimal = Convert.ToDecimal(itemValue.Replace(",", "."), new CultureInfo("en-US"));
                        value = Expression.Constant(nullableDecimal, typeof(decimal?));
                    }
                    else
                    {
                        value = Expression.Constant(Convert.ToDecimal(itemValue.Replace(",", "."), new CultureInfo("en-US")));
                    }
                }
                else if (type.FullName.Contains("DateTime"))
                {
                    DateTime.TryParse(itemValue, out DateTime date);
                    value = Expression.Constant(date);
                }
                else if (type.GetTypeInfo().IsEnum)
                {
                    object enumValue = Enum.ToObject(type, Convert.ToInt32(itemValue));
                    value = Expression.Constant(enumValue);
                }
                else
                {
                    value = Expression.Constant(Convert.ChangeType(itemValue.ToLower(), type));
                }

                return value;
            }
            catch (Exception)
            {
                throw new Exception($"Value {itemValue.ToLower()} is invalid for the type {type}");
            }
        }
        #endregion Public methods
    }

    public class FilterItem
    {
        #region Public variables
        public List<Item> Itens { get; set; }
        #endregion  Public variables
    }

    public static class FilterExtensions
    {
        #region Public methods
        public static List<T> OrderByFilter<T>(this List<T> list, FilterHelper filter) where T : class
        {
            if (filter.OrderType != null && filter.OrderBy != null)
            {
                Func<T, object> orderBy = filter.GetOrderBy<T>();

                if (filter.OrderType == OrderType.Ascending)
                {
                    return list.OrderBy(orderBy).ToList();
                }
                else
                {
                    return list.OrderByDescending(orderBy).ToList();
                }
            }
            else
            {
                return list;
            }
        }
        #endregion Public methods
    }

    public class Item
    {
        #region Public variables
        public string Field { get; set; }
        public string Clausule { get; set; }
        public string ValueOne { get; set; }
        public string ValueTwo { get; set; }
        #endregion Public variables
    }

    public class FilterResult<T> where T : class
    {
        #region Public variables
        public int Count { get; set; }
        public ICollection<T> List { get; set; }
        #endregion Public variables
    }

    public enum OrderType
    {
        Ascending = 0,
        Descending = 1
    }
}
