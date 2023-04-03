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
	public class UserToPublicDtoDecorator : IResponseObjectDecorator
	{
		private IMapper _mapper;

		public UserToPublicDtoDecorator(IMapper mapper)
        {
			_mapper = mapper;
        }

        public ResponseObjectDecorationResult Decorate(object? responseObject)
		{
			if (responseObject is not UserAccountDto userInternalDto)
				return new ResponseObjectDecorationResult(false);

			var publicUser = _mapper.Map(userInternalDto, new PublicDto.Users.UserAccountInfoDto());
			return new ResponseObjectDecorationResult(publicUser);
		}
	}
}
