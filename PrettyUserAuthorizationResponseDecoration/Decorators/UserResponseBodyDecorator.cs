using System.Threading.Tasks;

using AspResponseDecoration;

using PrettyUserAuthorizationShared.Dto.Users;

namespace PrettyUserAuthorizationResponseDecoration.Decorators
{
	public class UserResponseBodyDecorator : IResponseBodyDecorator
	{
		public ResponseBodyDecorationResult Decorate(object? body)
		{
			if (body is not UserAccountPublicDto)
				return new ResponseBodyDecorationResult(false);

			object newBody = new
			{
				user = body
			};

			return new ResponseBodyDecorationResult(newBody);
		}
	}
}
