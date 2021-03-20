using System;
using System.Collections.Generic;
using System.Reflection;
using Tools.Attributes;

namespace Tools.Core
{
	public class CustomComponentContainer
	{
		private static readonly CustomComponentContainer container = new CustomComponentContainer();
		private static readonly object obj = new object();
		public static List<string> BaseScanAssemblies { get; private set; }

		public Dictionary<string, CustomComponentInfo> ClassMapping { get; private set; }
		private CustomComponentContainer()
		{
			Init();
			AppDomain.CurrentDomain.AssemblyLoad += ReloadCustomComponentContainer;
		}

		private void Init()
		{
			BaseScanAssemblies = new List<string>();
			BaseScanAssemblies.Add("Tools, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
			BaseScanAssemblies.Add("Entity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
			ClassMapping = new Dictionary<string, CustomComponentInfo>(50);
			foreach (string s in BaseScanAssemblies)
			{
				SingleAssemblyScan(s);
			}
		}

		private void SingleAssemblyScan(string assemblyFullName)
		{
			Console.WriteLine("加载了");
			Type[] types = Assembly.Load(assemblyFullName).GetTypes();
			foreach (Type type in types)
			{
				CustomComponentAttribute customComponentAttribute = type.GetCustomAttribute<CustomComponentAttribute>(false);
				if (customComponentAttribute != null)
				{
					RouteMappingAttribute routeMapping = type.GetCustomAttribute<RouteMappingAttribute>(false);
					if (!ClassMapping.ContainsKey(routeMapping.Value))
						ClassMapping.Add(routeMapping.Value, new CustomComponentInfo(Activator.CreateInstance(type)));
					else
						ClassMapping[routeMapping.Value] = new CustomComponentInfo(Activator.CreateInstance(type));
				}
			}
		}

		private void ReloadCustomComponentContainer(object sender, AssemblyLoadEventArgs args)
		{
			string name = args.LoadedAssembly.FullName;
			Console.WriteLine(name);
			foreach(string s in BaseScanAssemblies)
			{
				if(s==name)
				{
					SingleAssemblyScan(s);
				}
			}
		}

		public static CustomComponentContainer GetContainer()
		{
			return container;
		}
	}
}
