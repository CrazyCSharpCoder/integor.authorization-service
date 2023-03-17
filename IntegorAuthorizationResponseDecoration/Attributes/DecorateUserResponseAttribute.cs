using IntegorSharedResponseDecorators.Attributes;

namespace IntegorAuthorizationResponseDecoration.Attributes
{
	using Decorators;

	public class DecorateUserResponseAttribute : DecorateErrorResponseAttribute
	{
		public DecorateUserResponseAttribute()
			: base(typeof(UserToPublicDtoDecorator), typeof(UserResponseBodyDecorator))
		{
		}
	}
}
