using System;
namespace LPU
{
    class Hostels
    {
        public string HostelName="";
        public int RoomNo=0;
        public string Bed="";

        public void DisplayHostel()
        {
            Console.WriteLine("Hostel Name: "+HostelName);
            Console.WriteLine("Room No: "+RoomNo);
            Console.WriteLine("Bed: "+Bed);
        }
    }
}