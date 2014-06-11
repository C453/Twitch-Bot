/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50532
Source Host           : localhost:3306
Source Database       : twitch_bot

Target Server Type    : MYSQL
Target Server Version : 50532
File Encoding         : 65001

Date: 2014-06-11 21:50:46
*/

SET FOREIGN_KEY_CHECKS=0;
-- ----------------------------
-- Table structure for `commands`
-- ----------------------------
DROP TABLE IF EXISTS `commands`;
CREATE TABLE `commands` (
  `command` varchar(24) NOT NULL DEFAULT '',
  `returnText` varchar(256) NOT NULL DEFAULT '',
  `rankRequired` int(1) NOT NULL DEFAULT '0',
  `channel` text,
  PRIMARY KEY (`command`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of commands
-- ----------------------------
INSERT INTO `commands` VALUES ('!helloworld', 'Hello World!', '0', null);

-- ----------------------------
-- Table structure for `users`
-- ----------------------------
DROP TABLE IF EXISTS `users`;
CREATE TABLE `users` (
  `name` varchar(24) NOT NULL,
  `authkey` varchar(37) NOT NULL,
  PRIMARY KEY (`name`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of users
-- ----------------------------
INSERT INTO `users` VALUES ('Edit', 'This');

