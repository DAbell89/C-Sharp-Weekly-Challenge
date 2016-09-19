using System;
using System.Collections.Generic;
using System.IO;

namespace CodeLou.CSharp.Week3.Challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            // Overview:
            // In this assignment, you will be creating a calendar application that will load and save data. An example of loading and saving data has been provided.
            // This calendar application will accept multiple event types which will be represented by their own class types. 
            // You will be dealing with Appointments, Meetings, and Reminders.

            // Task 1:
            // Create new classes that will represent the calendar items that you will be using. 
            // Each of your classes will inherit from the CalendarItemBase abstract class.
            // Reminders have already been created as an example.

            // Task 2:
            // Define Your Data
            // Appointments need to be assigned a start date and time, an end date and time, and a location.
            // Meetings need to be assigned a start date and time, an end date and time, a location, and attendees. You can decide what data you need for attendees.
            // Reminders need to be assigned a start date and time.
            // Hint: Use inheritance to make your life easier.

            // Task 3:
            // Add the missing code to the ReminderRepository. Hint: Look for instances of NotImplementedException.
            // Create repository classes for Appointments and Meetings. Use the ReminderRepository as an example.

            // Task 4:
            // We want our application to load data and to save data. The process for reminders has already been created. You will need to do the same thing
            // for the other data types.
            var reminderRepository = new ReminderRepository();
            if (!File.Exists("Reminders.json"))
            {
                File.Create("Reminders.json");
            }

            reminderRepository.LoadFromJson(File.ReadAllText("Reminders.json"));

            var appointmentRepository = new AppointmentRepository();

            if (!File.Exists("Appointments.json"))
            {
                File.Create("Appointments.json");
            }

            appointmentRepository.LoadFromJson(File.ReadAllText("Appointments.json"));

            var meetingRepository = new MeetingRepository();
            if (!File.Exists("Meetings.json"))
            {
                File.Create("Meetings.json");
            }

            meetingRepository.LoadFromJson(File.ReadAllText("Meetings.json"));

            // Task 5:
            // Fill in the missing options A, V, F, D for all classes
            var sessionEnded = false;
            while (!sessionEnded)
            {
                Console.WriteLine("Q: save and quit");
                Console.WriteLine("A: add item");
                Console.WriteLine("V: view all");
                Console.WriteLine("F: find by date");
                Console.WriteLine("D: delete an item");
                Console.WriteLine();

                Console.Write("Select an action: ");
                var selectedOption = Console.ReadKey().KeyChar;
                Console.Clear();

                switch (selectedOption)
                {
                    case ('Q'):
                        //End the session when they select q
                        sessionEnded = true;
                        break;
                    case ('A'):
                        Console.WriteLine("A: Appointment");
                        Console.WriteLine("M: Meeting");
                        Console.WriteLine("R: Reminder");
                        Console.WriteLine();
                        Console.Write("Select a type:");
                        var selectedType = Console.ReadKey().KeyChar;
                        Console.Clear();

                        switch (selectedType)
                        {//switch statements require a "break;", be careful not to experience this error
                            case ('A'):
                                var newAppointment = appointmentRepository.Create();
                                var appStartDate = RetrieveStartDate();
                                var appStartTime = RetrieveStartTime();
                                newAppointment.startDateTime = new DateTime(appStartDate.Year, appStartDate.Month, appStartDate.Day, appStartTime.Hour, appStartTime.Minute, appStartTime.Second);

                                var appEndDate = RetrieveEndDate();
                                var appEndTime = RetrieveEndTime();
                                newAppointment.endDateTime = new DateTime(appEndDate.Year, appEndDate.Month, appEndDate.Day, appEndTime.Hour, appEndTime.Minute, appEndTime.Second);

                                Console.WriteLine("Location:");
                                newAppointment.Location = Console.ReadLine();
                                newAppointment.attendees = RetrieveAttendees();

                                appointmentRepository.Update(newAppointment);
                                break;
                            case ('M'):
                                var newMeeting = meetingRepository.Create();
                                var meetStartDate = RetrieveStartDate();
                                var meetStartTime = RetrieveStartTime();
                                newMeeting.startDateTime = new DateTime(meetStartDate.Year, meetStartDate.Month, meetStartDate.Day, meetStartTime.Hour, meetStartTime.Minute, meetStartTime.Second);

                                var meetEndDate = RetrieveEndDate();
                                var meetEndTime = RetrieveEndTime();
                                newMeeting.endDateTime = new DateTime(meetEndDate.Year, meetEndDate.Month, meetEndDate.Day, meetEndTime.Hour, meetEndTime.Minute, meetEndTime.Second);

                                Console.WriteLine("Location:");
                                newMeeting.Location = Console.ReadLine();
                                newMeeting.attendees = RetrieveAttendees();

                                meetingRepository.Update(newMeeting);
                                break;
                            case ('R'):
                                var newReminder = reminderRepository.Create();
                                var remStartDate = RetrieveStartDate();
                                var remStartTime = RetrieveStartTime();
                                newReminder.startDateTime = new DateTime(remStartDate.Year, remStartDate.Month, remStartDate.Day, remStartTime.Hour, remStartTime.Minute, remStartTime.Second);

                                var remEndDate = RetrieveEndDate();
                                var remEndTime = RetrieveEndTime();
                                newReminder.endDateTime = new DateTime(remEndDate.Year, remEndDate.Month, remEndDate.Day, remEndTime.Hour, remEndTime.Minute, remEndTime.Second);

                                Console.WriteLine("Location:");
                                newReminder.Location = Console.ReadLine();
                                newReminder.attendees = RetrieveAttendees();

                                reminderRepository.Update(newReminder);
                                break;
                            default:
                                //Note: The $"abc {variable} def" syntax below is new syntactic sugar in C# 6.0 that can be used 
                                //in place of string.Format() in previous versions of C#.
                                Console.WriteLine($"Invalid Type {selectedType}, press any key to continue.");
                                Console.Read();
                                break;
                        }

                        break;
                    case ('V'):
                        var appointments = appointmentRepository.GetAllItems();
                        DisplayAppointments(appointments);

                        var meetings = meetingRepository.GetAllItems();
                        DisplayMeetings(meetings);

                        var reminders = reminderRepository.GetAllItems();
                        DisplayReminders(reminders);

                        break;
                    case ('F'):
                        Console.WriteLine("Input the start date you wish to surch for.");
                        var date = RetrieveStartDate();

                        var appointmentsOnDate = appointmentRepository.FindByDate(date);
                        DisplayAppointments(appointmentsOnDate);

                        var meetingsOnDate = meetingRepository.FindByDate(date);
                        DisplayMeetings(meetingsOnDate);

                        var remindersOnDate = reminderRepository.FindByDate(date);
                        DisplayReminders(remindersOnDate);

                        break;
                    case ('D'):
                        Console.WriteLine("A: Appointment");
                        Console.WriteLine("M: Meeting");
                        Console.WriteLine("R: Reminder");
                        Console.WriteLine();
                        Console.Write("Select a type:");
                        var typeToDelete = Console.ReadKey().KeyChar;
                        Console.Clear();

                        switch (typeToDelete)
                        {
                            case ('A'):
                                var appID = RetrieveID();
                                appointmentRepository.Delete(appointmentRepository.FindById(appID));

                                break;
                            case ('M'):
                                var meetID = RetrieveID();
                                meetingRepository.Delete(meetingRepository.FindById(meetID));

                                break;
                            case ('R'):
                                var reminderID = RetrieveID();
                                reminderRepository.Delete(reminderRepository.FindById(reminderID));

                                break;
                            default:
                                Console.WriteLine($"Invalid Option {selectedOption}, press any key to continue.");
                                Console.Read();
                                break;
                        }

                        break;
                    default:
                        Console.WriteLine($"Invalid Option {selectedOption}, press any key to continue.");
                        Console.Read();
                        break;
                }
            }

            File.WriteAllText("Appointments.json", appointmentRepository.ToJson());
            File.WriteAllText("Meetings.json", meetingRepository.ToJson());
            File.WriteAllText("Reminders.json", reminderRepository.ToJson());
        }

        private static int RetrieveID()
        {
            int convertedId;
            while (true)
            {
                Console.WriteLine("Please enter the ID of the Appointment to delete:");
                var id = Console.ReadLine();
                if (int.TryParse(id, out convertedId))
                {
                    break;
                }
                continue;
            }

            return convertedId;
        }

        private static void DisplayReminders(IEnumerable<Reminder> reminders)
        {
            Console.WriteLine("Reminders:");
            foreach (var item in reminders)
            {
                Console.WriteLine(string.Format("ID: {0}, StartDate: {1}, StartTime: {2}",
                    item.Id, item.startDateTime.Date, item.startDateTime.TimeOfDay));
            }
        }

        private static void DisplayMeetings(IEnumerable<Meeting> meetings)
        {
            Console.WriteLine("Meetings:");
            foreach (var item in meetings)
            {
                Console.WriteLine(string.Format("ID: {0}, StartDate: {1}, StartTime: {2}, Location: {3}",
                    item.Id, item.startDateTime.Date, item.startDateTime.TimeOfDay, item.Location));
            }
        }

        private static void DisplayAppointments(IEnumerable<Appointment> appointments)
        {
            Console.WriteLine("Appointments:");
            foreach (var item in appointments)
            {
                Console.WriteLine(string.Format("ID: {0}, StartDate: {1}, StartTime: {2}, Location: {3}",
                    item.Id, item.startDateTime.Date, item.startDateTime.TimeOfDay, item.Location));
            }
        }

        private static List<string> RetrieveAttendees()
        {
            var attendees = new List<string>();
            Console.WriteLine("Attendees (Enter each name individualy, enter blank line to finish):");
            while (true)
            {
                var entry = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(entry))
                {
                    break;
                }
                attendees.Add(entry);
            }
            return attendees;
        }

        private static DateTime RetrieveStartDate()
        {
            DateTime formatedDate;

            while (true)
            {
                Console.WriteLine("Start date(MM/DD/YYYY):");
                var unformatedDate = Console.ReadLine();
                if (DateTime.TryParse(unformatedDate, out formatedDate))
                {
                    break;
                }
                else
                {
                    Console.WriteLine(unformatedDate + " is not formated correctly");
                    continue;
                }
            }
            return formatedDate;
        }

        private static DateTime RetrieveStartTime()
        {
            DateTime formatedDate;

            while (true)
            {
                Console.WriteLine("Start Time(00:00am):");
                var unformatedDate = Console.ReadLine();
                if (DateTime.TryParse(unformatedDate, out formatedDate))
                {
                    break;
                }
                else
                {
                    Console.WriteLine(unformatedDate + " is not formated correctly");
                    continue;
                }
            }
            return formatedDate;
        }

        private static DateTime RetrieveEndDate()
        {
            DateTime formatedDate;

            while (true)
            {
                Console.WriteLine("End date(MM/DD/YYYY):");
                var unformatedDate = Console.ReadLine();
                if (DateTime.TryParse(unformatedDate, out formatedDate))
                {
                    break;
                }
                else
                {
                    Console.WriteLine(unformatedDate + " is not formated correctly");
                    continue;
                }
            }
            return formatedDate;
        }

        private static DateTime RetrieveEndTime()
        {
            DateTime formatedDate;

            while (true)
            {
                Console.WriteLine("End Time(00:00am):");
                var unformatedDate = Console.ReadLine();
                if (DateTime.TryParse(unformatedDate, out formatedDate))
                {
                    break;
                }
                else
                {
                    Console.WriteLine(unformatedDate + " is not formated correctly");
                    continue;
                }
            }
            return formatedDate;
        }
    }
}
