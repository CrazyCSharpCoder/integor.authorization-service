using System.Collections.Generic;
using System.Threading.Tasks;

using AspResponseDecoration;

using PrettyUserAuthorizationModel;
using PrettyUserAuthorizationShared.Dto.Roles;

namespace PrettyUserAuthorizationResponseDecoration.Decorators
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
