namespace AspResponseDecoration
{
	public class ResponseBodyDecorationResult
	{
		public bool Success { get; }
		public object? NewBody { get; }

		public ResponseBodyDecorationResult(bool success)
		{
			Success = success;
		}

		/// <summary>
		/// Successful result with a new body for response
		/// </summary>
		public ResponseBodyDecorationResult(object? newBody)
		{
			Success = true;
			NewBody = newBody;
		}
	}
}
