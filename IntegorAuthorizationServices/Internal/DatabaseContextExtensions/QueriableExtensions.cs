using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace IntegorAuthorizationServices.Internal.DatabaseContextExtensions
{
	internal static class QueriableExtensions
	{
		public static IQueryable<TEntity> ApplyAsNoTracking<TEntity>(this IQueryable<TEntity> dbSet, bool asNoTracking)
			where TEntity : class
		{
			IQueryable<TEntity> entities = dbSet;

			if (asNoTracking)
				entities = entities.AsNoTracking();

			return entities;
		}
	}
}
