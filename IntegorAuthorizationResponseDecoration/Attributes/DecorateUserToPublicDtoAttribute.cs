using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IntegorResponseDecoration;
using IntegorAuthorizationResponseDecoration.Decorators;

namespace IntegorAuthorizationResponseDecoration.Attributes
{
	public class DecorateUserToPublicDtoAttribute : ResponseObjectDecorationFilterFactory
	{
        public DecorateUserToPublicDtoAttribute() : base(typeof(UserToPublicDtoDecorator))
        {
        }
    }
}
