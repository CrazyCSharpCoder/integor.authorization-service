namespace IntegorAuthorizationResponseDecoration.Attributes
{
	using Decorators;

	public class DecorateUserRolesCollectionResponseAttribute : DecorateErrorResponseAttribute
	{
		public DecorateUserRolesCollectionResponseAttribute()
			: base(typeof(UserRolesCollectionResponseBodyDecorator))
		{
		}
	}
}
