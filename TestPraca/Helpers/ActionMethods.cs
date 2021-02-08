using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestPraca.Models;

namespace TestPraca.Helpers
{
    public class ActionMethods
    {

        private DBconn db = new DBconn();

        public Guid generateUniqueId()
        {
            return System.Guid.NewGuid();
        }

        public Results getAllSearchResults(SearchCriteria criteria)
        {
            List<Employee> jobTitles = getJobTitlesSearch(criteria.EmployeeJobTitles);

            List<Employee> DOBs = getDateOfBirthSearch(criteria.EmployeeDateOfBirthFrom, criteria.EmployeeDateOfBirthTo);

            List<Company> keywordsObj = getKeywordSearch(criteria.Keyword);

            Dictionary<System.Guid, Company> compValid = new Dictionary<System.Guid, Company>();

            Results results = new Results();


            // pierw dodaje obiekt company 
            if (keywordsObj != null)
            {
                foreach (var com in keywordsObj)
                {                    
                    compValid.Add(com.id, com);
                }
            }

            //// nastepnie filtruje liste DOBs 
            if (DOBs != null)
            {
                foreach (var dob in DOBs)
                {
                    if (compValid.ContainsKey(dob.CompanyId))
                    {
                        Company temp = compValid[dob.CompanyId];

                        //sprawdzanie duplikatow Employee w liscie
                        if (!temp.Employees.Any(a => a.Id == dob.Id))
                        {
                            temp.Employees.Add(dob);
                            //update company
                            compValid[dob.CompanyId] = temp;
                        }
                    }
                    else
                    {
                        Company company = getCompany(dob.CompanyId);
                        company.Employees.Clear();
                        company.Employees.Add(dob);

                        compValid.Add(dob.CompanyId, company);
                    }
                }
            }

            //nastepnie filtruje jobTitles
            if (jobTitles != null)
            {
                foreach (var job in jobTitles)
                {
                    if (compValid.ContainsKey(job.CompanyId))
                    {
                        Company temp = compValid[job.CompanyId];

                        //sprawdzanie duplikatow Employee w liscie
                        if (!temp.Employees.Any(a => a.Id == job.Id))
                        {
                            temp.Employees.Add(job);
                            //update company
                            compValid[job.CompanyId] = temp;
                        }
                    }
                    else
                    {
                        Company company = getCompany(job.CompanyId);
                        company.Employees.Clear();
                        company.Employees.Add(job);
                        compValid.Add(job.CompanyId, company);
                    }
                }
            }
            results.Companies = compValid.Values.Select(x => x).ToList();

            return results;
        }

        public List<Employee> getJobTitlesSearch(string[] jobTitle)
        {
            try
            {
                // zwraca employee z kilku rol

                var list = db.Employees.Where(x => jobTitle.Contains(x.JobTitle)).ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Employee> getDateOfBirthSearch(DateTime dobFrom, DateTime dobTo)
        {
            try
            {
                var list = db.Employees.Where(x => x.DataOfBirth >= dobFrom && x.DataOfBirth <= dobTo).ToList();

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Company> getKeywordSearch(string str)
        {
            try
            {
                var list = db.Employees.Where(x => x.FirstName.Contains(str) || x.LastName.Contains(str)).ToList();

                var list2 = db.Companies.Where(x => x.Name.Contains(str)).ToList();


                List<Company> mergingList = new List<Company>();

                Dictionary<System.Guid, Company> compValid = new Dictionary<System.Guid, Company>();


                // pierw dodaje obiekt company 
                if (list2 != null)
                {
                    foreach (var com in list2)
                    {
                        compValid.Add(com.id, com);
                    }
                }

                foreach (var emp in list)
                {
                    if (compValid.ContainsKey(emp.CompanyId))
                    {
                        Company temp = compValid[emp.CompanyId];

                        if (!temp.Employees.Any(a => a.Id == emp.Id))
                        {
                            temp.Employees.Add(emp);
                            compValid[emp.CompanyId] = temp;
                        }
                    }
                    else
                    {
                        Company company = getCompany(emp.CompanyId);
                        company.Employees.Clear();
                        company.Employees.Add(emp);

                        compValid.Add(emp.CompanyId, company);
                    }
                }


                mergingList = compValid.Values.Select(x => x).ToList();

                return mergingList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Company getCompany(System.Guid id)
        {
            try
            {
                Company comp = db.Companies.Find(id);
                return comp;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
}