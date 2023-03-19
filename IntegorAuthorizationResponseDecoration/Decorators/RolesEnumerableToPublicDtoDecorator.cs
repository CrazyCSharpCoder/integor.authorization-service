﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using IntegorResponseDecoration;

using IntegorAuthorizationModel;
using IntegorAuthorizationShared.Dto.Roles;

namespace IntegorAuthorizationResponseDecoration.Decorators
{
	public class RolesEnumerableToPublicDtoDecorator : IResponseObjectDecorator
	{
		private IMapper _mapper;

		public RolesEnumerableToPublicDtoDecorator(IMapper mapper)
		{
			_mapper = mapper;
		}

		public ResponseBodyDecorationResult Decorate(object? responseObject)
		{
			if (responseObject is UserRole role)
				return new ResponseBodyDecorationResult(ToFullPublicRole(role));

			if (responseObject is UserRoleShortDto shortRole)
				return new ResponseBodyDecorationResult(ToShortPublicRole(shortRole));

			if (responseObject is IEnumerable<UserRole> roles)
				return new ResponseBodyDecorationResult(roles.Select(r => ToFullPublicRole(r)));

			if (responseObject is IEnumerable<UserRoleShortDto> shortRoles)
				return new ResponseBodyDecorationResult(shortRoles.Select(r => ToShortPublicRole(r)));

			return new ResponseBodyDecorationResult(false);
		}

		private PublicDto.Roles.UserRoleFullDto ToFullPublicRole(UserRole role)
			=> _mapper.Map(role, new PublicDto.Roles.UserRoleFullDto());

		private PublicDto.Roles.UserRoleFullDto ToShortPublicRole(UserRoleShortDto role)
			=> _mapper.Map(role, new PublicDto.Roles.UserRoleFullDto());
	}
}