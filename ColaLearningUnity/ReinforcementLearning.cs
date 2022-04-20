using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ColaLearningUnity
{
	[Serializable]
	public abstract class ReinforcementLearning
    {
		[Serializable]
		protected struct SARS
		{
			public string currentState;
			public string action;
			public double reward;
			public string nextState;
			public SARS(string current_State, string _action, double _reward, string next_State)
            {
				currentState = current_State;
				action = _action;
				reward = _reward;
				nextState = next_State;
			}
		};
		protected string currentState;
		protected List<SARS> sarsList;
	}
}
