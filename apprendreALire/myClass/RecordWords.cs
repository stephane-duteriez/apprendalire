using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;

namespace apprendreALire.myClass
{
	class RecordWords : IEnumerator,IEnumerable
    {
		Dictionary<int, Word> _dictWord = new Dictionary<int, Word>();
		int position = -1;

		public RecordWords()
		{
			Initialize();
		}

		public object Current
		{
			get { return _dictWord[_dictWord.Keys.ToArray<int>()[position]]; }
		}

		public Word Get(int key)
		{
			return _dictWord[key];
		}

		public List<Word> Get(List<int> listKey)
		{
			List<Word> listWord = new List<Word>();
			foreach (int key in listKey)
			{
				listWord.Add(this.Get(key));
			}
			return listWord;
		}

		public IEnumerator GetEnumerator()
		{
			return (IEnumerator)this; 
		}

		public void Initialize()
		{
			Uri appUri = new System.Uri("ms-appx:///Assets/listWords.json");
			StorageFile anjFile = StorageFile.GetFileFromApplicationUriAsync(appUri).AsTask().ConfigureAwait(false).GetAwaiter().GetResult();
			string jsonText = FileIO.ReadTextAsync(anjFile).AsTask().ConfigureAwait(false).GetAwaiter().GetResult();
			var jsonSerializer = new DataContractJsonSerializer(typeof(Word));
			JsonArray anjarray = JsonArray.Parse(jsonText);
			foreach (JsonValue oJsonVal in anjarray)
			{
				JsonObject oJsonObj = oJsonVal.GetObject();
				using (MemoryStream jsonStream = new MemoryStream(Encoding.Unicode.GetBytes(oJsonObj.ToString())))
				{
					Word oContent = (Word)jsonSerializer.ReadObject(jsonStream);
					_dictWord.Add(oContent.id, oContent);
				}
			}
		}

		public bool MoveNext()
		{ 
			position++;
			return (position < _dictWord.Keys.Count);
		}

		public void Reset()
		{
			position = 0;
		}

		public List<int> GetKeys(int nbr, int toAvoid=-1)
		{
			List<int> listKeys = _dictWord.Keys.ToList<int>();
			listKeys.Shuffle();
			return listKeys.FindAll(x=>x!=toAvoid).GetRange(0, nbr);
		}
	}
}
