namespace IntegorAuthorizationResponseDecoration.Attributes
{
	using Decorators;

	public class DecorateUserResponseAttribute : DecorateErrorResponseAttribute
	{
		public DecorateUserResponseAttribute()
			: base(typeof(UserResponseBodyDecorator))
		{
		}
	}
}
