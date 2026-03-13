using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DoctorPatientSystem.Entities
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set => _name = !string.IsNullOrWhiteSpace(value) ? value.Trim() : throw new ArgumentException("Doctor Name cannot be null");
        }
        public string Specialization { get; set; } = string.Empty;
        private decimal _consultationFee;
        public decimal ConsultationFee
        {
            get => _consultationFee;
            set => _consultationFee = value > 0 ? value : throw new ArgumentException("Fees must be positive");
        }
        public ICollection<Patient> Patients { get; set; } = new List<Patient>();
        public Doctor() { }
        public Doctor(string name, string specialization, decimal consultationFee) {
            Name = name;
            Specialization = specialization;
            ConsultationFee = consultationFee;
        }
        public void AssignPatient(Patient patient)
        {
            if (patient == null) throw new ArgumentNullException(nameof(patient);
            patient.DoctorId = DoctorId;
            Patients.Add(patient);

        }
        public override string ToString() => $"[{DoctorId}] Dr.{ Name} "
}
