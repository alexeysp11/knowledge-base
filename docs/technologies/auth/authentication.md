# authentication

## Storing passwords 

Approaches to store passwords:
- Plain text: 
    - Storing passwords in a [plain text](https://en.wikipedia.org/wiki/Plain_text) string is a bad idea because if an attacker gets access to the database, they automatically get access to all the users' accounts.
- Hashing: 
    - [Hash function](https://en.wikipedia.org/wiki/Hash_function) allows to map data of arbitrary size to fixed-sized values, which makes it easier to verify the integrity of a piece of data.
    - The problem is that 20% of the users have one of the 5000 most commonly used passwords, so the attacker could apply hashing function against these passwords, and get access to 20% of those account whose passwords occur in the stolen dataset.
- Hashing + salt: 
    - [Salt](https://en.wikipedia.org/wiki/Salt_(cryptography)) is a random data fed as an additional input to a hash function.
    - In a simplest example, salt could be the same for all the users, however a systemwide salt allows an attacker to use hash tables to steal passwords and compromise accounts.
    - Salt could be generated for each user separately (it could be done depending on some of the user's data): 
        - for example, when salt is generated using the basic user's info (*login*, *username*, *email*, *date of birth*, *datetime of changing the password*) - when the user changes their data, they have to input their password to regenerate the salt and update hashed password.

## Sending passwords 

- [Challenge-response authentication](https://en.wikipedia.org/wiki/Challenge%E2%80%93response_authentication) is a family of protocols in which one party presents a question ("challenge") and another party must provide a valid answer ("response") to be authenticated. 
- The simplest example of a challenge–response protocol is password authentication, where the challenge is asking for the password and the valid response is the correct password.
- More advanced example could include the following steps:
    - client sends login and receives "challenge" from the server.
    - client hashes the password (the "challenge" could a salt), and sends it to the server.
- In order to enhance the protocol, server could send multiple "challenges", so the client could choose which one to use. This approach minimizes the risk to compromise the password.

## Anti-hammering defenses 

- Rate limit: number of password guesses per second/minute/hours.
- Time-outs: minimal time duration between two password guesses.

## Password recovery 

**Strength of a password** could be defined as a minimum value of **its entropy** (how difficult to predict the password) and **its recovery entorpy** (how difficult to recover the password), since the attacker could gain access to the account not only by knowing the password itself but by recovering it.
```
strength = min(pswd_entropy, recovery_entropy)
```

## Multi-factor authentication 

Multi-factor authentication should use different mechanisms: 
- Something you know (e.g. password).
- Something you posses (e.g. phone, smart-card).
    - User can get some verification code via SMS.
- Something that you are (e.g. biometrics).
    - You can remember user's devices or geolocation.

ATTENTION: some researches show that when making users to choose multi-factor authentication, they tend to choose weaker passwords.
