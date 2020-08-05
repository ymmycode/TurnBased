-- phpMyAdmin SQL Dump
-- version 4.9.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Aug 05, 2020 at 12:44 PM
-- Server version: 10.4.8-MariaDB
-- PHP Version: 7.3.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `gamedatabase`
--
CREATE DATABASE IF NOT EXISTS `gamedatabase` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `gamedatabase`;

-- --------------------------------------------------------

--
-- Table structure for table `admin`
--

CREATE TABLE `admin` (
  `id` int(11) NOT NULL,
  `admin` varchar(22) DEFAULT NULL,
  `pass` varchar(22) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `gamedes`
--

CREATE TABLE `gamedes` (
  `name` varchar(255) DEFAULT NULL,
  `genre` varchar(255) DEFAULT NULL,
  `detail` varchar(255) DEFAULT NULL,
  `version` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `gamedes`
--

INSERT INTO `gamedes` (`name`, `genre`, `detail`, `version`) VALUES
('F1_OW (Flow)', 'RPG, Puzzle, Math, Maze', 'Game ini bertujuan untuk memperkenalkan dunia programming melewati puzzle Flowchart.', '1.0.0');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `username` varchar(50) DEFAULT NULL,
  `password` varchar(50) DEFAULT NULL,
  `email` varchar(70) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`id`, `username`, `password`, `email`) VALUES
(1, 'ymmy', '170998', 'ymmy@gmail.com'),
(4, 'fauzi', 'qwerty', 'fauzi@gmail.com'),
(5, 'test1', '12345', 'qweqweqwe@gmail.com'),
(6, 'test3', '12345', 'aslkdjasd@gmail.com');

-- --------------------------------------------------------

--
-- Table structure for table `usersaveddata`
--

CREATE TABLE `usersaveddata` (
  `id` int(11) NOT NULL,
  `userID` int(11) NOT NULL,
  `lastScene` varchar(7) DEFAULT NULL,
  `lastPlayed` varchar(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `usersaveddata`
--

INSERT INTO `usersaveddata` (`id`, `userID`, `lastScene`, `lastPlayed`) VALUES
(1, 1, 'C1L1', 'August 5, 2020'),
(2, 4, 'C1', 'August 2, 2020');

-- --------------------------------------------------------

--
-- Table structure for table `usersavedoption`
--

CREATE TABLE `usersavedoption` (
  `id` int(11) NOT NULL,
  `userID` int(11) NOT NULL,
  `volume` varchar(15) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `usersavedoption`
--

INSERT INTO `usersavedoption` (`id`, `userID`, `volume`) VALUES
(1, 1, '0.246378'),
(2, 4, NULL);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `admin`
--
ALTER TABLE `admin`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `usersaveddata`
--
ALTER TABLE `usersaveddata`
  ADD PRIMARY KEY (`id`,`userID`) USING BTREE;

--
-- Indexes for table `usersavedoption`
--
ALTER TABLE `usersavedoption`
  ADD PRIMARY KEY (`id`,`userID`) USING BTREE;

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `admin`
--
ALTER TABLE `admin`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `usersaveddata`
--
ALTER TABLE `usersaveddata`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `usersavedoption`
--
ALTER TABLE `usersavedoption`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
