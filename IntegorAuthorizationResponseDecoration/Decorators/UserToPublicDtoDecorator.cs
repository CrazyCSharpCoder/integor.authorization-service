using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using IntegorResponseDecoration;

using IntegorAuthorizationShared.Dto.Users;

namespace IntegorAuthorizationResponseDecoration.Decorators
{
	internal class UserToPublicDtoDecorator : IResponseBodyDecorator
	{
		private IMapper _mapper;

		public UserToPublicDtoDecorator(IMapper mapper)
        {
			_mapper = mapper;
        }

        public ResponseBodyDecorationResult Decorate(object? bodyObject)
		{
			if (bodyObject is not UserAccountDto userInternalDto)
				return new ResponseBodyDecorationResult(false);

			var publicUser = _mapper.Map(userInternalDto, new PublicDto.Users.UserAccountInfoDto());
			return new ResponseBodyDecorationResult(publicUser);
		}
	}
}
