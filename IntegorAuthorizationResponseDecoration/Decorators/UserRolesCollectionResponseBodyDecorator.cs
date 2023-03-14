using System.Collections.Generic;
using System.Threading.Tasks;

using IntegorResponseDecoration;

using IntegorAuthorizationModel;
using IntegorAuthorizationShared.Dto.Roles;

namespace IntegorAuthorizationResponseDecoration.Decorators
{
	public class UserRolesCollectionResponseBodyDecorator : IResponseBodyDecorator
	{
		public ResponseBodyDecorationResult Decorate(object? bodyObject)
		{
			if (bodyObject is not IEnumerable<UserRole> &&
				bodyObject is not IEnumerable<UserRoleShortDto>)
			{
				return new ResponseBodyDecorationResult(false);
			}

			object newBody = new
			{
				roles = bodyObject
			};

			return new ResponseBodyDecorationResult(newBody);
		}
	}
}
