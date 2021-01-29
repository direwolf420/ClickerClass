using ClickerClass.Effects;
using ClickerClass.Prefixes;
using ClickerClass.UI;
using Terraria;
using Terraria.ModLoader;

namespace ClickerClass
{
	public class ClickerClass : Mod
	{
		public static ModHotKey AutoClickKey;
		internal static ClickerClass mod;

		/// <summary>
		/// To prevent certain methods being called when they shouldn't
		/// </summary>
		internal static bool finalizedRegisterCompat = false;

		public override void Load()
		{
			finalizedRegisterCompat = false;
			mod = this;
			AutoClickKey = RegisterHotKey("Clicker Accessory", "G");
			ClickerSystem.Load();

			ClickerPrefix.LoadPrefixes(mod);

			if (!Main.dedServ)
			{
				LoadClient();
			}
		}

		public override void Unload()
		{
			finalizedRegisterCompat = false;
			ShaderManager.Unload();
			ClickerSystem.Unload();
			ClickerPrefix.UnloadPrefixes();
			ClickerInterfaceResources.Unload();
			AutoClickKey = null;
			mod = null;
		}

		private void LoadClient()
		{
			ShaderManager.Load();
			ClickerInterfaceResources.Load();
		}

		public override object Call(params object[] args)
		{
			return ClickerModCalls.Call(args);
		}

		public override void AddRecipes()
		{
			ClickerRecipes.AddRecipes();
		}

		public override void PostAddRecipes()
		{
			finalizedRegisterCompat = true;
		}

		public override void AddRecipeGroups()
		{
			ClickerRecipes.AddRecipeGroups();
		}
	}
}
