using Appointment_BLL;
using Appointment_DAL;
using Appointment_DTO;
using Doctor_BLL;
using Doctor_DTO;
using DoctorConsult_DTO;
using HospitalSystem_BLL;
using Logger;
using Patient_BLL;
using Patient_DTO;
using System;

namespace Menu_Interface_PL
{
    public class MenuInterfacePL
    {
        // Methods
        public static void HospitalManagementSystem()
        {
            PatientBLL patientBLLCheck = new PatientBLL();
            DoctorBLL doctorBLLCheck = new DoctorBLL();
            HospitalSystemBLL temp = new HospitalSystemBLL();
            AppointmentBLL appointmentBLLCheck = new AppointmentBLL();
            Console.WriteLine("\t\t\tHospital Management App");
            int choise = 0;
            do
            {
                Console.WriteLine("Enter 1 to Add Patient");
                Console.WriteLine("Enter 2 to Update Patient");
                Console.WriteLine("Enter 3 to Delete Patient");
                Console.WriteLine("Enter 4 to Display Patients");
                Console.WriteLine("Enter 5 to Add Doctor");
                Console.WriteLine("Enter 6 to Update Doctor");
                Console.WriteLine("Enter 7 to Delete Doctor");
                Console.WriteLine("Enter 8 to Display Doctors");
                Console.WriteLine("Enter 9 to Book Appointment");
                Console.WriteLine("Enter 10 to Cancel Appointment");
                Console.WriteLine("Enter 11 to Declare Most Consulted Doctors");
                Console.WriteLine("Enter 0 for Exit");
                Console.Write("Select Option: ");
                string? choice = Console.ReadLine();
                if (int.TryParse(choice, out int value))
                {
                    choise = value;
                }
                else
                {
                    choise = 12;
                }

                if (choise == 1) // Add Patient
                {
                    Console.Write("Enter Patient CNIC: ");
                    string? cnic = Console.ReadLine();
                    while (string.IsNullOrEmpty(cnic) || string.IsNullOrWhiteSpace(cnic))
                    {
                        Console.Write("Invalid Input, Enter Patient CNIC again: ");
                        cnic = Console.ReadLine();
                    }
                    if (patientBLLCheck.Exists(cnic))
                    {
                        Console.WriteLine($"\nPatient already exist in the database\n");
                    }
                    else
                    {
                        Console.Write("Enter Patient Name: ");
                        string? name = Console.ReadLine();
                        while (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                        {
                            Console.Write("Invalid Input, Enter Patient name again: ");
                            name = Console.ReadLine();
                        }
                        temp.AddPatient(name, cnic);
                        Console.WriteLine($"\nPatient added succesfully\n");
                    }
                }

                else if (choise == 2) // Update Patient
                {
                    Console.Write("Enter patient CNIC: ");
                    string? cnic = Console.ReadLine();
                    while (string.IsNullOrEmpty(cnic) || string.IsNullOrWhiteSpace(cnic))
                    {
                        Console.Write("Invalid input, Enter patient CNIC again: ");
                        cnic = Console.ReadLine();
                    }
                    if (patientBLLCheck.Exists(cnic))
                    {
                        Console.Write("Enter patient new name: ");
                        string? name = Console.ReadLine();
                        while (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                        {
                            Console.Write("Invalid input, Enter patient new name again: ");
                            name = Console.ReadLine();
                        }
                        temp.UpdatePatient(name, cnic);
                        Console.WriteLine($"\nPatient data updated succesfully\n");
                    }
                    else
                    {
                        Console.WriteLine($"\nPatient does not exist in the database\n");
                    }

                }

                else if (choise == 3) //Delete Patient
                {
                    Console.Write("Enter patient CNIC: ");
                    string? cnic = Console.ReadLine();
                    while (string.IsNullOrEmpty(cnic) || string.IsNullOrWhiteSpace(cnic))
                    {
                        Console.Write("Invalid input, Enter patient CNIC again: ");
                        cnic = Console.ReadLine();
                    }
                    if (patientBLLCheck.Exists(cnic))
                    {
                        temp.DeletePatient(cnic);
                        Console.WriteLine($"\nPatient data deleted succesfully\n");
                    }
                    else
                    {
                        Console.WriteLine($"\nPatient does not exist in the database\n");
                    }
                }

                else if (choise == 4) //Display all Patients in Database
                {
                    List<PatientDTO> temp2 = temp.DisplayPatients();
                    Console.WriteLine("\n\t\tDisplaying all patiends.\n");
                    foreach (var patients in temp2)
                    {
                        Console.WriteLine($"Name: {patients.Name}\nCNIC: {patients.CNIC}\n");
                    }
                }

                else if (choise == 5) // Add Doctor in Database and Files
                {
                    Console.Write("Enter Doctor Name: ");
                    string? name = Console.ReadLine();
                    while (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                    {
                        Console.Write("Invalid Input, Enter Doctor name again: ");
                        name = Console.ReadLine();
                    }
                    Console.Write("Enter Doctor Specialization: ");
                    string? specialization = Console.ReadLine();
                    while (string.IsNullOrEmpty(specialization) || string.IsNullOrWhiteSpace(specialization))
                    {
                        Console.Write("Invalid Input, Enter Doctor Specialization again: ");
                        specialization = Console.ReadLine();
                    }
                    temp.AddDoctor(name, specialization);
                    Console.WriteLine($"\nDoctor data added succesfully\n");
                }

                else if (choise == 6) // Update Doctor
                {
                    Console.Write("Enter Doctor ID : ");
                    string? dID = Console.ReadLine();
                    int id = 0;
                    while (string.IsNullOrEmpty(dID) || string.IsNullOrWhiteSpace(dID) || !int.TryParse(dID, out id) || id <= 0)
                    {
                        Console.Write("Invalid Input. Enter Doctor ID again: ");
                        dID = Console.ReadLine();
                    }
                    if (doctorBLLCheck.Exists(id))
                    {
                        Console.Write("Select the field that you want to update {(Name,Specialization) or (1,2)}: ");
                        string? decision = Console.ReadLine();
                        while (decision != "Name" && decision != "Specialization" && decision != "1" && decision != "2")
                        {
                            Console.Write("Invalid Input. Select the field that you want to update {(Name,Specialization) or (1,2)} again: ");
                            decision = Console.ReadLine();
                        }
                        if (decision == "Name" || decision == "1")
                        {
                            DoctorDTO doctorDTO = doctorBLLCheck.GetById(id);
                            Console.Write("Enter Doctor new Name: ");
                            string? name = Console.ReadLine();
                            while (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                            {
                                Console.Write("Invalid Input, Enter Doctor new name again: ");
                                name = Console.ReadLine();
                            }
                            temp.UpdateDoctor(id, name, doctorDTO.Specialization);
                            Console.WriteLine($"\nDoctor data updated succesfully\n");
                        }
                        else if (decision == "Specialization" || decision == "2")
                        {
                            DoctorDTO doctorDTO = doctorBLLCheck.GetById(id);
                            Console.Write("Enter Doctor new Specialization: ");
                            string? specialization = Console.ReadLine();
                            while (string.IsNullOrEmpty(specialization) || string.IsNullOrWhiteSpace(specialization))
                            {
                                Console.Write("Invalid Input, Enter Doctor new Specialization again: ");
                                specialization = Console.ReadLine();
                            }
                            temp.UpdateDoctor(id, doctorDTO.Name, specialization);
                            Console.WriteLine($"\nDoctor data updated succesfully\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nDoctor doesn't exist in the database.\n");
                    }
                }

                else if (choise == 7) // Delete Doctor
                {
                    Console.Write("Enter Doctor ID : ");
                    string? dID = Console.ReadLine();
                    int id = 0;
                    while (string.IsNullOrEmpty(dID) || string.IsNullOrWhiteSpace(dID) || !int.TryParse(dID, out id) || id <= 0)
                    {
                        Console.Write("Invalid Input. Enter Doctor ID again: ");
                        dID = Console.ReadLine();
                    }
                    if (doctorBLLCheck.Exists(id))
                    {
                        temp.DeleteDoctor(id);
                        Console.WriteLine("\nDoctor data deleted successfully\n");
                    }
                    else
                    {
                        Console.WriteLine("\nDoctor doesn't exist in the database.\n");
                    }
                }

                else if (choise == 8) // Display Doctors
                {
                    List<DoctorDTO> temp2 = temp.DisplayDoctors();
                    Console.WriteLine("\n\t\tDisplaying all doctorss.\n");
                    foreach (var item in temp2)
                    {
                        Console.WriteLine($"DoctorID: {item.DoctorID}\nName: {item.Name}\nSpecialization: {item.Specialization}\nAvalibilite: {item.IsAvailable}\n");
                    }
                }

                else if (choise == 9) // Book Appointment
                {
                    Console.Write("Enter Doctor ID : ");
                    string? dID = Console.ReadLine();
                    int id = 0;
                    while (string.IsNullOrEmpty(dID) || string.IsNullOrWhiteSpace(dID) || !int.TryParse(dID, out id) || id <= 0)
                    {
                        Console.Write("Invalid Input. Enter Doctor ID again: ");
                        dID = Console.ReadLine();
                    }
                    if (doctorBLLCheck.Exists(id))
                    {
                        Console.Write("Enter Patient CNIC: ");
                        string? cnic = Console.ReadLine();
                        while (string.IsNullOrEmpty(cnic) || string.IsNullOrWhiteSpace(cnic))
                        {
                            Console.Write("Invalid Input, Enter Patient CNIC again: ");
                            cnic = Console.ReadLine();
                        }
                        if (patientBLLCheck.Exists(cnic))
                        {
                            Console.Write("Enter the datetime of the appointment: ");
                            string? dateS = Console.ReadLine();
                            DateTime date = DateTime.Now;
                            while (string.IsNullOrEmpty(dateS) || string.IsNullOrWhiteSpace(dateS) || !DateTime.TryParse(dateS, out date) || date <= DateTime.Now)
                            {
                                Console.Write("Invalid Input. Enter the datetime of the appointment again: ");
                                dateS = Console.ReadLine();
                            }
                            temp.BookAppointment(id, cnic, date);
                            Console.WriteLine($"\nAppointment has been added successfully\n");
                        }
                        else
                        {
                            Console.WriteLine("\nAppointment cannot be added because the requesting Patient does not exist.\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nAppointment cannot be added because the requested Doctor does not exist.\n");
                    }
                }

                else if (choise == 10) // Cancel Appointment
                {
                    Console.Write("Enter Appointment ID: ");
                    string? sID = Console.ReadLine();
                    int id = 0;
                    while (string.IsNullOrEmpty(sID) || string.IsNullOrWhiteSpace(sID) || !int.TryParse(sID, out id) || id <= 0)
                    {
                        Console.Write("Invalid Input. Enter Appointment ID again: ");
                        sID = Console.ReadLine();
                    }
                    if (appointmentBLLCheck.Exists(id))
                    {
                        Console.Write("Enter Patient CNIC: ");
                        string? cnic = Console.ReadLine();
                        while (string.IsNullOrEmpty(cnic) || string.IsNullOrWhiteSpace(cnic))
                        {
                            Console.Write("Invalid Input, Enter Patient CNIC again: ");
                            cnic = Console.ReadLine();
                        }
                        AppointmentDTO appointmentDTO = appointmentBLLCheck.GetById(id);
                        if (appointmentDTO.PatientCNIC == cnic)
                        {
                            temp.CancelAppointment(id, cnic);
                            Console.WriteLine("\nAppointment has been removed from the database.\n");
                        }
                        else
                        {
                            Console.WriteLine("\nAppointment cannot be cancelled because the patient cnic associated to the appointment and the given patient cnic does not match.\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nAppointment does not exist.\n");
                    }
                }

                else if (choise == 11)
                {
                    List<DoctorConsultedDTO> doctorConsultedDTOs = temp.GetMostConsultedDoctors();
                    if (doctorConsultedDTOs.Count() == 0)
                    {
                        Console.WriteLine("\nNo Doctor found\n");
                    }
                    else
                    {
                        foreach (var doctor in doctorConsultedDTOs)
                        {
                            Console.WriteLine($"\nDoctorID: {doctor.DoctorID}\nDoctor Name: {doctor.Name}\nSpecialization: {doctor.Specialization}\nAppointmentCount: {doctor.AppointmentCount}\n");
                        }
                    }
                }

                else if (choise == 0) // Exit Condition
                {
                    Console.WriteLine("\nExiting....");
                }

                else // Invalid Responce
                {
                    Console.WriteLine("\nInvalid input.\n");
                }
            }
            while (choise != 0);

        }

    }
}
