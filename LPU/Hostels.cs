using System;
namespace LPU
{
    class Hostels
    {
        public string HostelName{get;set;}
        public int RoomNo{get;set;}
        public string Bed{get;set;}

        public void DisplayHostel()
        {
            Console.WriteLine("Hostel Name: "+HostelName);
            Console.WriteLine("Room No: "+RoomNo);
            Console.WriteLine("Bed: "+Bed);
        }
    }
}