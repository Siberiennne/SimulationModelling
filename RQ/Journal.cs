using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RQ
{
    /// <summary>
    /// Реализует отсортированный по времени журнал событий
    /// </summary>
    class Journal
    {
        //список событий
        List<Event> ClientInfo = new List<Event>();
     //   List<Event> Client = new List<Event>();

        //сортировка событий по времени
        public void Sort()
        {
            for (int i = 0; i < ClientInfo.Count - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < ClientInfo.Count; j++)
                {
                    if (ClientInfo[j].GetTime() < ClientInfo[min].GetTime())
                    {
                        min = j;
                    }
                }
                Event e = new Event();
                e = ClientInfo[i];
                ClientInfo[i] = ClientInfo[min];
                ClientInfo[min] = e;
            }
        }

        //добавление события в журнал
        public void Add(Event e)
        {
            ClientInfo.Add(e);
         //  Client.Add(e);
            this.Sort();
        }

        //удаление события
        public void Delete(Event e)
        {
            ClientInfo.Remove(e);
        }

        //извлечение ближайшего события 
        public Event NextEvent()
        {
            return ClientInfo[0];
        }

        //проверка, есть ли в журнале записи
        public Boolean EmptyJournal()
        {
            if (ClientInfo.Count() == 0)
                return true;
            else
                return false;
        }
    }
}
