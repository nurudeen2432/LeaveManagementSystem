select * from AspNetUsers s inner join AspNetUserRoles r on s.Id=r.UserId;
--4ec7f2c6-1565-44cd-bcd4-c6cd3f03d179
--8d191100-59a5-4ca4-9b13-97f55234db38

select * from LeaveTypes;

select * from LeaveRequests l inner join AspNetUsers a on l.EmployeeId = a.Id;

select * from AspNetUsers where Id='4617c5b9-fba1-4e18-003b-08dd2c22a224'

select * from AspNetRoles

select * from Periods