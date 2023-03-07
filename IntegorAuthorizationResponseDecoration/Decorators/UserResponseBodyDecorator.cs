using System.Threading.Tasks;

using AspResponseDecoration;

using IntegorAuthorizationShared.Dto.Users;

namespace IntegorAuthorizationResponseDecoration.Decorators
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
