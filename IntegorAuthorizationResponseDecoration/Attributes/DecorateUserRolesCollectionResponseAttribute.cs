using IntegorSharedResponseDecorators.Attributes;

namespace IntegorAuthorizationResponseDecoration.Attributes
{
	using Decorators;

	public class DecorateUserRolesCollectionResponseAttribute : DecorateErrorResponseAttribute
	{
		public DecorateUserRolesCollectionResponseAttribute()
			: base(typeof(RolesToPublicDtoDecorator), typeof(UserRolesCollectionResponseBodyDecorator))
		{
		}
	}
}
