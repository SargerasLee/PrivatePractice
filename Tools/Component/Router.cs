namespace Tools.Component
{
	public class Router
	{
		public static object Routing(string route, params object[] objects)
		{
			return ComponentDispatcher.GetInstance().Dispatch(route, objects);
		}
	}
}
