-- Description: https://leetcode.com/problems/invalid-tweets/

-- Input: 
-- Tweets table:
-- +----------+----------------------------------+
-- | tweet_id | content                          |
-- +----------+----------------------------------+
-- | 1        | Vote for Biden                   |
-- | 2        | Let us make America great again! |
-- +----------+----------------------------------+
-- Output: 
-- +----------+
-- | tweet_id |
-- +----------+
-- | 2        |
-- +----------+
-- Explanation: 
-- Tweet 1 has length = 14. It is a valid tweet.
-- Tweet 2 has length = 32. It is an invalid tweet.

DROP TABLE IF EXISTS Tweets; 
CREATE TABLE IF NOT EXISTS Tweets (
    tweet_id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, 
    content TEXT
); 
INSERT INTO Tweets (content) VALUES ('Vote for Biden'); 
INSERT INTO Tweets (content) VALUES ('Let us make America great again!'); 
