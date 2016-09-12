using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeLou.CSharp.Week3.Challenge
{
    public class MeetingRepository : ICalendarItemRepository<Meeting>
    {
        //Info: This is a neat type that allows you to lookup items by ID, be careful not to ask for an item that isn't there.
        private readonly Dictionary<int, Meeting> _dictionary;

        public MeetingRepository()
        {
            _dictionary = new Dictionary<int, Meeting>();
        }

        public Meeting Create()
        {
            //Challenge: Can you find a more efficient way to do this?
            var nextAvailableId = _dictionary.Keys.LastOrDefault() + 1;

            var meeting = new Meeting();
            meeting.Id = nextAvailableId;
            _dictionary.Add(nextAvailableId, new Meeting());

            return meeting;
        }
        public Meeting FindById(int id)
        {
            return _dictionary[id];
        }

        public Meeting Update(Meeting item)
        {
            _dictionary[item.Id] = item;
            return item;
        }

        public void Delete(Meeting item)
        {
            _dictionary.Remove(item.Id);
        }

        public IEnumerable<Meeting> FindByDate(DateTime date)
        {
            return _dictionary.Values.Where(x => x.startDateTime.Date == date.Date);
        }

        public IEnumerable<Meeting> GetAllItems()
        {
            return _dictionary.Values.AsEnumerable();
        }

        public void LoadFromJson(string json)
        {
            var dictionary = JsonConvert.DeserializeObject<Dictionary<int, Meeting>>(json);
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

