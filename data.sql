-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               10.4.17-MariaDB - mariadb.org binary distribution
-- Server OS:                    Win64
-- HeidiSQL Version:             11.1.0.6116
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Dumping database structure for plumeesx_86bbe6
CREATE DATABASE IF NOT EXISTS `plumeesx_86bbe6` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `plumeesx_86bbe6`;

-- Dumping structure for table plumeesx_86bbe6.eclipse_ambulance_tickets
CREATE TABLE IF NOT EXISTS `eclipse_ambulance_tickets` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(50) DEFAULT NULL,
  `date` varchar(50) DEFAULT NULL,
  `fulltext` varchar(50) DEFAULT NULL,
  `name` varchar(50) DEFAULT NULL,
  `operatorId` int(11) DEFAULT NULL,
  `ownerId` int(11) DEFAULT NULL,
  `paid` tinyint(4) DEFAULT NULL,
  `price` varchar(50) DEFAULT NULL,
  `show` tinyint(4) DEFAULT NULL,
  `text` varchar(50) DEFAULT NULL,
  `penalty` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8;

-- Dumping data for table plumeesx_86bbe6.eclipse_ambulance_tickets: ~11 rows (approximately)
/*!40000 ALTER TABLE `eclipse_ambulance_tickets` DISABLE KEYS */;
REPLACE INTO `eclipse_ambulance_tickets` (`id`, `code`, `date`, `fulltext`, `name`, `operatorId`, `ownerId`, `paid`, `price`, `show`, `text`, `penalty`) VALUES
	(8, '1.1', '2021-08-15T13:30:19.078Z', NULL, 'Initial examination of a patient', 1, 1, 0, '500', 1, 'Initial examination of a patient', NULL),
	(9, '1.1', '2021-08-15T13:30:19.081Z', NULL, 'Initial examination of a patient', 1, 1, 0, '500', 1, 'Initial examination of a patient', NULL),
	(10, '1.2', '2021-08-15T13:30:19.081Z', NULL, 'Pills for treatment.', 1, 1, 0, '100', 1, 'Pills for treatment.', NULL),
	(11, '1.2', '2021-08-15T13:30:19.081Z', NULL, 'Pills for treatment.', 1, 1, 0, '100', 1, 'Pills for treatment.', NULL),
	(14, '5.1', '2021-08-15T16:41:32.804Z', NULL, 'Miscellaneous.', 1, 2, 0, '100', 1, 'Miscellaneous.', NULL),
	(15, '1.2', '2021-08-15T16:41:32.804Z', NULL, 'Pills for treatment.', 1, 2, 0, '100', 1, 'Pills for treatment.', NULL),
	(16, '1.1', '2021-08-15T16:48:17.745Z', NULL, 'Initial examination of a patient', 1, 2, 0, '500', 1, 'Initial examination of a patient', NULL),
	(17, '1.1', '2021-08-15T16:48:17.745Z', NULL, 'Initial examination of a patient', 1, 2, 0, '500', 1, 'Initial examination of a patient', NULL),
	(18, '1.1', '2021-08-15T16:48:17.745Z', NULL, 'Initial examination of a patient', 1, 2, 0, '500', 1, 'Initial examination of a patient', NULL),
	(19, '1.3', '2021-08-15T16:48:17.745Z', NULL, 'Surgeon\'s Examination.', 1, 2, 0, '500', 1, 'Surgeon\'s Examination.', NULL),
	(20, '5.1', '2021-08-15T16:48:17.745Z', NULL, 'Miscellaneous.', 1, 2, 0, '100', 1, 'Miscellaneous.', NULL);
/*!40000 ALTER TABLE `eclipse_ambulance_tickets` ENABLE KEYS */;

-- Dumping structure for table plumeesx_86bbe6.eclipse_cad_ambulance_calls
CREATE TABLE IF NOT EXISTS `eclipse_cad_ambulance_calls` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `from` varchar(50) DEFAULT NULL,
  `date` varchar(50) DEFAULT NULL,
  `status` varchar(50) DEFAULT NULL,
  `crew` varchar(50) DEFAULT NULL,
  `message` varchar(50) DEFAULT NULL,
  `position` varchar(9999) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- Dumping data for table plumeesx_86bbe6.eclipse_cad_ambulance_calls: ~0 rows (approximately)
/*!40000 ALTER TABLE `eclipse_cad_ambulance_calls` DISABLE KEYS */;
REPLACE INTO `eclipse_cad_ambulance_calls` (`id`, `from`, `date`, `status`, `crew`, `message`, `position`) VALUES
	(1, '960-3549', '08/15/2021 15:01:58', '3', '["9603549"]', 'фцвцфв', '{"x":-268.3481140136719,"y":-957.2870483398438,"z":31.22305107116699}');
/*!40000 ALTER TABLE `eclipse_cad_ambulance_calls` ENABLE KEYS */;

-- Dumping structure for table plumeesx_86bbe6.eclipse_cad_ambulance_users_status
CREATE TABLE IF NOT EXISTS `eclipse_cad_ambulance_users_status` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `status` int(11) NOT NULL DEFAULT 0,
  `name` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- Dumping data for table plumeesx_86bbe6.eclipse_cad_ambulance_users_status: ~2 rows (approximately)
/*!40000 ALTER TABLE `eclipse_cad_ambulance_users_status` DISABLE KEYS */;
REPLACE INTO `eclipse_cad_ambulance_users_status` (`id`, `status`, `name`) VALUES
	(1, 4, 'Player 1'),
	(2, 1, 'Player 2');
/*!40000 ALTER TABLE `eclipse_cad_ambulance_users_status` ENABLE KEYS */;

-- Dumping structure for table plumeesx_86bbe6.eclipse_cad_ambulance_vehicles_status
CREATE TABLE IF NOT EXISTS `eclipse_cad_ambulance_vehicles_status` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `number` varchar(50) DEFAULT NULL,
  `status` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table plumeesx_86bbe6.eclipse_cad_ambulance_vehicles_status: ~0 rows (approximately)
/*!40000 ALTER TABLE `eclipse_cad_ambulance_vehicles_status` DISABLE KEYS */;
/*!40000 ALTER TABLE `eclipse_cad_ambulance_vehicles_status` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
