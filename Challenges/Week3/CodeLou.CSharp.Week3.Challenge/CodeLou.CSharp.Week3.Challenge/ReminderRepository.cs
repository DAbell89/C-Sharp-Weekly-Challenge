using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeLou.CSharp.Week3.Challenge
{
    public class ReminderRepository: ICalendarItemRepository<Reminder>
	{
		//Info: This is a neat type that allows you to lookup items by ID, be careful not to ask for an item that isn't there.
		private readonly Dictionary<int, Reminder> _dictionary; 

		public ReminderRepository()
		{
			_dictionary = new Dictionary<int, Reminder>();
		}

		public Reminder Create()
		{
			//Challenge: Can you find a more efficient way to do this?
			var nextAvailableId = 0;
			foreach (var currentId in _dictionary.Keys)
			{
				if (nextAvailableId > currentId)
					continue;
				if (nextAvailableId < currentId)
					break;

				nextAvailableId++;
			}

			var reminder = new Reminder();
			reminder.Id = nextAvailableId;
			_dictionary.Add(nextAvailableId, new Reminder());

			return reminder;
        }
        public Reminder FindById(int id)
        {
            return _dictionary[id];
        }

        public Reminder Update(Reminder item)
        {
            _dictionary[item.Id] = item;
            return item;
        }

        public void Delete(Reminder item)
        {
            _dictionary.Remove(item.Id);
        }

        public IEnumerable<Reminder> FindByDate(DateTime date)
        {
            return _dictionary.Values.Where(x => x.startDateTime.Date == date.Date);
        }

        public IEnumerable<Reminder> GetAllItems()
        {
            return _dictionary.Values.AsEnumerable();
        }

        public void LoadFromJson(string json)
        {
            var dictionary = JsonConvert.DeserializeObject<Dictionary<int, Reminder>>(json);
            if (dictionary != null)
            {
                foreach (var item in dictionary)
                {
                    //This will add or update an item
                    _dictionary[item.Key] = item.Value;
                }
            }
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(_dictionary, Formatting.Indented);
        }
    }
}

