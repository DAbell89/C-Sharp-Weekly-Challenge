using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeLou.CSharp.Week3.Challenge
{
    public class AppointmentRepository : ICalendarItemRepository<Appointment>
    {
        //Info: This is a neat type that allows you to lookup items by ID, be careful not to ask for an item that isn't there.
        private readonly Dictionary<int, Appointment> _dictionary;

        public AppointmentRepository()
        {
            _dictionary = new Dictionary<int, Appointment>();
        }

        public Appointment Create()
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

            var appointment = new Appointment();
            appointment.Id = nextAvailableId;
            _dictionary.Add(nextAvailableId, new Appointment());

            return appointment;
        }
        public Appointment FindById(int id)
        {
            return _dictionary[id];
        }

        public Appointment Update(Appointment item)
        {
            _dictionary[item.Id] = item;
            return item;
        }

        public void Delete(Appointment item)
        {
            _dictionary.Remove(item.Id);
        }

        public IEnumerable<Appointment> FindByDate(DateTime date)
        {
            return _dictionary.Values.Where(x => x.startDateTime.Date == date.Date);
        }

        public IEnumerable<Appointment> GetAllItems()
        {
            return _dictionary.Values.AsEnumerable();
        }

        public void LoadFromJson(string json)
        {
            var dictionary = JsonConvert.DeserializeObject<Dictionary<int, Appointment>>(json);
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
