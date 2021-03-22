namespace Tools.Component
{
	public class PublicComponent
	{
		public object MethodMapping(string route, string json)
		{
			return ComponentDispatcher.GetInstance().Dispatch(route, json);
		}
	}
}
