/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Datenbank: `twitch_bot`
--
CREATE DATABASE IF NOT EXISTS `twitch_bot` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `twitch_bot`;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `commands`
--

CREATE TABLE IF NOT EXISTS `commands` (
  `command` varchar(24) NOT NULL DEFAULT '',
  `returnText` varchar(256) NOT NULL DEFAULT '',
  `rankRequired` int(1) NOT NULL DEFAULT '0',
  `chanel` varchar(60) DEFAULT NULL,
  PRIMARY KEY (`command`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Daten für Tabelle `commands`
--

INSERT INTO `commands` (`command`, `returnText`, `rankRequired`, `chanel`) VALUES
('!social', 'You can follow Donran on twitter here: https://twitter.com/donran And like his facebook page here: https://www.facebook.com/Donranl', 0, NULL),
('!helloworld', 'Hello World!', 0, NULL),
('!bot', 'This bot was made by Donran and ossimc82 helped him c:, awesome right? yus', 0, NULL),
('Who is kappa?', 'Kappa = Kappa', 0, NULL),
('!db', 'this command is stored on a database', 0, NULL);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
