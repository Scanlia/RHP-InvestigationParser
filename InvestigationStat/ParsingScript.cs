using System;
using System.Collections.Generic;
using System.IO;

namespace InvestigationStat
{
    class ParsingScript
    {
        static string fileLocation = "C:\\Users\\scanl\\Google Drive\\University\\UQ\\Year 3\\Rural Remote Medicine\\Rural Health Project\\365-All.csv";

        static void Main(string[] args)
        {
            if (!File.Exists(fileLocation))
            {
                Console.WriteLine("Could not find file, exiting...");
                return;
            }
            
            Console.WriteLine("Found file, starting...");

            using (var reader = new StreamReader(fileLocation))
            {
                SortedList<string, List<string>> uniquePatients = new SortedList<string, List<string>>();
                int entryCount = 0;

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] cells = line.Split(',');

                    if (cells.Length < 3)
                        Console.WriteLine("Found a line with " + cells.Length + " cells?");
                    else
                    {
                        if (string.IsNullOrWhiteSpace(cells[0]) ||
                            string.IsNullOrWhiteSpace(cells[1]) ||
                            string.IsNullOrWhiteSpace(cells[2]))
                            continue;

                        if (!uniquePatients.ContainsKey(cells[1]))
                            uniquePatients.Add(cells[1], new List<string>());

                        uniquePatients[cells[1]].Add(cells[2]);
                        entryCount++;

                        Console.WriteLine(cells[0] + " " + cells[1] + " " + cells[2]);
                    }
                }

                Console.WriteLine("Entries: " + entryCount);
                Console.WriteLine("Patients: " + uniquePatients.Count);

                int xray = 0;
                int ct = 0;
                int mri = 0;
                int us = 0;

                int mri_knee = 0;
                int mri_head = 0;
                int mri_spine = 0;
                int mri_elbow = 0;
                int mri_hip = 0;
                int mri_ankle = 0;
                int mri_shoulder = 0;

                foreach (var pt in uniquePatients)
                {
                    foreach (var investigation in pt.Value)
                    {
                        bool current_xray = investigation.ToLower().Contains("xr") ||
                                            investigation.ToLower().Contains("x-ray") ||
                                            investigation.ToLower().Contains("xray") ||
                                            investigation.ToLower().Contains("x-rya");
                        bool current_ct = investigation.Contains("CT") ||
                                          investigation.ToLower().Contains("ct scan");
                        bool current_mri = investigation.Contains(" MRI") ||
                                           investigation.Contains("MRI ");
                        bool current_us = investigation.ToLower().Contains("ultrasound") ||
                                          investigation.Contains(" US") ||
                                          investigation.Contains("US ") ||
                                          investigation.Contains("USS");

                        if (current_xray)
                            xray++;
                        if (current_ct)
                            ct++;
                        if (current_us)
                            us++;
                        if (current_mri)
                        {
                            mri++;
                            if (investigation.ToLower().Contains("knee"))
                                mri_knee++;
                            if (investigation.ToLower().Contains("brain") ||
                                investigation.ToLower().Contains("head"))
                                mri_head++;
                            if (investigation.ToLower().Contains("spine") ||
                                investigation.ToLower().Contains("cervical") ||
                                investigation.ToLower().Contains("lumbar"))
                                mri_spine++;
                            if (investigation.ToLower().Contains("elbow"))
                                mri_elbow++;
                            if (investigation.ToLower().Contains("hip"))
                                mri_hip++;
                            if (investigation.ToLower().Contains("shoulder"))
                                mri_shoulder++;
                            if (investigation.ToLower().Contains("ankle") ||
                                investigation.ToLower().Contains("heel"))
                                mri_ankle++;
                        }
                    }
                }

                Console.WriteLine("Xrays: " + xray);
                Console.WriteLine("CT: " + ct);
                Console.WriteLine("USS: " + us);
                Console.WriteLine("MRI: " + mri);

                Console.WriteLine("MRI_Knee: " + mri_knee);
                Console.WriteLine("MRI_Head: " + mri_head);
                Console.WriteLine("MRI_Spine: " + mri_spine);
                Console.WriteLine("MRI_Elbow: " + mri_elbow);
                Console.WriteLine("MRI_Hip: " + mri_hip);
                Console.WriteLine("MRI_Shoulder: " + mri_shoulder);
                Console.WriteLine("MRI_Ankle: " + mri_ankle);
            }
        }
    }
}
