-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 05, 2021 at 07:53 AM
-- Server version: 10.4.11-MariaDB
-- PHP Version: 7.4.5

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `diu-spa-db`
--

-- --------------------------------------------------------

--
-- Table structure for table `approles`
--

CREATE TABLE `approles` (
  `RoleId` int(11) NOT NULL,
  `CreatedBy` int(11) DEFAULT NULL,
  `CreatedDate` datetime(6) DEFAULT NULL,
  `UpdatedBy` int(11) DEFAULT NULL,
  `UpdatedDate` datetime(6) DEFAULT NULL,
  `EffectiveFrom` datetime(6) DEFAULT NULL,
  `EffectiveTo` datetime(6) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT NULL,
  `Status` int(11) DEFAULT NULL,
  `Attribute1` longtext DEFAULT NULL,
  `Attribute2` longtext DEFAULT NULL,
  `Attribute3` longtext DEFAULT NULL,
  `Attribute4` longtext DEFAULT NULL,
  `Attribute5` longtext DEFAULT NULL,
  `Attribute6` longtext DEFAULT NULL,
  `Attribute7` longtext DEFAULT NULL,
  `Attribute8` longtext DEFAULT NULL,
  `Attribute9` longtext DEFAULT NULL,
  `Attribute10` longtext DEFAULT NULL,
  `RoleName` longtext DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `approles`
--

INSERT INTO `approles` (`RoleId`, `CreatedBy`, `CreatedDate`, `UpdatedBy`, `UpdatedDate`, `EffectiveFrom`, `EffectiveTo`, `IsActive`, `Status`, `Attribute1`, `Attribute2`, `Attribute3`, `Attribute4`, `Attribute5`, `Attribute6`, `Attribute7`, `Attribute8`, `Attribute9`, `Attribute10`, `RoleName`) VALUES
(1, 1, '2021-04-04 01:27:14.266992', 1, '2021-04-04 01:27:14.267289', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'SuperAdmin'),
(2, 1, '2021-04-04 01:27:14.267477', 1, '2021-04-04 01:27:14.267478', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'AppAdmin'),
(3, 1, '2021-04-04 01:27:14.267479', 1, '2021-04-04 01:27:14.267479', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Employee'),
(4, 1, '2021-04-04 01:27:14.267480', 1, '2021-04-04 01:27:14.267480', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Stundent');

-- --------------------------------------------------------

--
-- Table structure for table `appuserroles`
--

CREATE TABLE `appuserroles` (
  `UserId` int(11) NOT NULL,
  `RoleId` int(11) NOT NULL,
  `CreatedBy` int(11) DEFAULT NULL,
  `CreatedDate` datetime(6) DEFAULT NULL,
  `UpdatedBy` int(11) DEFAULT NULL,
  `UpdatedDate` datetime(6) DEFAULT NULL,
  `EffectiveFrom` datetime(6) DEFAULT NULL,
  `EffectiveTo` datetime(6) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT NULL,
  `Status` int(11) DEFAULT NULL,
  `Attribute1` longtext DEFAULT NULL,
  `Attribute2` longtext DEFAULT NULL,
  `Attribute3` longtext DEFAULT NULL,
  `Attribute4` longtext DEFAULT NULL,
  `Attribute5` longtext DEFAULT NULL,
  `Attribute6` longtext DEFAULT NULL,
  `Attribute7` longtext DEFAULT NULL,
  `Attribute8` longtext DEFAULT NULL,
  `Attribute9` longtext DEFAULT NULL,
  `Attribute10` longtext DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `appuserroles`
--

INSERT INTO `appuserroles` (`UserId`, `RoleId`, `CreatedBy`, `CreatedDate`, `UpdatedBy`, `UpdatedDate`, `EffectiveFrom`, `EffectiveTo`, `IsActive`, `Status`, `Attribute1`, `Attribute2`, `Attribute3`, `Attribute4`, `Attribute5`, `Attribute6`, `Attribute7`, `Attribute8`, `Attribute9`, `Attribute10`) VALUES
(1, 1, 1, '2021-04-04 01:27:14.652193', 1, '2021-04-04 01:27:14.652195', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(2, 2, 1, '2021-04-04 01:27:14.652196', 1, '2021-04-04 01:27:14.652197', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(3, 3, 1, '2021-04-04 01:27:14.652197', 1, '2021-04-04 01:27:14.652197', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(4, 4, 1, '2021-04-04 01:27:14.652198', 1, '2021-04-04 01:27:14.652198', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `appusers`
--

CREATE TABLE `appusers` (
  `UserId` int(11) NOT NULL,
  `CreatedBy` int(11) DEFAULT NULL,
  `CreatedDate` datetime(6) DEFAULT NULL,
  `UpdatedBy` int(11) DEFAULT NULL,
  `UpdatedDate` datetime(6) DEFAULT NULL,
  `EffectiveFrom` datetime(6) DEFAULT NULL,
  `EffectiveTo` datetime(6) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT NULL,
  `Status` int(11) DEFAULT NULL,
  `Attribute1` longtext DEFAULT NULL,
  `Attribute2` longtext DEFAULT NULL,
  `Attribute3` longtext DEFAULT NULL,
  `Attribute4` longtext DEFAULT NULL,
  `Attribute5` longtext DEFAULT NULL,
  `Attribute6` longtext DEFAULT NULL,
  `Attribute7` longtext DEFAULT NULL,
  `Attribute8` longtext DEFAULT NULL,
  `Attribute9` longtext DEFAULT NULL,
  `Attribute10` longtext DEFAULT NULL,
  `UserName` longtext DEFAULT NULL,
  `Password` longtext DEFAULT NULL,
  `UserType` int(11) DEFAULT NULL,
  `ReferenceId` int(11) DEFAULT NULL,
  `IsTemporaryPasswordOn` tinyint(1) DEFAULT NULL,
  `TemporaryPassword` longtext DEFAULT NULL,
  `IsMobileVerified` tinyint(1) DEFAULT NULL,
  `IsEmailVerified` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `appusers`
--

INSERT INTO `appusers` (`UserId`, `CreatedBy`, `CreatedDate`, `UpdatedBy`, `UpdatedDate`, `EffectiveFrom`, `EffectiveTo`, `IsActive`, `Status`, `Attribute1`, `Attribute2`, `Attribute3`, `Attribute4`, `Attribute5`, `Attribute6`, `Attribute7`, `Attribute8`, `Attribute9`, `Attribute10`, `UserName`, `Password`, `UserType`, `ReferenceId`, `IsTemporaryPasswordOn`, `TemporaryPassword`, `IsMobileVerified`, `IsEmailVerified`) VALUES
(1, 1, '2021-04-04 01:27:14.599045', 1, '2021-04-04 01:27:14.599049', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'superadmin', 'Q890qWhIvIKy3LTPAnXfGxOVAqbARieqikQoDuWzQZ4=', 1, 0, NULL, NULL, NULL, NULL),
(2, 1, '2021-04-04 01:27:14.599050', 1, '2021-04-04 01:27:14.599050', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'spaadmin', 'Q890qWhIvIKy3LTPAnXfGxOVAqbARieqikQoDuWzQZ4=', 2, 0, NULL, NULL, NULL, NULL),
(3, 1, '2021-04-04 01:27:14.599054', 1, '2021-04-04 01:27:14.599055', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'mremployee', 'Q890qWhIvIKy3LTPAnXfGxOVAqbARieqikQoDuWzQZ4=', 3, 0, NULL, NULL, NULL, NULL),
(4, 1, '2021-04-04 01:27:14.599055', 1, '2021-04-04 01:27:14.599055', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'mariful', 'Q890qWhIvIKy3LTPAnXfGxOVAqbARieqikQoDuWzQZ4=', 4, 0, NULL, NULL, NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `authenticationtracers`
--

CREATE TABLE `authenticationtracers` (
  `TracerId` int(11) NOT NULL,
  `CreatedBy` int(11) DEFAULT NULL,
  `CreatedDate` datetime(6) DEFAULT NULL,
  `UpdatedBy` int(11) DEFAULT NULL,
  `UpdatedDate` datetime(6) DEFAULT NULL,
  `EffectiveFrom` datetime(6) DEFAULT NULL,
  `EffectiveTo` datetime(6) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT NULL,
  `Status` int(11) DEFAULT NULL,
  `Attribute1` longtext DEFAULT NULL,
  `Attribute2` longtext DEFAULT NULL,
  `Attribute3` longtext DEFAULT NULL,
  `Attribute4` longtext DEFAULT NULL,
  `Attribute5` longtext DEFAULT NULL,
  `Attribute6` longtext DEFAULT NULL,
  `Attribute7` longtext DEFAULT NULL,
  `Attribute8` longtext DEFAULT NULL,
  `Attribute9` longtext DEFAULT NULL,
  `Attribute10` longtext DEFAULT NULL,
  `UserType` int(11) DEFAULT NULL,
  `ReferenceId` longtext DEFAULT NULL,
  `IPAddress` longtext DEFAULT NULL,
  `CountryName` longtext DEFAULT NULL,
  `Region` longtext DEFAULT NULL,
  `CityName` longtext DEFAULT NULL,
  `PostalCode` longtext DEFAULT NULL,
  `Latitude` longtext DEFAULT NULL,
  `Longitude` longtext DEFAULT NULL,
  `TimeZone` longtext DEFAULT NULL,
  `Organization` longtext DEFAULT NULL,
  `UserAgent` longtext DEFAULT NULL,
  `RemarksOrNote` longtext DEFAULT NULL,
  `RedirectURL` longtext DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `authenticationtracers`
--

INSERT INTO `authenticationtracers` (`TracerId`, `CreatedBy`, `CreatedDate`, `UpdatedBy`, `UpdatedDate`, `EffectiveFrom`, `EffectiveTo`, `IsActive`, `Status`, `Attribute1`, `Attribute2`, `Attribute3`, `Attribute4`, `Attribute5`, `Attribute6`, `Attribute7`, `Attribute8`, `Attribute9`, `Attribute10`, `UserType`, `ReferenceId`, `IPAddress`, `CountryName`, `Region`, `CityName`, `PostalCode`, `Latitude`, `Longitude`, `TimeZone`, `Organization`, `UserAgent`, `RemarksOrNote`, `RedirectURL`) VALUES
(1, 2, '2021-04-04 02:05:56.496377', 2, '2021-04-04 02:05:56.496763', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2, '2', '103.142.68.157', 'Bangladesh', 'Dhaka Division', 'Dhaka', '1000', '23.7272', '90.4093', 'Asia/Dhaka', 'Green Computer & Mobile Care', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.114 Safari/537.36', NULL, NULL),
(2, 2, '2021-04-04 16:08:50.589599', 2, '2021-04-04 16:08:50.590266', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2, '2', '103.142.68.157', 'Bangladesh', 'Dhaka Division', 'Dhaka', '1000', '23.7272', '90.4093', 'Asia/Dhaka', 'Green Computer & Mobile Care', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:83.0) Gecko/20100101 Firefox/83.0', NULL, NULL),
(3, 2, '2021-04-05 11:08:32.571200', 2, '2021-04-05 11:08:32.571554', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2, '2', '103.142.68.147', 'Bangladesh', 'Dhaka Division', 'Dhaka', '1000', '23.7272', '90.4093', 'Asia/Dhaka', 'Green Computer & Mobile Care', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.114 Safari/537.36', NULL, NULL),
(4, 2, '2021-04-05 11:26:29.134382', 2, '2021-04-05 11:26:29.134708', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2, '2', '103.142.68.147', 'Bangladesh', 'Dhaka Division', 'Dhaka', '1000', '23.7272', '90.4093', 'Asia/Dhaka', 'Green Computer & Mobile Care', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:83.0) Gecko/20100101 Firefox/83.0', NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `companysettings`
--

CREATE TABLE `companysettings` (
  `Id` int(11) NOT NULL,
  `CreatedBy` int(11) DEFAULT NULL,
  `CreatedDate` datetime(6) DEFAULT NULL,
  `UpdatedBy` int(11) DEFAULT NULL,
  `UpdatedDate` datetime(6) DEFAULT NULL,
  `EffectiveFrom` datetime(6) DEFAULT NULL,
  `EffectiveTo` datetime(6) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT NULL,
  `Status` int(11) DEFAULT NULL,
  `Attribute1` longtext DEFAULT NULL,
  `Attribute2` longtext DEFAULT NULL,
  `Attribute3` longtext DEFAULT NULL,
  `Attribute4` longtext DEFAULT NULL,
  `Attribute5` longtext DEFAULT NULL,
  `Attribute6` longtext DEFAULT NULL,
  `Attribute7` longtext DEFAULT NULL,
  `Attribute8` longtext DEFAULT NULL,
  `Attribute9` longtext DEFAULT NULL,
  `Attribute10` longtext DEFAULT NULL,
  `CompanyName` longtext NOT NULL,
  `LogoUrl` longtext DEFAULT NULL,
  `Email` longtext DEFAULT NULL,
  `Mobile1` longtext DEFAULT NULL,
  `Mobile2` longtext DEFAULT NULL,
  `Phone1` longtext DEFAULT NULL,
  `Phone2` longtext DEFAULT NULL,
  `TagLine` longtext DEFAULT NULL,
  `Address1` longtext DEFAULT NULL,
  `Address2` longtext DEFAULT NULL,
  `SMSUserName` longtext DEFAULT NULL,
  `SMSPassword` longtext DEFAULT NULL,
  `SMSSender` longtext DEFAULT NULL,
  `MikrotikUrl` longtext DEFAULT NULL,
  `MikrotikUserName` longtext DEFAULT NULL,
  `MikrotikPassword` longtext DEFAULT NULL,
  `UserType` int(11) DEFAULT NULL,
  `ReferenceId` int(11) DEFAULT NULL,
  `BusinessPackage` int(11) DEFAULT NULL,
  `SmsProvider` int(11) DEFAULT NULL,
  `IsCustomerIdEditable` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `courses`
--

CREATE TABLE `courses` (
  `CourseId` int(11) NOT NULL,
  `CreatedBy` int(11) DEFAULT NULL,
  `CreatedDate` datetime(6) DEFAULT NULL,
  `UpdatedBy` int(11) DEFAULT NULL,
  `UpdatedDate` datetime(6) DEFAULT NULL,
  `EffectiveFrom` datetime(6) DEFAULT NULL,
  `EffectiveTo` datetime(6) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT NULL,
  `Status` int(11) DEFAULT NULL,
  `Attribute1` longtext DEFAULT NULL,
  `Attribute2` longtext DEFAULT NULL,
  `Attribute3` longtext DEFAULT NULL,
  `Attribute4` longtext DEFAULT NULL,
  `Attribute5` longtext DEFAULT NULL,
  `Attribute6` longtext DEFAULT NULL,
  `Attribute7` longtext DEFAULT NULL,
  `Attribute8` longtext DEFAULT NULL,
  `Attribute9` longtext DEFAULT NULL,
  `Attribute10` longtext DEFAULT NULL,
  `CourseCode` varchar(50) NOT NULL,
  `CourseName` varchar(100) NOT NULL,
  `Details` varchar(500) DEFAULT NULL,
  `DepartmentId` int(11) NOT NULL,
  `SemesterId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `courses`
--

INSERT INTO `courses` (`CourseId`, `CreatedBy`, `CreatedDate`, `UpdatedBy`, `UpdatedDate`, `EffectiveFrom`, `EffectiveTo`, `IsActive`, `Status`, `Attribute1`, `Attribute2`, `Attribute3`, `Attribute4`, `Attribute5`, `Attribute6`, `Attribute7`, `Attribute8`, `Attribute9`, `Attribute10`, `CourseCode`, `CourseName`, `Details`, `DepartmentId`, `SemesterId`) VALUES
(1, 2, '2021-03-31 23:40:35.018062', 2, '2021-03-31 23:40:35.018062', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-405', 'Computer Graphics & Multimedia', NULL, 1, 11),
(2, 2, '2021-03-31 23:41:36.588438', 2, '2021-03-31 23:41:36.588438', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-406', 'Computer Graphics & Multimedia Lab', NULL, 1, 11),
(3, 2, '2021-03-31 23:42:04.167439', 2, '2021-03-31 23:42:04.167439', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-407', 'Artificial Intelligence & Neural Networks', NULL, 1, 11),
(4, 2, '2021-03-31 23:42:26.041166', 2, '2021-03-31 23:42:26.041166', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-408', 'Artificial Intelligence & Neural Networks Lab', NULL, 1, 11),
(5, 2, '2021-03-31 23:42:47.583767', 2, '2021-03-31 23:42:47.583767', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-411', 'Parallel and Distributed Processing', NULL, 1, 11),
(6, 2, '2021-03-31 23:43:14.135814', 2, '2021-03-31 23:43:14.135814', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-103', 'Structured Programming', NULL, 1, 2),
(7, 2, '2021-03-31 23:43:37.345830', 2, '2021-03-31 23:43:37.345830', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-104', 'Structured Programming Lab', NULL, 1, 2),
(8, 2, '2021-03-31 23:43:55.184000', 2, '2021-04-01 11:01:46.207556', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'PHY-103', 'Physice-II', NULL, 1, 2),
(9, 2, '2021-03-31 23:44:11.032644', 2, '2021-03-31 23:44:11.032644', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'PHY-104', 'Physice-II Lab', NULL, 1, 2),
(10, 2, '2021-03-31 23:44:26.609159', 2, '2021-03-31 23:44:26.609159', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'ENG-101', 'Basic English', NULL, 1, 2),
(11, 2, '2021-03-31 23:44:44.529548', 2, '2021-03-31 23:44:44.529548', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'MATH-101', 'Linear Algebra & Co-ordinate Geometry', NULL, 1, 2),
(12, 2, '2021-03-31 23:45:10.327854', 2, '2021-03-31 23:45:10.327854', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-401', 'Compiler Design', NULL, 1, 10),
(13, 2, '2021-03-31 23:45:27.599614', 2, '2021-03-31 23:45:27.599614', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-402', 'Compiler Design Lab', NULL, 1, 10),
(14, 2, '2021-03-31 23:45:45.903527', 2, '2021-03-31 23:45:45.903527', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-403', 'Peripherals & Interfacing', NULL, 1, 10),
(15, 2, '2021-03-31 23:46:04.913835', 2, '2021-03-31 23:46:04.913835', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-404', 'Peripherals & Interfacing Lab', NULL, 1, 10),
(16, 2, '2021-03-31 23:46:24.025898', 2, '2021-03-31 23:46:24.025898', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-409', 'E-Commerce & Web Engineering', NULL, 1, 10),
(17, 2, '2021-03-31 23:46:42.656601', 2, '2021-03-31 23:46:42.656601', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-410', 'E-Commerce & Web Engineering Lab', NULL, 1, 10),
(18, 2, '2021-04-02 01:20:35.650432', 2, '2021-04-02 01:20:35.650432', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-309', 'Operating Systems', NULL, 1, 9),
(19, 2, '2021-04-02 01:22:30.896390', 2, '2021-04-02 01:22:30.896390', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-310', 'Operating Systems Lab', NULL, 1, 9),
(20, 2, '2021-04-02 01:22:50.498825', 2, '2021-04-02 01:22:50.498825', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-311', 'Computer Networks', NULL, 1, 9),
(21, 2, '2021-04-02 01:23:13.276354', 2, '2021-04-02 01:23:13.276354', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-312', 'Computer Networks Lab', NULL, 1, 9),
(22, 2, '2021-04-02 01:23:31.449006', 2, '2021-04-02 01:23:31.449006', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-315', 'Theory of Computing', NULL, 1, 9),
(23, 2, '2021-04-02 01:23:56.678746', 2, '2021-04-02 01:23:56.678746', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-317', 'Software Engineering', NULL, 1, 9),
(24, 2, '2021-04-02 01:24:51.781044', 2, '2021-04-02 01:26:27.229016', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-305', 'Microprocessor, Microcontroller & Assembly Language', NULL, 1, 8),
(25, 2, '2021-04-02 01:25:08.102190', 2, '2021-04-02 01:26:37.703348', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-306', 'Microprocessor, Microcontroller & Assembly Language Lab', NULL, 1, 8),
(26, 2, '2021-04-02 01:25:24.234794', 2, '2021-04-02 01:26:54.434672', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-307', 'Database Management Systems', NULL, 1, 8),
(27, 2, '2021-04-02 01:25:41.509396', 2, '2021-04-02 01:27:04.337800', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-308', 'Database Management Systems Lab', NULL, 1, 8),
(28, 2, '2021-04-02 01:26:02.472601', 2, '2021-04-02 01:27:25.550682', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-313', 'System Analysis and Design', NULL, 1, 8),
(29, 2, '2021-04-02 01:28:39.110601', 2, '2021-04-02 01:28:39.110601', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-301', 'Algorithm Design & Analysis', NULL, 1, 7),
(30, 2, '2021-04-02 01:29:00.185154', 2, '2021-04-02 01:29:00.185154', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-302', 'Algorithm Design & Analysis Lab', NULL, 1, 7),
(31, 2, '2021-04-02 01:29:18.550662', 2, '2021-04-02 01:29:18.550662', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-303', 'Numerical Analysis', NULL, 1, 7),
(32, 2, '2021-04-02 01:29:36.969539', 2, '2021-04-02 01:29:36.969539', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-304', 'Numerical Analysis Lab', NULL, 1, 7),
(33, 2, '2021-04-02 01:29:54.586763', 2, '2021-04-02 01:29:54.586763', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'EEE-301', 'Data and Telecommunication', NULL, 1, 7),
(34, 2, '2021-04-02 01:30:10.860063', 2, '2021-04-02 01:30:10.860063', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'GED-301', 'Law & Ethics in Engineering Practice', NULL, 1, 7),
(35, 2, '2021-04-02 01:33:45.490878', 2, '2021-04-02 01:33:45.490878', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-207', 'Object Oriented programming', NULL, 1, 6),
(36, 2, '2021-04-02 01:34:04.274938', 2, '2021-04-02 01:34:04.274938', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-208', 'Object Oriented programming Lab', NULL, 1, 6),
(37, 2, '2021-04-02 01:34:22.988718', 2, '2021-04-02 01:34:22.988718', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-209', 'Computer Organization & Architecture', NULL, 1, 6),
(38, 2, '2021-04-02 01:34:39.071007', 2, '2021-04-02 01:34:39.071007', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'GED-202', 'Principles of Management', NULL, 1, 6),
(39, 2, '2021-04-02 01:34:57.487126', 2, '2021-04-02 01:34:57.487126', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'MATH-203', 'Complex Variables and Transforms(Laplace & Fourier)', NULL, 1, 6),
(40, 2, '2021-04-02 01:35:24.276363', 2, '2021-04-02 01:35:24.276363', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-203', 'Electronic Devices & Circuits', NULL, 1, 5),
(41, 2, '2021-04-02 01:35:40.694614', 2, '2021-04-02 01:35:40.694614', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-204', 'Electronic Devices & Circuits Lab', NULL, 1, 5),
(42, 2, '2021-04-02 01:35:58.625352', 2, '2021-04-02 01:35:58.625352', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-205', 'Data Structures', NULL, 1, 5),
(43, 2, '2021-04-02 01:36:16.251493', 2, '2021-04-02 01:36:16.251493', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-206', 'Data Structures Lab', NULL, 1, 5),
(44, 2, '2021-04-02 01:36:33.009862', 2, '2021-04-02 01:36:33.009862', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'MATH-202', 'Differential Equations & Vector Analysis', NULL, 1, 5),
(45, 2, '2021-04-03 23:30:26.985555', 2, '2021-04-03 23:30:26.985555', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-101', 'Introduction to Computer Systems', NULL, 1, 1),
(46, 2, '2021-04-03 23:31:18.088409', 2, '2021-04-03 23:31:18.088409', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'PHY-101', 'Physics-I', NULL, 1, 1),
(47, 2, '2021-04-03 23:31:35.231432', 2, '2021-04-03 23:31:35.231432', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'GED-101', 'Principle of Accounting', NULL, 1, 1),
(48, 2, '2021-04-03 23:31:56.777797', 2, '2021-04-03 23:31:56.777797', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'GED-102', 'Bangladesh Studies', NULL, 1, 1),
(49, 2, '2021-04-03 23:32:25.268822', 2, '2021-04-03 23:32:25.268822', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-105', 'Digital Systems', NULL, 1, 3),
(50, 2, '2021-04-03 23:32:43.532921', 2, '2021-04-03 23:32:43.532921', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-106', 'Digital Systems Lab', NULL, 1, 3),
(51, 2, '2021-04-03 23:33:00.853444', 2, '2021-04-03 23:33:00.853444', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CHEM-101', 'Chemistry', NULL, 1, 3),
(52, 2, '2021-04-03 23:33:18.019880', 2, '2021-04-03 23:33:18.019880', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CHEM-102', 'Chemistry Lab', NULL, 1, 3),
(53, 2, '2021-04-03 23:33:35.652065', 2, '2021-04-03 23:33:35.652065', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'ENG-102', 'Communicative English', NULL, 1, 3),
(54, 2, '2021-04-03 23:33:54.525976', 2, '2021-04-03 23:33:54.525976', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'MATH-102', 'Differential and Integral Calculus', NULL, 1, 3),
(55, 2, '2021-04-03 23:34:12.717434', 2, '2021-04-03 23:34:12.717434', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'CSE-201', 'Discrete Mathematics', NULL, 1, 4),
(56, 2, '2021-04-03 23:35:13.773728', 2, '2021-04-03 23:35:13.773728', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'EEE-201', 'Electrical Circuits', NULL, 1, 4),
(57, 2, '2021-04-03 23:35:32.261999', 2, '2021-04-03 23:35:32.261999', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'EEE-202', 'Electrical Circuits Lab', NULL, 1, 4),
(58, 2, '2021-04-03 23:35:49.773580', 2, '2021-04-03 23:35:49.773580', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'GED-201', 'Principles of Economics', NULL, 1, 4),
(59, 2, '2021-04-03 23:36:07.604558', 2, '2021-04-03 23:36:07.604558', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Math-201', 'Statistical Methods & Probability', NULL, 1, 4);

-- --------------------------------------------------------

--
-- Table structure for table `departments`
--

CREATE TABLE `departments` (
  `DepartmentId` int(11) NOT NULL,
  `CreatedBy` int(11) DEFAULT NULL,
  `CreatedDate` datetime(6) DEFAULT NULL,
  `UpdatedBy` int(11) DEFAULT NULL,
  `UpdatedDate` datetime(6) DEFAULT NULL,
  `EffectiveFrom` datetime(6) DEFAULT NULL,
  `EffectiveTo` datetime(6) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT NULL,
  `Status` int(11) DEFAULT NULL,
  `Attribute1` longtext DEFAULT NULL,
  `Attribute2` longtext DEFAULT NULL,
  `Attribute3` longtext DEFAULT NULL,
  `Attribute4` longtext DEFAULT NULL,
  `Attribute5` longtext DEFAULT NULL,
  `Attribute6` longtext DEFAULT NULL,
  `Attribute7` longtext DEFAULT NULL,
  `Attribute8` longtext DEFAULT NULL,
  `Attribute9` longtext DEFAULT NULL,
  `Attribute10` longtext DEFAULT NULL,
  `DepartmentName` varchar(50) DEFAULT NULL,
  `Details` varchar(500) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `departments`
--

INSERT INTO `departments` (`DepartmentId`, `CreatedBy`, `CreatedDate`, `UpdatedBy`, `UpdatedDate`, `EffectiveFrom`, `EffectiveTo`, `IsActive`, `Status`, `Attribute1`, `Attribute2`, `Attribute3`, `Attribute4`, `Attribute5`, `Attribute6`, `Attribute7`, `Attribute8`, `Attribute9`, `Attribute10`, `DepartmentName`, `Details`) VALUES
(1, 2, '2021-03-31 23:06:36.009599', 2, '2021-03-31 23:24:50.513386', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Department of CSE', 'Computer Science & Engineering');

-- --------------------------------------------------------

--
-- Table structure for table `employees`
--

CREATE TABLE `employees` (
  `EmployeeId` int(11) NOT NULL,
  `CreatedBy` int(11) DEFAULT NULL,
  `CreatedDate` datetime(6) DEFAULT NULL,
  `UpdatedBy` int(11) DEFAULT NULL,
  `UpdatedDate` datetime(6) DEFAULT NULL,
  `EffectiveFrom` datetime(6) DEFAULT NULL,
  `EffectiveTo` datetime(6) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT NULL,
  `Status` int(11) DEFAULT NULL,
  `Attribute1` longtext DEFAULT NULL,
  `Attribute2` longtext DEFAULT NULL,
  `Attribute3` longtext DEFAULT NULL,
  `Attribute4` longtext DEFAULT NULL,
  `Attribute5` longtext DEFAULT NULL,
  `Attribute6` longtext DEFAULT NULL,
  `Attribute7` longtext DEFAULT NULL,
  `Attribute8` longtext DEFAULT NULL,
  `Attribute9` longtext DEFAULT NULL,
  `Attribute10` longtext DEFAULT NULL,
  `EmailAddress` varchar(50) DEFAULT NULL,
  `RegistrationNumber` varchar(50) DEFAULT NULL,
  `FirstName` varchar(50) DEFAULT NULL,
  `LastName` varchar(50) DEFAULT NULL,
  `ContactNumber` varchar(50) DEFAULT NULL,
  `FatherName` varchar(50) DEFAULT NULL,
  `MotherName` varchar(50) DEFAULT NULL,
  `GurdianNumber` varchar(50) DEFAULT NULL,
  `Gender` varchar(10) DEFAULT NULL,
  `Religion` varchar(20) DEFAULT NULL,
  `NationalID` varchar(20) DEFAULT NULL,
  `PresentAddress` varchar(100) DEFAULT NULL,
  `PermanentAddress` varchar(100) DEFAULT NULL,
  `ProfilePicture` varchar(500) DEFAULT NULL,
  `JoiningDate` datetime(6) DEFAULT NULL,
  `MonthlySalary` decimal(65,30) DEFAULT NULL,
  `Type` int(11) NOT NULL,
  `DepartmentId` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `mailconfigurations`
--

CREATE TABLE `mailconfigurations` (
  `MailConfigurationId` int(11) NOT NULL,
  `CreatedBy` int(11) DEFAULT NULL,
  `CreatedDate` datetime(6) DEFAULT NULL,
  `UpdatedBy` int(11) DEFAULT NULL,
  `UpdatedDate` datetime(6) DEFAULT NULL,
  `EffectiveFrom` datetime(6) DEFAULT NULL,
  `EffectiveTo` datetime(6) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT NULL,
  `Status` int(11) DEFAULT NULL,
  `Attribute1` longtext DEFAULT NULL,
  `Attribute2` longtext DEFAULT NULL,
  `Attribute3` longtext DEFAULT NULL,
  `Attribute4` longtext DEFAULT NULL,
  `Attribute5` longtext DEFAULT NULL,
  `Attribute6` longtext DEFAULT NULL,
  `Attribute7` longtext DEFAULT NULL,
  `Attribute8` longtext DEFAULT NULL,
  `Attribute9` longtext DEFAULT NULL,
  `Attribute10` longtext DEFAULT NULL,
  `CompanyId` int(11) NOT NULL,
  `ConfigType` int(11) NOT NULL,
  `FromName` longtext DEFAULT NULL,
  `FromEmail` longtext DEFAULT NULL,
  `Host` longtext DEFAULT NULL,
  `Port` int(11) DEFAULT NULL,
  `Username` longtext DEFAULT NULL,
  `Password` longtext DEFAULT NULL,
  `EncryptionType` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `markingbadges`
--

CREATE TABLE `markingbadges` (
  `MarkingBadgeId` int(11) NOT NULL,
  `CreatedBy` int(11) DEFAULT NULL,
  `CreatedDate` datetime(6) DEFAULT NULL,
  `UpdatedBy` int(11) DEFAULT NULL,
  `UpdatedDate` datetime(6) DEFAULT NULL,
  `EffectiveFrom` datetime(6) DEFAULT NULL,
  `EffectiveTo` datetime(6) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT NULL,
  `Status` int(11) DEFAULT NULL,
  `Attribute1` longtext DEFAULT NULL,
  `Attribute2` longtext DEFAULT NULL,
  `Attribute3` longtext DEFAULT NULL,
  `Attribute4` longtext DEFAULT NULL,
  `Attribute5` longtext DEFAULT NULL,
  `Attribute6` longtext DEFAULT NULL,
  `Attribute7` longtext DEFAULT NULL,
  `Attribute8` longtext DEFAULT NULL,
  `Attribute9` longtext DEFAULT NULL,
  `Attribute10` longtext DEFAULT NULL,
  `MarkingBadgeName` varchar(100) DEFAULT NULL,
  `BadgeColorCode` varchar(10) DEFAULT NULL,
  `Details` varchar(500) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `markingbadges`
--

INSERT INTO `markingbadges` (`MarkingBadgeId`, `CreatedBy`, `CreatedDate`, `UpdatedBy`, `UpdatedDate`, `EffectiveFrom`, `EffectiveTo`, `IsActive`, `Status`, `Attribute1`, `Attribute2`, `Attribute3`, `Attribute4`, `Attribute5`, `Attribute6`, `Attribute7`, `Attribute8`, `Attribute9`, `Attribute10`, `MarkingBadgeName`, `BadgeColorCode`, `Details`) VALUES
(1, 2, '2021-04-04 02:07:11.733935', 2, '2021-04-04 02:12:17.219742', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Poor', '#e74a3b', 'This badge represent very poor quality'),
(2, 2, '2021-04-04 02:08:11.223360', 2, '2021-04-04 02:08:11.223360', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Average', '#f6c23e', 'This is the average badge'),
(3, 2, '2021-04-04 02:09:28.478293', 2, '2021-04-04 02:09:28.478293', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Good', '#36b9cc', 'This badge represent good quality'),
(4, 2, '2021-04-04 02:10:05.047120', 2, '2021-04-04 02:10:05.047120', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Very Good', '#4e73df', 'This badge represent very good quality'),
(5, 2, '2021-04-04 02:11:35.835786', 2, '2021-04-04 02:11:35.835786', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Excellent', '#1cc88a', 'Last and very top level badge');

-- --------------------------------------------------------

--
-- Table structure for table `markingcriterias`
--

CREATE TABLE `markingcriterias` (
  `MarkingCriteriaId` int(11) NOT NULL,
  `CreatedBy` int(11) DEFAULT NULL,
  `CreatedDate` datetime(6) DEFAULT NULL,
  `UpdatedBy` int(11) DEFAULT NULL,
  `UpdatedDate` datetime(6) DEFAULT NULL,
  `EffectiveFrom` datetime(6) DEFAULT NULL,
  `EffectiveTo` datetime(6) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT NULL,
  `Status` int(11) DEFAULT NULL,
  `Attribute1` longtext DEFAULT NULL,
  `Attribute2` longtext DEFAULT NULL,
  `Attribute3` longtext DEFAULT NULL,
  `Attribute4` longtext DEFAULT NULL,
  `Attribute5` longtext DEFAULT NULL,
  `Attribute6` longtext DEFAULT NULL,
  `Attribute7` longtext DEFAULT NULL,
  `Attribute8` longtext DEFAULT NULL,
  `Attribute9` longtext DEFAULT NULL,
  `Attribute10` longtext DEFAULT NULL,
  `MarkingCriteriaName` varchar(100) DEFAULT NULL,
  `Details` varchar(500) DEFAULT NULL,
  `MinimumValue` decimal(65,30) NOT NULL,
  `MaximumValue` decimal(65,30) NOT NULL,
  `PassingValue` decimal(65,30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `markingcriterias`
--

INSERT INTO `markingcriterias` (`MarkingCriteriaId`, `CreatedBy`, `CreatedDate`, `UpdatedBy`, `UpdatedDate`, `EffectiveFrom`, `EffectiveTo`, `IsActive`, `Status`, `Attribute1`, `Attribute2`, `Attribute3`, `Attribute4`, `Attribute5`, `Attribute6`, `Attribute7`, `Attribute8`, `Attribute9`, `Attribute10`, `MarkingCriteriaName`, `Details`, `MinimumValue`, `MaximumValue`, `PassingValue`) VALUES
(1, 2, '2021-03-31 23:08:12.216488', 2, '2021-03-31 23:47:29.610154', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Class Test Marks', NULL, '0.000000000000000000000000000000', '10.000000000000000000000000000000', '4.000000000000000000000000000000'),
(2, 2, '2021-03-31 23:48:10.409274', 2, '2021-03-31 23:48:10.409274', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Mid Term Marks', NULL, '0.000000000000000000000000000000', '20.000000000000000000000000000000', '8.000000000000000000000000000000'),
(3, 2, '2021-03-31 23:48:28.944430', 2, '2021-03-31 23:48:28.944430', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Attendance Marks', NULL, '0.000000000000000000000000000000', '10.000000000000000000000000000000', '4.000000000000000000000000000000'),
(4, 2, '2021-03-31 23:48:48.848294', 2, '2021-03-31 23:48:48.848294', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Presentation Marks', NULL, '0.000000000000000000000000000000', '10.000000000000000000000000000000', '4.000000000000000000000000000000'),
(5, 2, '2021-03-31 23:49:10.746183', 2, '2021-03-31 23:49:10.746183', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Final  Marks', NULL, '0.000000000000000000000000000000', '50.000000000000000000000000000000', '20.000000000000000000000000000000');

-- --------------------------------------------------------

--
-- Table structure for table `markingcriteriasbadges`
--

CREATE TABLE `markingcriteriasbadges` (
  `CriteriasBadgeId` int(11) NOT NULL,
  `CreatedBy` int(11) DEFAULT NULL,
  `CreatedDate` datetime(6) DEFAULT NULL,
  `UpdatedBy` int(11) DEFAULT NULL,
  `UpdatedDate` datetime(6) DEFAULT NULL,
  `EffectiveFrom` datetime(6) DEFAULT NULL,
  `EffectiveTo` datetime(6) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT NULL,
  `Status` int(11) DEFAULT NULL,
  `Attribute1` longtext DEFAULT NULL,
  `Attribute2` longtext DEFAULT NULL,
  `Attribute3` longtext DEFAULT NULL,
  `Attribute4` longtext DEFAULT NULL,
  `Attribute5` longtext DEFAULT NULL,
  `Attribute6` longtext DEFAULT NULL,
  `Attribute7` longtext DEFAULT NULL,
  `Attribute8` longtext DEFAULT NULL,
  `Attribute9` longtext DEFAULT NULL,
  `Attribute10` longtext DEFAULT NULL,
  `MarkingCriteriaId` int(11) NOT NULL,
  `MarkingBadgeId` int(11) NOT NULL,
  `MinimumValue` decimal(65,30) NOT NULL,
  `MaximumValue` decimal(65,30) NOT NULL,
  `Details` varchar(500) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `markingcriteriasbadges`
--

INSERT INTO `markingcriteriasbadges` (`CriteriasBadgeId`, `CreatedBy`, `CreatedDate`, `UpdatedBy`, `UpdatedDate`, `EffectiveFrom`, `EffectiveTo`, `IsActive`, `Status`, `Attribute1`, `Attribute2`, `Attribute3`, `Attribute4`, `Attribute5`, `Attribute6`, `Attribute7`, `Attribute8`, `Attribute9`, `Attribute10`, `MarkingCriteriaId`, `MarkingBadgeId`, `MinimumValue`, `MaximumValue`, `Details`) VALUES
(1, 2, '2021-04-04 02:15:51.769115', 2, '2021-04-04 02:15:51.769115', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, '0.000000000000000000000000000000', '2.990000000000000000000000000000', 'poor badge'),
(2, 2, '2021-04-04 02:17:05.340098', 2, '2021-04-04 02:17:05.340098', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2, 1, '0.000000000000000000000000000000', '7.990000000000000000000000000000', 'poor badge'),
(3, 2, '2021-04-04 02:17:39.322567', 2, '2021-04-04 02:17:39.322567', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 3, 1, '0.000000000000000000000000000000', '2.990000000000000000000000000000', 'poor badge'),
(4, 2, '2021-04-04 02:17:54.034172', 2, '2021-04-04 02:17:54.034172', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 4, 1, '0.000000000000000000000000000000', '2.990000000000000000000000000000', 'poor badge'),
(5, 2, '2021-04-04 02:18:22.708706', 2, '2021-04-04 02:18:33.318963', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 5, 1, '0.000000000000000000000000000000', '19.990000000000000000000000000000', 'poor badge'),
(6, 2, '2021-04-04 02:19:38.046273', 2, '2021-04-04 02:19:38.046273', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 2, '3.000000000000000000000000000000', '4.990000000000000000000000000000', 'average badge'),
(7, 2, '2021-04-04 02:20:06.669375', 2, '2021-04-04 02:20:06.669375', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2, 2, '8.000000000000000000000000000000', '11.990000000000000000000000000000', 'average badge'),
(8, 2, '2021-04-04 02:20:28.172959', 2, '2021-04-04 02:20:28.172959', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 3, 2, '3.000000000000000000000000000000', '4.990000000000000000000000000000', 'average badge'),
(9, 2, '2021-04-04 02:21:37.873948', 2, '2021-04-04 02:21:37.873948', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 4, 2, '3.000000000000000000000000000000', '4.990000000000000000000000000000', 'average badge'),
(10, 2, '2021-04-04 02:22:14.830793', 2, '2021-04-04 02:22:14.830793', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 5, 2, '20.000000000000000000000000000000', '27.990000000000000000000000000000', 'average badge'),
(11, 2, '2021-04-04 02:23:01.637971', 2, '2021-04-04 02:23:01.637971', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 3, '5.000000000000000000000000000000', '6.990000000000000000000000000000', 'good badge'),
(12, 2, '2021-04-04 02:23:24.614298', 2, '2021-04-04 02:23:24.614298', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2, 3, '12.000000000000000000000000000000', '14.990000000000000000000000000000', 'good badge'),
(13, 2, '2021-04-04 02:23:47.211420', 2, '2021-04-04 02:23:47.211420', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 3, 3, '5.000000000000000000000000000000', '6.990000000000000000000000000000', 'good badge'),
(14, 2, '2021-04-04 02:24:19.126066', 2, '2021-04-04 02:24:19.126066', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 4, 3, '5.000000000000000000000000000000', '6.990000000000000000000000000000', 'good badge'),
(15, 2, '2021-04-04 02:24:44.114843', 2, '2021-04-04 02:24:44.114843', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 5, 3, '28.000000000000000000000000000000', '37.990000000000000000000000000000', 'good badge'),
(16, 2, '2021-04-04 02:25:41.038081', 2, '2021-04-04 02:25:41.038081', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 4, '7.000000000000000000000000000000', '7.990000000000000000000000000000', 'very good badge'),
(17, 2, '2021-04-04 02:26:02.828440', 2, '2021-04-04 02:26:02.828440', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2, 4, '15.000000000000000000000000000000', '17.990000000000000000000000000000', 'very good badge'),
(18, 2, '2021-04-04 02:26:29.442923', 2, '2021-04-04 02:26:29.442923', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 3, 4, '7.000000000000000000000000000000', '7.990000000000000000000000000000', 'very good badge'),
(19, 2, '2021-04-04 02:26:58.298205', 2, '2021-04-04 02:26:58.298205', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 4, 4, '7.000000000000000000000000000000', '7.990000000000000000000000000000', 'very good badge'),
(20, 2, '2021-04-04 02:28:01.765454', 2, '2021-04-04 02:28:01.765454', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 5, 4, '38.000000000000000000000000000000', '43.990000000000000000000000000000', 'very good badge'),
(21, 2, '2021-04-04 02:28:59.968825', 2, '2021-04-04 02:28:59.968825', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 5, '8.000000000000000000000000000000', '10.000000000000000000000000000000', 'this is the excellent badge'),
(22, 2, '2021-04-04 02:29:20.146204', 2, '2021-04-04 02:29:20.146204', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2, 5, '18.000000000000000000000000000000', '20.000000000000000000000000000000', 'this is the excellent badge'),
(23, 2, '2021-04-04 02:29:34.787078', 2, '2021-04-04 02:29:34.787078', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 3, 5, '8.000000000000000000000000000000', '10.000000000000000000000000000000', 'this is the excellent badge'),
(24, 2, '2021-04-04 02:29:52.919190', 2, '2021-04-04 02:29:52.919190', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 4, 5, '8.000000000000000000000000000000', '10.000000000000000000000000000000', 'this is the excellent badge'),
(25, 2, '2021-04-04 02:30:36.608149', 2, '2021-04-04 02:30:36.608149', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 5, 5, '44.000000000000000000000000000000', '50.000000000000000000000000000000', 'this is the excellent badge');

-- --------------------------------------------------------

--
-- Table structure for table `semesters`
--

CREATE TABLE `semesters` (
  `SemesterId` int(11) NOT NULL,
  `CreatedBy` int(11) DEFAULT NULL,
  `CreatedDate` datetime(6) DEFAULT NULL,
  `UpdatedBy` int(11) DEFAULT NULL,
  `UpdatedDate` datetime(6) DEFAULT NULL,
  `EffectiveFrom` datetime(6) DEFAULT NULL,
  `EffectiveTo` datetime(6) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT NULL,
  `Status` int(11) DEFAULT NULL,
  `Attribute1` longtext DEFAULT NULL,
  `Attribute2` longtext DEFAULT NULL,
  `Attribute3` longtext DEFAULT NULL,
  `Attribute4` longtext DEFAULT NULL,
  `Attribute5` longtext DEFAULT NULL,
  `Attribute6` longtext DEFAULT NULL,
  `Attribute7` longtext DEFAULT NULL,
  `Attribute8` longtext DEFAULT NULL,
  `Attribute9` longtext DEFAULT NULL,
  `Attribute10` longtext DEFAULT NULL,
  `SemesterName` varchar(50) DEFAULT NULL,
  `Details` varchar(500) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `semesters`
--

INSERT INTO `semesters` (`SemesterId`, `CreatedBy`, `CreatedDate`, `UpdatedBy`, `UpdatedDate`, `EffectiveFrom`, `EffectiveTo`, `IsActive`, `Status`, `Attribute1`, `Attribute2`, `Attribute3`, `Attribute4`, `Attribute5`, `Attribute6`, `Attribute7`, `Attribute8`, `Attribute9`, `Attribute10`, `SemesterName`, `Details`) VALUES
(1, 2, '2021-03-31 23:07:47.275688', 2, '2021-04-04 01:43:58.456470', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '1st Semester', 'Spring (January - April)'),
(2, 2, '2021-03-31 23:28:41.518977', 2, '2021-04-04 01:44:50.439818', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '2nd Semester', 'Summer (May - August)'),
(3, 2, '2021-03-31 23:31:33.926309', 2, '2021-04-04 01:45:34.950028', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '3rd Semester', 'Fall (October - December)'),
(4, 2, '2021-03-31 23:33:01.487056', 2, '2021-04-04 01:46:01.769041', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '4th Semester', 'Spring (January - April)'),
(5, 2, '2021-03-31 23:34:05.832205', 2, '2021-04-04 01:46:15.726318', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '5th Semester', 'Summer (May - August)'),
(6, 2, '2021-03-31 23:34:46.609234', 2, '2021-04-04 01:46:31.606908', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '6th Semester', 'Fall (October - December)'),
(7, 2, '2021-03-31 23:35:30.570244', 2, '2021-04-04 01:46:51.136467', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '7th Semester', 'Spring (January - April)'),
(8, 2, '2021-03-31 23:35:57.264289', 2, '2021-04-04 01:47:05.363566', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '8th Semester', 'Summer (May - August)'),
(9, 2, '2021-03-31 23:36:22.074349', 2, '2021-04-04 01:47:17.264902', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '9th Semester', 'Fall (October - December)'),
(10, 2, '2021-03-31 23:36:46.425682', 2, '2021-04-04 01:47:38.439173', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '10th Semester', 'Spring (January - April)'),
(11, 2, '2021-03-31 23:37:20.599756', 2, '2021-04-04 01:47:50.765206', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '11th Semester', 'Summer (May - August)'),
(12, 2, '2021-03-31 23:37:50.631535', 2, '2021-04-04 01:48:14.951084', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '12th Semester', 'Fall (October - December)');

-- --------------------------------------------------------

--
-- Table structure for table `studentbatches`
--

CREATE TABLE `studentbatches` (
  `BatchId` int(11) NOT NULL,
  `CreatedBy` int(11) DEFAULT NULL,
  `CreatedDate` datetime(6) DEFAULT NULL,
  `UpdatedBy` int(11) DEFAULT NULL,
  `UpdatedDate` datetime(6) DEFAULT NULL,
  `EffectiveFrom` datetime(6) DEFAULT NULL,
  `EffectiveTo` datetime(6) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT NULL,
  `Status` int(11) DEFAULT NULL,
  `Attribute1` longtext DEFAULT NULL,
  `Attribute2` longtext DEFAULT NULL,
  `Attribute3` longtext DEFAULT NULL,
  `Attribute4` longtext DEFAULT NULL,
  `Attribute5` longtext DEFAULT NULL,
  `Attribute6` longtext DEFAULT NULL,
  `Attribute7` longtext DEFAULT NULL,
  `Attribute8` longtext DEFAULT NULL,
  `Attribute9` longtext DEFAULT NULL,
  `Attribute10` longtext DEFAULT NULL,
  `BatchName` varchar(50) DEFAULT NULL,
  `Details` varchar(500) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `studentbatches`
--

INSERT INTO `studentbatches` (`BatchId`, `CreatedBy`, `CreatedDate`, `UpdatedBy`, `UpdatedDate`, `EffectiveFrom`, `EffectiveTo`, `IsActive`, `Status`, `Attribute1`, `Attribute2`, `Attribute3`, `Attribute4`, `Attribute5`, `Attribute6`, `Attribute7`, `Attribute8`, `Attribute9`, `Attribute10`, `BatchName`, `Details`) VALUES
(1, 2, '2021-03-31 23:07:04.343612', 2, '2021-03-31 23:25:12.457086', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'E-68', 'Evening');

-- --------------------------------------------------------

--
-- Table structure for table `studentcoursemarks`
--

CREATE TABLE `studentcoursemarks` (
  `CourseMarksId` int(11) NOT NULL,
  `CreatedBy` int(11) DEFAULT NULL,
  `CreatedDate` datetime(6) DEFAULT NULL,
  `UpdatedBy` int(11) DEFAULT NULL,
  `UpdatedDate` datetime(6) DEFAULT NULL,
  `EffectiveFrom` datetime(6) DEFAULT NULL,
  `EffectiveTo` datetime(6) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT NULL,
  `Status` int(11) DEFAULT NULL,
  `Attribute1` longtext DEFAULT NULL,
  `Attribute2` longtext DEFAULT NULL,
  `Attribute3` longtext DEFAULT NULL,
  `Attribute4` longtext DEFAULT NULL,
  `Attribute5` longtext DEFAULT NULL,
  `Attribute6` longtext DEFAULT NULL,
  `Attribute7` longtext DEFAULT NULL,
  `Attribute8` longtext DEFAULT NULL,
  `Attribute9` longtext DEFAULT NULL,
  `Attribute10` longtext DEFAULT NULL,
  `StudentId` int(11) NOT NULL,
  `CourseId` int(11) NOT NULL,
  `MarkingCriteriaId` int(11) NOT NULL,
  `CourseCriteriaMarks` decimal(65,30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `studentcoursemarks`
--

INSERT INTO `studentcoursemarks` (`CourseMarksId`, `CreatedBy`, `CreatedDate`, `UpdatedBy`, `UpdatedDate`, `EffectiveFrom`, `EffectiveTo`, `IsActive`, `Status`, `Attribute1`, `Attribute2`, `Attribute3`, `Attribute4`, `Attribute5`, `Attribute6`, `Attribute7`, `Attribute8`, `Attribute9`, `Attribute10`, `StudentId`, `CourseId`, `MarkingCriteriaId`, `CourseCriteriaMarks`) VALUES
(1, 2, '2021-04-04 02:00:18.377057', 2, '2021-04-04 02:00:18.377057', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 48, 45, 1, '2.000000000000000000000000000000'),
(2, 2, '2021-04-04 02:00:18.377057', 2, '2021-04-04 02:00:18.377057', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 48, 48, 3, '6.000000000000000000000000000000'),
(3, 2, '2021-04-04 02:00:18.377057', 2, '2021-04-04 02:00:18.377057', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 48, 48, 2, '20.000000000000000000000000000000'),
(4, 2, '2021-04-04 02:00:18.377057', 2, '2021-04-04 02:00:18.377057', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 48, 48, 1, '7.000000000000000000000000000000'),
(5, 2, '2021-04-04 02:00:18.377057', 2, '2021-04-04 02:00:18.377057', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 48, 47, 5, '23.000000000000000000000000000000'),
(6, 2, '2021-04-04 02:00:18.377057', 2, '2021-04-04 02:00:18.377057', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 48, 47, 4, '4.000000000000000000000000000000'),
(7, 2, '2021-04-04 02:00:18.377057', 2, '2021-04-04 02:00:18.377057', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 48, 47, 3, '8.000000000000000000000000000000'),
(8, 2, '2021-04-04 02:00:18.377057', 2, '2021-04-04 02:00:18.377057', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 48, 47, 2, '16.000000000000000000000000000000'),
(9, 2, '2021-04-04 02:00:18.377057', 2, '2021-04-04 02:00:18.377057', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 48, 47, 1, '4.000000000000000000000000000000'),
(10, 2, '2021-04-04 02:00:18.377057', 2, '2021-04-04 02:00:18.377057', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 48, 46, 5, '18.000000000000000000000000000000'),
(11, 2, '2021-04-04 02:00:18.377057', 2, '2021-04-04 02:00:18.377057', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 48, 46, 4, '9.000000000000000000000000000000'),
(12, 2, '2021-04-04 02:00:18.377057', 2, '2021-04-04 02:00:18.377057', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 48, 46, 3, '9.000000000000000000000000000000'),
(13, 2, '2021-04-04 02:00:18.377057', 2, '2021-04-04 02:00:18.377057', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 48, 46, 2, '12.000000000000000000000000000000'),
(14, 2, '2021-04-04 02:00:18.377057', 2, '2021-04-04 02:00:18.377057', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 48, 46, 1, '3.000000000000000000000000000000'),
(15, 2, '2021-04-04 02:00:18.377057', 2, '2021-04-04 02:00:18.377057', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 48, 45, 5, '10.000000000000000000000000000000'),
(16, 2, '2021-04-04 02:00:18.377057', 2, '2021-04-04 02:00:18.377057', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 48, 45, 4, '8.000000000000000000000000000000'),
(17, 2, '2021-04-04 02:00:18.377057', 2, '2021-04-04 02:00:18.377057', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 48, 45, 3, '6.000000000000000000000000000000'),
(18, 2, '2021-04-04 02:00:18.377057', 2, '2021-04-04 02:00:18.377057', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 48, 45, 2, '8.000000000000000000000000000000'),
(19, 2, '2021-04-04 02:00:18.377057', 2, '2021-04-04 02:00:18.377057', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 48, 48, 4, '5.000000000000000000000000000000'),
(20, 2, '2021-04-04 02:00:18.377057', 2, '2021-04-04 02:00:18.377057', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 48, 48, 5, '30.000000000000000000000000000000');

-- --------------------------------------------------------

--
-- Table structure for table `students`
--

CREATE TABLE `students` (
  `StudentId` int(11) NOT NULL,
  `CreatedBy` int(11) DEFAULT NULL,
  `CreatedDate` datetime(6) DEFAULT NULL,
  `UpdatedBy` int(11) DEFAULT NULL,
  `UpdatedDate` datetime(6) DEFAULT NULL,
  `EffectiveFrom` datetime(6) DEFAULT NULL,
  `EffectiveTo` datetime(6) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT NULL,
  `Status` int(11) DEFAULT NULL,
  `Attribute1` longtext DEFAULT NULL,
  `Attribute2` longtext DEFAULT NULL,
  `Attribute3` longtext DEFAULT NULL,
  `Attribute4` longtext DEFAULT NULL,
  `Attribute5` longtext DEFAULT NULL,
  `Attribute6` longtext DEFAULT NULL,
  `Attribute7` longtext DEFAULT NULL,
  `Attribute8` longtext DEFAULT NULL,
  `Attribute9` longtext DEFAULT NULL,
  `Attribute10` longtext DEFAULT NULL,
  `RegistrationNumber` varchar(50) DEFAULT NULL,
  `RegistrationDate` datetime(6) DEFAULT NULL,
  `RollNumber` int(11) NOT NULL,
  `EmailAddress` varchar(50) DEFAULT NULL,
  `FirstName` varchar(50) DEFAULT NULL,
  `LastName` varchar(50) DEFAULT NULL,
  `ContactNumber` varchar(50) DEFAULT NULL,
  `FatherName` varchar(50) DEFAULT NULL,
  `MotherName` varchar(50) DEFAULT NULL,
  `GurdianNumber` varchar(50) DEFAULT NULL,
  `Gender` varchar(10) DEFAULT NULL,
  `Religion` varchar(20) DEFAULT NULL,
  `NationalID` varchar(20) DEFAULT NULL,
  `PresentAddress` varchar(100) DEFAULT NULL,
  `PermanentAddress` varchar(100) DEFAULT NULL,
  `ProfilePicture` varchar(500) DEFAULT NULL,
  `AppliedTermsAndCondition` tinyint(1) NOT NULL,
  `Type` int(11) NOT NULL,
  `BatchId` int(11) NOT NULL,
  `BatchInfoBatchId` int(11) DEFAULT NULL,
  `DepartmentId` int(11) NOT NULL,
  `CourseId` int(11) DEFAULT NULL,
  `SemesterId` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `students`
--

INSERT INTO `students` (`StudentId`, `CreatedBy`, `CreatedDate`, `UpdatedBy`, `UpdatedDate`, `EffectiveFrom`, `EffectiveTo`, `IsActive`, `Status`, `Attribute1`, `Attribute2`, `Attribute3`, `Attribute4`, `Attribute5`, `Attribute6`, `Attribute7`, `Attribute8`, `Attribute9`, `Attribute10`, `RegistrationNumber`, `RegistrationDate`, `RollNumber`, `EmailAddress`, `FirstName`, `LastName`, `ContactNumber`, `FatherName`, `MotherName`, `GurdianNumber`, `Gender`, `Religion`, `NationalID`, `PresentAddress`, `PermanentAddress`, `ProfilePicture`, `AppliedTermsAndCondition`, `Type`, `BatchId`, `BatchInfoBatchId`, `DepartmentId`, `CourseId`, `SemesterId`) VALUES
(1, 2, '2021-03-31 23:11:19.726884', 2, '2021-04-01 00:09:17.952427', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '01', '2021-03-10 00:00:00.000000', 1, 'mariful@gmailcom', 'MD. KAWSAR ', 'MIA', '01734280272', 'Md. kader Mia', 'Mst. Sultana Raziya', '01700000000', 'Male', 'Islam', '1195659654', 'Dkaka', 'Dhaka', '/uploads/Student/2021-03-31/2f15041c-ee00-4c92-bb6e-07a1754f408f-diu-logo.png', 1, 1, 1, NULL, 1, NULL, NULL),
(2, 2, '2021-04-01 00:14:30.624469', 2, '2021-04-01 00:14:46.015457', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '02', '2021-04-07 00:00:00.000000', 2, 'arif@gmail.com', 'BABY ', 'AKHTER', ' 01734280272 ', '  hghg  ', 'hga', '0938383', 'Female', 'Islam', '123456789', 'Dhaka', 'Dhaka', NULL, 1, 2, 1, NULL, 1, NULL, NULL),
(3, 2, '2021-04-01 00:16:31.303671', 2, '2021-04-01 00:16:31.303671', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '03', '2021-03-31 00:00:00.000000', 3, 'arif@gmail.com', 'MST. BITHI ', 'AKTER', ' 017 ', 'hghhg', 'jggg', '433', 'Female', 'Islam', '1234', 'f', 'f', NULL, 1, 2, 1, NULL, 1, NULL, NULL),
(4, 2, '2021-04-01 00:17:37.667413', 2, '2021-04-01 00:17:37.667413', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '04', '2021-03-30 00:00:00.000000', 4, 'gg@gmail.com', 'MST. MOUSHUMI ', 'KHATUN', '55', 'rr', 'rr', '55', 'Female', 'Islam', '44', 'gg', 'gg', NULL, 1, 2, 1, NULL, 1, NULL, NULL),
(5, 2, '2021-04-01 00:18:31.778665', 2, '2021-04-01 00:18:31.778665', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '05', '2021-03-30 00:00:00.000000', 5, 'd@gmail.com', 'MD. TARIKUL ', 'ISLAM', '55', 'e', 'e', '4', 'Male', 'Islam', '44', '4', '4', NULL, 1, 2, 1, NULL, 1, NULL, NULL),
(6, 2, '2021-04-01 00:22:59.924723', 2, '2021-04-01 00:22:59.924723', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '06', '2021-03-30 00:00:00.000000', 6, 'dsfs@gmail.com', 'MD. ', 'AL-MAMON', '4685435', 'fdefhtg', 'trhytutui', '5688', 'Male', 'Islam', '365657', 'fghgj', 'dhhhh', NULL, 1, 2, 1, NULL, 1, NULL, NULL),
(7, 2, '2021-04-01 00:26:26.081674', 2, '2021-04-01 00:26:26.081674', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '07', '2021-04-02 00:00:00.000000', 7, 'dsggs@gmail.com', 'SHAMIM ', 'HASAN', '43554', 'asgdhj', 'fhdhhf', '542532', 'Male', 'Islam', '53464646', 'sghgh', 'sgfg', NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(8, 2, '2021-04-01 00:27:38.924977', 2, '2021-04-01 00:27:38.924977', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '08', '2021-03-31 00:00:00.000000', 8, 'dshg@gmail.com', 'MD AZIZUL ', 'HOQUE ', '2551', 'resf', 'dsfsdg', '41442', 'Male', 'Islam', '5335', 'fdsg', 'ffwefew', NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(9, 2, '2021-04-01 00:29:52.871177', 2, '2021-04-01 00:29:52.871177', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '09', '2021-04-07 00:00:00.000000', 9, 'arif@gmail.com', 'MD. MAZEDUR ', 'RAHMAN', '34643', 'fgsgf', 'gsfgfg', '5525', 'Male', 'Islam', '4646', 'fgf', 'fgfg', NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(10, 2, '2021-04-01 00:31:32.993041', 2, '2021-04-01 00:31:32.993041', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '10', '2021-03-31 00:00:00.000000', 10, 'arif@gmail.com', 'MD. MOHSIN ', 'REJA', '526645', 'dsgff', 'dfhhh', '567', 'Male', 'Islam', '655477', 'dfh', 'hgf', NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(11, 2, '2021-04-01 00:32:18.435404', 2, '2021-04-01 00:32:18.435404', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '11', '2021-03-30 00:00:00.000000', 11, 'arif@gmail.com', 'MD. ABU SAZZAD ', 'HOSSAIN', '24513', 'fsgdgds', 'sdggdsg', '3214', 'Male', 'Islam', '53535', 'dfsgs', 'gsghg', NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(12, 2, '2021-04-01 00:33:01.125084', 2, '2021-04-01 00:33:01.125084', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '12', '2021-03-31 00:00:00.000000', 12, 'arif@gmail.com', 'MD. RESINUL ', 'HAQUE', '4215151', 'sgdgd', 'gdgdgs', '142124', 'Male', 'Islam', '12512152', 'dfsdgs', 'gdg', NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(13, 2, '2021-04-01 00:33:52.613866', 2, '2021-04-01 00:33:52.613866', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '13', '2021-03-30 00:00:00.000000', 13, 'arif@gmail.com', 'MD. NAJMUL ', 'HOSSAIN', '62346', 'fdsds', 'gsdgdg', '2314', 'Male', 'Islam', '141', 'vdggf', 'gffg', NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(14, 2, '2021-04-01 00:35:35.861575', 2, '2021-04-01 00:35:35.861575', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '14', '2021-03-30 00:00:00.000000', 14, 'arif@gmail.com', 'SUDAM', ' ROY', '15535', 'fsdg', 'gdg', '255252', 'Male', 'Islam', '15366', 'gdgf', 'fgdf', NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(15, 2, '2021-04-01 00:36:29.009253', 2, '2021-04-01 00:36:29.009253', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '15', '2021-04-07 00:00:00.000000', 15, 'arif@gmail.com', 'MD. TOHASIN ', 'AHAMED', '4135', 'gdfh', 'dghhf', '1454', 'Male', 'Islam', '513', 'vfgfg', 'fgdggd', NULL, 1, 2, 1, NULL, 1, NULL, NULL),
(16, 2, '2021-04-01 00:40:01.928716', 2, '2021-04-01 00:40:01.928716', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '16', '2021-03-30 00:00:00.000000', 16, 'arif@gmail.com', 'LEMON ', 'HOSSAIN', '4124', 'fdfe', 'fffe', '4444', 'Male', 'Islam', '4412', 'fdf', 'fdfsfdfds', NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(17, 2, '2021-04-01 00:40:56.742486', 2, '2021-04-01 00:40:56.742486', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '17', '2021-03-29 00:00:00.000000', 17, 'arif@gmail.com', 'MD. SHAJAHAN ', 'MIA', '5436', 'dfhhhf', 'hdfhdfhdf', '34252', 'Male', 'Islam', '555', 'dffgd', 'gffg', NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(18, 2, '2021-04-01 00:42:45.661573', 2, '2021-04-01 00:45:31.276183', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '18', '2021-03-29 00:00:00.000000', 18, 'arif@gmail.com', 'MAHMUDA NOWRIN ', 'MONI ', NULL, 'wf', 'fdfdfd', '235523', 'Female', 'Islam', NULL, 'ggsg', 'gsdgs', NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(19, 2, '2021-04-01 00:44:56.360648', 2, '2021-04-01 00:44:56.360648', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '19', '2021-03-30 00:00:00.000000', 19, 'arif@gmail.com', 'MD AFSER', ' HOQUE  ', '4626', 'fgfg', 'fjfj', '544', 'Male', 'Islam', '466634', 'fgn', 'hghk', NULL, 1, 2, 1, NULL, 1, NULL, NULL),
(20, 2, '2021-04-01 00:46:24.525358', 2, '2021-04-01 00:46:24.525358', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '20', '2021-03-31 00:00:00.000000', 20, NULL, 'MD. ', 'SALAUDDIN', NULL, 'vvv', 'vv', NULL, 'Male', 'Islam', NULL, NULL, NULL, NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(21, 2, '2021-04-01 00:46:59.436709', 2, '2021-04-01 00:46:59.436709', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '21', '2021-03-30 00:00:00.000000', 21, NULL, 'SAJAL KUMAR ', 'GANGULY', NULL, NULL, NULL, NULL, 'Male', 'Islam', NULL, NULL, NULL, NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(22, 2, '2021-04-01 00:47:37.122230', 2, '2021-04-01 00:47:37.122230', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '22', '2021-03-29 00:00:00.000000', 22, NULL, 'AFSER', ' HOSSAIN ', NULL, NULL, NULL, NULL, 'Male', 'Islam', NULL, NULL, NULL, NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(23, 2, '2021-04-01 00:48:22.247999', 2, '2021-04-01 00:48:22.247999', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '23', '2021-03-30 00:00:00.000000', 23, NULL, 'MD. JOHIRUL', ' HOQUE', NULL, NULL, NULL, NULL, 'Male', 'Islam', NULL, NULL, NULL, NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(24, 2, '2021-04-01 00:48:43.596042', 2, '2021-04-01 00:48:43.596042', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '24', '2021-03-29 00:00:00.000000', 24, NULL, 'ISRAT JAHAN ', 'URME', NULL, NULL, NULL, NULL, 'Male', 'Islam', NULL, NULL, NULL, NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(25, 2, '2021-04-01 00:49:05.855437', 2, '2021-04-01 00:49:05.855437', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '25', '2021-03-30 00:00:00.000000', 25, NULL, 'K M. HASIBUR ', 'RAHMAN', NULL, NULL, NULL, NULL, 'Male', 'Islam', NULL, NULL, NULL, NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(26, 2, '2021-04-01 00:49:31.125377', 2, '2021-04-01 00:49:31.125377', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '26', '2021-03-30 00:00:00.000000', 26, NULL, 'MD. MAHMUDUN ', 'HASAN', NULL, NULL, NULL, NULL, 'Male', 'Islam', NULL, NULL, NULL, NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(27, 2, '2021-04-01 00:50:03.980620', 2, '2021-04-01 00:50:03.980620', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '27', '2021-03-30 00:00:00.000000', 27, NULL, 'SYED MD. MEHEDI', ' HASAN', NULL, NULL, NULL, NULL, 'Male', 'Islam', NULL, NULL, NULL, NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(28, 2, '2021-04-01 00:50:49.163676', 2, '2021-04-01 00:50:49.163676', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '28', '2021-03-31 00:00:00.000000', 28, NULL, 'SABINA ', 'AKTAR', NULL, NULL, NULL, NULL, 'Male', 'Islam', NULL, NULL, NULL, NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(29, 2, '2021-04-01 00:51:15.854911', 2, '2021-04-01 00:51:15.854911', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '29', '2021-03-30 00:00:00.000000', 29, NULL, 'SABUJ KUMAR ', 'DAS', NULL, NULL, NULL, NULL, 'Male', 'Islam', NULL, NULL, NULL, NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(30, 2, '2021-04-01 00:52:13.511537', 2, '2021-04-01 00:52:13.511537', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '30', '2021-03-29 00:00:00.000000', 30, NULL, 'MONOWARA', '.', NULL, NULL, NULL, NULL, 'Male', 'Islam', NULL, NULL, NULL, NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(31, 2, '2021-04-01 00:52:42.148380', 2, '2021-04-01 00:52:42.148380', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '31', '2021-03-30 00:00:00.000000', 31, NULL, 'FIROJ ', 'AHMED', NULL, NULL, NULL, NULL, 'Male', 'Islam', NULL, NULL, NULL, NULL, 1, 2, 1, NULL, 1, NULL, NULL),
(32, 2, '2021-04-01 00:53:09.190985', 2, '2021-04-01 00:53:09.190985', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '32', '2021-03-31 00:00:00.000000', 32, NULL, 'MD. OYEGKURUNI ', 'SARKER ', NULL, NULL, NULL, NULL, 'Male', 'Islam', NULL, NULL, NULL, NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(33, 2, '2021-04-01 00:54:05.167260', 2, '2021-04-01 00:54:05.167260', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '33', '2021-04-08 00:00:00.000000', 33, NULL, 'Nur JAHAN ', 'URME', NULL, NULL, NULL, NULL, 'Male', 'Islam', NULL, NULL, NULL, NULL, 1, 2, 1, NULL, 1, NULL, NULL),
(34, 2, '2021-04-01 00:54:30.343482', 2, '2021-04-01 00:54:30.343482', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '34', '2021-03-31 00:00:00.000000', 34, NULL, 'MONIR ', 'HOSSEN', NULL, NULL, NULL, NULL, 'Male', 'Islam', NULL, NULL, NULL, NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(35, 2, '2021-04-01 00:54:54.428354', 2, '2021-04-01 00:55:07.963676', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '35', '2021-03-29 00:00:00.000000', 35, NULL, 'SHEAK ', 'AHMED', NULL, NULL, NULL, NULL, 'Male', 'Islam', NULL, NULL, NULL, NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(36, 2, '2021-04-01 00:55:32.718229', 2, '2021-04-01 00:55:32.718229', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '36', '2021-04-05 00:00:00.000000', 36, NULL, 'MD. AMDADUL ', 'HOQUE', NULL, NULL, NULL, NULL, 'Male', 'Islam', NULL, NULL, NULL, NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(37, 2, '2021-04-01 00:56:00.881294', 2, '2021-04-01 00:56:00.881294', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '37', '2021-03-30 00:00:00.000000', 37, NULL, 'TARINA.', ' SULTANA', NULL, NULL, NULL, NULL, 'Male', 'Islam', NULL, NULL, NULL, NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(38, 2, '2021-04-01 00:56:28.565086', 2, '2021-04-01 00:56:28.565086', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '38', '2021-03-30 00:00:00.000000', 38, NULL, 'NAZRUL ', 'ISLAM', NULL, NULL, NULL, NULL, 'Male', 'Islam', NULL, NULL, NULL, NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(39, 2, '2021-04-01 00:56:54.134782', 2, '2021-04-01 00:56:54.134782', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '39', '2021-03-30 00:00:00.000000', 39, NULL, 'MD. ENAMUL ', 'HOSSAIN ', NULL, NULL, NULL, NULL, 'Male', 'Islam', NULL, NULL, NULL, NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(40, 2, '2021-04-01 00:57:18.557848', 2, '2021-04-01 00:57:18.557848', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '40', '2021-04-06 00:00:00.000000', 40, NULL, 'REBEKA SULTANA', ' SEPAT', NULL, NULL, NULL, NULL, 'Male', 'Islam', NULL, NULL, NULL, NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(41, 2, '2021-04-01 00:57:59.149907', 2, '2021-04-01 00:57:59.149907', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '41', '2021-03-30 00:00:00.000000', 41, NULL, 'KALEKUR', 'JAHAN ', NULL, NULL, NULL, NULL, 'Male', 'Islam', NULL, NULL, NULL, NULL, 1, 2, 1, NULL, 1, NULL, NULL),
(42, 2, '2021-04-01 00:58:25.311786', 2, '2021-04-01 00:58:25.311786', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '42', '2021-03-30 00:00:00.000000', 42, NULL, 'MST. SOMA ', 'AKTER', NULL, NULL, NULL, NULL, 'Male', 'Islam', NULL, NULL, NULL, NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(43, 2, '2021-04-01 00:58:51.277586', 2, '2021-04-01 00:58:51.277586', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '43', '2021-04-06 00:00:00.000000', 43, NULL, 'MD. SAFIQUL ', 'ISLAM', NULL, NULL, NULL, NULL, 'Male', 'Islam', NULL, NULL, NULL, NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(44, 2, '2021-04-01 00:59:14.549094', 2, '2021-04-01 00:59:39.597525', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '44', '2021-04-06 00:00:00.000000', 44, NULL, 'MD. AZIM', ' UDDIN ', NULL, NULL, NULL, NULL, 'Male', 'Islam', NULL, NULL, NULL, NULL, 1, 2, 1, NULL, 1, NULL, NULL),
(45, 2, '2021-04-01 01:00:04.716291', 2, '2021-04-01 01:00:04.716291', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '45', '2021-04-06 00:00:00.000000', 45, NULL, 'MD. RASEL ', 'HOSSAIN', NULL, NULL, NULL, NULL, 'Male', 'Islam', NULL, NULL, NULL, NULL, 1, 2, 1, NULL, 1, NULL, NULL),
(46, 2, '2021-04-01 01:00:41.932694', 2, '2021-04-01 01:00:41.932694', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '46', '2021-03-30 00:00:00.000000', 46, NULL, 'MD. MONIRUZZAMAN ', 'RONY', NULL, NULL, NULL, NULL, 'Male', 'Islam', NULL, NULL, NULL, NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(47, 2, '2021-04-01 01:01:15.345745', 2, '2021-04-01 01:01:15.345745', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '47', '2021-04-06 00:00:00.000000', 47, NULL, 'JAMSHAD ', 'HOSSAN', NULL, NULL, NULL, NULL, 'Male', 'Islam', NULL, NULL, NULL, NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(48, 2, '2021-04-01 01:03:04.589910', 2, '2021-04-01 01:03:04.589910', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '48', '2021-04-13 00:00:00.000000', 48, 'arifbogra6@gmail.com', 'ARIFUL', ' ISLAM', '01734280272', 'hafijur Rahman', 'Mst Farida Begum', '01688691436', 'Male', 'Islam', '4126478351252', 'Dhaka ', 'Bogura', NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(49, 2, '2021-04-01 01:03:33.932705', 2, '2021-04-01 01:03:33.932705', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '49', '2021-03-29 00:00:00.000000', 49, NULL, 'SHAMSUL KARIM ', 'CHOWDHURY', NULL, NULL, NULL, NULL, 'Male', 'Islam', NULL, NULL, NULL, NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(50, 2, '2021-04-01 01:04:11.288879', 2, '2021-04-01 01:04:11.288879', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '50', '2021-03-30 00:00:00.000000', 50, NULL, 'MD. JOYNAL ', 'ABEDIN', NULL, NULL, NULL, NULL, 'Male', 'Islam', NULL, NULL, NULL, NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(51, 2, '2021-04-01 01:04:36.588260', 2, '2021-04-01 01:04:36.588260', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '51', '2021-04-21 00:00:00.000000', 51, NULL, 'MD. MUSHFIUR ', 'RAHMAN', NULL, NULL, NULL, NULL, 'Male', 'Islam', NULL, NULL, NULL, NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(52, 2, '2021-04-01 01:05:02.469882', 2, '2021-04-01 01:05:02.469882', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '52', '2021-04-06 00:00:00.000000', 52, NULL, 'MD. HASAN ', 'ALI', NULL, NULL, NULL, NULL, 'Male', 'Islam', NULL, NULL, NULL, NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(53, 2, '2021-04-01 01:05:26.809892', 2, '2021-04-01 01:05:26.809892', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '53', '2021-03-30 00:00:00.000000', 53, NULL, 'SYED EARFAN ', 'EARTAZA ', NULL, NULL, NULL, NULL, 'Male', 'Islam', NULL, NULL, NULL, NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(54, 2, '2021-04-01 01:05:52.077829', 2, '2021-04-01 01:05:52.077829', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '54', '2021-03-30 00:00:00.000000', 54, NULL, 'KHAYRUL AMIN ', 'KAYSH', NULL, NULL, NULL, NULL, 'Male', 'Islam', NULL, NULL, NULL, NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(55, 2, '2021-04-01 01:06:13.105924', 2, '2021-04-01 01:06:13.105924', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '55', '2021-03-30 00:00:00.000000', 55, NULL, 'POBITRA', ' KUMER', NULL, NULL, NULL, NULL, 'Male', 'Islam', NULL, NULL, NULL, NULL, 1, 2, 1, NULL, 1, NULL, NULL),
(56, 2, '2021-04-01 01:06:36.119998', 2, '2021-04-01 01:06:36.119998', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '56', '2021-04-20 00:00:00.000000', 56, NULL, 'MD. RAIHAN UDDIN', ' SUFAL', NULL, NULL, NULL, NULL, 'Male', 'Islam', NULL, NULL, NULL, NULL, 1, 1, 1, NULL, 1, NULL, NULL),
(57, 2, '2021-04-01 01:06:59.326861', 2, '2021-04-01 01:06:59.326861', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '57', '2021-04-12 00:00:00.000000', 57, NULL, 'MD. ARIFUL ', 'ISLAM', NULL, NULL, NULL, NULL, 'Male', 'Islam', NULL, NULL, NULL, NULL, 1, 1, 1, NULL, 1, NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `__efmigrationshistory`
--

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(95) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `approles`
--
ALTER TABLE `approles`
  ADD PRIMARY KEY (`RoleId`);

--
-- Indexes for table `appuserroles`
--
ALTER TABLE `appuserroles`
  ADD PRIMARY KEY (`UserId`,`RoleId`),
  ADD KEY `IX_AppUserRoles_RoleId` (`RoleId`);

--
-- Indexes for table `appusers`
--
ALTER TABLE `appusers`
  ADD PRIMARY KEY (`UserId`);

--
-- Indexes for table `authenticationtracers`
--
ALTER TABLE `authenticationtracers`
  ADD PRIMARY KEY (`TracerId`);

--
-- Indexes for table `companysettings`
--
ALTER TABLE `companysettings`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `courses`
--
ALTER TABLE `courses`
  ADD PRIMARY KEY (`CourseId`),
  ADD KEY `IX_Courses_DepartmentId` (`DepartmentId`),
  ADD KEY `IX_Courses_SemesterId` (`SemesterId`);

--
-- Indexes for table `departments`
--
ALTER TABLE `departments`
  ADD PRIMARY KEY (`DepartmentId`);

--
-- Indexes for table `employees`
--
ALTER TABLE `employees`
  ADD PRIMARY KEY (`EmployeeId`),
  ADD KEY `IX_Employees_DepartmentId` (`DepartmentId`);

--
-- Indexes for table `mailconfigurations`
--
ALTER TABLE `mailconfigurations`
  ADD PRIMARY KEY (`MailConfigurationId`),
  ADD KEY `IX_MailConfigurations_CompanyId` (`CompanyId`);

--
-- Indexes for table `markingbadges`
--
ALTER TABLE `markingbadges`
  ADD PRIMARY KEY (`MarkingBadgeId`);

--
-- Indexes for table `markingcriterias`
--
ALTER TABLE `markingcriterias`
  ADD PRIMARY KEY (`MarkingCriteriaId`);

--
-- Indexes for table `markingcriteriasbadges`
--
ALTER TABLE `markingcriteriasbadges`
  ADD PRIMARY KEY (`CriteriasBadgeId`),
  ADD KEY `IX_MarkingCriteriasBadges_MarkingBadgeId` (`MarkingBadgeId`),
  ADD KEY `IX_MarkingCriteriasBadges_MarkingCriteriaId` (`MarkingCriteriaId`);

--
-- Indexes for table `semesters`
--
ALTER TABLE `semesters`
  ADD PRIMARY KEY (`SemesterId`);

--
-- Indexes for table `studentbatches`
--
ALTER TABLE `studentbatches`
  ADD PRIMARY KEY (`BatchId`);

--
-- Indexes for table `studentcoursemarks`
--
ALTER TABLE `studentcoursemarks`
  ADD PRIMARY KEY (`CourseMarksId`),
  ADD KEY `IX_StudentCourseMarks_CourseId` (`CourseId`),
  ADD KEY `IX_StudentCourseMarks_MarkingCriteriaId` (`MarkingCriteriaId`),
  ADD KEY `IX_StudentCourseMarks_StudentId` (`StudentId`);

--
-- Indexes for table `students`
--
ALTER TABLE `students`
  ADD PRIMARY KEY (`StudentId`),
  ADD KEY `IX_Students_BatchInfoBatchId` (`BatchInfoBatchId`),
  ADD KEY `IX_Students_CourseId` (`CourseId`),
  ADD KEY `IX_Students_DepartmentId` (`DepartmentId`),
  ADD KEY `IX_Students_SemesterId` (`SemesterId`);

--
-- Indexes for table `__efmigrationshistory`
--
ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `approles`
--
ALTER TABLE `approles`
  MODIFY `RoleId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `appusers`
--
ALTER TABLE `appusers`
  MODIFY `UserId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `authenticationtracers`
--
ALTER TABLE `authenticationtracers`
  MODIFY `TracerId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `companysettings`
--
ALTER TABLE `companysettings`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `courses`
--
ALTER TABLE `courses`
  MODIFY `CourseId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=60;

--
-- AUTO_INCREMENT for table `departments`
--
ALTER TABLE `departments`
  MODIFY `DepartmentId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `employees`
--
ALTER TABLE `employees`
  MODIFY `EmployeeId` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `mailconfigurations`
--
ALTER TABLE `mailconfigurations`
  MODIFY `MailConfigurationId` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `markingbadges`
--
ALTER TABLE `markingbadges`
  MODIFY `MarkingBadgeId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `markingcriterias`
--
ALTER TABLE `markingcriterias`
  MODIFY `MarkingCriteriaId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `markingcriteriasbadges`
--
ALTER TABLE `markingcriteriasbadges`
  MODIFY `CriteriasBadgeId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=27;

--
-- AUTO_INCREMENT for table `semesters`
--
ALTER TABLE `semesters`
  MODIFY `SemesterId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT for table `studentbatches`
--
ALTER TABLE `studentbatches`
  MODIFY `BatchId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `studentcoursemarks`
--
ALTER TABLE `studentcoursemarks`
  MODIFY `CourseMarksId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT for table `students`
--
ALTER TABLE `students`
  MODIFY `StudentId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=58;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `appuserroles`
--
ALTER TABLE `appuserroles`
  ADD CONSTRAINT `FK_AppUserRoles_AppRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `approles` (`RoleId`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_AppUserRoles_AppUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `appusers` (`UserId`) ON DELETE CASCADE;

--
-- Constraints for table `courses`
--
ALTER TABLE `courses`
  ADD CONSTRAINT `FK_Courses_Departments_DepartmentId` FOREIGN KEY (`DepartmentId`) REFERENCES `departments` (`DepartmentId`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Courses_Semesters_SemesterId` FOREIGN KEY (`SemesterId`) REFERENCES `semesters` (`SemesterId`) ON DELETE CASCADE;

--
-- Constraints for table `employees`
--
ALTER TABLE `employees`
  ADD CONSTRAINT `FK_Employees_Departments_DepartmentId` FOREIGN KEY (`DepartmentId`) REFERENCES `departments` (`DepartmentId`);

--
-- Constraints for table `mailconfigurations`
--
ALTER TABLE `mailconfigurations`
  ADD CONSTRAINT `FK_MailConfigurations_CompanySettings_CompanyId` FOREIGN KEY (`CompanyId`) REFERENCES `companysettings` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `markingcriteriasbadges`
--
ALTER TABLE `markingcriteriasbadges`
  ADD CONSTRAINT `FK_MarkingCriteriasBadges_MarkingBadges_MarkingBadgeId` FOREIGN KEY (`MarkingBadgeId`) REFERENCES `markingbadges` (`MarkingBadgeId`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_MarkingCriteriasBadges_MarkingCriterias_MarkingCriteriaId` FOREIGN KEY (`MarkingCriteriaId`) REFERENCES `markingcriterias` (`MarkingCriteriaId`) ON DELETE CASCADE;

--
-- Constraints for table `studentcoursemarks`
--
ALTER TABLE `studentcoursemarks`
  ADD CONSTRAINT `FK_StudentCourseMarks_Courses_CourseId` FOREIGN KEY (`CourseId`) REFERENCES `courses` (`CourseId`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_StudentCourseMarks_MarkingCriterias_MarkingCriteriaId` FOREIGN KEY (`MarkingCriteriaId`) REFERENCES `markingcriterias` (`MarkingCriteriaId`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_StudentCourseMarks_Students_StudentId` FOREIGN KEY (`StudentId`) REFERENCES `students` (`StudentId`) ON DELETE CASCADE;

--
-- Constraints for table `students`
--
ALTER TABLE `students`
  ADD CONSTRAINT `FK_Students_Courses_CourseId` FOREIGN KEY (`CourseId`) REFERENCES `courses` (`CourseId`),
  ADD CONSTRAINT `FK_Students_Departments_DepartmentId` FOREIGN KEY (`DepartmentId`) REFERENCES `departments` (`DepartmentId`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Students_Semesters_SemesterId` FOREIGN KEY (`SemesterId`) REFERENCES `semesters` (`SemesterId`),
  ADD CONSTRAINT `FK_Students_StudentBatches_BatchInfoBatchId` FOREIGN KEY (`BatchInfoBatchId`) REFERENCES `studentbatches` (`BatchId`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
