﻿using System;
using System.Linq;

using AspResponseDecoration;

namespace IntegorAuthorizationResponseDecoration.Attributes
{
	using Decorators;

	public class DecorateErrorResponseAttribute : ResponseBodyDecorationFilterFactory
	{
		public DecorateErrorResponseAttribute()
			: base(typeof(ErrorResponseBodyDecorator))
		{
		}

		public DecorateErrorResponseAttribute(params Type[] decoratorTypes)
			: base(decoratorTypes.Append(typeof(ErrorResponseBodyDecorator)).ToArray())
		{
		}
	}
}
