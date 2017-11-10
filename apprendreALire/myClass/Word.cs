using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace apprendreALire.myClass { 

	public class Word
	{
		public int id;
		public string name;
		public string imageFileName;
		public string soundFileName;

		public Word(int id, string name, string imageFileName, string soundFileName)
		{
			this.id = id;
			this.name = name;
			this.imageFileName = imageFileName;
			this.soundFileName = soundFileName;
		}
        public Word() {}
	}
}
