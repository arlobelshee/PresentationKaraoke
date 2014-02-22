using JetBrains.Annotations;
using Player.Model;

namespace Player
{
	internal interface _IShowKaraokeMachines
	{
		void UseMachine([NotNull] _KaraokeMachine machine);
	}
}