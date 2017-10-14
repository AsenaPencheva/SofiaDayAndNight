using AutoMapper.QueryableExtensions;
using SofiaDayAndNight.Web.App_Start;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SofiaDayAndNight.Web.Infrastructure
{
    public static class QueryableExtensions
    {
        public static IQueryable<TDestination> MapTo<TDestination>(this IQueryable source, params Expression<Func<TDestination, object>>[] membersToExpand)
        {
            return source.ProjectTo(AutoMapperConfig.Configuration, membersToExpand);
        }
    }
}