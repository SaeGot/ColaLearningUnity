using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ColaLearningUnity
{
	[Serializable]
    public class QLearning : ReinforcementLearning
    {
		[Serializable]
		public struct StateAction
		{
			public string state;
			public string action;
			public StateAction(string _state, string _action) { state = _state; action = _action; }
			public void Set(string _state, string _action) { state = _state; action = _action; }
		};
		protected Dictionary<StateAction, double> qTable;

		// 생성자
		public QLearning(Dictionary<StateAction, double> q_Table)
        {
			sarsList = new List<SARS>();
			qTable = new Dictionary<StateAction, double>(q_Table);
			Initialize();
		}

		// 에피소드 초기화
		public void Initialize()
        {
			currentState = "";
			sarsList.Clear();
		}

		// 행동 진행하여 다음 상태로 전이
		public void Action(string action, string next_State, double reward)
		{
			StateAction state_action = new StateAction(currentState, action);
			SARS sars = new SARS(currentState, action, reward, next_State);
			sarsList.Add(sars);
			// 다음 상태로 전이
			currentState = next_State;
		}

		// 현재 상태에서 가장 좋은 행동 가져오기
		public string GetBestAction(string state, List<string> enable_Actions)
        {
			// 첫번째
			List<string> best_actions = new List<string>();
			string action = enable_Actions[0];
			best_actions.Add(action);
			string best_action = action;
			StateAction state_action= new StateAction(state, best_action);
			double best_q = qTable[state_action];
			// 두번째 부터
			for (int n = 1; n < enable_Actions.Count; n++)
			{
				action = enable_Actions[n];
				state_action.Set(state, action);
				if (qTable[state_action] > best_q)
				{
					best_action = action;
					best_q = qTable[state_action];
					best_actions.Clear();
					best_actions.Add(best_action);
				}
				else if (qTable[state_action] == best_q)
				{
					best_actions.Add(action);
				}
			}

			// 최고 Q값 행동 가져오기
			Random rd = new Random();
			int random_num = rd.Next(0, best_actions.Count - 1);
			best_action = best_actions[random_num];

			return best_action;
		}

		// 현재 상태에서 행동 가져오기
		public string GetAction(string state, double epsilon_Greedy, List<string> enable_Actions)
        {
			string action;
			Random rd = new Random();
			double random_num = rd.NextDouble();
			// 탐욕
			if (random_num >= epsilon_Greedy)
			{
				action = GetBestAction(state, enable_Actions);
			}
			// 랜덤
			else
			{
				int index = rd.Next(0, enable_Actions.Count - 1);
				action = enable_Actions[index];
			}

			return action;
		}

		// 현재 상태 설정
		public void SetCurrentState(string state)
        {
			currentState = state;
		}

		// 현재 상태 가져오기
		public string GetCurrentState()
        {
			return currentState;
		}

		// QTable 업데이트
		public void UpdateQTable(double discount_Factor, double reward, List<string> next_StateEnableActions, double learning_Rate)
        {
			SARS sars = sarsList[sarsList.Count - 1];
			StateAction state_action = new StateAction(sars.currentState, sars.action);
			double max_q = 0;
			StateAction next_state_action = new StateAction();
			bool first_action = true;
			foreach (string next_next_action in next_StateEnableActions)
			{
				next_state_action.state = sars.nextState;
				next_state_action.action = next_next_action;
				if (first_action)
				{
					max_q = qTable[next_state_action];
					first_action = false;
				}
				else
				{
					max_q = Math.Max(max_q, qTable[next_state_action]);
				}
			}
			qTable[state_action] = (1 - learning_Rate) * qTable[state_action] + learning_Rate * (reward + (discount_Factor * max_q));
		}

		// Q값 가져오기
		public double GetQValue(StateAction state_Action)
        {
			return qTable[state_Action];
        }
	}
}
