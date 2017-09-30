using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apprendreALire.myClass
{
	public class Play
	{
		RecordWords recordWord = new RecordWords();
		List<int> listToPlay = new List<int>();
		int turn = 0;
		bool finish = false;

		public Play()
		{
			listToPlay = recordWord.GetKeys(7);
		}

		public Turn GetTurn()
		{
			var newTurn = new Turn();
			newTurn.toFind = recordWord.Get(listToPlay[turn]);
			newTurn.toPlay = recordWord.Get(recordWord.GetKeys(4, listToPlay[turn]));
			newTurn.toPlay.Add(newTurn.toFind);
			newTurn.toPlay.Shuffle();
			turn += 1;
			if (turn==listToPlay.Count()-1)
			{
				finish = true;
			}
			return newTurn;
		}

		public bool IsFinish()
		{
			return finish;
		}
	}
}
